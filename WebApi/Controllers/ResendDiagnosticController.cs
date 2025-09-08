using Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Resend;
using System.Text.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ResendDiagnosticController : ControllerBase
    {
        private readonly IResend _resend;
        private readonly ResendSettings _resendSettings;
        private readonly ILogger<ResendDiagnosticController> _logger;

        public ResendDiagnosticController(
            IResend resend,
            IOptions<ResendSettings> resendSettings,
            ILogger<ResendDiagnosticController> logger)
        {
            _resend = resend;
            _resendSettings = resendSettings.Value;
            _logger = logger;
        }

        [HttpGet("validate-settings")]
        public IActionResult ValidateSettings()
        {
            var issues = new List<string>();

            // Check API Key
            if (string.IsNullOrEmpty(_resendSettings.ApiKey))
                issues.Add("API Key is missing");
            else if (!_resendSettings.ApiKey.StartsWith("re_"))
                issues.Add("API Key should start with 're_'");

            // Check From Email
            if (string.IsNullOrEmpty(_resendSettings.FromEmail))
                issues.Add("From Email is missing");
            else if (!IsValidEmail(_resendSettings.FromEmail))
                issues.Add("From Email format is invalid");

            // Check domain
            if (!string.IsNullOrEmpty(_resendSettings.FromEmail))
            {
                var domain = _resendSettings.FromEmail.Split('@').LastOrDefault();
                if (domain == "gmail.com" || domain == "yahoo.com" || domain == "hotmail.com")
                    issues.Add("Cannot use personal email domains (gmail, yahoo, hotmail). Use your own domain.");
            }

            return Ok(new
            {
                isValid = issues.Count == 0,
                issues = issues,
                settings = new
                {
                    hasApiKey = !string.IsNullOrEmpty(_resendSettings.ApiKey),
                    apiKeyPrefix = _resendSettings.ApiKey?.Substring(0, Math.Min(5, _resendSettings.ApiKey?.Length ?? 0)),
                    fromEmail = _resendSettings.FromEmail,
                    senderName = _resendSettings.SenderName,
                    resendClientInitialized = _resend != null
                }
            });
        }

        [HttpPost("test-otp-email")]
        public async Task<IActionResult> TestOTPEmail([FromBody] TestOTPRequest request)
        {
            try
            {
                _logger.LogInformation("Testing OTP email to {Email}", request.ToEmail);

                // Validate settings first
                var validation = ValidateSettings();
                if (validation is OkObjectResult okResult &&
                    okResult.Value is { } value &&
                    value.GetType().GetProperty("isValid")?.GetValue(value) is false)
                {
                    return BadRequest(new { error = "Configuration issues found", validation = value });
                }

                // Create test OTP message
                var otp = "123456"; // Test OTP
                var message = new EmailMessage
                {
                    From = $"{_resendSettings.SenderName} <{_resendSettings.FromEmail}>",
                    Subject = "Test OTP - GKVK",
                    HtmlBody = GenerateTestOTPEmail(otp),
                    TextBody = $"Test OTP: {otp}"
                };

                message.To.Add(request.ToEmail);

                _logger.LogInformation("Sending test email with settings - From: {From}, To: {To}",
                    message.From, request.ToEmail);

                var response = await _resend.EmailSendAsync(message);

                return Ok(new
                {
                    success = response.Success,
                    emailId = response.Success,
                    message = "Test OTP email sent successfully",
                    testOTP = otp,
                    from = message.From,
                    to = request.ToEmail
                });
            }
            catch (ResendException ex)
            {
                _logger.LogError(ex, "Resend API error during test");

                var errorDetails = new
                {
                    success = false,
                    error = "Resend API Error",
                    statusCode = ex.StatusCode,
                    message = ex.Message,
                    type = nameof(ResendException),
                    suggestions = GetSuggestionsByStatusCode((int?)ex.StatusCode)
                };

                return BadRequest(errorDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during test");
                return BadRequest(new
                {
                    success = false,
                    error = "Unexpected Error",
                    message = ex.Message,
                    type = ex.GetType().Name
                });
            }
        }

        [HttpPost("test-minimal")]
        public async Task<IActionResult> TestMinimal([FromBody] TestOTPRequest request)
        {
            try
            {
                // Test with onboarding@resend.dev (always works)
                var response = await _resend.EmailSendAsync(new EmailMessage
                {
                    From = "Acme <onboarding@resend.dev>",
                    To = { request.ToEmail },
                    Subject = "Test from GKVK API",
                    HtmlBody = "<strong>This is a test email from GKVK API</strong>",
                });

                return Ok(new
                {
                    success = true,
                    emailId = response.Success,
                    message = "Minimal test successful - Resend service is working",
                    note = "This used onboarding@resend.dev. Your custom domain might have issues."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message,
                    message = "Basic Resend service is not working"
                });
            }
        }

        private string GenerateTestOTPEmail(string otp)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <title>Test OTP Email</title>
</head>
<body style='font-family: Arial, sans-serif; padding: 20px;'>
    <h2>Test OTP Email - GKVK</h2>
    <p>This is a test email to verify OTP functionality.</p>
    <div style='background: #f0f0f0; padding: 20px; border-radius: 5px; text-align: center;'>
        <h3>Test OTP: {otp}</h3>
    </div>
    <p>If you received this email, the OTP service is working correctly.</p>
</body>
</html>";
        }

        private List<string> GetSuggestionsByStatusCode(int? statusCode)
        {
            return statusCode switch
            {
                401 => new List<string> { "Check your API key", "Ensure API key starts with 're_'", "Verify API key is active" },
                403 => new List<string> { "Verify your domain in Resend dashboard", "Check domain DNS records", "Ensure domain is approved" },
                422 => new List<string> { "Check email format", "Verify from email domain", "Ensure all required fields are present" },
                429 => new List<string> { "Rate limit exceeded", "Wait before retrying", "Check your Resend plan limits" },
                _ => new List<string> { "Check Resend documentation", "Verify API configuration", "Contact Resend support" }
            };
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

    public class TestOTPRequest
    {
        public string ToEmail { get; set; } = string.Empty;
    }
}