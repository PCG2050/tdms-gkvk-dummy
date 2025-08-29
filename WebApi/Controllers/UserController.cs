using Application.Interface;
using Application.Models;
using Domain.Entities.Enum;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IOrganizationService _organizationService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrganizationUnitService _organizationUnitService;


        public UserController(ILogger<UserController> logger, IUserService userService, IOrganizationService organizationService, ICurrentUserService currentUserService, IOrganizationUnitService organizationUnitService)
        {
            _logger = logger;
            _userService = userService;
            _organizationService = organizationService;
            _currentUserService = currentUserService;
            _organizationUnitService = organizationUnitService;

        }
        [HttpPost]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead}")]
        public async Task<IActionResult> RegisterUser(UserRegisterDto registerDto)
        {
            try
            {
                int orgId = _currentUserService.OrganizationId;
                var user = await _organizationService.CreateUser(registerDto, orgId);
                return Ok(user);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("profile/me")]
        [Authorize]
        public async Task<ActionResult> CurrentUserProfile()
        {
            var user = await _userService.GetCurrentUserDetailsAsync();
            return Ok(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.OrganizationId,
                user.ProfileImageUrl,
                user.Phone
            });
        }
        [HttpGet("profile/{userId}")]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.SuperAdmin}")]
        public async Task<ActionResult> UserProfile(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user is null) return NotFound();
            return Ok(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.OrganizationId,
                user.ProfileImageUrl,
                user.Phone
            });
        }

        [HttpPost("{userId}/deactivate")]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.SuperAdmin},{RoleString.UnitHead}")]
        public async Task<IActionResult> DeactivateAccount(int userId)
        {
            var result = await _userService.UpdatedAccountStatus(userId, activate: false);
            if (!result.IsSuccess) return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);
            return NoContent();
        }

        [HttpPost("{userId}/activate")]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.SuperAdmin},{RoleString.UnitHead}")]
        public async Task<IActionResult> ActivateAccount(int userId)
        {
            var result = await _userService.UpdatedAccountStatus(userId, activate: true);
            if (!result.IsSuccess) return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);
            return NoContent();
        }
        [HttpPatch("{userId}/")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int userId, UserUpdateDto updateDto)
        {
            var result = await _userService.UpdateUserAsync(updateDto);
            if (!result.IsSuccess)
            {
                _logger.LogWarning(result.ErrorMessage);
                return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);
            }
            _logger.LogInformation($"Updated user {userId} successfully");
            return NoContent();
        }
        // Anyone can request a reset link
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var result = await _userService.ForgotPasswordAsync(dto);
            if (!result.IsSuccess)
                return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);

            // Always 204 (no info leak)
            return NoContent();
        }

        // Anyone with a token can reset
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var result = await _userService.ResetPasswordAsync(dto);
            if (!result.IsSuccess)
                return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);

            return NoContent();
        }

        [HttpPost("/UnitHead")]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead}")]
        public async Task<IActionResult> RegisterUnitHead(UserRegisterDto registerDto)
        {
            try
            {
                int orgId = _currentUserService.OrganizationId;

                // Step 1: Create user
                var user = await _organizationService.CreateUser(registerDto, orgId);

                // Step 2: If UnitHead, sync the assignments
                if (user.Role == Role.UNITHEAD && registerDto.OrganizationUnitLocationIds?.Any() == true)
                {
                    var request = new BulkUnitHeadAssignmentByLocationDto
                    {
                        UnitHeadId = user.Id,
                        OrganizationUnitLocationIds = registerDto.OrganizationUnitLocationIds
                    };

                    await _organizationUnitService.SyncUnitHeadAssignmentsByLocationAsync(request);
                }

                return Ok(user);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("UnitHead/{id}")]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead}")]
        public async Task<IActionResult> UpdateUnitHead(int id, UserUpdateDto updateDto)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user is null) return NotFound($"User {id} not found");

                // Update user basic details
                await _userService.UpdateUserAsync(updateDto);

                // If UnitHead role, sync assignments
                if (updateDto.Role == Role.UNITHEAD && updateDto.OrganizationUnitLocationIds != null)
                {
                    var request = new BulkUnitHeadAssignmentByLocationDto
                    {
                        UnitHeadId = user.Id,
                        OrganizationUnitLocationIds = updateDto.OrganizationUnitLocationIds
                    };

                    await _organizationUnitService.SyncUnitHeadAssignmentsByLocationAsync(request);
                }

                return Ok(ServiceResult.Success("User updated successfully"));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}/UnitHead")]
        [Authorize(Roles = $"{RoleString.Admin}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUnitHeadAsync(id, _currentUserService.UserId);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    try
        //    {
        //        var user = await _userService.GetUserByIdAsync(id);
        //        if (user is null)
        //            return NotFound($"User {id} not found");

        //        // 🔐 Check ownership
        //        var currentUserId = _currentUserService.UserId;
        //        if (user.CreatedById != currentUserId)
        //            return Forbid("You are not allowed to delete this user");

        //        // If UnitHead, remove assignments first
        //        if (user.Role == Role.UNITHEAD)
        //        {
        //            var assignments = await _unitHeadAssignment.GetByUnitHeadIdAsync(id);
        //            if (assignments.Any())
        //                await _unitHeadAssignment.DeleteRangeAsync(assignments);
        //        }

        //        await _userService.DeleteUserAsync(user); // <-- service method to actually delete
        //        return Ok(ServiceResult.Success("User deleted successfully"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}



    }
}
