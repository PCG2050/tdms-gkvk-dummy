using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.ConsultSocialMedia;
using Application.Interface.Repository.DataTables.TblService;
using Application.Interface.Services;
using Application.Interface.Services.Reports;
using Application.Models;
using Application.Services;
using Application.Services.Reports;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Infrastructure.Services.Reports
{
    public class DynamicReportService : IDynamicReportService
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly INominationRewardRepository _nominationRepository;
        private readonly IConsultingServiceRepository _consultancyRepository;
        private readonly ITableServiceRepository _serviceRepository;
        private readonly ITableOtherActivityRepository _otherActivityRepository;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly ICurrentUserService _currentUserService;

        public DynamicReportService(
            IPublicationRepository publicationRepository,
            INominationRewardRepository nominationRepository,
            IConsultingServiceRepository consultancyRepository,
            ITableServiceRepository serviceRepository,
            ITableOtherActivityRepository otherActivityRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            ICurrentUserService currentUserService)
        {
            _publicationRepository = publicationRepository;
            _nominationRepository = nominationRepository;
            _consultancyRepository = consultancyRepository;
            _serviceRepository = serviceRepository;
            _otherActivityRepository = otherActivityRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ServiceResult<DynamicReportData>> GenerateDynamicReportAsync(
            DynamicReportRequest request,
            bool previewMode = false,
            int maxRowsPerSection = int.MaxValue)
        {
            try
            {
                // Get unit location details
                var unitLocation = await _organizationUnitRepository.GetByIdAsync(request.UnitLocationId);
                if (unitLocation == null)
                    return ServiceResult<DynamicReportData>.Failure("Unit location not found");

                // Build response
                var reportData = new DynamicReportData
                {
                    UnitName = unitLocation.Unit?.Name ?? "Unknown",
                    UnitLocationName = $"{unitLocation.District?.Name}, {unitLocation.District?.State?.Name}",
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.Month),
                    Month = request.Month,
                    Year = request.Year,
                    GeneratedAt = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                    Sections = new List<ReportSection>()
                };

                // Process each requested section
                foreach (var sectionRequest in request.Sections)
                {
                    var section = await BuildSectionAsync(
                        sectionRequest,
                        request.UnitLocationId,
                        request.Month,
                        request.Year,
                        previewMode,
                        maxRowsPerSection);

                    if (section != null)
                    {
                        reportData.Sections.Add(section);
                        reportData.TotalEntries += section.TotalRecords;
                    }
                }

                return ServiceResult<DynamicReportData>.Success(reportData);
            }
            catch (Exception ex)
            {
                return ServiceResult<DynamicReportData>.Failure($"Failed to generate report: {ex.Message}");
            }
        }

        private async Task<ReportSection?> BuildSectionAsync(
            SectionRequest sectionRequest,
            int unitLocationId,
            int month,
            int year,
            bool previewMode,
            int maxRows)
        {
            // Get section definition from registry
            var sectionDef = ReportColumnRegistry.GetSectionDefinition(sectionRequest.SectionKey);
            if (sectionDef == null)
                return null;

            // Build section based on key
            return sectionRequest.SectionKey switch
            {
                ReportColumnRegistry.PUBLICATIONS => await BuildPublicationsSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                ReportColumnRegistry.NOMINATIONS => await BuildNominationsSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                ReportColumnRegistry.CONSULTANCIES => await BuildConsultanciesSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                ReportColumnRegistry.SERVICES => await BuildServicesSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                ReportColumnRegistry.OTHER_ACTIVITIES => await BuildOtherActivitiesSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                // Programs, FIU Activities, and ASM Activities require specific unit repositories
                // These will be implemented separately or through unit-specific endpoints
                _ => null
            };
        }

        // ========================================
        // SECTION BUILDERS
        // ========================================

        private async Task<ReportSection> BuildPublicationsSectionAsync(
            SectionRequest request,
            int unitLocationId,
            int month,
            int year,
            SectionDefinition sectionDef,
            int maxRows)
        {
            var query = _publicationRepository.GetQueryable()
                .Where(p => p.UnitLocationId == unitLocationId
                    && p.FormStatus == "Approved"
                    && p.CreatedAt.Month == month
                    && p.CreatedAt.Year == year)
                .Include(p => p.Category);

            var total = await query.CountAsync();
            var data = await query.Take(maxRows).ToListAsync();

            var rows = data.Select(p => BuildRow(p, request.SelectedColumns, new Dictionary<string, Func<object, object?>>
            {
                ["category"] = (entity) => ((dynamic)entity).Category?.Name,
                ["title"] = (entity) => ((dynamic)entity).Title,
                ["pages"] = (entity) => ((dynamic)entity).TotalPages,
                ["publishedDate"] = (entity) => ((dynamic)entity).PublishedDate?.ToString("yyyy-MM-dd")
            })).ToList();

            return new ReportSection
            {
                SectionKey = request.SectionKey,
                DisplayName = sectionDef.DisplayName,
                TotalRecords = total,
                Columns = BuildColumnMetadata(request.SelectedColumns, sectionDef),
                Rows = rows
            };
        }

        private async Task<ReportSection> BuildNominationsSectionAsync(
            SectionRequest request,
            int unitLocationId,
            int month,
            int year,
            SectionDefinition sectionDef,
            int maxRows)
        {
            var query = _nominationRepository.GetQueryable()
                .Where(n => n.UnitLocationId == unitLocationId
                    && n.FormStatus == "Approved"
                    && n.CreatedAt.Month == month
                    && n.CreatedAt.Year == year);

            var total = await query.CountAsync();
            var data = await query.Take(maxRows).ToListAsync();

            var rows = data.Select(n => BuildRow(n, request.SelectedColumns, new Dictionary<string, Func<object, object?>>
            {
                ["type"] = (entity) => ((dynamic)entity).IsNomination ? "Nomination" : "Reward",
                ["awardName"] = (entity) => ((dynamic)entity).AwardName,
                ["category"] = (entity) => ((dynamic)entity).Category,
                ["date"] = (entity) => ((dynamic)entity).Date?.ToString("yyyy-MM-dd")
            })).ToList();

            return new ReportSection
            {
                SectionKey = request.SectionKey,
                DisplayName = sectionDef.DisplayName,
                TotalRecords = total,
                Columns = BuildColumnMetadata(request.SelectedColumns, sectionDef),
                Rows = rows
            };
        }

        private async Task<ReportSection> BuildConsultanciesSectionAsync(
            SectionRequest request,
            int unitLocationId,
            int month,
            int year,
            SectionDefinition sectionDef,
            int maxRows)
        {
            var query = _consultancyRepository.GetQueryable()
                .Where(c => c.UnitLocationId == unitLocationId
                    && c.FormStatus == "Approved"
                    && c.CreatedAt.Month == month
                    && c.CreatedAt.Year == year)
                .Include(c => c.Category);

            var total = await query.CountAsync();
            var data = await query.Take(maxRows).ToListAsync();

            var rows = data.Select(c => BuildRow(c, request.SelectedColumns, new Dictionary<string, Func<object, object?>>
            {
                ["category"] = (entity) => ((dynamic)entity).Category?.Name,
                ["title"] = (entity) => ((dynamic)entity).Title,
                ["date"] = (entity) => ((dynamic)entity).Date?.ToString("yyyy-MM-dd"),
                ["clientName"] = (entity) => ((dynamic)entity).ClientName
            })).ToList();

            return new ReportSection
            {
                SectionKey = request.SectionKey,
                DisplayName = sectionDef.DisplayName,
                TotalRecords = total,
                Columns = BuildColumnMetadata(request.SelectedColumns, sectionDef),
                Rows = rows
            };
        }

        private async Task<ReportSection> BuildServicesSectionAsync(
            SectionRequest request,
            int unitLocationId,
            int month,
            int year,
            SectionDefinition sectionDef,
            int maxRows)
        {
            var query = _serviceRepository.GetQueryable()
                .Where(s => s.UnitLocationId == unitLocationId
                    && s.FormStatus == "Approved"
                    && s.CreatedAt.Month == month
                    && s.CreatedAt.Year == year)
                .Include(s => s.Category)
                .Include(s => s.Theme)
                .Include(s => s.QuantityUnit);

            var total = await query.CountAsync();
            var data = await query.Take(maxRows).ToListAsync();

            var rows = data.Select(s => BuildRow(s, request.SelectedColumns, new Dictionary<string, Func<object, object?>>
            {
                ["category"] = (entity) => ((dynamic)entity).Category?.Name,
                ["theme"] = (entity) => ((dynamic)entity).Theme?.Name,
                ["unit"] = (entity) => ((dynamic)entity).QuantityUnit?.Name,
                ["quantity"] = (entity) => ((dynamic)entity).Number,
                ["amount"] = (entity) => ((dynamic)entity).AmountGenerated,
                ["status"] = (entity) => ((dynamic)entity).FormStatus
            })).ToList();

            return new ReportSection
            {
                SectionKey = request.SectionKey,
                DisplayName = sectionDef.DisplayName,
                TotalRecords = total,
                Columns = BuildColumnMetadata(request.SelectedColumns, sectionDef),
                Rows = rows
            };
        }

        private async Task<ReportSection> BuildOtherActivitiesSectionAsync(
            SectionRequest request,
            int unitLocationId,
            int month,
            int year,
            SectionDefinition sectionDef,
            int maxRows)
        {
            var query = _otherActivityRepository.GetQueryable()
                .Where(a => a.UnitLocationId == unitLocationId
                    && a.FormStatus == "Approved"
                    && a.CreatedAt.Month == month
                    && a.CreatedAt.Year == year);

            var total = await query.CountAsync();
            var data = await query.Take(maxRows).ToListAsync();

            var rows = data.Select(a => BuildRow(a, request.SelectedColumns, new Dictionary<string, Func<object, object?>>
            {
                ["title"] = (entity) => ((dynamic)entity).Title,
                ["description"] = (entity) => ((dynamic)entity).Description
            })).ToList();

            return new ReportSection
            {
                SectionKey = request.SectionKey,
                DisplayName = sectionDef.DisplayName,
                TotalRecords = total,
                Columns = BuildColumnMetadata(request.SelectedColumns, sectionDef),
                Rows = rows
            };
        }

        // ========================================
        // HELPER METHODS
        // ========================================

        private Dictionary<string, object?> BuildRow(
            object entity,
            List<string> selectedColumns,
            Dictionary<string, Func<object, object?>> columnMappings)
        {
            var row = new Dictionary<string, object?>();

            foreach (var columnKey in selectedColumns)
            {
                if (columnMappings.ContainsKey(columnKey))
                {
                    row[columnKey] = columnMappings[columnKey](entity);
                }
                else
                {
                    row[columnKey] = null;
                }
            }

            return row;
        }

        private List<ColumnMetadata> BuildColumnMetadata(
            List<string> selectedColumns,
            SectionDefinition sectionDef)
        {
            var columns = selectedColumns
                .Select(colKey =>
                {
                    var colDef = sectionDef.AvailableColumns.FirstOrDefault(c => c.Key == colKey);
                    return colDef != null
                        ? new ColumnMetadata
                        {
                            Key = colDef.Key,
                            DisplayName = colDef.DisplayName,
                            DataType = colDef.DataType,
                            Width = CalculateSmartWidth(colDef, selectedColumns.Count)
                        }
                        : new ColumnMetadata { Key = colKey, DisplayName = colKey, Width = 20 };
                })
                .ToList();

            // Normalize widths to fit page
            return NormalizeWidths(columns);
        }

        /// <summary>
        /// Calculate smart width based on column type and number of columns
        /// </summary>
        private int CalculateSmartWidth(ColumnDefinition col, int totalColumns)
        {
            // If width is explicitly set and > 0, use it (for special cases)
            if (col.Width.HasValue && col.Width.Value > 0)
                return col.Width.Value;

            // Calculate based on data type and field name
            return col.DataType switch
            {
                // String fields - content-aware widths
                "string" => CalculateStringFieldWidth(col.Key, col.DisplayName, totalColumns),

                // Fixed-size fields
                "date" => 12,
                "number" => 8,
                "decimal" => 10,
                "currency" => 12,

                _ => 15
            };
        }

        /// <summary>
        /// Calculate width for string fields based on content type
        /// </summary>
        private int CalculateStringFieldWidth(string key, string displayName, int totalColumns)
        {
            var keyLower = key.ToLower();

            // Long string fields (descriptions, notes, addresses, remarks)
            if (keyLower.Contains("description") ||
                keyLower.Contains("note") ||
                keyLower.Contains("address") ||
                keyLower.Contains("remark") ||
                keyLower.Contains("purpose"))
            {
                return totalColumns > 5 ? 30 : 40;  // Reduce if many columns
            }

            // Medium string fields (titles, names, activities)
            if (keyLower.Contains("title") ||
                keyLower.Contains("name") ||
                keyLower.Contains("activity") ||
                keyLower.Contains("particulars"))
            {
                return totalColumns > 5 ? 25 : 30;
            }

            // Short string fields (categories, types, status, theme)
            if (keyLower.Contains("category") ||
                keyLower.Contains("type") ||
                keyLower.Contains("status") ||
                keyLower.Contains("theme") ||
                keyLower.Contains("unit"))
            {
                return 15;
            }

            // Default for unknown strings
            return 20;
        }

        /// <summary>
        /// Normalize column widths to fit page (target 95% to leave margins)
        /// </summary>
        private List<ColumnMetadata> NormalizeWidths(List<ColumnMetadata> columns)
        {
            const int TARGET_WIDTH = 95;  // Leave 5% margin

            var totalWidth = columns.Sum(c => c.Width ?? 20);

            if (totalWidth <= TARGET_WIDTH)
                return columns;  // Already fits!

            // Proportionally scale down all columns
            var scaleFactor = (double)TARGET_WIDTH / totalWidth;

            foreach (var col in columns)
            {
                var originalWidth = col.Width ?? 20;
                var scaledWidth = (int)Math.Round(originalWidth * scaleFactor);

                // Set minimum widths based on data type
                var minWidth = GetMinimumWidth(col.DataType);
                col.Width = Math.Max(scaledWidth, minWidth);
            }

            // Recalculate total after applying minimums
            totalWidth = columns.Sum(c => c.Width ?? 20);

            // If still over limit, reduce string columns first (they can wrap)
            if (totalWidth > TARGET_WIDTH)
            {
                var excessWidth = totalWidth - TARGET_WIDTH;
                var stringColumns = columns.Where(c => c.DataType == "string").ToList();

                foreach (var col in stringColumns)
                {
                    if (excessWidth <= 0) break;

                    var reduction = Math.Min(2, excessWidth);  // Reduce by max 2% per column
                    var newWidth = Math.Max((col.Width ?? 20) - reduction, GetMinimumWidth("string"));
                    excessWidth -= (col.Width ?? 20) - newWidth;
                    col.Width = newWidth;
                }
            }

            return columns;
        }

        /// <summary>
        /// Get minimum acceptable width for a data type
        /// </summary>
        private int GetMinimumWidth(string dataType)
        {
            return dataType switch
            {
                "string" => 12,    // Strings can wrap, so allow smaller
                "date" => 10,
                "number" => 6,
                "decimal" => 8,
                "currency" => 10,
                _ => 10
            };
        }
    }
}
