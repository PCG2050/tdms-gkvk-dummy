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

       
     
     



    }
}
