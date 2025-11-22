using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.FIU;
using Application.Interface.Repository.DataTables.TblService;
using Application.Interface.Services.Reports;
using Application.Models;
using Application.Models.Reports;
using Domain.Entities.Enum;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.Reports
{
    public class AdminReportService : IAdminReportService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;

        private readonly IDeuProgramDetailsRepository _deuRepository;
        private readonly IEeuProgramDetailsRepository _eeuRepository;
        private readonly IKvkProgramDetailsRepository _kvkRepository;
        private readonly IIbtvaProgramDetailsRepository _ibtvaRepository;
        private readonly INaepProgramDetailsRepository _naepRepository;
        private readonly IStuProgramDetailsRepository _stuRepository;
        private readonly IAticProgramDetailsRepository _aticRepository;
        //private readonly IFtiProgramDetailsRepository _ftiRepository;
        private readonly IASMVisitorDetailsRepository _asmRepository;

        private readonly IPublicationRepository _publicationRepository;
        private readonly INominationRewardRepository _nominationRewardRepository;

        private readonly IConsultingServiceRepository _consultingRepository;
        private readonly ITableServiceRepository _tblServiceRepository;
        private readonly ITableOtherActivityRepository _otherActivityRepository;

        // NEW: FIU Repository
        private readonly IFIUProgramActivityRepository _fiuRepository;

        public AdminReportService(
            ICurrentUserService currentUserService,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            ITrainerAssignmentRepository trainerAssignmentRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            INominationRewardRepository nominationRewardRepository,
            IPublicationRepository publicationRepository,
            IConsultingServiceRepository consultingRepository,
            ITableServiceRepository tblServiceRepository,
            INaepProgramDetailsRepository naepRepository,
            IIbtvaProgramDetailsRepository ibtvaRepository,
            IKvkProgramDetailsRepository kvkRepository,
            IStuProgramDetailsRepository stuRepository,
            IAticProgramDetailsRepository aticRepository,
            IEeuProgramDetailsRepository eeuRepository,
            IDeuProgramDetailsRepository deuRepository,
            IFIUProgramActivityRepository fiuRepository,
            IASMVisitorDetailsRepository asmRepository,
            ITableOtherActivityRepository otherActivityRepository
            )
        {
            _currentUserService = currentUserService;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _nominationRewardRepository = nominationRewardRepository;
            _publicationRepository = publicationRepository;
            _consultingRepository = consultingRepository;
            _tblServiceRepository = tblServiceRepository;
            _otherActivityRepository = otherActivityRepository;
            _naepRepository = naepRepository;
            _ibtvaRepository = ibtvaRepository;
            _kvkRepository = kvkRepository;
            _stuRepository = stuRepository;
            _aticRepository = aticRepository;
            _eeuRepository = eeuRepository;
            _deuRepository = deuRepository;
            _fiuRepository = fiuRepository;
            _asmRepository = asmRepository;
        }

        public async Task<ServiceResult<ReportFilterOptionsDto>> GetFilterOptionsAsync()
        {
            // Allow both Admin and UnitHead
            if (_currentUserService.Role != Role.ADMIN && _currentUserService.Role != Role.UNITHEAD)
                return ServiceResult<ReportFilterOptionsDto>.Failure("Access denied");

            var orgId = _currentUserService.OrganizationId;
            List<int> allowedLocationIds;

            if (_currentUserService.Role == Role.ADMIN)
            {
                // Admin: Get ALL assigned locations in organization
                var unitHeadAssignments = await _unitHeadAssignmentRepository.GetAllAsync();
                var trainerAssignments = await _trainerAssignmentRepository.GetAllAsync();

                allowedLocationIds = unitHeadAssignments.Select(x => x.UnitLocationId)
                    .Union(trainerAssignments.Select(x => x.UnitLocationId))
                    .Distinct().ToList();
            }
            else // UNITHEAD
            {
                // Unit Head: Get ONLY their assigned locations
                allowedLocationIds = await _unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);
            }

            // Get location details
            var locations = await _organizationUnitRepository.GetByIdsAsync(allowedLocationIds);

            // Group by unit
            var units = locations
                .GroupBy(ul => new { ul.Unit.Id, ul.Unit.Name })
                .Select(g => new UnitWithLocationsDto
                {
                    UnitId = g.Key.Id,
                    UnitName = g.Key.Name,
                    Locations = g.Select(ul => new UnitLocationDto
                    {
                        UnitLocationId = ul.Id,
                        DistrictName = ul.District?.Name ?? "Unknown",
                        StateName = ul.District?.State?.Name ?? "Unknown"
                    }).OrderBy(l => l.DistrictName).ToList()
                })
                .OrderBy(u => u.UnitName)
                .ToList();

            var years = Enumerable.Range(DateTime.Now.Year - 5, 6).Reverse().ToList();

            return ServiceResult<ReportFilterOptionsDto>.Success(new ReportFilterOptionsDto
            {
                Units = units,
                Years = years
            });
        }

        public async Task<ServiceResult<AdminReportResponseDto>> GenerateReportAsync(AdminReportFilterDto filter)
        {
            // Allow both Admin and UnitHead
            if (_currentUserService.Role != Role.ADMIN && _currentUserService.Role != Role.UNITHEAD)
                return ServiceResult<AdminReportResponseDto>.Failure("Access denied");

            // Validate Unit Head has access to this location
            if (_currentUserService.Role == Role.UNITHEAD)
            {
                var allowedLocationIds = await _unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);

                if (!allowedLocationIds.Contains(filter.UnitLocationId))
                {
                    return ServiceResult<AdminReportResponseDto>.Failure(
                        "You don't have access to this unit location",
                        ServiceErrorStatus.FORBIDDEN);
                }
            }

            if (filter.Month < 1 || filter.Month > 12)
                return ServiceResult<AdminReportResponseDto>.Failure("Month must be 1-12");

            // Date range
            var startDate = new DateTimeOffset(new DateTime(filter.Year, filter.Month, 1), TimeSpan.Zero);
            var endDate = startDate.AddMonths(1);

            // Get location info
            var location = await _organizationUnitRepository.GetByIdAsync(filter.UnitLocationId);
            if (location == null)
                return ServiceResult<AdminReportResponseDto>.Failure("Unit location not found");

            var report = new AdminReportResponseDto
            {
                UnitName = location.Unit?.Name ?? "Unknown",
                UnitLocationName = location.District?.Name ?? "Unknown",
                MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(filter.Month),
                Month = filter.Month,
                Year = filter.Year,
                GeneratedAt = DateTime.UtcNow
            };

            // ========== NEW: CHECK IF FIU UNIT AND ADD FIU ACTIVITIES ==========

            if (location.UnitId == UnitConstants.FIU_UNIT_ID)
            {
                // Get FIU monthly summary
                var (fiuActivities, totalEntries) = await _fiuRepository.GetMonthlyActivitySummaryAsync(new List<int> { filter.UnitLocationId }, filter.Year, filter.Month);


                report.FIUActivities = new ReportFIUActivitiesDto
                {
                    Activities = fiuActivities.Select(a => new ReportFIUActivityItemDto
                    {
                        SlNo = a.SlNo,
                        ActivityName = a.ActivityName,
                        Count = a.Count
                    }).ToList(),
                    TotalActivities = fiuActivities.Count,
                    TotalCount = fiuActivities.Sum(a => a.Count),
                    TotalEntries = totalEntries

                };

                // IMPORTANT: Set total entries to FIU count
                report.TotalEntries = totalEntries;
                // Return early for FIU units (don't query other tables)
                return ServiceResult<AdminReportResponseDto>.Success(report);
            }

            // ========== NEW: ASM UNIT LOGIC ==========

            if (location.UnitId == UnitConstants.ASM_UNIT_ID)
            {
                var (summary, totalEntries) = await _asmRepository.GetMonthlyVisitorSummaryAsync(
                    new List<int> { filter.UnitLocationId },
                    filter.Year,
                    filter.Month
                );


                var visitorItems = new List<ASMVisitorReportItemDto>
            {
                new ASMVisitorReportItemDto
                {
                    SlNo = 1,
                    Particulars = "Farmers",
                    NoOfVisitors = summary.TotalFarmers
                },
                new ASMVisitorReportItemDto
                {
                    SlNo = 2,
                    Particulars = "Students",
                    NoOfVisitors = summary.TotalStudents
                },
                new ASMVisitorReportItemDto
                {
                    SlNo = 3,
                    Particulars = "Other public visitors",
                    NoOfVisitors = summary.TotalPublic
                }
            };

                report.ASMActivities = new ReportASMActivitiesDto
                {
                    Visitors = visitorItems,
                    TotalVisitors = summary.TotalVisitors,
                    TotalEntries = totalEntries
                };

                report.TotalEntries = totalEntries;
                return ServiceResult<AdminReportResponseDto>.Success(report);
            }




            // ========== EXISTING LOGIC FOR OTHER UNITS ==========


            // ========== NOMINATION & REWARDS ==========
            var nominations = await _nominationRewardRepository.GetAllAsync();
            report.Nominations = nominations
                .Where(x => x.UnitLocationId == filter.UnitLocationId &&
                           (x.FormStatus == "Draft" || x.FormStatus == "Pending" || x.FormStatus == "Approved") &&
                           x.CreatedAt >= startDate && x.CreatedAt < endDate)
                .Select(x => new ReportNominationDto
                {
                    Type = x.Type?.Name ?? "-",
                    AwardName = x.AwardName ?? "-",
                    Category = x.NominationCategory?.Name ?? "-",
                    Date = x.StartDate?.ToString("dd/MM/yyyy") ?? "-"
                })
                .ToList();

            // ========== PUBLICATIONS ==========
            var publications = await _publicationRepository.GetAllAsync();
            report.Publications = publications
                .Where(x => x.UnitLocationId == filter.UnitLocationId &&
                          (x.FormStatus == "Draft" || x.FormStatus == "Pending" || x.FormStatus == "Approved") &&
                           x.CreatedAt >= startDate && x.CreatedAt <= endDate)
                .Select(x => new ReportPublicationDto
                {
                    Category = x.Category != null ? (x.Category.Name.Equals("Others") ? (string.IsNullOrWhiteSpace(x.OtherPublication) ? "-" : x.OtherPublication) : x.Category.Name) : "-",
                    Title = x.Title ?? "N/A",
                    Pages = $"{x.PublicationPagesFrom}-{x.PublicationPagesTo}",
                    Mode = x.Mode != null ? (x.Mode.Name.Equals("Others") ? (string.IsNullOrWhiteSpace(x.OtherPublication) ? "-" : x.OtherPublication) : x.Mode.Name) : "-"                  

                })
                .ToList();

            // ========== PROGRAMS ==========
            // DEU Programs
            var deuPrograms = await _deuRepository.GetAllAsync();
            report.Programs.AddRange(deuPrograms
                .Where(x => x.UnitLocationId == filter.UnitLocationId &&
                          (x.FormStatus == "Draft" || x.FormStatus == "Pending" || x.FormStatus == "Approved") &&
                           x.CreatedAt >= startDate && x.CreatedAt < endDate)
                .Select(x => new ReportProgramDto
                {
                    ProgramType = x.ProgramType?.Name ?? "-",
                    Title = x.Title ?? "-",
                    DateFrom = x.StartDate != default(DateOnly) ? x.StartDate.ToString("dd/MM/yyyy") : "-",
                    DateTo = x.EndDate != default(DateOnly) ? x.EndDate.ToString("dd/MM/yyyy") : "-",
                    Duration = x.Duration ?? "-",
                    Participants = x.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0,
                    Status = x.Status?.Name ?? "-"
                }));

            // EEU Programs
            var eeuPrograms = await _eeuRepository.GetAllAsync();
            report.Programs.AddRange(eeuPrograms
                .Where(x => x.UnitLocationId == filter.UnitLocationId &&
                          (x.FormStatus == "Draft" || x.FormStatus == "Pending" || x.FormStatus == "Approved") &&
                           x.CreatedAt >= startDate && x.CreatedAt < endDate)
                .Select(x => new ReportProgramDto
                {
                    ProgramType = x.ProgramType?.Name ?? "-",
                    Title = x.Title ?? "-",
                    DateFrom = x.StartDate != default(DateOnly) ? x.StartDate.ToString("dd/MM/yyyy") : "-",
                    DateTo = x.EndDate != default(DateOnly) ? x.EndDate.ToString("dd/MM/yyyy") : "-",
                    Duration = x.Duration ?? "-",
                    Participants = x.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0,
                    Status = x.Status?.Name ?? "-"
                }));

            // KVK Programs
            var kvkPrograms = await _kvkRepository.GetAllAsync();
            report.Programs.AddRange(kvkPrograms
                .Where(x => x.UnitLocationId == filter.UnitLocationId &&
                           (x.FormStatus == "Draft" || x.FormStatus == "Pending" || x.FormStatus == "Approved") &&
                           x.CreatedAt >= startDate && x.CreatedAt < endDate)
                .Select(x => new ReportProgramDto
                {
                    ProgramType = x.ProgramType?.Name ?? "-",
                    Title = x.Title ?? "-",
                    DateFrom = x.StartDate != default(DateOnly) ? x.StartDate.ToString("dd/MM/yyyy") : "-",
                    DateTo = x.EndDate != default(DateOnly) ? x.EndDate.ToString("dd/MM/yyyy") : "-",
                    Duration = x.Duration ?? "-",
                    Participants = x.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0,
                    Status = x.Status?.Name ?? "-"
                }));

            // IBTVA Programs
            var ibtvaPrograms = await _ibtvaRepository.GetAllAsync();
            report.Programs.AddRange(ibtvaPrograms
                .Where(x => x.UnitLocationId == filter.UnitLocationId &&
                          (x.FormStatus == "Draft" || x.FormStatus == "Pending" || x.FormStatus == "Approved") &&
                           x.CreatedAt >= startDate && x.CreatedAt < endDate)
                .Select(x => new ReportProgramDto
                {
                    ProgramType = x.ProgramType?.Name ?? "-",
                    Title = x.Title ?? "-",
                    DateFrom = x.StartDate != default(DateOnly) ? x.StartDate.ToString("dd/MM/yyyy") : "-",
                    DateTo = x.EndDate != default(DateOnly) ? x.EndDate.ToString("dd/MM/yyyy") : "-",
                    Duration = x.Duration ?? "-",
                    Participants = x.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0,
                    Status = x.Status?.Name ?? "-"
                }));

            // STU Programs
            var stuPrograms = await _stuRepository.GetAllAsync();
            report.Programs.AddRange(stuPrograms
                .Where(x => x.UnitLocationId == filter.UnitLocationId &&
                           (x.FormStatus == "Draft" || x.FormStatus == "Pending" || x.FormStatus == "Approved") &&
                           x.CreatedAt >= startDate && x.CreatedAt < endDate)
                .Select(x => new ReportProgramDto
                {
                    ProgramType = x.ProgramType?.Name ?? "-",
                    Title = x.Title ?? "-",
                    DateFrom = x.StartDate != default(DateOnly) ? x.StartDate.ToString("dd/MM/yyyy") : "-",
                    DateTo = x.EndDate != default(DateOnly) ? x.EndDate.ToString("dd/MM/yyyy") : "-",
                    Duration = x.Duration ?? "-",
                    Participants = x.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0,
                    Status = x.Status?.Name ?? "-"
                }));

            // ATIC Programs
            var aticPrograms = await _aticRepository.GetAllAsync();
            report.Programs.AddRange(aticPrograms
                .Where(x => x.UnitLocationId == filter.UnitLocationId &&
                           (x.FormStatus == "Draft" || x.FormStatus == "Pending" || x.FormStatus == "Approved") &&
                           x.CreatedAt >= startDate && x.CreatedAt < endDate)
                .Select(x => new ReportProgramDto
                {
                    ProgramType = x.ProgramType?.Name ?? "-",
                    Title = x.Title ?? "-",
                    DateFrom = x.StartDate != default(DateOnly) ? x.StartDate.ToString("dd/MM/yyyy") : "-",
                    DateTo = x.EndDate != default(DateOnly) ? x.EndDate.ToString("dd/MM/yyyy") : "-",
                    Duration = x.Duration ?? "-",
                    Participants = x.ParticipantDemographics?.Sum(pd => pd.Total ?? 0) ?? 0,
                    Status = x.Status?.Name ?? "-"
                }));

            // ========== CONSULTANCY ==========
            var consultancies = await _consultingRepository.GetAllAsync();
            report.Consultancies = consultancies
                .Where(x => x.UnitLocationId == filter.UnitLocationId &&
                           (x.FormStatus == "Draft" || x.FormStatus == "Pending" || x.FormStatus == "Approved") &&
                           x.CreatedAt >= startDate && x.CreatedAt < endDate)
                .Select(x => new ReportConsultancyDto
                {
                    Category = x.Category?.Name ?? "-",
                    Title = x.Title ?? "-",
                    Date = x.Date != default(DateTime) ? x.Date.ToString("dd/MM/yyyy") : "-"
                })
                .ToList();

            // ========== SERVICES ==========
            var services = await _tblServiceRepository.GetAllAsync();
            report.Services = services
                .Where(x => x.UnitLocationId == filter.UnitLocationId &&
                           x.FormStatus == "Draft" &&
                           x.CreatedAt >= startDate && x.CreatedAt < endDate)
                .Select(x => new ReportServiceDto
                {
                    Category = x.Category != null ? (x.Category.Name.Equals("Others") ? (string.IsNullOrWhiteSpace(x.OtherCategory) ? "-" : x.OtherCategory) : x.Category.Name) : "-",
                    Theme = x.Theme != null ? (x.Theme.Name.Equals("Others") ? (string.IsNullOrWhiteSpace(x.OtherTheme) ? "-" : x.OtherTheme) : x.Theme.Name) : "-",
                    Title = x.TitleOfActivityConducted ?? "-",
                    Unit = x.QuantityUnit?.Name ?? "-",
                    Quantity = x.Number,
                    Amount = x.AmountGenerated
                })
                .ToList();

            // ========== OTHER ACTIVITIES ==========
            var otherActivities = await _otherActivityRepository.GetAllAsync();
            report.OtherActivities = otherActivities
                .Where(x => x.UnitLocationId == filter.UnitLocationId &&
                           x.FormStatus == "Approved" &&
                           x.CreatedAt >= startDate && x.CreatedAt < endDate)
                .Select(x => new ReportOtherActivityDto
                {
                    Title = x.Title ?? "-",
                    Description = x.Description ?? "-"
                })
                .ToList();

            report.TotalEntries = report.Programs.Count + report.Publications.Count +
                                 report.Nominations.Count + report.Consultancies.Count +
                                 report.Services.Count + report.OtherActivities.Count;

            return ServiceResult<AdminReportResponseDto>.Success(report);
        }
    }
}