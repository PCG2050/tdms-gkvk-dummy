using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.ConsultSocialMedia;
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

        public UnitHeadAssignmentService(
            IUnitHeadAssignmentRepository unitHeadAssignment,
            ICurrentUserService currentUser,
            IUserRepository userRepository,
            ITableServiceRepository tableServiceRepository,
            IConsultingServiceRepository consultingServiceRepository,
            IPublicationRepository publicationRepository,
            INominationRewardRepository nominationRewardRepository,
            ITableOtherActivityRepository tableOtherActivityRepository)
        {
            _unitHeadAssignment = unitHeadAssignment;
            _userRepository = userRepository;
            _currentUser = currentUser;
            _tableServiceRepository = tableServiceRepository;
            _consultingServiceRepository = consultingServiceRepository;
            _publicationRepository = publicationRepository;
            _nominationRewardRepository = nominationRewardRepository;
            _tableOtherActivityRepository = tableOtherActivityRepository;
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
