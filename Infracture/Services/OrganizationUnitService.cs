using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities.Enum;
using Domain.Entities.Junction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class OrganizationUnitService : IOrganizationUnitService
    {
        private readonly IOrganizationUnitRepository _organizationUnit;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserService _userService;
        private readonly ITrainerAssignmentRepository _trainerAssignment;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignment;


        public OrganizationUnitService(IOrganizationUnitRepository organizationUnit, ICurrentUserService currentUser,
                                       IUserService userService, ITrainerAssignmentRepository trainerAssignment, IUnitHeadAssignmentRepository unitHeadAssignment)
        {
            _organizationUnit = organizationUnit;
            _currentUser = currentUser;
            _userService = userService;
            _trainerAssignment = trainerAssignment;
            _unitHeadAssignment = unitHeadAssignment;
        }



        public async Task<OrganizationUnitLocation> AddUnitToOrganization(OrganizationUnitLocationDto addUnitLocationDto)
        {
            var role = _currentUser.Role;
            if (role != Role.ADMIN) throw new UnauthorizedAccessException($"{role} does not have access to the action {nameof(this.AddUnitToOrganization)} at {this.GetType()}");
            var newOrgUnit = new OrganizationUnitLocation()
            {
                OrganizationId = _currentUser.OrganizationId,
                CreatedAt = DateTime.UtcNow,
                CreatedById = _currentUser.UserId,
                DistrictId = addUnitLocationDto.DistrictId,
                UnitId = addUnitLocationDto.UnitId,
            };
            if (await _organizationUnit.ExistsAsync(newOrgUnit.OrganizationId, newOrgUnit.UnitId, newOrgUnit.DistrictId))
                throw new InvalidOperationException("Cannot add duplicate unit");
            await _organizationUnit.SaveAsync(newOrgUnit);
            return newOrgUnit;
        }
        public async Task<IEnumerable<OrganizationUnitLocation>> GetOrganizationUnits()
        {
            // Based on the OrgId in user token
            var role = _currentUser.Role;
            if (role != Role.ADMIN) throw new UnauthorizedAccessException($"{role} does not have access to the action {nameof(this.AddUnitToOrganization)} at {this.GetType()}");
            var orgUnits = await _organizationUnit.GetByOrganizationIdAsync(_currentUser.OrganizationId, _currentUser.UserId);
            if (orgUnits == null || !orgUnits.Any()) return [];
            else return orgUnits;
        }

        public async Task<IEnumerable<OrgUnitLocationIdDetailsDto>> GetOrganizationUnitsDetails()
        {
            return await _organizationUnit.GetQueryable()
                .Where(x => x.OrganizationId == _currentUser.OrganizationId
                            && x.CreatedById == _currentUser.UserId)
                .Include(x => x.Unit)
                .Include(x => x.District)
                    .ThenInclude(x => x.State)
                .Select(x => new OrgUnitLocationIdDetailsDto

                {
                    OrgUnitLocationId = x.Id,
                    UnitId = x.Unit.Id,
                    UnitName = x.Unit.Name,
                    StateId = x.District.State.Id,
                    StateName = x.District.State.Name,
                    DistrictId = x.District.Id,
                    DistrictName = x.District.Name
                })
                .ToListAsync();
        }


        public async Task RemoveUnitFromOrganization(OrganizationUnitLocationDto organizationUnitLocationDto)
        {
            var role = _currentUser.Role;
            if (role != Role.ADMIN) throw new UnauthorizedAccessException($"{role} does not have access to the action {nameof(this.AddUnitToOrganization)} at {this.GetType()}");
            var mapping = await _organizationUnit.GetByOrganizationUnitDistrictAsync(_currentUser.OrganizationId, organizationUnitLocationDto.UnitId, organizationUnitLocationDto.DistrictId);
            if (mapping != null)
                await _organizationUnit.DeleteAsync(mapping);
        }
        public async Task<ServiceResult> MapExistingTrainersAsync(ExistingTrainerAssignmentDto trainerAssignment)
        {
            var unitLocation = await _organizationUnit.GetByOrganizationUnitDistrictAsync(_currentUser.OrganizationId, trainerAssignment.UnitId, trainerAssignment.DistrictId);
            if (unitLocation is null)
                return ServiceResult.Failure("This Unit mapping does not exist");
            var trainer = await _userService.GetUserByIdAsync(trainerAssignment.TrainerId);
            if (trainer is null)
                return ServiceResult.Failure("Trainer does not exist");
            var alreadyExists = await _trainerAssignment.AssignmentExistsAsync(trainerAssignment);
            if (alreadyExists)
                return ServiceResult.Failure("The trainer is already assigned to this Unit");
            var newAssignment = new TrainerAssignment
            {
                TrainerId = trainerAssignment.TrainerId,
                UnitLocationId = unitLocation.Id,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedById = _currentUser.UserId


            };
            await _trainerAssignment.AddAsync(newAssignment);
            return ServiceResult.Success();

        }
        public async Task<ServiceResult> UnMapTrainerFromUnitLocationAsync(ExistingTrainerAssignmentDto trainerAssignment)
        {
            var unitLocation = await _organizationUnit.GetByOrganizationUnitDistrictAsync(_currentUser.OrganizationId, trainerAssignment.UnitId, trainerAssignment.DistrictId);
            if (unitLocation is null)
                return ServiceResult.Failure("This Unit mapping does not exist");
            var existingAssignment = await _trainerAssignment.GetByTrainerLocationAsync(unitLocation.Id, trainerAssignment.TrainerId);
            if (existingAssignment is null)
                return ServiceResult.Failure("This assignment does not exist");
            await _trainerAssignment.DeleteAsync(existingAssignment);
            return ServiceResult.Success();
        }


        public async Task<ServiceResult> SyncUnitHeadAssignmentsByLocationAsync(BulkUnitHeadAssignmentByLocationDto request)
        {
            var unitHead = await _userService.GetUserByIdAsync(request.UnitHeadId);
            if (unitHead is null)
                return ServiceResult.Failure($"UnitHead {request.UnitHeadId} does not exist");

            // Current assignments
            var existingAssignments = await _unitHeadAssignment.GetByUnitHeadIdAsync(request.UnitHeadId);
            var existingIds = existingAssignments.Select(x => x.UnitLocationId).ToList();
            var newIds = request.OrganizationUnitLocationIds ?? new List<int>();

            // Find what to add / remove
            var toAddIds = newIds.Except(existingIds).ToList();
            var toRemove = existingAssignments.Where(x => !newIds.Contains(x.UnitLocationId)).ToList();

            // ---- ADD ----
            var newAssignments = new List<UnitHeadAssignment>();
            foreach (var locationId in toAddIds)
            {
                var unitLocation = await _organizationUnit.GetByOrganizationUnitLocationsIdAsync(locationId);
                if (unitLocation is null) continue;

                // Validation: Only allow if the UnitHead was created by same admin as UnitLocation
                if (unitLocation.CreatedById != unitHead.CreatedById)
                    continue;

                newAssignments.Add(new UnitHeadAssignment
                {
                    UnitHeadId = request.UnitHeadId,
                    UnitLocationId = locationId,
                    CreatedAt = DateTimeOffset.UtcNow,
                    CreatedById = _currentUser.UserId
                });
            }

            if (newAssignments.Any())
                await _unitHeadAssignment.AddRangeAsync(newAssignments);

            // ---- REMOVE ----
            if (toRemove.Any())
                await _unitHeadAssignment.DeleteRangeAsync(toRemove);

            return ServiceResult.Success("Assignments synced successfully");
        }

        public async Task<ServiceResult> SyncTrainerAssignmentsByLocationAsync(BulkTrainerAssignmentByLocationDto request)
        {
            var trainer = await _userService.GetUserByIdAsync(request.TrainerId);
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
                if (unitLocation.CreatedById != trainer.CreatedById)
                    continue;

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

        // NEW: Get paginated UnitLocations created by a specific Admin
        public async Task<PaginatedResult<OrgUnitLocationIdDetailsDto>> GetUnitLocationsCreatedByAdmin(int adminId, int pageNumber = 1, int pageSize = 10)
        {
            // Check permissions: only the Admin themselves or SuperAdmin can access this
            if (_currentUser.UserId != adminId && _currentUser.Role != Role.SUPERADMIN)
                throw new UnauthorizedAccessException("You don't have permission to access this data");

            // Verify the user is actually an Admin
            var admin = await _userService.GetUserByIdAsync(adminId);
            if (admin == null || admin.Role != Role.ADMIN)
                throw new InvalidOperationException("User is not an Admin");
            return await _organizationUnit.GetPaginatedUnitLocationsCreatedByAsync(adminId, pageNumber, pageSize);
        }
        

        // NEW: Get all UnitLocations created by a specific Admin (non-paginated)
        public async Task<List<OrgUnitLocationIdDetailsDto>> GetAllUnitLocationsCreatedByAdmin(int adminId)
        {
            // Check permissions: only the Admin themselves or SuperAdmin can access this
            if (_currentUser.UserId != adminId && _currentUser.Role != Role.SUPERADMIN)
                throw new UnauthorizedAccessException("You don't have permission to access this data");

            // Verify the user is actually an Admin
            var admin = await _userService.GetUserByIdAsync(adminId);
            if (admin == null || admin.Role != Role.ADMIN)
                throw new InvalidOperationException("User is not an Admin");

            var unitLocations = await _organizationUnit.GetUnitLocationsCreatedByAsync(adminId);

            // Convert to OrgUnitLocationIdDetailsDto
            return unitLocations.Select(ul => new OrgUnitLocationIdDetailsDto
            {
                OrgUnitLocationId = ul.Id,
                UnitId = ul.UnitId,
                UnitName = ul.Unit?.Name ?? "Unknown Unit",
                StateId = ul.District?.State?.Id ?? 0,
                StateName = ul.District?.State?.Name ?? "Unknown State",
                DistrictId = ul.DistrictId,
                DistrictName = ul.District?.Name ?? "Unknown District"
            }).ToList();
        }

        // NEW: Get organization unit location by ID
        public async Task<OrgUnitLocationIdDetailsDto?> GetOrganizationUnitLocationById(int id)
        {
            var role = _currentUser.Role;
            if (role == Role.UNDEFINED)
                throw new UnauthorizedAccessException($"{role} does not have access to this action");

            var unitLocation = await _organizationUnit.GetByIdAsync(id);
            if (unitLocation == null) return null;

            // Check if user has access to this unit location
            if (role == Role.ADMIN && unitLocation.CreatedById != _currentUser.UserId)
                throw new UnauthorizedAccessException("You can only access unit locations you created");

            return new OrgUnitLocationIdDetailsDto
            {
                OrgUnitLocationId = unitLocation.Id,
                UnitId = unitLocation.UnitId,
                UnitName = unitLocation.Unit?.Name ?? "Unknown Unit",
                StateId = unitLocation.District?.State?.Id ?? 0,
                StateName = unitLocation.District?.State?.Name ?? "Unknown State",
                DistrictId = unitLocation.DistrictId,
                DistrictName = unitLocation.District?.Name ?? "Unknown District"
            };
        }

        // NEW: Update organization unit location
        public async Task<ServiceResult<OrgUnitLocationIdDetailsDto>> UpdateOrganizationUnitLocationAsync(OrganizationUnitLocationUpdateDto updateDto)
        {
            var role = _currentUser.Role;
            if (role != Role.ADMIN)
                throw new UnauthorizedAccessException($"{role} does not have access to update unit locations");

            var existingUnitLocation = await _organizationUnit.GetByIdAsync(updateDto.Id);
            if (existingUnitLocation == null)
                return ServiceResult<OrgUnitLocationIdDetailsDto>.Failure("Unit location not found", ServiceErrorStatus.NOTFOUND);

            // Check if admin created this unit location
            if (existingUnitLocation.CreatedById != _currentUser.UserId)
                throw new UnauthorizedAccessException("You can only update unit locations you created");

            // Update fields if provided
            bool hasChanges = false;

            if (updateDto.UnitId.HasValue && updateDto.UnitId.Value != existingUnitLocation.UnitId)
            {
                existingUnitLocation.UnitId = updateDto.UnitId.Value;
                hasChanges = true;
            }

            if (updateDto.DistrictId.HasValue && updateDto.DistrictId.Value != existingUnitLocation.DistrictId)
            {
                // Check if the new combination doesn't create a duplicate
                if (await _organizationUnit.ExistsAsync(existingUnitLocation.OrganizationId,
                    updateDto.UnitId ?? existingUnitLocation.UnitId, updateDto.DistrictId.Value))
                {
                    return ServiceResult<OrgUnitLocationIdDetailsDto>.Failure(
                        "A unit location with this unit and district combination already exists",
                        ServiceErrorStatus.INVALIDOPERATION);
                }
                existingUnitLocation.DistrictId = updateDto.DistrictId.Value;
                hasChanges = true;
            }

            if (hasChanges)
            {
                existingUnitLocation.UpdatedAt = DateTimeOffset.UtcNow;
                existingUnitLocation.UpdatedById = _currentUser.UserId;
                await _organizationUnit.SaveAsync(existingUnitLocation);
            }

            // Return updated details
            var updatedDetails = await GetOrganizationUnitLocationById(updateDto.Id);
            return ServiceResult<OrgUnitLocationIdDetailsDto>.Success(updatedDetails!);
        }

        // NEW: Remove unit location by ID
        public async Task<ServiceResult> RemoveUnitFromOrganizationById(int id)
        {
            var role = _currentUser.Role;
            if (role != Role.ADMIN)
                throw new UnauthorizedAccessException($"{role} does not have access to delete unit locations");

            var unitLocation = await _organizationUnit.GetByIdAsync(id);
            if (unitLocation == null)
                return ServiceResult.Failure("Unit location not found", ServiceErrorStatus.NOTFOUND);

            // Check if admin created this unit location
            if (unitLocation.CreatedById != _currentUser.UserId)
                throw new UnauthorizedAccessException("You can only delete unit locations you created");

            // Check if there are any assignments that would be affected
            var hasTrainerAssignments = await _trainerAssignment.HasAssignmentsForUnitLocationAsync(id);
            var hasUnitHeadAssignments = await _unitHeadAssignment.HasAssignmentsForUnitLocationAsync(id);

            if (hasTrainerAssignments || hasUnitHeadAssignments)
            {
                return ServiceResult.Failure(
                    "Cannot delete unit location as it has active trainer or unit head assignments. Please remove assignments first.",
                    ServiceErrorStatus.INVALIDOPERATION);
            }

            await _organizationUnit.DeleteAsync(unitLocation);
            return ServiceResult.Success();
        }

    }
}
