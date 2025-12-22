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
using Application.Models.ComprehensiveReports;
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
            List<Domain.Entities.OrganizationUnitLocation> unitLocations,
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
                Month = request.Month,
                Year = request.Year,
                MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.Month),
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
            var startDate = GetStartDate(request.Year, request.Month);
            var endDate = GetEndDate(request.Year, request.Month);
            var formStatus = request.FormStatus ?? "Approved";

            // Handle FIU unit
            if (unitId == UnitConstants.FIU_UNIT_ID)
            {
                await BuildFIUActivitiesAsync(response, unitLocationIds, request.Year, request.Month);
                return;
            }

            // Handle ASM unit
            if (unitId == UnitConstants.ASM_UNIT_ID)
            {
                await BuildASMActivitiesAsync(response, unitLocationIds, request.Year, request.Month);
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

            if (includeSubSections)
            {
                query = query
                    .Include(p => p.ProgramContentAndResources)
                        .ThenInclude(c => c.ResourcePersons)
                    .Include(p => p.ProgramContentAndResources)
                        .ThenInclude(c => c.TopicsCoveredInClasses)
                    .Include(p => p.ProgramContentAndResources)
                        .ThenInclude(c => c.TeachingAidsDeveloped)
                    .Include(p => p.AdvisoryServices)
                        .ThenInclude(a => a.CriticalInputsDistributed)
                    .Include(p => p.Results)
                        .ThenInclude(r => r.FldResults)
                    .Include(p => p.Results)
                        .ThenInclude(r => r.OftResults)
                    .Include(p => p.Report)
                    .Include(p => p.Recommendation);
            }

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
                TotalOutlay = p.TotalOutlay ?? 0,
                FormStatus = p.FormStatus,
                TotalParticipants = p.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0,
                Collaborator = p.Collaborator,
                CollaborativeProgram = p.IsCollaborative,

                // Sub-sections (if requested)
                Demographics = includeSubSections ? MapDemographics(p.ParticipantDemographics) : null,
                Content = includeSubSections ? MapProgramContent(p.ProgramContentAndResources) : null,
                AdvisoryServices = includeSubSections ? MapAdvisoryServices(p.AdvisoryServices) : null,
                Results = includeSubSections ? MapResults(p.Results) : null,
                ReportSection = includeSubSections ? MapReportSection(p.Report) : null,
                Recommendations = includeSubSections ? MapRecommendations(p.Recommendation) : null
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
                TotalOutlay = p.TotalOutlay ?? 0,
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
                TotalOutlay = p.TotalOutlay ?? 0,
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
                TotalOutlay = p.TotalOutlay ?? 0,
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
                TotalOutlay = p.TotalOutlay ?? 0,
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
                TotalOutlay = p.TotalOutlay ?? 0,
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
                TotalOutlay = p.TotalOutlay ?? 0,
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
                TotalOutlay = p.TotalOutlay ?? 0,
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
                TotalOutlay = p.TotalOutlay ?? 0,
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
            int year,
            int month)
        {
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
            int year,
            int month)
        {
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
            var startDate = GetStartDate(request.Year, request.Month);
            var endDate = GetEndDate(request.Year, request.Month);
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
                JournalName = p.JournalTitle,
                PublicationDate = p.PublishedDate,
                TotalPages = p.PublicationPagesTo - p.PublicationPagesFrom,
                WebLink = p.PublicationWebLink,
                FormStatus = p.FormStatus,
                Authors = p.PublisherDetails?.Select(a => new AuthorData
                {
                    PublicationId = p.Id,
                    Name = a.Name ?? "",
                    Institution = a.Institution ?? "",
                    Address = a.Address ?? ""
                }).ToList(),
                ExtensionLiterature = p.ExtensionLiteratures?.Select(e => new ExtensionLiteratureItem
                {
                    PublicationId = p.Id,
                    Date = e.Date,
                    AmountPerCopy = e.AmountPerCopy ?? 0,
                    CopiesSoldOrDistributed = e.NumberOfCopies ?? 0,
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
            var startDate = GetStartDate(request.Year, request.Month);
            var endDate = GetEndDate(request.Year, request.Month);
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
                        Institution = a.Institution ?? "",
                        Contribution = a.Contribution ?? ""
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
            var startDate = GetStartDate(request.Year, request.Month);
            var endDate = GetEndDate(request.Year, request.Month);
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
                RelatedToDiscipline = c.RelatedTo ?? "",
                TotalNoOfBeneficiaries = c.TotalBeneficiaries ?? 0,
                FormStatus = c.FormStatus,
                MaleSC = c.Male_SC ?? 0,
                FemaleSC = c.Female_SC ?? 0,
                MaleST = c.Male_ST ?? 0,
                FemaleST = c.Female_ST ?? 0,
                MaleOBC = c.Male_OBC ?? 0,
                FemaleOBC = c.Female_OBC ?? 0,
                MaleGEN = c.Male_GEN ?? 0,
                FemaleGEN = c.Female_GEN ?? 0
            }).ToList();

            response.Metadata.TotalEntries = response.Consultancies.Count;
        }

        private async Task BuildServicesReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request,
            List<int> unitLocationIds)
        {
            var startDate = GetStartDate(request.Year, request.Month);
            var endDate = GetEndDate(request.Year, request.Month);
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
                .Include(s => s.TableHostels);

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
                MaleSC = allVisitors.Sum(v => v.Male_SC ?? 0),
                FemaleSC = allVisitors.Sum(v => v.Female_SC ?? 0),
                MaleST = allVisitors.Sum(v => v.Male_ST ?? 0),
                FemaleST = allVisitors.Sum(v => v.Female_ST ?? 0),
                MaleOBC = allVisitors.Sum(v => v.Male_OBC ?? 0),
                FemaleOBC = allVisitors.Sum(v => v.Female_OBC ?? 0),
                MaleGEN = allVisitors.Sum(v => v.Male_GEN ?? 0),
                FemaleGEN = allVisitors.Sum(v => v.Female_GEN ?? 0)
            };
            visitors.Male = visitors.MaleSC + visitors.MaleST + visitors.MaleOBC + visitors.MaleGEN;
            visitors.Female = visitors.FemaleSC + visitors.FemaleST + visitors.FemaleOBC + visitors.FemaleGEN;
            visitors.Total = visitors.Male + visitors.Female;

            // Aggregate accommodation
            var accommodation = data.SelectMany(s => s.TableHostels ?? new List<Domain.Entities.GenericTables.Service.TableHostel>())
                .Select(h => new AccommodationData
                {
                    Id = h.Id,
                    DateFrom = h.DateFrom,
                    DateTo = h.DateTo,
                    MaleSC = h.Male_SC ?? 0,
                    FemaleSC = h.Female_SC ?? 0,
                    MaleST = h.Male_ST ?? 0,
                    FemaleST = h.Female_ST ?? 0,
                    MaleOBC = h.Male_OBC ?? 0,
                    FemaleOBC = h.Female_OBC ?? 0,
                    MaleGEN = h.Male_GEN ?? 0,
                    FemaleGEN = h.Female_GEN ?? 0,
                    Male = (h.Male_SC ?? 0) + (h.Male_ST ?? 0) + (h.Male_OBC ?? 0) + (h.Male_GEN ?? 0),
                    Female = (h.Female_SC ?? 0) + (h.Female_ST ?? 0) + (h.Female_OBC ?? 0) + (h.Female_GEN ?? 0),
                    NoOfDays = h.DaysStayed ?? 0,
                    Purpose = h.Purpose ?? "",
                    Amount = h.Amount ?? 0,
                    Village = h.Village,
                    Taluk = h.Taluk
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
            var startDate = GetStartDate(request.Year, request.Month);
            var endDate = GetEndDate(request.Year, request.Month);
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
                    StartDate = first.StartDate,
                    EndDate = first.EndDate,
                    FormStatus = first.FormStatus,
                    Budgets = allBudgets.Select(b => new BudgetData
                    {
                        Id = b.Id,
                        Particulars = b.Particulars ?? "",
                        ABAC = b.ABAC,
                        DAC = b.DAC,
                        Sanctioned = b.Sanctioned ?? 0,
                        Released = b.Released ?? 0,
                        Expenditure = b.Expenditure ?? 0,
                        Balance = b.Balance ?? 0,
                        Percentage = b.Percentage
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
                        BankName = b.BankName ?? "",
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
                CategoryName = d.Category?.Name ?? "",
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
                StayedInHostel = d.StayedInHostel ?? 0
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
                    ResourcePersonType = r.ResourcePersonType ?? "",
                    Institution = r.Institution ?? ""
                }).ToList() ?? new List<ResourcePersonData>(),

                TopicsCovered = content.TopicsCoveredInClasses?.Select(t => new TopicCoveredData
                {
                    Date = t.Date,
                    Title = t.Title ?? ""
                }).ToList() ?? new List<TopicCoveredData>(),

                TeachingAids = content.TeachingAidsDeveloped?.Select(t => new TeachingAidData
                {
                    TypeOfAidUsed = t.TypeOfAidUsed ?? "",
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
                NoOfFacebookSMS = advisory.NoOfFacebookSMS ?? 0,
                NoOfWhatsAppSMS = advisory.NoOfWhatsAppSMS ?? 0,
                NoOfWhatsAppQueries = advisory.NoOfWhatsAppQueries ?? 0,
                NoOfPhoneCalls = advisory.NoOfPhoneCalls ?? 0,
                NoOfFaceToFaceDiscussions = advisory.NoOfFaceToFaceDiscussions ?? 0,
                NoOfEmailsSent = advisory.NoOfEmailsSent ?? 0,
                NoOfBeneficiaries = advisory.NoOfBeneficiaries ?? 0,
                CriticalInputsDistributed = advisory.CriticalInputsDistributed?.Select(c => new CriticalInputData
                {
                    Component = c.Component ?? "",
                    Number = c.Number ?? 0
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
                    DetailsOfDemonstration = f.DetailsOfDemonstration ?? "",
                    Crop = f.Crop ?? "",
                    Variety = f.Variety ?? "",
                    Area = f.Area ?? 0,
                    FarmersInvolved = f.FarmersInvolved ?? 0,
                    Yield = f.Yield ?? 0,
                    Remarks = f.Remarks
                }).ToList() ?? new List<FLDResultData>(),

                OFTResults = results.OftResults?.Select(o => new OFTResultData
                {
                    DetailsOfTechnology = o.DetailsOfTechnology ?? "",
                    NumberOfTrials = o.NumberOfTrials ?? 0,
                    Yield = o.Yield ?? 0,
                    Remarks = o.Remarks
                }).ToList() ?? new List<OFTResultData>()
            };
        }

        private ReportSectionData? MapReportSection(Domain.Entities.KVK.KvkReport? report)
        {
            if (report == null)
                return null;

            return new ReportSectionData
            {
                ReportingDate = report.ReportingDate,
                SignificantOutcome = report.SignificantOutcome ?? "",
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
                Outcome = recommendation.Outcome,
                VideoUrl = recommendation.VideoUrl
            };
        }
    }
}
