using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities.Enum;

namespace Infrastructure.Services
{
    public class TrainerAssignmentService:ITrainerAssignmentService
    {
        private readonly ITrainerAssignmentRepository _trainerAssignment;
        private readonly ICurrentUserService _currentUser;

        public TrainerAssignmentService(ITrainerAssignmentRepository trainerAssignment, ICurrentUserService currentUser)
        {
            _trainerAssignment = trainerAssignment;
            _currentUser = currentUser;
        }
        public async Task<ServiceResult<List<TrainerUnitWithLocationsDto>>> GetTrainerUnits(int trainerId)
        {
            if (_currentUser.UserId != trainerId && _currentUser.Role != Role.ADMIN) return ServiceResult<List<TrainerUnitWithLocationsDto>>.Failure("Current user does not have necessary permission to access this.");
            var trainerAssignments = await _trainerAssignment.GetAssignmentsDetailsByTrainerAsync(trainerId);                
            return ServiceResult<List<TrainerUnitWithLocationsDto>>.Success(trainerAssignments);
        }
    }
}