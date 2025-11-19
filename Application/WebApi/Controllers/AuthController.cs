

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;


        public AuthController(IAuthService authService, ICurrentUserService currentUserService,IUserRepository userRepository, ITrainerAssignmentRepository trainerAssignmentRepository, IUnitHeadAssignmentRepository unitHeadAssignmentRepository)
        {
            _authService = authService;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
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
        /// Trainer and UnitHead login that returns unit assignments along with tokens
        /// </summary>
        [HttpPost("trainer-login")]
        public async Task<IActionResult> TrainerLogin(LoginRequestDto request)
        {
            try
            {
                var deviceInfo = GetDeviceInfo();

                // First, perform regular authentication
                var tokenResponse = await _authService.LoginAsync(request, deviceInfo);

                // Verify user
                var user = await _userRepository.GetByEmailAsync(request.Email);
                if (user == null)
                    return Unauthorized(new { message = "Invalid credentials" });

                if (user.Role != Role.TRAINER && user.Role != Role.UNITHEAD)
                    return Unauthorized(new { message = "This endpoint is only for trainers and unitheads" });

                List<UnitLocationDetailsDto> unitLocationDetails;
                List<int> assignedLocationIds;

                // Get assignments based on role
                if (user.Role == Role.TRAINER)
                {
                    var trainerAssignments = await _trainerAssignmentRepository.GetByTrainerIdAsync(user.Id);
                    unitLocationDetails = trainerAssignments.Select(a => new UnitLocationDetailsDto
                    {
                        UnitLocationId = a.UnitLocationId,
                        UnitId = a.UnitLocation.Unit.Id,
                        UnitName = a.UnitLocation.Unit.Name,
                        OrganizationId = a.UnitLocation.OrganizationId,
                        StateId = a.UnitLocation.District.State.Id,
                        StateName = a.UnitLocation.District.State.Name,
                        DistrictId = a.UnitLocation.District.Id,
                        DistrictName = a.UnitLocation.District.Name
                    }).ToList();
                    assignedLocationIds = trainerAssignments.Select(a => a.UnitLocationId).ToList();
                }
                else // Role.UNITHEAD
                {
                    var unitHeadAssignments = await _unitHeadAssignmentRepository.GetByUnitHeadIdAsync(user.Id);
                    unitLocationDetails = unitHeadAssignments.Select(a => new UnitLocationDetailsDto
                    {
                        UnitLocationId = a.UnitLocationId,
                        UnitId = a.UnitLocation.Unit.Id,
                        UnitName = a.UnitLocation.Unit.Name,
                        OrganizationId = a.UnitLocation.OrganizationId,
                        StateId = a.UnitLocation.District.State.Id,
                        StateName = a.UnitLocation.District.State.Name,
                        DistrictId = a.UnitLocation.District.Id,
                        DistrictName = a.UnitLocation.District.Name
                    }).ToList();
                    assignedLocationIds = unitHeadAssignments.Select(a => a.UnitLocationId).ToList();
                }

                var trainerDetails = new TrainerWithAssignmentsDto
                {
                    TrainerId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone ?? string.Empty,
                    Gender = user.Gender,
                    EmployementType = user.EmployementType,
                    DateOfBirth = user.DateOfBirth,
                    DateOfJoining = user.DateOfJoining,
                    IsDeactivated = user.IsDeactivated,
                    Qualification = user.Qualification,
                    AssignedLocationIds = assignedLocationIds,
                    UnitLocationDetails = unitLocationDetails
                };

                var response = new TrainerLoginResponseDto
                {
                    AccessToken = tokenResponse.AccessToken,
                    RefreshToken = tokenResponse.RefreshToken,
                    UserRole = user.Role.ToString(),
                    TrainerDetails = trainerDetails
                };

                return Ok(response);
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
