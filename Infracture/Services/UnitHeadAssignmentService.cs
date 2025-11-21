using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.ASM;
using Application.Interface.Repository.DataTables.ATIC;
using Application.Interface.Repository.DataTables.ConsultSocialMedia;
using Application.Interface.Repository.DataTables.DEU;
using Application.Interface.Repository.DataTables.EEU;
using Application.Interface.Repository.DataTables.FTI;
using Application.Interface.Repository.DataTables.IBTVA;
using Application.Interface.Repository.DataTables.NAEP;
using Application.Interface.Repository.DataTables.STU;
using Application.Interface.Repository.DataTables.TblService;
using Application.Models;

using Domain.Entities.Enum;

namespace Infrastructure.Services
{
    public class UnitHeadAssignmentService : IUnitHeadAssignmentService
    {
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignment;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserRepository _userRepository;
        private readonly ITableServiceRepository _tableServiceRepository;
        private readonly IConsultingServiceRepository _consultingServiceRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly INominationRewardRepository _nominationRewardRepository;
        private readonly ITableOtherActivityRepository _tableOtherActivityRepository;
        private readonly IAticProgramDetailsRepository _aticProgramDetailsRepository;
        private readonly IDeuProgramDetailsRepository _deuProgramDetailsRepository;
        private readonly IEeuProgramDetailsRepository _eeuProgramDetailsRepository;
        private readonly IFtiProgramDetailsRepository _ftiProgramDetailsRepository;
        private readonly IIbtvaProgramDetailsRepository _ibtvaProgramDetailsRepository;
        private readonly INaepProgramDetailsRepository _naepProgramDetailsRepository;
        private readonly IStuProgramDetailsRepository _stuProgramDetailsRepository;
        private readonly IASMVisitorDetailsRepository _asmVisitorDetailsRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;

        public UnitHeadAssignmentService(
            IUnitHeadAssignmentRepository unitHeadAssignment,
            ICurrentUserService currentUser,
            IUserRepository userRepository,
            ITableServiceRepository tableServiceRepository,
            IConsultingServiceRepository consultingServiceRepository,
            IPublicationRepository publicationRepository,
            INominationRewardRepository nominationRewardRepository,
            ITableOtherActivityRepository tableOtherActivityRepository,
            IAticProgramDetailsRepository aticProgramDetailsRepository,
            IDeuProgramDetailsRepository deuProgramDetailsRepository,
            IEeuProgramDetailsRepository eeuProgramDetailsRepository,
            IFtiProgramDetailsRepository ftiProgramDetailsRepository,
            IIbtvaProgramDetailsRepository ibtvaProgramDetailsRepository,
            INaepProgramDetailsRepository naepProgramDetailsRepository,
            IStuProgramDetailsRepository stuProgramDetailsRepository,
            IASMVisitorDetailsRepository asmVisitorDetailsRepository,
            ITrainerAssignmentRepository trainerAssignmentRepository)
        {
            _unitHeadAssignment = unitHeadAssignment;
            _userRepository = userRepository;
            _currentUser = currentUser;
            _tableServiceRepository = tableServiceRepository;
            _consultingServiceRepository = consultingServiceRepository;
            _publicationRepository = publicationRepository;
            _nominationRewardRepository = nominationRewardRepository;
            _tableOtherActivityRepository = tableOtherActivityRepository;
            _aticProgramDetailsRepository = aticProgramDetailsRepository;
            _deuProgramDetailsRepository = deuProgramDetailsRepository;
            _eeuProgramDetailsRepository = eeuProgramDetailsRepository;
            _ftiProgramDetailsRepository = ftiProgramDetailsRepository;
            _ibtvaProgramDetailsRepository = ibtvaProgramDetailsRepository;
            _naepProgramDetailsRepository = naepProgramDetailsRepository;
            _stuProgramDetailsRepository = stuProgramDetailsRepository;
            _asmVisitorDetailsRepository = asmVisitorDetailsRepository;
            _trainerAssignmentRepository = trainerAssignmentRepository;
        }

        //public async Task<ServiceResult<List<UnitHeadFlatDto>> GetAllUnitHeadsDetails()
        //{
        //    if (_currentUser.Role != Role.ADMIN) return ServiceResult<List<UnitHeadFlatDto>>.Failure("Current user does not have necessary permission to access this.");
        //    var unitHeads = await _userRepository.Get

        //}

        //Old method
        public async Task<ServiceResult<List<UnitWithLocationsDto>>> GetUnitHeadUnits(int unitHeadId)
        {
            if (_currentUser.UserId != unitHeadId && _currentUser.Role != Role.ADMIN) return ServiceResult<List<UnitWithLocationsDto>>.Failure("Current user does not have necessary permission to access this.");
            var unitHeadAssignments = await _unitHeadAssignment.GetAssignmentsDetailsByUnitHeadAsync(unitHeadId);
            return ServiceResult<List<UnitWithLocationsDto>>.Success(unitHeadAssignments);
        }

