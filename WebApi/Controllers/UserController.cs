



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
        private readonly IAzureStorageService _azureStorageService;


        public UserController(ILogger<UserController> logger, 
            IUserService userService, 
            IOrganizationService organizationService, 
            ICurrentUserService currentUserService, 
            IOrganizationUnitService organizationUnitService,
            IAzureStorageService azureStorageService)
        {
            _logger = logger;
            _userService = userService;
            _organizationService = organizationService;
            _currentUserService = currentUserService;
            _organizationUnitService = organizationUnitService;
            _azureStorageService = azureStorageService;
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
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.SuperAdmin},{RoleString.UnitHead},{RoleString.Trainer}")]
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

        [HttpPost("verify-reset-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyPasswordResetOTP([FromBody] VerifyOTPDto dto)
        {
            var result = await _userService.VerifyPasswordResetOTPAsync(dto);

            if (!result.IsSuccess)
                return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);

            var otpResult = result.Data;

            if (!otpResult.IsValid)
            {
                return BadRequest(new
                {
                    message = otpResult.ErrorMessage,
                    remainingAttempts = otpResult.RemainingAttempts,
                    isBlocked = otpResult.IsBlocked,
                    success = false
                });
            }

            return Ok(new
            {
                message = "OTP verified successfully. You can now reset your password.",
                success = true,
                verified = true
            });
        }
        

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordWithOTP([FromBody] ResetPasswordWithOTPDto dto)
        {
            var result = await _userService.ResetPasswordWithOTPAsync(dto);

            if (!result.IsSuccess)
                return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);

            return Ok(new
            {
                message = "Password reset successfully. You can now login with your new password.",
                success = true
            });
        }

        //Request new OTP if previous was not sent or blocked
        [HttpPost("resend-reset-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendPasswordResetOTP([FromBody] ForgotPasswordDto dto)
        {            
            return await ForgotPassword(dto);
        }

        [HttpPost("profile/upload-image")]
        [Authorize]
        public async Task<IActionResult> UploadProfileImage([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded");

                // Validate file type and size
                var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
                if (!allowedTypes.Contains(file.ContentType))
                    return BadRequest("Invalid file type. Only JPEG, PNG, GIF, and WebP are allowed.");

                const int maxSizeInBytes = 5 * 1024 * 1024; // 5MB
                if (file.Length > maxSizeInBytes)
                    return BadRequest("File size exceeds 5MB limit.");

                var userId = _currentUserService.UserId;
                var organizationId = _currentUserService.OrganizationId;

                var organization = await _organizationService.GetOrganizationAsync(organizationId);
                if (organization == null || string.IsNullOrEmpty(organization.StorageContainerName))
                {
                    return BadRequest("Invalid organization or storage container name is missing.");
                }

                var containerName = $"{organization.StorageContainerName}-public";

                var fileExtension = Path.GetExtension(file.FileName).TrimStart('.');
                var blobName = $"profiles/{userId}/profile_{DateTimeOffset.UtcNow:yyyyMMdd_HHmmss}.{fileExtension}";

                using var stream = file.OpenReadStream();
                var blobUrl = await _azureStorageService.UploadBlobAsync(containerName, blobName, stream, file.ContentType);

                // Update user profile with new image URL
                var updateDto = new UserUpdateDto
                {
                    Id = userId,
                    ProfileImageUrl = blobUrl
                };

                var updateResult = await _userService.UpdateUserAsync(updateDto);
                if (!updateResult.IsSuccess)
                    return ServiceResponseToActionResult.Error(updateResult.ErrorMessage, updateResult.ErrorStatus);

                return Ok(new
                {
                    profileImageUrl = blobUrl,
                    message = "Profile image uploaded successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload profile image for user {UserId}", _currentUserService.UserId);
                return BadRequest("Failed to upload profile image");
            }
        }

    }
}
