using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities.Enum;
using Domain.Entities.Junction;

namespace Infrastructure.Services
{
    public class TrainerAssignmentService : ITrainerAssignmentService
    {
        private readonly ITrainerAssignmentRepository _trainerAssignment;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationUnitRepository _organizationUnit;

        public TrainerAssignmentService(ITrainerAssignmentRepository trainerAssignment, ICurrentUserService currentUser, IOrganizationUnitRepository organizationUnit, IUserRepository userRepository)
        {
            _trainerAssignment = trainerAssignment;
            _currentUser = currentUser;
            _userRepository = userRepository;
            _organizationUnit = organizationUnit;
        }
        public async Task<ServiceResult<List<TrainerUnitWithLocationsDto>>> GetTrainerUnits(int trainerId)
        {
            if (_currentUser.UserId != trainerId && _currentUser.Role != Role.UNITHEAD) return ServiceResult<List<TrainerUnitWithLocationsDto>>.Failure("Current user does not have necessary permission to access this.");
            var trainerAssignments = await _trainerAssignment.GetAssignmentsDetailsByTrainerAsync(trainerId);
            return ServiceResult<List<TrainerUnitWithLocationsDto>>.Success(trainerAssignments);
        }

        // NEW: Sync trainer assignments by location (add new, remove old)
        public async Task<ServiceResult> SyncTrainerAssignmentsByLocationAsync(BulkTrainerAssignmentByLocationDto request)
        {
            var trainer = await _userRepository.GetByIdAsync(request.TrainerId);
            if (trainer is null)
                return ServiceResult.Failure($"Trainer {request.TrainerId} does not exist");



            // Verify trainer role
            if (trainer.Role != Role.TRAINER)
                return ServiceResult.Failure($"User {request.TrainerId} is not a trainer");

            // Current assignments
            var existingAssignments = await _trainerAssignment.GetByTrainerIdAsync(request.TrainerId);
            var existingIds = existingAssignments.Select(x => x.UnitLocationId).ToList();
            var newIds = request.OrganizationUnitLocationIds ?? new List<int>();

            // Find what to add / remove
            var toAddIds = newIds.Except(existingIds).ToList();
            var toRemove = existingAssignments.Where(x => !newIds.Contains(x.UnitLocationId)).ToList();

            // ---- ADD ----
            var newAssignments = new List<TrainerAssignment>();
            foreach (var locationId in toAddIds)
            {
                var unitLocation = await _organizationUnit.GetByOrganizationUnitLocationsIdAsync(locationId);
                if (unitLocation is null) continue;

                // Validation: Only allow if the Trainer was created by same user as UnitLocation
                //if (unitLocation.CreatedById == trainer.CreatedById)
                //    continue;

                newAssignments.Add(new TrainerAssignment
                {
                    TrainerId = request.TrainerId,
                    UnitLocationId = locationId,
                    CreatedAt = DateTimeOffset.UtcNow,
                    CreatedById = _currentUser.UserId
                });
            }

            if (newAssignments.Any())
                await _trainerAssignment.AddRangeAsync(newAssignments);

            // ---- REMOVE ----
            if (toRemove.Any())
                await _trainerAssignment.DeleteRangeAsync(toRemove);

            return ServiceResult.Success("Trainer assignments synced successfully");
        }

        // NEW: Get all trainers created by current unit head with their assignments
        public async Task<List<TrainerWithAssignmentsDto>> GetTrainersWithAssignmentsCreatedByCurrentUserAsync()
        {
            var currentUserId = _currentUser.UserId;
            var trainers = await _userRepository.GetTrainersCreatedByAsync(currentUserId);

            return trainers.Select(trainer => new TrainerWithAssignmentsDto
            {
                TrainerId = trainer.Id,
                FirstName = trainer.FirstName,
                LastName = trainer.LastName,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Gender = trainer.Gender,
                EmployementType = trainer.EmployementType,
                DateOfBirth = trainer.DateOfBirth,
                DateOfJoining = trainer.DateOfJoining,
                IsDeactivated = trainer.IsDeactivated,
                Qualification = trainer.Qualification,
                AssignedLocationIds = trainer.TrainerAssignments.Select(ta => ta.UnitLocationId).ToList(),
                UnitLocationDetails = trainer.TrainerAssignments
                    .Select(ta => new UnitLocationDetailsDto
                    {
                        UnitLocationId = ta.UnitLocationId,
                        UnitId = ta.UnitLocation.UnitId,
                        UnitName = ta.UnitLocation.Unit.Name,
                        StateId = ta.UnitLocation.District.State.Id,
                        StateName = ta.UnitLocation.District.State.Name,
                        DistrictId = ta.UnitLocation.District.Id,
                        DistrictName = ta.UnitLocation.District.Name
                    })
                    .ToList()
            }).ToList();
        }
    }
}