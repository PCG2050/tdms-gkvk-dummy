using Application.Interface;
using Application.Models;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerAssignmentService _assignmentService;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationUnitService _organizationUnitService;

        public TrainerController(ITrainerAssignmentService assignmentService, IUserService userService, ICurrentUserService currentUser, IOrganizationService organizationService, IOrganizationUnitService organizationUnitService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
            _currentUser = currentUser;
            _organizationService = organizationService;
            _organizationUnitService = organizationUnitService;
        }

        // NEW: Get trainers with their current assignments (for UnitHead to manage)
        [HttpGet("with-assignments")]
        [Authorize(Roles = RoleString.UnitHead)]
        public async Task<IActionResult> GetTrainersWithAssignments()
        {
            try
            {
                var trainers = await _assignmentService.GetTrainersWithAssignmentsCreatedByCurrentUserAsync();
                return Ok(trainers);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "An error occurred while retrieving trainers with assignments");
            }
        }

        // NEW: Create trainer with assignments
        [HttpPost]
        [Authorize(Roles = RoleString.UnitHead)]
        public async Task<IActionResult> RegisterTrainer(UserRegisterDto registerDto)
        {
            try
            {
                int orgId = _currentUser.OrganizationId;

                // Step 1: Create user
                var user = await _organizationService.CreateUser(registerDto, orgId);

                // Step 2: If Trainer, sync the assignments
                if (user.Role == Role.TRAINER && registerDto.OrganizationUnitLocationIds?.Any() == true)
                {
                    var request = new BulkTrainerAssignmentByLocationDto
                    {
                        TrainerId = user.Id,
                        OrganizationUnitLocationIds = registerDto.OrganizationUnitLocationIds
                    };

                    await _assignmentService.SyncTrainerAssignmentsByLocationAsync(request);
                }
                var trainerResponseDto = new UserWithAssignmentsResponseDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Role = user.Role,
                    OrganizationId = user.OrganizationId,
                    IsDeactivated = user.IsDeactivated,
                    DateOfBirth = user.DateOfBirth,
                    DateOfJoining = user.DateOfJoining,
                    EmploymentType = user.EmployementType,
                    Gender = user.Gender,
                    Qualification = user.Qualification,
                    OrganizationUnitLocationIds = registerDto.OrganizationUnitLocationIds ?? new List<int>()
                };

                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                return Forbid(e.Message);
            }
        }

        // NEW: Update trainer with assignments
        [HttpPatch("{id}")]
        [Authorize(Roles = RoleString.UnitHead)]
        public async Task<IActionResult> UpdateTrainer(int id, UserUpdateDto updateDto)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user is null)
                    return NotFound($"User {id} not found");

                if (user.Role != Role.TRAINER)
                    return BadRequest("User is not a trainer");

                // Check if current user created this trainer
                if (user.CreatedById != _currentUser.UserId)
                    return Forbid("You can only update trainers you created");

                // Update user basic details
                updateDto.Id = id;
                var updateResult = await _userService.UpdateUserAsync(updateDto);
                if (!updateResult.IsSuccess)
                    return BadRequest(updateResult.ErrorMessage);

                // If assignments are provided, sync them
                if (updateDto.OrganizationUnitLocationIds != null)
                {
                    var request = new BulkTrainerAssignmentByLocationDto
                    {
                        TrainerId = user.Id,
                        OrganizationUnitLocationIds = updateDto.OrganizationUnitLocationIds
                    };

                    var syncResult = await _assignmentService.SyncTrainerAssignmentsByLocationAsync(request);
                    if (!syncResult.IsSuccess)
                        return BadRequest(syncResult.ErrorMessage);
                }

                return Ok(ServiceResult.Success("Trainer updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{trainerId}")]
        [Authorize(Roles = RoleString.UnitHead)]
        public async Task<IActionResult> DeleteTrainer(int trainerId)
        {
            var result = await _userService.DeleteTrainerAsync(trainerId);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }


    }
}