        public async Task<ServiceResult<UnitHeadStatisticsDto>> GetUnitHeadStatisticsAsync(int unitHeadId)
        {
            if (_currentUser.UserId != unitHeadId && _currentUser.Role != Role.ADMIN)
                return ServiceResult<UnitHeadStatisticsDto>.Failure("Current user does not have necessary permission to access this.");

            // Get assigned unit locations for this unit head
            var unitLocationIds = await _unitHeadAssignment.GetUnitLocationIdsByUnitHeadIdAsync(unitHeadId);

            // Count distinct units (not locations)
            var unitIds = await _unitHeadAssignment.GetUnitIdsByUnitHeadIdAsync(unitHeadId);
            var assignedUnitsCount = unitIds.Distinct().Count();

            // Get trainers count - trainers created by this unit head
            var trainers = await _userRepository.GetTrainersCreatedByAsync(unitHeadId);
            var trainersCount = trainers.Count;

            // Get status summaries from all data tables
            var pendingCount = 0;
            var approvedCount = 0;

            // TblService (Services table)
            var tblServiceSummary = await _tableServiceRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += tblServiceSummary.GetValueOrDefault("Pending", 0);
            approvedCount += tblServiceSummary.GetValueOrDefault("Approved", 0);

            // Consulting & Social Media Services
            var consultingSummary = await _consultingServiceRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += consultingSummary.GetValueOrDefault("Pending", 0);
            approvedCount += consultingSummary.GetValueOrDefault("Approved", 0);

            // Publications
            var publicationSummary = await _publicationRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += publicationSummary.GetValueOrDefault("Pending", 0);
            approvedCount += publicationSummary.GetValueOrDefault("Approved", 0);

            // Nominations & Rewards
            var nominationSummary = await _nominationRewardRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += nominationSummary.GetValueOrDefault("Pending", 0);
            approvedCount += nominationSummary.GetValueOrDefault("Approved", 0);

            // Other Activities
            var otherActivitySummary = await _tableOtherActivityRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += otherActivitySummary.GetValueOrDefault("Pending", 0);
            approvedCount += otherActivitySummary.GetValueOrDefault("Approved", 0);

            // ATIC Program Details
            var aticSummary = await _aticProgramDetailsRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += aticSummary.GetValueOrDefault("Pending", 0);
            approvedCount += aticSummary.GetValueOrDefault("Approved", 0);

            // DEU Program Details
            var deuSummary = await _deuProgramDetailsRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += deuSummary.GetValueOrDefault("Pending", 0);
            approvedCount += deuSummary.GetValueOrDefault("Approved", 0);

            // EEU Program Details
            var eeuSummary = await _eeuProgramDetailsRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += eeuSummary.GetValueOrDefault("Pending", 0);
            approvedCount += eeuSummary.GetValueOrDefault("Approved", 0);

            // FTI Program Details
            var ftiSummary = await _ftiProgramDetailsRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += ftiSummary.GetValueOrDefault("Pending", 0);
            approvedCount += ftiSummary.GetValueOrDefault("Approved", 0);

            // IBTVA Program Details
            var ibtvaSummary = await _ibtvaProgramDetailsRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += ibtvaSummary.GetValueOrDefault("Pending", 0);
            approvedCount += ibtvaSummary.GetValueOrDefault("Approved", 0);

            // NAEP Program Details
            var naepSummary = await _naepProgramDetailsRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += naepSummary.GetValueOrDefault("Pending", 0);
            approvedCount += naepSummary.GetValueOrDefault("Approved", 0);

            // STU Program Details
            var stuSummary = await _stuProgramDetailsRepository.GetStatusSummaryAsync(unitLocationIds);
            pendingCount += stuSummary.GetValueOrDefault("Pending", 0);
            approvedCount += stuSummary.GetValueOrDefault("Approved", 0);

            // ASM Visitor Details (uses trainer IDs instead of unit location IDs)
            // Get all trainers assigned to the unit head's unit locations
            var trainerIds = new List<int>();
            foreach (var unitLocationId in unitLocationIds)
            {
                var trainersInLocation = await _trainerAssignmentRepository.GetTrainerIdsByUnitLocationIdAsync(unitLocationId);
                trainerIds.AddRange(trainersInLocation);
            }
            trainerIds = trainerIds.Distinct().ToList();

            if (trainerIds.Any())
            {
                var asmSummary = await _asmVisitorDetailsRepository.GetStatusSummaryAsync(trainerIds);
                pendingCount += asmSummary.GetValueOrDefault("Pending", 0);
                approvedCount += asmSummary.GetValueOrDefault("Approved", 0);
            }

            // NOTE: approvedCount is total approved, not filtered by month
            // To get approved this month, we would need to query each repository separately
            // For now, returning total approved count
            var approvedThisMonth = approvedCount; // TODO: Filter by current month

            var statistics = new UnitHeadStatisticsDto
            {
                AssignedUnitsCount = assignedUnitsCount,
                TrainersCount = trainersCount,
                PendingApprovalsCount = pendingCount,
                ApprovedThisMonthCount = approvedThisMonth
            };

            return ServiceResult<UnitHeadStatisticsDto>.Success(statistics);
        }
    }

}
