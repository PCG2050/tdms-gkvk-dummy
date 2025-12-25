using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.ConsultSocialMedia;
using Application.Interface.Repository.DataTables.TblService;
using Application.Interface.Repository.DataTables.KVK;
using Application.Interface.Repository.DataTables.EEU;
using Application.Interface.Repository.DataTables.FTI;
using Application.Interface.Repository.DataTables.STU;
using Application.Interface.Repository.DataTables.IBTVA;
using Application.Interface.Repository.DataTables.ATIC;
using Application.Interface.Repository.DataTables.DEU;
using Application.Interface.Repository.DataTables.NAEP;
using Application.Interface.Repository.DataTables.SAMETI;
using Application.Interface.Services;
using Application.Interface.Services.Reports;
using Application.Models;
using Application.Services;
using Application.Services.Reports;
using Application.Constants;
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
        private readonly IFinancialBudgetRepository _financialBudgetRepository;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly ICurrentUserService _currentUserService;

        // Unit-specific program repositories
        private readonly IKvkProgramDetailsRepository _kvkRepository;
        private readonly IEeuProgramDetailsRepository _eeuRepository;
        private readonly IFtiProgramDetailsRepository _ftiRepository;
        private readonly IStuProgramDetailsRepository _stuRepository;
        private readonly IIbtvaProgramDetailsRepository _ibtvaRepository;
        private readonly IAticProgramDetailsRepository _aticRepository;
        private readonly IDeuProgramDetailsRepository _deuRepository;
        private readonly INaepProgramDetailsRepository _naepRepository;
        private readonly ISametiProgramDetailsRepository _sametiRepository;

        public DynamicReportService(
            IPublicationRepository publicationRepository,
            INominationRewardRepository nominationRepository,
            IConsultingServiceRepository consultancyRepository,
            ITableServiceRepository serviceRepository,
            ITableOtherActivityRepository otherActivityRepository,
            IFinancialBudgetRepository financialBudgetRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            ICurrentUserService currentUserService,
            IKvkProgramDetailsRepository kvkRepository,
            IEeuProgramDetailsRepository eeuRepository,
            IFtiProgramDetailsRepository ftiRepository,
            IStuProgramDetailsRepository stuRepository,
            IIbtvaProgramDetailsRepository ibtvaRepository,
            IAticProgramDetailsRepository aticRepository,
            IDeuProgramDetailsRepository deuRepository,
            INaepProgramDetailsRepository naepRepository,
            ISametiProgramDetailsRepository sametiRepository)
        {
            _publicationRepository = publicationRepository;
            _nominationRepository = nominationRepository;
            _consultancyRepository = consultancyRepository;
            _serviceRepository = serviceRepository;
            _otherActivityRepository = otherActivityRepository;
            _financialBudgetRepository = financialBudgetRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _currentUserService = currentUserService;
            _kvkRepository = kvkRepository;
            _eeuRepository = eeuRepository;
            _ftiRepository = ftiRepository;
            _stuRepository = stuRepository;
            _ibtvaRepository = ibtvaRepository;
            _aticRepository = aticRepository;
            _deuRepository = deuRepository;
            _naepRepository = naepRepository;
            _sametiRepository = sametiRepository;
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
                ReportColumnRegistry.PROGRAMS => await BuildProgramsSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                ReportColumnRegistry.PUBLICATIONS => await BuildPublicationsSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                ReportColumnRegistry.NOMINATIONS => await BuildNominationsSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                ReportColumnRegistry.CONSULTANCIES => await BuildConsultanciesSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                ReportColumnRegistry.SERVICES => await BuildServicesSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                ReportColumnRegistry.FINANCIAL_STATUS => await BuildFinancialStatusSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                ReportColumnRegistry.OTHER_ACTIVITIES => await BuildOtherActivitiesSectionAsync(sectionRequest, unitLocationId, month, year, sectionDef, maxRows),
                // FIU Activities, and ASM Activities require special handling
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
                .Include(p => p.Category)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.PublisherDetails)
                .Include(p => p.ExtensionLiteratures);

            var total = await query.CountAsync();
            var data = await query.Take(maxRows).ToListAsync();

            var rows = data.Select(p => BuildRow(p, request.SelectedColumns, new Dictionary<string, Func<object, object?>>
            {
                ["title"] = (entity) => ((dynamic)entity).Title,
                ["category"] = (entity) => ((dynamic)entity).Category?.Name,
                ["mode"] = (entity) => ((dynamic)entity).Mode?.Name ?? ((dynamic)entity).ModePublication ?? "-",
                ["region"] = (entity) => ((dynamic)entity).Region?.Name ?? "-",
                ["publicationYear"] = (entity) => ((dynamic)entity).PublicationYear,
                ["publicationDate"] = (entity) => ((dynamic)entity).PublicationDate?.ToString("yyyy-MM-dd"),
                ["publisherName"] = (entity) => ((dynamic)entity).PublisherDetails?.PublisherName ?? "-",
                ["publisherInstitution"] = (entity) => ((dynamic)entity).PublisherDetails?.PublisherInstitutionName ?? "-",
                ["publisherAddress"] = (entity) => ((dynamic)entity).PublisherDetails?.PublisherAddress ?? "-",
                ["extensionLiterature"] = (entity) => ((dynamic)entity).ExtensionLiteratures
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
                    && n.CreatedAt.Year == year)
                .Include(n => n.Type)
                .Include(n => n.Region)
                .Include(n => n.Achievements)
                .Include(n => n.AwardRecognitions)
                    .ThenInclude(a => a.Contribution);

            var total = await query.CountAsync();
            var data = await query.Take(maxRows).ToListAsync();

            var rows = data.Select(n => BuildRow(n, request.SelectedColumns, new Dictionary<string, Func<object, object?>>
            {
                ["type"] = (entity) => ((dynamic)entity).Type?.Name ?? ((dynamic)entity).OtherType ?? "-",
                ["region"] = (entity) => ((dynamic)entity).Region?.Name ?? ((dynamic)entity).OtherRegion ?? "-",
                ["achievements"] = (entity) => ((dynamic)entity).Achievements,
                ["awards"] = (entity) => ((dynamic)entity).AwardRecognitions
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

        private async Task<ReportSection> BuildFinancialStatusSectionAsync(
            SectionRequest request,
            int unitLocationId,
            int month,
            int year,
            SectionDefinition sectionDef,
            int maxRows)
        {
            var query = _financialBudgetRepository.GetQueryable()
                .Where(f => f.UnitLocationId == unitLocationId
                    && f.FormStatus == "Approved"
                    && f.CreatedAt.Month == month
                    && f.CreatedAt.Year == year)
                .Include(f => f.Budgets)
                .Include(f => f.RevolvingFunds);

            var total = await query.CountAsync();
            var data = await query.Take(maxRows).ToListAsync();

            var rows = data.Select(f => BuildRow(f, request.SelectedColumns, new Dictionary<string, Func<object, object?>>
            {
                ["budgets"] = (entity) => ((dynamic)entity).Budgets,
                ["revolvingFunds"] = (entity) => ((dynamic)entity).RevolvingFunds
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

        private async Task<ReportSection?> BuildProgramsSectionAsync(
            SectionRequest request,
            int unitLocationId,
            int month,
            int year,
            SectionDefinition sectionDef,
            int maxRows)
        {
            // Get unit location to determine which unit repository to use
            var unitLocation = await _organizationUnitRepository.GetByIdAsync(unitLocationId);
            if (unitLocation == null)
                return null;

            var unitId = unitLocation.UnitId;

            // Query the appropriate repository based on unit ID
            IQueryable<dynamic> query = unitId switch
            {
                UnitConstants.KVK_UNIT_ID => _kvkRepository.GetQueryable()
                    .Where(p => p.UnitLocationId == unitLocationId && p.FormStatus == "Approved"
                        && p.CreatedAt.Month == month && p.CreatedAt.Year == year),
                UnitConstants.EEU_UNIT_ID => _eeuRepository.GetQueryable()
                    .Where(p => p.UnitLocationId == unitLocationId && p.FormStatus == "Approved"
                        && p.CreatedAt.Month == month && p.CreatedAt.Year == year),
                UnitConstants.FTI_UNIT_ID => _ftiRepository.GetQueryable()
                    .Where(p => p.UnitLocationId == unitLocationId && p.FormStatus == "Approved"
                        && p.CreatedAt.Month == month && p.CreatedAt.Year == year),
                UnitConstants.STU_UNIT_ID => _stuRepository.GetQueryable()
                    .Where(p => p.UnitLocationId == unitLocationId && p.FormStatus == "Approved"
                        && p.CreatedAt.Month == month && p.CreatedAt.Year == year),
                UnitConstants.IBTVA_UNIT_ID => _ibtvaRepository.GetQueryable()
                    .Where(p => p.UnitLocationId == unitLocationId && p.FormStatus == "Approved"
                        && p.CreatedAt.Month == month && p.CreatedAt.Year == year),
                UnitConstants.ATIC_UNIT_ID => _aticRepository.GetQueryable()
                    .Where(p => p.UnitLocationId == unitLocationId && p.FormStatus == "Approved"
                        && p.CreatedAt.Month == month && p.CreatedAt.Year == year),
                UnitConstants.DEU_UNIT_ID => _deuRepository.GetQueryable()
                    .Where(p => p.UnitLocationId == unitLocationId && p.FormStatus == "Approved"
                        && p.CreatedAt.Month == month && p.CreatedAt.Year == year),
                UnitConstants.NAEP_UNIT_ID => _naepRepository.GetQueryable()
                    .Where(p => p.UnitLocationId == unitLocationId && p.FormStatus == "Approved"
                        && p.CreatedAt.Month == month && p.CreatedAt.Year == year),
                UnitConstants.SAMETI_UNIT_ID => _sametiRepository.GetQueryable()
                    .Where(p => p.UnitLocationId == unitLocationId && p.FormStatus == "Approved"
                        && p.CreatedAt.Month == month && p.CreatedAt.Year == year),
                _ => null
            };

            if (query == null)
                return null;

            var total = await query.CountAsync();
            var data = await query.Take(maxRows).ToListAsync();

            // Build column mappings for program fields
            var columnMappings = BuildProgramColumnMappings();

            var rows = data.Select(p => BuildRow((object)p, request.SelectedColumns, columnMappings)).ToList();

            return new ReportSection
            {
                SectionKey = request.SectionKey,
                DisplayName = sectionDef.DisplayName,
                TotalRecords = total,
                Columns = BuildColumnMetadata(request.SelectedColumns, sectionDef),
                Rows = rows
            };
        }

        private Dictionary<string, Func<object, object?>> BuildProgramColumnMappings()
        {
            return new Dictionary<string, Func<object, object?>>
            {
                // Stepper 1: Program Details (direct properties on entity)
                ["title"] = (entity) => ((dynamic)entity).Title,
                ["startDate"] = (entity) => ((dynamic)entity).StartDate.ToString("yyyy-MM-dd"),
                ["endDate"] = (entity) => ((dynamic)entity).EndDate.ToString("yyyy-MM-dd"),
                ["programType"] = (entity) => ((dynamic)entity).ProgramType?.Type,
                ["category"] = (entity) => ((dynamic)entity).Category?.Name,
                ["type"] = (entity) => ((dynamic)entity).Type?.Name,
                ["theme"] = (entity) => ((dynamic)entity).Theme?.Name,
                ["sponsoredOrganization"] = (entity) => ((dynamic)entity).SponsoredOrganizationName,
                ["collaborator"] = (entity) => ((dynamic)entity).Collaborator?.Name,
                ["collaborativeProgram"] = (entity) => ((dynamic)entity).CollaborativeProgramOption?.Name,
                ["mode"] = (entity) => ((dynamic)entity).Mode?.Name,
                ["region"] = (entity) => ((dynamic)entity).Region?.Name,
                ["duration"] = (entity) => ((dynamic)entity).Duration,
                ["tpNo"] = (entity) => ((dynamic)entity).TPNo,
                ["location"] = (entity) => ((dynamic)entity).Location,
                ["sourceOfFund"] = (entity) => ((dynamic)entity).SourceOfFund?.Name,
                ["totalOutlay"] = (entity) => ((dynamic)entity).TotalOutlayRs,

                // Stepper 2: Participant Demographics (collection property)
                ["participantCategory"] = (entity) => ((dynamic)entity).ParticipantDemographics,
                ["maleTotal"] = (entity) => ((dynamic)entity).ParticipantDemographics,
                ["femaleTotal"] = (entity) => ((dynamic)entity).ParticipantDemographics,
                ["grandTotal"] = (entity) => ((dynamic)entity).ParticipantDemographics,
                ["stayedInHostel"] = (entity) => ((dynamic)entity).ParticipantDemographics,

                // Stepper 3: Program Content & Resources (nested collections)
                ["resourcePersons"] = (entity) => ((dynamic)entity).ProgramContent,
                ["topicsCovered"] = (entity) => ((dynamic)entity).ProgramContent,
                ["teachingAids"] = (entity) => ((dynamic)entity).ProgramContent,

                // Stepper 4: Advisory Services (single object)
                ["noOfFacebookSMS"] = (entity) => ((dynamic)entity).AdvisoryServices?.NoOfFacebookSMS,
                ["noOfWhatsAppSMS"] = (entity) => ((dynamic)entity).AdvisoryServices?.NoOfWhatsAppSMS,
                ["noOfWhatsAppQueries"] = (entity) => ((dynamic)entity).AdvisoryServices?.NoOfAnsweredWhatsappQueries,
                ["noOfPhoneCalls"] = (entity) => ((dynamic)entity).AdvisoryServices?.NoOfPhoneCalls,
                ["noOfFaceToFaceDiscussions"] = (entity) => ((dynamic)entity).AdvisoryServices?.NoOfFaceToFaceDiscussions,
                ["noOfEmailsSent"] = (entity) => ((dynamic)entity).AdvisoryServices?.NoOfEmailsSent,
                ["noOfBeneficiaries"] = (entity) => ((dynamic)entity).AdvisoryServices?.NoOfBeneficiaries,
                ["criticalInputsDistributed"] = (entity) => ((dynamic)entity).AdvisoryServices?.CriticalInputsDistributed,

                // Stepper 5: Results (nested within Results object)
                ["fldResults"] = (entity) => ((dynamic)entity).Results?.FldResults,
                ["oftResults"] = (entity) => ((dynamic)entity).Results?.OftResults,

                // Stepper 6: Report (Reports property, not Report)
                ["reportingDate"] = (entity) => ((dynamic)entity).Reports?.ReportDate?.ToString("yyyy-MM-dd"),
                ["significantOutcome"] = (entity) => ((dynamic)entity).Reports?.Outcome,
                ["geoTaggedPhoto"] = (entity) => ((dynamic)entity).Reports?.GeoTaggedPhoto,

                // Stepper 7: Recommendations (Recommendations property, not Recommendation)
                ["problemsIdentified"] = (entity) => ((dynamic)entity).Recommendations?.ProblemsIdentified,
                ["recommendations"] = (entity) => ((dynamic)entity).Recommendations?.Recommendation,
                ["actionTaken"] = (entity) => ((dynamic)entity).Recommendations?.ActionTaken,
                ["significantAchievement"] = (entity) => ((dynamic)entity).Recommendations?.SignificantAchievement,
                ["successStories"] = (entity) => ((dynamic)entity).Recommendations?.SuccessStories,
                ["outcome"] = (entity) => ((dynamic)entity).Recommendations?.ImpactOutcome
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
                "date" => 8,
                "number" => 8,
                "decimal" => 8,
                "currency" => 8,

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
                "date" => 8,
                "number" => 8,
                "decimal" => 8,
                "currency" => 8,
                _ => 10
            };
        }
    }
}
