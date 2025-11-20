using Application.Interface;
using Application.Interface.Repository;
using Application.Models;

using Domain.Entities.Enum;

namespace Infrastructure.Services
{
    public class UnitHeadAssignmentService : IUnitHeadAssignmentService
    {
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignment;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserRepository _userRepository;
        public UnitHeadAssignmentService(IUnitHeadAssignmentRepository unitHeadAssignment, ICurrentUserService currentUser,IUserRepository userRepository)
        {
            _unitHeadAssignment = unitHeadAssignment;
            _userRepository = userRepository;
            _currentUser = currentUser;
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

            // TODO: Implement pending approvals and approved count when repository methods are available
            // For now, returning 0 for these statistics
            var pendingCount = 0;
            var approvedThisMonth = 0;

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
