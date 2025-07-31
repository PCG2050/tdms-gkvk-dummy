using Application.Interface;
using Application.Models;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrganizationService _organizationService;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IUserService userService, IOrganizationService organizationService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _organizationService = organizationService;
            _currentUserService = currentUserService;
        }
        [HttpPost]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead}")]
        public async Task<IActionResult> RegisterUser(UserRegisterDto registerDto)
        {
            try
            {
                int orgId = _currentUserService.OrganizationId;
                var user = await _organizationService.CreateUser(registerDto, orgId);
                return CreatedAtRoute("",user);
            }
            catch(InvalidOperationException e)
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
                user.ProfileImageUrl
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
                user.ProfileImageUrl
            });
        }
    }
}
