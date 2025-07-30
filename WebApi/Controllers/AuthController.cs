using Application.Interface;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUserService;

        public AuthController(IAuthService authService, ICurrentUserService currentUserService)
        {
            _authService = authService;
            _currentUserService = currentUserService;
        }
        /// <summary>
        /// Authenticate user and create session with device tracking
        /// </summary>
        /// <param name="request">User login credentials</param>
        /// <returns>Access token and refresh token</returns>
        /// <remarks>
        /// Authenticates user credentials and creates a new session with device tracking.
        /// 
        /// **Required Headers:**
        /// - User-Agent: Browser/client identification (Required)
        /// - X-Device-Type: Unique device identifier (Recommended)
        /// 
        /// </remarks>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            try
            {
                var deviceInfo = GetDeviceInfo();
                var result = await _authService.LoginAsync(request, deviceInfo);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// REQUIRED HEADERS: Same as login endpoint
        // - User-Agent: Must match original login session
        // - X-Device-Type: Should match original device (recommended)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            try
            {
                var deviceInfo = GetDeviceInfo();
                var result = await _authService.RefreshTokenAsync(request, deviceInfo);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout(RefreshTokenRequestDto request)
        {
            try
            {
                await _authService.LogoutAsync(request.RefreshToken);
                return Ok(new { message = "Logged out successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenRequestDto request)
        {
            try
            {
                var result = await _authService.RevokeTokenAsync(request.RefreshToken);
                if (result)
                    return Ok(new { message = "Token revoked successfully" });
                else
                    return BadRequest(new { message = "Token not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("revoke-all")]
        [Authorize]
        public async Task<IActionResult> RevokeAllTokens()
        {
            try
            {
                var userId = _currentUserService.UserId;
                await _authService.RevokeAllUserTokensAsync(userId);
                return Ok(new { message = "All tokens revoked successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("sessions")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserSessionDto>>> GetActiveSessions()
        {
            try
            {
                var userId = _currentUserService.UserId;
                var sessions = await _authService.GetActiveSessionsAsync(userId);
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private DeviceInfoDto GetDeviceInfo()
        {
            return new DeviceInfoDto
            {
                UserAgent = Request.Headers["User-Agent"].ToString(),
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                Location = Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? "Unknown",
                DeviceType = Request.Headers["X-Device-Type"].FirstOrDefault() ?? "Unknown"
            };
        }
    }
}
