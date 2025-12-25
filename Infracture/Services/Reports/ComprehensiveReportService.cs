using Application.Constants;
using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.ASM;
using Application.Interface.Repository.DataTables.ConsultSocialMedia;
using Application.Interface.Repository.DataTables.DEU;
using Application.Interface.Repository.DataTables.EEU;
using Application.Interface.Repository.DataTables.FIU;
using Application.Interface.Repository.DataTables.FTI;
using Application.Interface.Repository.DataTables.IBTVA;
using Application.Interface.Repository.DataTables.KVK;
using Application.Interface.Repository.DataTables.NAEP;
using Application.Interface.Repository.DataTables.SAMETI;
using Application.Interface.Repository.DataTables.STU;
using Application.Interface.Repository.DataTables.TblService;
using Application.Interface.Services;
using Application.Interface.Services.Reports;
using Application.Models.Reports;
using Application.Services;
using Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Infrastructure.Services.Reports
{
    public class ComprehensiveReportService : IComprehensiveReportService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;

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

        // Special unit repositories
        private readonly IFIUProgramActivityRepository _fiuRepository;
        private readonly IASMVisitorDetailsRepository _asmRepository;

        // Generic table repositories
        private readonly IPublicationRepository _publicationRepository;
        private readonly INominationRewardRepository _nominationRepository;
        private readonly IConsultingServiceRepository _consultancyRepository;
        private readonly ITableServiceRepository _serviceRepository;
        private readonly IFinancialBudgetRepository _financialRepository;

        public ComprehensiveReportService(
            ICurrentUserService currentUserService,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            IKvkProgramDetailsRepository kvkRepository,
            IEeuProgramDetailsRepository eeuRepository,
            IFtiProgramDetailsRepository ftiRepository,
            IStuProgramDetailsRepository stuRepository,
            IIbtvaProgramDetailsRepository ibtvaRepository,
            IAticProgramDetailsRepository aticRepository,
            IDeuProgramDetailsRepository deuRepository,
            INaepProgramDetailsRepository naepRepository,
            ISametiProgramDetailsRepository sametiRepository,
            IFIUProgramActivityRepository fiuRepository,
            IASMVisitorDetailsRepository asmRepository,
            IPublicationRepository publicationRepository,
            INominationRewardRepository nominationRepository,
            IConsultingServiceRepository consultancyRepository,
            ITableServiceRepository serviceRepository,
            IFinancialBudgetRepository financialRepository)
        {
            _currentUserService = currentUserService;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _kvkRepository = kvkRepository;
            _eeuRepository = eeuRepository;
            _ftiRepository = ftiRepository;
            _stuRepository = stuRepository;
            _ibtvaRepository = ibtvaRepository;
            _aticRepository = aticRepository;
            _deuRepository = deuRepository;
            _naepRepository = naepRepository;
            _sametiRepository = sametiRepository;
            _fiuRepository = fiuRepository;
            _asmRepository = asmRepository;
            _publicationRepository = publicationRepository;
            _nominationRepository = nominationRepository;
            _consultancyRepository = consultancyRepository;
            _serviceRepository = serviceRepository;
            _financialRepository = financialRepository;
        }

        public async Task<ServiceResult<ComprehensiveReportResponse>> GenerateReportAsync(
            ComprehensiveReportRequest request)
        {
            try
            {
                // Resolve UnitLocationIds from UnitId if provided
                List<int> unitLocationIds;
                int unitId;

                if (request.UnitId.HasValue)
                {
                    // Get ALL unit locations for this unit type
                    unitId = request.UnitId.Value;
                    var allLocationsResult = await GetUnitLocationsByUnitIdAsync(unitId);

                    if (!allLocationsResult.IsSuccess)
                        return ServiceResult<ComprehensiveReportResponse>.Failure(allLocationsResult.ErrorMessage);

                    unitLocationIds = allLocationsResult.Data!;

                    if (!unitLocationIds.Any())
                        return ServiceResult<ComprehensiveReportResponse>.Failure(
                            $"No unit locations found for unit ID: {unitId}");
                }
                else if (request.UnitLocationIds != null && request.UnitLocationIds.Any())
                {
                    unitLocationIds = request.UnitLocationIds;
                    // Get unit ID from first location
                    var firstLoc = await _organizationUnitRepository.GetByIdAsync(unitLocationIds.First());
                    if (firstLoc == null)
                        return ServiceResult<ComprehensiveReportResponse>.Failure("Invalid unit location ID");
                    unitId = firstLoc.UnitId;
                }
                else
                {
                    return ServiceResult<ComprehensiveReportResponse>.Failure(
                        "Either UnitId or UnitLocationIds must be provided");
                }

                // Validate access
                var accessResult = await ValidateAccessAsync(unitLocationIds);
                if (!accessResult.IsSuccess)
                    return ServiceResult<ComprehensiveReportResponse>.Failure(accessResult.ErrorMessage);

                // Get unit location details
                var unitLocations = await _organizationUnitRepository.GetByIdsAsync(unitLocationIds);
                if (!unitLocations.Any())
                    return ServiceResult<ComprehensiveReportResponse>.Failure("No valid unit locations found");

                // Build response
                var response = new ComprehensiveReportResponse
                {
                    Metadata = BuildMetadata(unitLocations, request, unitId)
                };

                // Build report based on type
                switch (request.ReportType.ToLower())
                {
                    case "program":
                        await BuildProgramReportAsync(response, request, unitId, unitLocationIds);
                        break;

                    case "publication":
                        await BuildPublicationReportAsync(response, request, unitLocationIds);
                        break;

                    case "awards":
                        await BuildAwardsReportAsync(response, request, unitLocationIds);
                        break;

                    case "consultancy":
                        await BuildConsultancyReportAsync(response, request, unitLocationIds);
                        break;

                    case "services":
                        await BuildServicesReportAsync(response, request, unitLocationIds);
                        break;

                    case "financial":
                        await BuildFinancialReportAsync(response, request, unitLocationIds);
                        break;

                    default:
                        return ServiceResult<ComprehensiveReportResponse>.Failure(
                            $"Unknown report type: {request.ReportType}");
                }

                return ServiceResult<ComprehensiveReportResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return ServiceResult<ComprehensiveReportResponse>.Failure(
                    $"Failed to generate report: {ex.Message}");
            }
        }

        public async Task<ServiceResult<ComprehensiveReportResponse>> GenerateMultiUnitReportAsync(
            ComprehensiveReportRequest request)
        {
            // Multi-unit report uses the same logic, just with multiple unit location IDs
            if (request.UnitLocationIds.Count < 2)
                return ServiceResult<ComprehensiveReportResponse>.Failure(
                    "Multi-unit report requires at least 2 unit locations");

            return await GenerateReportAsync(request);
        }

        public async Task<ServiceResult<List<string>>> GetAvailableReportTypesAsync(int unitId)
        {
            var reportTypes = new List<string>();

            // FIU and ASM don't support program reports
            if (unitId != UnitConstants.FIU_UNIT_ID && unitId != UnitConstants.ASM_UNIT_ID)
            {
                reportTypes.Add("program");
            }

            // All units support these generic reports
            reportTypes.Add("publication");
            reportTypes.Add("awards");
            reportTypes.Add("consultancy");
            reportTypes.Add("services");
            reportTypes.Add("financial");

            return ServiceResult<List<string>>.Success(reportTypes);
        }

        public async Task<ServiceResult<ComprehensiveReportResponse>> PreviewReportAsync(
            ComprehensiveReportRequest request,
            int maxRowsPerSection = 5)
        {
            // Generate with preview flag - will limit rows
            var result = await GenerateReportAsync(request);

            if (result.IsSuccess && result.Data != null)
            {
                // Limit rows in each section for preview
                LimitPreviewRows(result.Data, maxRowsPerSection);
            }

            return result;
        }

        // ========================================
        // PRIVATE HELPER METHODS
        // ========================================

        private async Task<ServiceResult> ValidateAccessAsync(List<int> unitLocationIds)
        {
            if (_currentUserService.Role != Role.ADMIN && _currentUserService.Role != Role.UNITHEAD)
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (_currentUserService.Role == Role.UNITHEAD)
            {
                var allowedLocationIds = await _unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);

                var unauthorizedIds = unitLocationIds.Except(allowedLocationIds).ToList();
                if (unauthorizedIds.Any())
                {
                    return ServiceResult.Failure(
                        $"You don't have access to unit location(s): {string.Join(", ", unauthorizedIds)}",
                        ServiceErrorStatus.FORBIDDEN);
                }
            }

            return ServiceResult.Success();
        }

        /// <summary>
        /// Get all unit location IDs for a specific unit type, filtered by user access
        /// </summary>
        private async Task<ServiceResult<List<int>>> GetUnitLocationsByUnitIdAsync(int unitId)
        {
            try
            {
                // Get all locations for this unit type
                var allLocations = await _organizationUnitRepository.GetByUnitIdAsync(unitId);

                if (allLocations == null || !allLocations.Any())
                    return ServiceResult<List<int>>.Failure($"No locations found for unit ID: {unitId}");

                var allLocationIds = allLocations.Select(l => l.Id).ToList();

                // Filter by user access
                if (_currentUserService.Role == Role.UNITHEAD)
                {
                    var allowedLocationIds = await _unitHeadAssignmentRepository
                        .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);

                    allLocationIds = allLocationIds.Intersect(allowedLocationIds).ToList();

                    if (!allLocationIds.Any())
                        return ServiceResult<List<int>>.Failure(
                            "You don't have access to any locations for this unit type",
                            ServiceErrorStatus.FORBIDDEN);
                }
                else if (_currentUserService.Role == Role.ADMIN)
                {
                    // Admin can access all locations in their organization
                    var orgLocations = await _organizationUnitRepository
                        .GetUnitLocationIdsByOrganizationIdAsync(_currentUserService.OrganizationId);

                    allLocationIds = allLocationIds.Intersect(orgLocations).ToList();
                }

                return ServiceResult<List<int>>.Success(allLocationIds);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<int>>.Failure($"Failed to get unit locations: {ex.Message}");
            }
        }

        private ReportMetadata BuildMetadata(
            List<Domain.Entities.Junction.OrganizationUnitLocation> unitLocations,
            ComprehensiveReportRequest request,
            int unitId)
        {
            var firstLocation = unitLocations.First();

            return new ReportMetadata
            {
                UnitId = unitId,
                UnitName = firstLocation.Unit?.Name ?? "Unknown",
                UnitLocations = unitLocations.Select(ul => new UnitLocationInfo
                {
                    Id = ul.Id,
                    Name = $"{ul.District?.Name}, {ul.District?.State?.Name}",
                    District = ul.District?.Name ?? "",
                    State = ul.District?.State?.Name ?? ""
                }).ToList(),
                District = firstLocation.District?.Name ?? "",
                State = firstLocation.District?.State?.Name ?? "",
                PeriodType = request.PeriodType,
                Month = request.Month,
                Quarter = request.Quarter,
                Year = request.Year,
                MonthName = request.Month.HasValue
                    ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.Month.Value)
                    : "",
                PeriodDescription = GetPeriodDescription(request),
                ReportType = request.ReportType,
                GeneratedAt = DateTimeOffset.UtcNow,
                IsMultiUnitReport = unitLocations.Count > 1
            };
        }

        private DateTimeOffset GetStartDate(int year, int month) =>
            new DateTimeOffset(new DateTime(year, month, 1), TimeSpan.Zero);

        private DateTimeOffset GetEndDate(int year, int month) =>
            GetStartDate(year, month).AddMonths(1);

        private void LimitPreviewRows(ComprehensiveReportResponse response, int maxRows)
        {
            if (response.Programs != null && response.Programs.Count > maxRows)
                response.Programs = response.Programs.Take(maxRows).ToList();

            if (response.Publications != null && response.Publications.Count > maxRows)
                response.Publications = response.Publications.Take(maxRows).ToList();

            if (response.Consultancies != null && response.Consultancies.Count > maxRows)
                response.Consultancies = response.Consultancies.Take(maxRows).ToList();
        }

        // ========================================
        // PROGRAM REPORT BUILDER
        // ========================================

        private async Task BuildProgramReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request,
            int unitId,
            List<int> unitLocationIds)
        {
            var (startDate, endDate) = GetDateRangeForPeriod(request);
            var formStatus = request.FormStatus ?? "Approved";

            // Handle FIU unit
            if (unitId == UnitConstants.FIU_UNIT_ID)
            {
                await BuildFIUActivitiesAsync(response, unitLocationIds, startDate, endDate);
                return;
            }

            // Handle ASM unit
            if (unitId == UnitConstants.ASM_UNIT_ID)
            {
                await BuildASMActivitiesAsync(response, unitLocationIds, startDate, endDate);
                return;
            }

            // Get programs based on unit type
            response.Programs = await GetProgramsByUnitAsync(
                unitId, unitLocationIds, startDate, endDate, formStatus, request.IncludeSubSections);

            response.Metadata.TotalEntries = response.Programs?.Count ?? 0;
        }

        private async Task<List<ProgramOrganizedData>> GetProgramsByUnitAsync(
            int unitId,
            List<int> unitLocationIds,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            string formStatus,
            bool includeSubSections)
        {
            var programs = new List<ProgramOrganizedData>();

            switch (unitId)
            {
                case UnitConstants.KVK_UNIT_ID:
                    programs = await GetKvkProgramsAsync(unitLocationIds, startDate, endDate, formStatus, includeSubSections);
                    break;

                case UnitConstants.EEU_UNIT_ID:
                    programs = await GetEeuProgramsAsync(unitLocationIds, startDate, endDate, formStatus, includeSubSections);
                    break;

                case UnitConstants.FTI_UNIT_ID:
                    programs = await GetFtiProgramsAsync(unitLocationIds, startDate, endDate, formStatus);
                    break;

                case UnitConstants.STU_UNIT_ID:
                    programs = await GetStuProgramsAsync(unitLocationIds, startDate, endDate, formStatus);
                    break;

                case UnitConstants.IBTVA_UNIT_ID:
                    programs = await GetIbtvaProgramsAsync(unitLocationIds, startDate, endDate, formStatus);
                    break;

                case UnitConstants.ATIC_UNIT_ID:
                    programs = await GetAticProgramsAsync(unitLocationIds, startDate, endDate, formStatus);
                    break;

                case UnitConstants.DEU_UNIT_ID:
                    programs = await GetDeuProgramsAsync(unitLocationIds, startDate, endDate, formStatus);
                    break;

                case UnitConstants.NAEP_UNIT_ID:
                    programs = await GetNaepProgramsAsync(unitLocationIds, startDate, endDate, formStatus);
                    break;

                case UnitConstants.SAMETI_UNIT_ID:
                    programs = await GetSametiProgramsAsync(unitLocationIds, startDate, endDate, formStatus);
                    break;
            }

            return programs;
        }

        private async Task<List<ProgramOrganizedData>> GetKvkProgramsAsync(
            List<int> unitLocationIds,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            string formStatus,
            bool includeSubSections)
        {
            var query = _kvkRepository.GetQueryable()
                .Where(p => unitLocationIds.Contains(p.UnitLocationId)
                    && p.FormStatus == formStatus
                    && p.CreatedAt >= startDate && p.CreatedAt < endDate)
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Theme)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.Status)
                .Include(p => p.SourceOfFund)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.District)
                .Include(p => p.ParticipantDemographics);

            IQueryable<KvkProgramDetails> finalQuery = query;
            if (includeSubSections)
            {
                finalQuery = query
                    .Include(p => p.ProgramContent)
                    .Include(p => p.AdvisoryServices)
                        .ThenInclude(a => a.CriticalInputsDistributed)
                    .Include(p => p.Results)
                        .ThenInclude(r => r.FldResults)
                    .Include(p => p.Results)
                        .ThenInclude(r => r.OftResults)
                    .Include(p => p.Reports)
                    .Include(p => p.Recommendations);
            }

            var data = await finalQuery.ToListAsync();

            return data.Select(p => new ProgramOrganizedData
            {
                Id = p.Id,
                UnitLocationId = p.UnitLocationId,
                UnitLocationName = $"{p.UnitLocation?.District?.Name}, {p.UnitLocation?.District?.State?.Name}",
                Title = p.Title ?? "",
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProgramType = p.ProgramType?.Name ?? "",
                Category = p.Category?.Name ?? "",
                Theme = p.Theme?.Name ?? "",
                Mode = p.Mode?.Name ?? "",
                Region = p.Region?.Name ?? "",
                Duration = int.TryParse(p.Duration, out var d) ? d : 0,
                Location = p.Location ?? "",
                SourceOfFund = p.SourceOfFund?.Name ?? "",
                TotalOutlay = p.TotalOutlayRs ?? 0,
                FormStatus = p.FormStatus,
                TotalParticipants = p.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0,
                Collaborator = p.Collaborator?.Name ?? "",
                CollaborativeProgram = (p.CollaborativeProgramOption?.Name ?? "") != "",

                // Sub-sections (if requested)
                Demographics = includeSubSections ? MapDemographics(p.ParticipantDemographics) : null,
                Content = includeSubSections ? MapProgramContent(p.ProgramContent?.FirstOrDefault()) : null,
                AdvisoryServices = includeSubSections ? MapAdvisoryServices(p.AdvisoryServices) : null,
                Results = includeSubSections ? MapResults(p.Results) : null,
                ReportSection = includeSubSections ? MapReportSection(p.Reports) : null,
                Recommendations = includeSubSections ? MapRecommendations(p.Recommendations) : null
            }).ToList();
        }

        private async Task<List<ProgramOrganizedData>> GetEeuProgramsAsync(
            List<int> unitLocationIds,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            string formStatus,
            bool includeSubSections)
        {
            var query = _eeuRepository.GetQueryable()
                .Where(p => unitLocationIds.Contains(p.UnitLocationId)
                    && p.FormStatus == formStatus
                    && p.CreatedAt >= startDate && p.CreatedAt < endDate)
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Theme)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.Status)
                .Include(p => p.SourceOfFund)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.District)
                .Include(p => p.ParticipantDemographics);

            var data = await query.ToListAsync();

            return data.Select(p => new ProgramOrganizedData
            {
                Id = p.Id,
                UnitLocationId = p.UnitLocationId,
                UnitLocationName = $"{p.UnitLocation?.District?.Name}, {p.UnitLocation?.District?.State?.Name}",
                Title = p.Title ?? "",
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProgramType = p.ProgramType?.Name ?? "",
                Category = p.Category?.Name ?? "",
                Theme = p.Theme?.Name ?? "",
                Mode = p.Mode?.Name ?? "",
                Region = p.Region?.Name ?? "",
                Duration = int.TryParse(p.Duration, out var d) ? d : 0,
                Location = p.Location ?? "",
                SourceOfFund = p.SourceOfFund?.Name ?? "",
                TotalOutlay = p.TotalOutlayRs ?? 0,
                FormStatus = p.FormStatus,
                TotalParticipants = p.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0
            }).ToList();
        }

        // Simplified implementations for other units (following same pattern)
        private async Task<List<ProgramOrganizedData>> GetFtiProgramsAsync(
            List<int> unitLocationIds, DateTimeOffset startDate, DateTimeOffset endDate, string formStatus)
        {
            var query = _ftiRepository.GetQueryable()
                .Where(p => unitLocationIds.Contains(p.UnitLocationId)
                    && p.FormStatus == formStatus
                    && p.CreatedAt >= startDate && p.CreatedAt < endDate)
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Theme)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.Status)
                .Include(p => p.SourceOfFund)
                .Include(p => p.UnitLocation).ThenInclude(ul => ul.District)
                .Include(p => p.ParticipantDemographics);

            var data = await query.ToListAsync();

            return data.Select(p => new ProgramOrganizedData
            {
                Id = p.Id,
                UnitLocationId = p.UnitLocationId,
                UnitLocationName = $"{p.UnitLocation?.District?.Name}",
                Title = p.Title ?? "",
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProgramType = p.ProgramType?.Name ?? "",
                Category = p.Category?.Name ?? "",
                Theme = p.Theme?.Name ?? "",
                Mode = p.Mode?.Name ?? "",
                Region = p.Region?.Name ?? "",
                Duration = int.TryParse(p.Duration, out var d) ? d : 0,
                SourceOfFund = p.SourceOfFund?.Name ?? "",
                TotalOutlay = p.TotalOutlayRs ?? 0,
                FormStatus = p.FormStatus,
                TotalParticipants = p.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0
            }).ToList();
        }

        private async Task<List<ProgramOrganizedData>> GetStuProgramsAsync(
            List<int> unitLocationIds, DateTimeOffset startDate, DateTimeOffset endDate, string formStatus)
        {
            var query = _stuRepository.GetQueryable()
                .Where(p => unitLocationIds.Contains(p.UnitLocationId)
                    && p.FormStatus == formStatus
                    && p.CreatedAt >= startDate && p.CreatedAt < endDate)
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Theme)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.Status)
                .Include(p => p.SourceOfFund)
                .Include(p => p.UnitLocation).ThenInclude(ul => ul.District)
                .Include(p => p.ParticipantDemographics);

            var data = await query.ToListAsync();

            return data.Select(p => new ProgramOrganizedData
            {
                Id = p.Id,
                UnitLocationId = p.UnitLocationId,
                UnitLocationName = $"{p.UnitLocation?.District?.Name}",
                Title = p.Title ?? "",
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProgramType = p.ProgramType?.Name ?? "",
                Category = p.Category?.Name ?? "",
                Theme = p.Theme?.Name ?? "",
                Mode = p.Mode?.Name ?? "",
                Region = p.Region?.Name ?? "",
                Duration = int.TryParse(p.Duration, out var d) ? d : 0,
                SourceOfFund = p.SourceOfFund?.Name ?? "",
                TotalOutlay = p.TotalOutlayRs ?? 0,
                FormStatus = p.FormStatus,
                TotalParticipants = p.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0
            }).ToList();
        }

        private async Task<List<ProgramOrganizedData>> GetIbtvaProgramsAsync(
            List<int> unitLocationIds, DateTimeOffset startDate, DateTimeOffset endDate, string formStatus)
        {
            var query = _ibtvaRepository.GetQueryable()
                .Where(p => unitLocationIds.Contains(p.UnitLocationId)
                    && p.FormStatus == formStatus
                    && p.CreatedAt >= startDate && p.CreatedAt < endDate)
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Theme)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.Status)
                .Include(p => p.SourceOfFund)
                .Include(p => p.UnitLocation).ThenInclude(ul => ul.District)
                .Include(p => p.ParticipantDemographics);

            var data = await query.ToListAsync();

            return data.Select(p => new ProgramOrganizedData
            {
                Id = p.Id,
                UnitLocationId = p.UnitLocationId,
                UnitLocationName = $"{p.UnitLocation?.District?.Name}",
                Title = p.Title ?? "",
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProgramType = p.ProgramType?.Name ?? "",
                Category = p.Category?.Name ?? "",
                Theme = p.Theme?.Name ?? "",
                Mode = p.Mode?.Name ?? "",
                Region = p.Region?.Name ?? "",
                Duration = int.TryParse(p.Duration, out var d) ? d : 0,
                SourceOfFund = p.SourceOfFund?.Name ?? "",
                TotalOutlay = p.TotalOutlayRs ?? 0,
                FormStatus = p.FormStatus,
                TotalParticipants = p.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0
            }).ToList();
        }

        private async Task<List<ProgramOrganizedData>> GetAticProgramsAsync(
            List<int> unitLocationIds, DateTimeOffset startDate, DateTimeOffset endDate, string formStatus)
        {
            var query = _aticRepository.GetQueryable()
                .Where(p => unitLocationIds.Contains(p.UnitLocationId)
                    && p.FormStatus == formStatus
                    && p.CreatedAt >= startDate && p.CreatedAt < endDate)
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Theme)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.Status)
                .Include(p => p.SourceOfFund)
                .Include(p => p.UnitLocation).ThenInclude(ul => ul.District)
                .Include(p => p.ParticipantDemographics);

            var data = await query.ToListAsync();

            return data.Select(p => new ProgramOrganizedData
            {
                Id = p.Id,
                UnitLocationId = p.UnitLocationId,
                UnitLocationName = $"{p.UnitLocation?.District?.Name}",
                Title = p.Title ?? "",
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProgramType = p.ProgramType?.Name ?? "",
                Category = p.Category?.Name ?? "",
                Theme = p.Theme?.Name ?? "",
                Mode = p.Mode?.Name ?? "",
                Region = p.Region?.Name ?? "",
                Duration = int.TryParse(p.Duration, out var d) ? d : 0,
                SourceOfFund = p.SourceOfFund?.Name ?? "",
                TotalOutlay = p.TotalOutlayRs ?? 0,
                FormStatus = p.FormStatus,
                TotalParticipants = p.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0
            }).ToList();
        }

        private async Task<List<ProgramOrganizedData>> GetDeuProgramsAsync(
            List<int> unitLocationIds, DateTimeOffset startDate, DateTimeOffset endDate, string formStatus)
        {
            var query = _deuRepository.GetQueryable()
                .Where(p => unitLocationIds.Contains(p.UnitLocationId)
                    && p.FormStatus == formStatus
                    && p.CreatedAt >= startDate && p.CreatedAt < endDate)
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Theme)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.Status)
                .Include(p => p.SourceOfFund)
                .Include(p => p.UnitLocation).ThenInclude(ul => ul.District)
                .Include(p => p.ParticipantDemographics);

            var data = await query.ToListAsync();

            return data.Select(p => new ProgramOrganizedData
            {
                Id = p.Id,
                UnitLocationId = p.UnitLocationId,
                UnitLocationName = $"{p.UnitLocation?.District?.Name}",
                Title = p.Title ?? "",
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProgramType = p.ProgramType?.Name ?? "",
                Category = p.Category?.Name ?? "",
                Theme = p.Theme?.Name ?? "",
                Mode = p.Mode?.Name ?? "",
                Region = p.Region?.Name ?? "",
                Duration = int.TryParse(p.Duration, out var d) ? d : 0,
                SourceOfFund = p.SourceOfFund?.Name ?? "",
                TotalOutlay = p.TotalOutlayRs ?? 0,
                FormStatus = p.FormStatus,
                TotalParticipants = p.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0
            }).ToList();
        }

        private async Task<List<ProgramOrganizedData>> GetNaepProgramsAsync(
            List<int> unitLocationIds, DateTimeOffset startDate, DateTimeOffset endDate, string formStatus)
        {
            var query = _naepRepository.GetQueryable()
                .Where(p => unitLocationIds.Contains(p.UnitLocationId)
                    && p.FormStatus == formStatus
                    && p.CreatedAt >= startDate && p.CreatedAt < endDate)
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Theme)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.Status)
                .Include(p => p.SourceOfFund)
                .Include(p => p.UnitLocation).ThenInclude(ul => ul.District)
                .Include(p => p.ParticipantDemographics);

            var data = await query.ToListAsync();

            return data.Select(p => new ProgramOrganizedData
            {
                Id = p.Id,
                UnitLocationId = p.UnitLocationId,
                UnitLocationName = $"{p.UnitLocation?.District?.Name}",
                Title = p.Title ?? "",
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProgramType = p.ProgramType?.Name ?? "",
                Category = p.Category?.Name ?? "",
                Theme = p.Theme?.Name ?? "",
                Mode = p.Mode?.Name ?? "",
                Region = p.Region?.Name ?? "",
                Duration = int.TryParse(p.Duration, out var d) ? d : 0,
                SourceOfFund = p.SourceOfFund?.Name ?? "",
                TotalOutlay = p.TotalOutlayRs ?? 0,
                FormStatus = p.FormStatus,
                TotalParticipants = p.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0
            }).ToList();
        }

        private async Task<List<ProgramOrganizedData>> GetSametiProgramsAsync(
            List<int> unitLocationIds, DateTimeOffset startDate, DateTimeOffset endDate, string formStatus)
        {
            var query = _sametiRepository.GetQueryable()
                .Where(p => unitLocationIds.Contains(p.UnitLocationId)
                    && p.FormStatus == formStatus
                    && p.CreatedAt >= startDate && p.CreatedAt < endDate)
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Theme)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.Status)
                .Include(p => p.SourceOfFund)
                .Include(p => p.UnitLocation).ThenInclude(ul => ul.District)
                .Include(p => p.ParticipantDemographics);

            var data = await query.ToListAsync();

            return data.Select(p => new ProgramOrganizedData
            {
                Id = p.Id,
                UnitLocationId = p.UnitLocationId,
                UnitLocationName = $"{p.UnitLocation?.District?.Name}",
                Title = p.Title ?? "",
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProgramType = p.ProgramType?.Name ?? "",
                Category = p.Category?.Name ?? "",
                Theme = p.Theme?.Name ?? "",
                Mode = p.Mode?.Name ?? "",
                Region = p.Region?.Name ?? "",
                Duration = int.TryParse(p.Duration, out var d) ? d : 0,
                SourceOfFund = p.SourceOfFund?.Name ?? "",
                TotalOutlay = p.TotalOutlayRs ?? 0,
                FormStatus = p.FormStatus,
                TotalParticipants = p.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0
            }).ToList();
        }

        // ========================================
        // FIU & ASM SPECIAL UNIT BUILDERS
        // ========================================

        private async Task BuildFIUActivitiesAsync(
            ComprehensiveReportResponse response,
            List<int> unitLocationIds,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            // Extract year and month from startDate for backward compatibility with repository
            int year = startDate.Year;
            int month = startDate.Month;

            var (activities, totalEntries) = await _fiuRepository.GetMonthlyActivitySummaryAsync(
                unitLocationIds, year, month);

            response.FIUActivities = new FIUActivitiesData
            {
                Activities = activities.Select(a => new FIUActivityItem
                {
                    ActivityName = a.ActivityName,
                    Count = a.Count
                }).ToList(),
                TotalActivities = activities.Count,
                TotalCount = activities.Sum(a => a.Count)
            };

            response.Metadata.TotalEntries = totalEntries;
        }

        private async Task BuildASMActivitiesAsync(
            ComprehensiveReportResponse response,
            List<int> unitLocationIds,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            // Extract year and month from startDate for backward compatibility with repository
            int year = startDate.Year;
            int month = startDate.Month;

            var (summary, totalEntries) = await _asmRepository.GetMonthlyVisitorSummaryAsync(
                unitLocationIds, year, month);

            response.ASMActivities = new ASMActivitiesData
            {
                Visitors = new List<ASMVisitorItem>
                {
                    new() { Particulars = "Farmers", NoOfVisitors = summary.TotalFarmers },
                    new() { Particulars = "Students", NoOfVisitors = summary.TotalStudents },
                    new() { Particulars = "Other public visitors", NoOfVisitors = summary.TotalPublic }
                },
                TotalVisitors = summary.TotalVisitors,
                TotalEntries = totalEntries,
                TotalFarmers = summary.TotalFarmers,
                TotalStudents = summary.TotalStudents,
                TotalPublic = summary.TotalPublic
            };

            response.Metadata.TotalEntries = totalEntries;
        }

        // ========================================
        // OTHER REPORT BUILDERS
        // ========================================

        private async Task BuildPublicationReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request,
            List<int> unitLocationIds)
        {
            var (startDate, endDate) = GetDateRangeForPeriod(request);
            var formStatus = request.FormStatus ?? "Approved";

            var query = _publicationRepository.GetQueryable()
                .Where(p => unitLocationIds.Contains(p.UnitLocationId)
                    && p.FormStatus == formStatus
                    && p.CreatedAt >= startDate && p.CreatedAt < endDate)
                .Include(p => p.Category)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.UnitLocation).ThenInclude(ul => ul.District)
                .Include(p => p.PublisherDetails)
                .Include(p => p.ExtensionLiteratures);

            var data = await query.ToListAsync();

            response.Publications = data.Select(p => new PublicationData
            {
                Id = p.Id,
                UnitLocationId = p.UnitLocationId,
                UnitLocationName = $"{p.UnitLocation?.District?.Name}",
                Title = p.Title ?? "",
                Category = p.Category?.Name ?? "",
                Mode = p.Mode?.Name ?? "",
                Region = p.Region?.Name ?? "",
                JournalName = p.PublicationJournalTitle,
                PublicationDate = p.PublicationDate,
                TotalPages = p.PublicationPagesTo - p.PublicationPagesFrom,
                WebLink = p.PublicationWebLink,
                FormStatus = p.FormStatus,
                Authors = p.PublisherDetails != null ? new List<AuthorData>
                {
                    new AuthorData
                    {
                        PublicationId = p.Id,
                        Name = p.PublisherDetails.PublisherName ?? "",
                        Institution = p.PublisherDetails.PublisherInstitutionName ?? "",
                        Address = p.PublisherDetails.PublisherAddress ?? ""
                    }
                } : null,
                ExtensionLiterature = p.ExtensionLiteratures?.Select(e => new ExtensionLiteratureItem
                {
                    PublicationId = p.Id,
                    Date = e.Date,
                    AmountPerCopy = e.AmountPerCopy ?? 0,
                    CopiesSoldOrDistributed = e.NumberOfCopies ??0,
                    AmountGenerated = (e.AmountPerCopy ?? 0) * (e.NumberOfCopies ?? 0)
                }).ToList()
            }).ToList();

            // Aggregate authors for the Authors section
            response.Authors = response.Publications
                .Where(p => p.Authors != null)
                .SelectMany(p => p.Authors!)
                .ToList();

            // Aggregate extension literature
            var allExtLit = response.Publications
                .Where(p => p.ExtensionLiterature != null)
                .SelectMany(p => p.ExtensionLiterature!)
                .ToList();

            response.ExtensionLiterature = new ExtensionLiteratureData
            {
                Items = allExtLit,
                TotalAmountGenerated = allExtLit.Sum(e => e.AmountGenerated),
                TotalCopiesSold = allExtLit.Sum(e => e.CopiesSoldOrDistributed)
            };

            response.Metadata.TotalEntries = response.Publications.Count;
        }

        private async Task BuildAwardsReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request,
            List<int> unitLocationIds)
        {
            var (startDate, endDate) = GetDateRangeForPeriod(request);
            var formStatus = request.FormStatus ?? "Approved";

            var query = _nominationRepository.GetQueryable()
                .Where(n => unitLocationIds.Contains(n.UnitLocationId)
                    && n.FormStatus == formStatus
                    && n.CreatedAt >= startDate && n.CreatedAt < endDate)
                .Include(n => n.Type)
                .Include(n => n.Region)
                .Include(n => n.UnitLocation).ThenInclude(ul => ul.District)
                .Include(n => n.Achievements)
                .Include(n => n.AwardRecognitions);

            var data = await query.ToListAsync();

            if (data.Any())
            {
                var first = data.First();
                response.Awards = new AwardsData
                {
                    Id = first.Id,
                    UnitLocationId = first.UnitLocationId,
                    UnitLocationName = $"{first.UnitLocation?.District?.Name}",
                    Type = first.Type?.Name ?? "",
                    Region = first.Region?.Name ?? "",
                    FormStatus = first.FormStatus,
                    Achievements = data.SelectMany(n => n.Achievements?.Select(a => new AchievementData
                    {
                        Name = a.Name ?? "",
                        Achievement = a.AchievementDetail ?? ""
                    }) ?? Enumerable.Empty<AchievementData>()).ToList(),
                    AwardsReceived = data.SelectMany(n => n.AwardRecognitions?.Select(a => new AwardReceivedData
                    {
                        AwardingAgency = a.AwardingAgency ?? "",
                        Institution = a.InstitutionName ?? "",
                        Contribution = a.Contribution.Name ?? ""
                    }) ?? Enumerable.Empty<AwardReceivedData>()).ToList()
                };
            }

            response.Metadata.TotalEntries = data.Count;
        }

        private async Task BuildConsultancyReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request,
            List<int> unitLocationIds)
        {
            var (startDate, endDate) = GetDateRangeForPeriod(request);
            var formStatus = request.FormStatus ?? "Approved";

            var query = _consultancyRepository.GetQueryable()
                .Where(c => unitLocationIds.Contains(c.UnitLocationId)
                    && c.FormStatus == formStatus
                    && c.CreatedAt >= startDate && c.CreatedAt < endDate)
                .Include(c => c.Category)
                .Include(c => c.UnitLocation).ThenInclude(ul => ul.District);

            var data = await query.ToListAsync();

            response.Consultancies = data.Select(c => new ConsultancyData
            {
                Id = c.Id,
                UnitLocationId = c.UnitLocationId,
                UnitLocationName = $"{c.UnitLocation?.District?.Name}",
                Category = c.Category?.Name ?? "",
                Title = c.Title ?? "",
                RelatedToDiscipline = c.RelatedTo.Name,
                TotalNoOfBeneficiaries = c.NoOfBeneficiaries ,
                FormStatus = c.FormStatus,
                MaleSC = c.Male_SC ,
                FemaleSC = c.Female_SC ,
                MaleST = c.Male_ST ,
                FemaleST = c.Female_ST,
                MaleOBC = c.Male_OBC ,
                FemaleOBC = c.Female_OBC,
                MaleGEN = c.Male_GEN ,
                FemaleGEN = c.Female_GEN 
            }).ToList();

            response.Metadata.TotalEntries = response.Consultancies.Count;
        }

        private async Task BuildServicesReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request,
            List<int> unitLocationIds)
        {
            var (startDate, endDate) = GetDateRangeForPeriod(request);
            var formStatus = request.FormStatus ?? "Approved";

            var query = _serviceRepository.GetQueryable()
                .Where(s => unitLocationIds.Contains(s.UnitLocationId)
                    && s.FormStatus == formStatus
                    && s.CreatedAt >= startDate && s.CreatedAt < endDate)
                .Include(s => s.Category)
                .Include(s => s.Theme)
                .Include(s => s.SourceOfFund)
                .Include(s => s.QuantityUnit)
                .Include(s => s.UnitLocation).ThenInclude(ul => ul.District)
                .Include(s => s.VisitorDetails)
                .Include(s => s.Hostels);

            var data = await query.ToListAsync();

            var services = data.Select(s => new ServiceItem
            {
                Id = s.Id,
                UnitLocationId = s.UnitLocationId,
                UnitLocationName = $"{s.UnitLocation?.District?.Name}",
                Category = s.Category?.Name ?? "",
                Theme = s.Theme?.Name ?? "",
                SourceOfFunds = s.SourceOfFund?.Name ?? "",
                Components = s.Component ?? "",
                Units = s.QuantityUnit?.Name ?? "",
                Number = s.Number,
                AmountGenerated = s.AmountGenerated,
                FormStatus = s.FormStatus
            }).ToList();

            // Aggregate visitors
            var allVisitors = data.SelectMany(s => s.VisitorDetails ?? new List<Domain.Entities.GenericTables.Service.VisitorDetail>()).ToList();
            var visitors = new VisitorsData
            {
                MaleSC = allVisitors.Sum(v => v.Male_SC),
                FemaleSC = allVisitors.Sum(v => v.Female_SC),
                MaleST = allVisitors.Sum(v => v.Male_ST),
                FemaleST = allVisitors.Sum(v => v.Female_ST),
                MaleOBC = allVisitors.Sum(v => v.Male_OBC),
                FemaleOBC = allVisitors.Sum(v => v.Female_OBC),
                MaleGEN = allVisitors.Sum(v => v.Male_GEN),
                FemaleGEN = allVisitors.Sum(v => v.Female_GEN)
            };
            visitors.Male = visitors.MaleSC + visitors.MaleST + visitors.MaleOBC + visitors.MaleGEN;
            visitors.Female = visitors.FemaleSC + visitors.FemaleST + visitors.FemaleOBC + visitors.FemaleGEN;
            visitors.Total = visitors.Male + visitors.Female;

            // Aggregate accommodation
            var accommodation = data.SelectMany(s => s.Hostels ?? new List<Domain.Entities.GenericTables.Service.TableHostel>())
                .Select(h => new AccommodationData
                {
                    Id = h.Id,
                    DateFrom = h.Date,
                    DateTo = h.Date,
                    MaleSC = h.Male_SC,
                    FemaleSC = h.Female_SC,
                    MaleST = h.Male_ST,
                    FemaleST = h.Female_ST,
                    MaleOBC = h.Male_OBC,
                    FemaleOBC = h.Female_OBC,
                    MaleGEN = h.Male_GEN,
                    FemaleGEN = h.Female_GEN,
                    Male = h.Male_SC + h.Male_ST + h.Male_OBC + h.Male_GEN,
                    Female = h.Female_SC + h.Female_ST + h.Female_OBC + h.Female_GEN,
                    NoOfDays = h.NumberOfDaysStayed,
                    Purpose = h.Purpose ?? "",
                    Amount = h.AmountGenerated,
                    Village = h.VillageOrTaluk,
                    Taluk = h.VillageOrTaluk
                }).ToList();

            foreach (var acc in accommodation)
            {
                acc.Total = acc.Male + acc.Female;
            }

            response.ServicesFacilities = new ServicesFacilitiesData
            {
                Services = services,
                Visitors = visitors,
                Accommodation = accommodation,
                TotalAmountGenerated = services.Sum(s => s.AmountGenerated)
            };

            response.Metadata.TotalEntries = services.Count;
        }

        private async Task BuildFinancialReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request,
            List<int> unitLocationIds)
        {
            var (startDate, endDate) = GetDateRangeForPeriod(request);
            var formStatus = request.FormStatus ?? "Approved";

            var query = _financialRepository.GetQueryable()
                .Where(f => unitLocationIds.Contains(f.UnitLocationId)
                    && f.FormStatus == formStatus
                    && f.CreatedAt >= startDate && f.CreatedAt < endDate)
                .Include(f => f.UnitLocation).ThenInclude(ul => ul.District)
                .Include(f => f.Budgets)
                .Include(f => f.RevolvingFunds)
                .Include(f => f.DetailsOfBankAccounts);

            var data = await query.ToListAsync();

            if (data.Any())
            {
                var first = data.First();
                var allBudgets = data.SelectMany(f => f.Budgets ?? new List<Domain.Entities.GenericTables.Budget>()).ToList();
                var allRevolvingFunds = data.SelectMany(f => f.RevolvingFunds ?? new List<Domain.Entities.GenericTables.RevolvingFund>()).ToList();
                var allBankAccounts = data.SelectMany(f => f.DetailsOfBankAccounts ?? new List<Domain.Entities.GenericTables.DetailsOfBankAccount>()).ToList();

                response.FinancialStatus = new FinancialStatusData
                {
                    Id = first.Id,
                    UnitLocationId = first.UnitLocationId,
                    UnitLocationName = $"{first.UnitLocation?.District?.Name}",
                    StartDate = first.StartDate ,
                    EndDate = first.EndDate,
                    FormStatus = first.FormStatus,
                    Budgets = allBudgets.Select(b => new BudgetData
                    {
                        Id = b.Id,
                        Particulars = b.Particulars ?? "",
                        ABAC = b.ABAC?.ToString() ?? "",
                        DAC = b.DAC?.ToString() ?? "",
                        Sanctioned = b.Sanctioned ?? 0,
                        Released = b.Released ?? 0,
                        Expenditure = b.Expenditure ?? 0,
                        Balance = b.Balance ?? 0,
                        Percentage = b.PercentageOfExpenditure ?? 0
                    }).ToList(),
                    RevolvingFunds = allRevolvingFunds.Select(r => new RevolvingFundData
                    {
                        Id = r.Id,
                        YearMonth = r.YearMonth,
                        OpeningBalance = r.OpeningBalance ?? 0,
                        Receipts = r.Receipt ?? 0,
                        Expenditure = r.Expenditure ?? 0,
                        ClosingBalance = r.ClosingBalance ?? 0
                    }).ToList(),
                    BankAccounts = allBankAccounts.Select(b => new BankAccountData
                    {
                        Id = b.Id,
                        BankName = b.NameOfBank ?? "",
                        LocationBranch = b.LocationBranch ?? "",
                        BranchCode = b.BranchCode,
                        AccountName = b.AccountName ?? "",
                        AccountNumber = b.AccountNumber ?? "",
                        MICRNumber = b.MICRNumber,
                        IFSCCode = b.IFSCCode ?? ""
                    }).ToList(),
                    TotalSanctioned = allBudgets.Sum(b => b.Sanctioned ?? 0),
                    TotalReleased = allBudgets.Sum(b => b.Released ?? 0),
                    TotalExpenditure = allBudgets.Sum(b => b.Expenditure ?? 0),
                    TotalBalance = allBudgets.Sum(b => b.Balance ?? 0)
                };
            }

            response.Metadata.TotalEntries = data.Count;
        }

        // ========================================
        // SUB-SECTION MAPPING HELPERS
        // ========================================

        private List<ParticipantCategory>? MapDemographics(ICollection<Domain.Entities.KVK.KvkParticipantDemographics>? demographics)
        {
            if (demographics == null || !demographics.Any())
                return null;

            return demographics.Select(d => new ParticipantCategory
            {
                CategoryName = d.Participant?.Name ?? "",
                MaleSC = d.Male_SC ?? 0,
                FemaleSC = d.Female_SC ?? 0,
                MaleST = d.Male_ST ?? 0,
                FemaleST = d.Female_ST ?? 0,
                MaleOBC = d.Male_OBC ?? 0,
                FemaleOBC = d.Female_OBC ?? 0,
                MaleGEN = d.Male_GEN ?? 0,
                FemaleGEN = d.Female_GEN ?? 0,
                MaleTotal = (d.Male_SC ?? 0) + (d.Male_ST ?? 0) + (d.Male_OBC ?? 0) + (d.Male_GEN ?? 0),
                FemaleTotal = (d.Female_SC ?? 0) + (d.Female_ST ?? 0) + (d.Female_OBC ?? 0) + (d.Female_GEN ?? 0),
                GrandTotal = d.Total ?? 0,
                StayedInHostel = (d.SC_Female_StayedInHostel?? 0)+(d.SC_Male_StayedInHostel??0)+(d.OBC_Female_StayedInHostel??0)+(d.OBC_Male_StayedInHostel??0)+(d.GEN_Female_StayedInHostel??0)+(d.GEN_Male_StayedInHostel??0)
                 +(d.ST_Female_StayedInHostel?? 0)+(d.ST_Male_StayedInHostel??0)
            }).ToList();
        }

        private ProgramContentData? MapProgramContent(Domain.Entities.KVK.KvkProgramContentAndResources? content)
        {
            if (content == null)
                return null;

            return new ProgramContentData
            {
                ResourcePersons = content.ResourcePersons?.Select(r => new ResourcePersonData
                {
                    Name = r.Name ?? "",
                    Designation = r.Designation ?? "",
                    ResourcePersonType = r.ResourceType?.Name ?? "",
                    Institution = r.InstitutionOrDepartment ?? ""
                }).ToList() ?? new List<ResourcePersonData>(),

                TopicsCovered = content.TopicsCovered?.Select(t => new TopicCoveredData
                {
                    Date = t.Date,
                    Title = t.Title ?? ""
                }).ToList() ?? new List<TopicCoveredData>(),

                TeachingAids = content.TeachingAids?.Select(t => new TeachingAidData
                {
                    TypeOfAidUsed = t.TypeOfAid.Name ?? "",
                    Purpose = t.Purpose ?? ""
                }).ToList() ?? new List<TeachingAidData>()
            };
        }

        private AdvisoryServicesData? MapAdvisoryServices(Domain.Entities.KVK.KvkAdvisoryServices? advisory)
        {
            if (advisory == null)
                return null;

            return new AdvisoryServicesData
            {
                NoOfFacebookSMS = advisory.NoOfFacebookSMS,
                NoOfWhatsAppSMS = advisory.NoOfWhatsappSMS ,
                NoOfWhatsAppQueries = advisory.NoOfAnsweredWhatsappQueries,
                NoOfPhoneCalls = advisory.NoOfPhoneCalls,
                NoOfFaceToFaceDiscussions = advisory.NoOfFaceToFaceDiscussions,
                NoOfEmailsSent = advisory.NoOfEmailsSent,
                NoOfBeneficiaries = advisory.NoOfBeneficiaries,
                CriticalInputsDistributed = advisory.CriticalInputsDistributed?.Select(c => new CriticalInputData
                {
                    Component = c.InputName ?? "",
                    Number = c.QuantityDistributed ?? 0
                }).ToList() ?? new List<CriticalInputData>()
            };
        }

        private ResultsData? MapResults(Domain.Entities.KVK.KvkResult? results)
        {
            if (results == null)
                return null;

            return new ResultsData
            {
                FLDResults = results.FldResults?.Select(f => new FLDResultData
                {
                    DetailsOfDemonstration = f.DetailsOfDemo?.Name ?? "",
                    Crop = f.Parameter1 ?? "", // Using Parameter1 as Crop placeholder
                    Variety = f.Parameter2 ?? "", // Using Parameter2 as Variety placeholder
                    Area = 0, // Area field not available in entity
                    FarmersInvolved = 0, // FarmersInvolved field not available in entity
                    Yield = f.Yield ?? 0,
                    Remarks = f.BC ?? "" // Using BC (Benefit-Cost) as remarks placeholder
                }).ToList() ?? new List<FLDResultData>(),

                OFTResults = results.OftResults?.Select(o => new OFTResultData
                {
                    DetailsOfTechnology = o.DetailsOfDemo?.Name ?? "",
                    NumberOfTrials = 1, // NumberOfTrials field not available in entity
                    Yield = o.Yield ?? 0,
                    Remarks = o.BC ?? "" // Using BC as remarks placeholder
                }).ToList() ?? new List<OFTResultData>()
            };
        }

        private ReportSectionData? MapReportSection(Domain.Entities.KVK.KvkReport? report)
        {
            if (report == null)
                return null;

            return new ReportSectionData
            {
                ReportingDate = report.ReportDate ?? DateOnly.FromDateTime(DateTime.Now),
                SignificantOutcome = report.Outcome ?? "",
                GeoTaggedPhoto = report.GeoTaggedPhoto,
                ProgressReport = report.ProgressReport
            };
        }

        private RecommendationData? MapRecommendations(Domain.Entities.KVK.KvkRecommendation? recommendation)
        {
            if (recommendation == null)
                return null;

            return new RecommendationData
            {
                ProblemsIdentified = recommendation.ProblemsIdentified,
                Recommendations = recommendation.Recommendation,
                ActionTaken = recommendation.ActionTaken,
                SignificantAchievement = recommendation.SignificantAchievement,
                SuccessStories = recommendation.SuccessStories,
                Outcome = recommendation.ImpactOutcome,
                VideoUrl = recommendation.UploadVideoUrl
            };
        }

        // ========================================
        // PERIOD HANDLING METHODS
        // ========================================

        private (DateTimeOffset startDate, DateTimeOffset endDate) GetDateRangeForPeriod(
            ComprehensiveReportRequest request)
        {
            switch (request.PeriodType.ToLower())
            {
                case "monthly":
                    if (!request.Month.HasValue)
                        throw new ArgumentException("Month is required for monthly reports");

                    return (
                        GetStartDate(request.Year, request.Month.Value),
                        GetEndDate(request.Year, request.Month.Value)
                    );

                case "quarterly":
                    if (!request.Quarter.HasValue || request.Quarter < 1 || request.Quarter > 4)
                        throw new ArgumentException("Valid quarter (1-4) is required for quarterly reports");

                    int startMonth = (request.Quarter.Value - 1) * 3 + 1;
                    var qStartDate = new DateTimeOffset(new DateTime(request.Year, startMonth, 1), TimeSpan.Zero);
                    var qEndDate = qStartDate.AddMonths(3);

                    return (qStartDate, qEndDate);

                case "yearly":
                    var yStartDate = new DateTimeOffset(new DateTime(request.Year, 1, 1), TimeSpan.Zero);
                    var yEndDate = new DateTimeOffset(new DateTime(request.Year + 1, 1, 1), TimeSpan.Zero);

                    return (yStartDate, yEndDate);

                default:
                    throw new ArgumentException($"Invalid period type: {request.PeriodType}");
            }
        }

        private string GetPeriodDescription(ComprehensiveReportRequest request)
        {
            switch (request.PeriodType.ToLower())
            {
                case "monthly":
                    return request.Month.HasValue
                        ? $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.Month.Value)} {request.Year}"
                        : $"{request.Year}";

                case "quarterly":
                    return request.Quarter.HasValue
                        ? $"Q{request.Quarter.Value} {request.Year}"
                        : $"{request.Year}";

                case "yearly":
                    return $"Year {request.Year}";

                default:
                    return $"{request.Year}";
            }
        }

        // ========================================
        // REPORT CONFIGURATION METHOD
        // ========================================

        public async Task<ServiceResult<ReportConfigurationResponse>> GetReportConfigurationAsync(int unitId)
        {
            try
            {
                var config = new ReportConfigurationResponse
                {
                    UnitId = unitId,
                    UnitName = GetUnitName(unitId),
                    AvailableSections = GetAvailableSectionsForUnit(unitId),
                    DefaultSections = GetDefaultSectionsForUnit(unitId)
                };

                return ServiceResult<ReportConfigurationResponse>.Success(config);
            }
            catch (Exception ex)
            {
                return ServiceResult<ReportConfigurationResponse>.Failure(
                    $"Failed to get report configuration: {ex.Message}");
            }
        }

        private string GetUnitName(int unitId)
        {
            return unitId switch
            {
                UnitConstants.KVK_UNIT_ID => "KVK",
                UnitConstants.EEU_UNIT_ID => "EEU",
                UnitConstants.FTI_UNIT_ID => "FTI",
                UnitConstants.STU_UNIT_ID => "STU",
                UnitConstants.IBTVA_UNIT_ID => "IBTVA",
                UnitConstants.ATIC_UNIT_ID => "ATIC",
                UnitConstants.DEU_UNIT_ID => "DEU",
                UnitConstants.FIU_UNIT_ID => "FIU",
                UnitConstants.NAEP_UNIT_ID => "NAEP",
                UnitConstants.SAMETI_UNIT_ID => "SAMETI",
                UnitConstants.ASM_UNIT_ID => "ASM",
                _ => "Unknown"
            };
        }

        private List<SectionDefinitionDto> GetAvailableSectionsForUnit(int unitId)
        {
            // FIU Unit - Special media activities unit
            if (unitId == UnitConstants.FIU_UNIT_ID)
            {
                return new List<SectionDefinitionDto>
                {
                    new SectionDefinitionDto
                    {
                        SectionKey = "fiu_activities",
                        DisplayName = "FIU Media Activities",
                        Icon = "📻",
                        AvailableFields = new List<FieldDefinitionDto>
                        {
                            new() { Key = "activityName", DisplayName = "Activity Name", DataType = "string", DefaultSelected = true },
                            new() { Key = "count", DisplayName = "Count", DataType = "int", DefaultSelected = true },
                            new() { Key = "date", DisplayName = "Date", DataType = "date", DefaultSelected = false }
                        },
                        DefaultFields = new List<string> { "activityName", "count" },
                        IsAvailable = true
                    },
                    new SectionDefinitionDto
                    {
                        SectionKey = "other_activities",
                        DisplayName = "Other Activities",
                        Icon = "📋",
                        AvailableFields = new List<FieldDefinitionDto>
                        {
                            new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true },
                            new() { Key = "description", DisplayName = "Description", DataType = "string", DefaultSelected = true }
                        },
                        DefaultFields = new List<string> { "title", "description" },
                        IsAvailable = true
                    }
                };
            }

            // ASM Unit - Agricultural Service Management
            if (unitId == UnitConstants.ASM_UNIT_ID)
            {
                return new List<SectionDefinitionDto>
                {
                    new SectionDefinitionDto
                    {
                        SectionKey = "asm_visitors",
                        DisplayName = "ASM Visitor Statistics",
                        Icon = "👥",
                        AvailableFields = new List<FieldDefinitionDto>
                        {
                            new() { Key = "particulars", DisplayName = "Particulars", DataType = "string", DefaultSelected = true },
                            new() { Key = "noOfVisitors", DisplayName = "No. of Visitors", DataType = "int", DefaultSelected = true }
                        },
                        DefaultFields = new List<string> { "particulars", "noOfVisitors" },
                        IsAvailable = true
                    },
                    new SectionDefinitionDto
                    {
                        SectionKey = "other_activities",
                        DisplayName = "Other Activities",
                        Icon = "📋",
                        AvailableFields = new List<FieldDefinitionDto>
                        {
                            new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true },
                            new() { Key = "description", DisplayName = "Description", DataType = "string", DefaultSelected = true }
                        },
                        DefaultFields = new List<string> { "title", "description" },
                        IsAvailable = true
                    }
                };
            }

            // KVK, EEU - Full 7 stepper program reports
            if (unitId == UnitConstants.KVK_UNIT_ID || unitId == UnitConstants.EEU_UNIT_ID)
            {
                return GetFullProgramSections();
            }

            // Other units - Simplified program reports
            return GetSimplifiedProgramSections();
        }

        private List<string> GetDefaultSectionsForUnit(int unitId)
        {
            if (unitId == UnitConstants.FIU_UNIT_ID)
                return new List<string> { "fiu_activities" };

            if (unitId == UnitConstants.ASM_UNIT_ID)
                return new List<string> { "asm_visitors" };

            // Default for program-based units
            return new List<string> { "program_organized", "demographics" };
        }

        private List<SectionDefinitionDto> GetFullProgramSections()
        {
            return new List<SectionDefinitionDto>
            {
                // Stepper 1: Program Organized
                new SectionDefinitionDto
                {
                    SectionKey = "program_organized",
                    DisplayName = "Program Organized",
                    StepperNumber = 1,
                    Group = "program",
                    Icon = "📅",
                    AvailableFields = new List<FieldDefinitionDto>
                    {
                        new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true },
                        new() { Key = "startDate", DisplayName = "Start Date", DataType = "date", DefaultSelected = true },
                        new() { Key = "endDate", DisplayName = "End Date", DataType = "date", DefaultSelected = true },
                        new() { Key = "programType", DisplayName = "Program Type", DataType = "string", DefaultSelected = true },
                        new() { Key = "category", DisplayName = "Category", DataType = "string", DefaultSelected = true },
                        new() { Key = "type", DisplayName = "Type", DataType = "string", DefaultSelected = false },
                        new() { Key = "theme", DisplayName = "Theme", DataType = "string", DefaultSelected = false },
                        new() { Key = "thematicArea", DisplayName = "Thematic Area", DataType = "string", DefaultSelected = false },
                        new() { Key = "sponsoredOrganization", DisplayName = "Sponsored Organization", DataType = "string", DefaultSelected = false },
                        new() { Key = "mode", DisplayName = "Mode", DataType = "string", DefaultSelected = true },
                        new() { Key = "region", DisplayName = "Region", DataType = "string", DefaultSelected = false },
                        new() { Key = "duration", DisplayName = "Duration (days)", DataType = "string", DefaultSelected = true },
                        new() { Key = "tpNo", DisplayName = "TP No", DataType = "string", DefaultSelected = false },
                        new() { Key = "location", DisplayName = "Location", DataType = "string", DefaultSelected = true },
                        new() { Key = "sourceOfFund", DisplayName = "Source of Fund", DataType = "string", DefaultSelected = false },
                        new() { Key = "totalOutlay", DisplayName = "Total Outlay (Rs)", DataType = "currency", DefaultSelected = false },
                        new() { Key = "collaborator", DisplayName = "Collaborator", DataType = "string", DefaultSelected = false },
                        new() { Key = "collaborativeProgram", DisplayName = "Collaborative Program", DataType = "boolean", DefaultSelected = false }
                    },
                    DefaultFields = new List<string> { "title", "startDate", "endDate", "programType", "category", "mode", "duration", "location" },
                    IsAvailable = true
                },
                // Stepper 2: Demographics
                new SectionDefinitionDto
                {
                    SectionKey = "demographics",
                    DisplayName = "Participant Demographics",
                    StepperNumber = 2,
                    Group = "program",
                    Icon = "👥",
                    AvailableFields = new List<FieldDefinitionDto>
                    {
                        new() { Key = "totalParticipants", DisplayName = "Total Participants", DataType = "int", DefaultSelected = true }
                    },
                    DefaultFields = new List<string> { "totalParticipants" },
                    IsAvailable = true
                }
                // Add more steppers as needed...
            };
        }

        private List<SectionDefinitionDto> GetSimplifiedProgramSections()
        {
            return new List<SectionDefinitionDto>
            {
                new SectionDefinitionDto
                {
                    SectionKey = "program_organized",
                    DisplayName = "Programs Organized",
                    Icon = "📅",
                    AvailableFields = new List<FieldDefinitionDto>
                    {
                        new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true },
                        new() { Key = "startDate", DisplayName = "Start Date", DataType = "date", DefaultSelected = true },
                        new() { Key = "endDate", DisplayName = "End Date", DataType = "date", DefaultSelected = true },
                        new() { Key = "location", DisplayName = "Location", DataType = "string", DefaultSelected = true }
                    },
                    DefaultFields = new List<string> { "title", "startDate", "endDate", "location" },
                    IsAvailable = true
                }
            };
        }
    }
}
