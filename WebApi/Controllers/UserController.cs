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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Authorize(Roles = RoleString.Admin)]
        public async Task<IActionResult> RegisterUser(UserRegisterDto userRegisterDto)
        {
            try
            {
                var user = await _userService.CreateUserAsync(userRegisterDto);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ProfileImageUrl = user.ProfileImageUrl
                };
                return CreatedAtRoute("",userDto);
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
