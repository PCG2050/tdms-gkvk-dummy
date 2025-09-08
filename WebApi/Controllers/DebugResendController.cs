// Controllers/DebugResendController.cs - Better debugging
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resend;
using System.Text.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous] // Remove auth for easier testing
    public class DebugResendController : ControllerBase
    {
        private readonly IResend _resend;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DebugResendController> _logger;

        public DebugResendController(IResend resend, IConfiguration configuration, ILogger<DebugResendController> logger)
        {
            _resend = resend;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("test-basic")]
        public async Task<IActionResult> TestBasic([FromBody] TestEmailRequest request)
        {
            try
            {
                _logger.LogInformation("Testing basic Resend functionality");

                // Validate input
                if (string.IsNullOrEmpty(request.ToEmail))
                {
                    return BadRequest("ToEmail is required");
                }

                // Create message step by step for debugging
                var message = new EmailMessage();

                _logger.LogInformation("Setting From address...");
                message.From = "Acme <onboarding@resend.dev>";

                _logger.LogInformation("Adding recipient: {Email}", request.ToEmail);
                message.To.Add(request.ToEmail);

                _logger.LogInformation("Setting subject...");
                message.Subject = "Test from GKVK API";

                _logger.LogInformation("Setting body...");
                message.HtmlBody = "<strong>Hello from GKVK!</strong><p>This is a test email.</p>";

                _logger.LogInformation("Message created, sending via Resend...");
                _logger.LogInformation("Message details: From={From}, To={To}, Subject={Subject}",
                    message.From, string.Join(",", message.To), message.Subject);

                var response = await _resend.EmailSendAsync(message);

                _logger.LogInformation("Email sent successfully! Response ID: {Id}", response.Success);

                return Ok(new
                {
                    success = true,
                    emailId = response.Success,
                    message = "Email sent successfully!",
                    details = new
                    {
                        from = message.From,
                        to = message.To,
                        subject = message.Subject
                    }
                });
            }
            catch (ResendException ex)
            {
                _logger.LogError(ex, "Resend API error: {Message}", ex.Message);

                return BadRequest(new
                {
                    success = false,
                    error = "Resend API Error",
                    message = ex.Message,
                    statusCode = ex.StatusCode,                   
                    type = nameof(ResendException)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error: {Message}", ex.Message);

                return BadRequest(new
                {
                    success = false,
                    error = "Unexpected Error",
                    message = ex.Message,
                    stackTrace = ex.StackTrace,
                    type = ex.GetType().Name
                });
            }
        }

        [HttpGet("validate-config")]
        public IActionResult ValidateConfig()
        {
            try
            {
                var apiKey = _configuration["ResendMail:ApiKey"];
                var fromEmail = _configuration["ResendMail:FromEmail"];
                var senderName = _configuration["ResendMail:SenderName"];

                var issues = new List<string>();

                if (string.IsNullOrEmpty(apiKey))
                    issues.Add("API Key is missing");
                else if (!apiKey.StartsWith("re_"))
                    issues.Add("API Key should start with 're_'");

                if (string.IsNullOrEmpty(fromEmail))
                    issues.Add("From Email is missing");

                return Ok(new
                {
                    valid = issues.Count == 0,
                    issues = issues,
                    config = new
                    {
                        hasApiKey = !string.IsNullOrEmpty(apiKey),
                        apiKeyPrefix = apiKey?.Substring(0, Math.Min(5, apiKey?.Length ?? 0)),
                        apiKeyLength = apiKey?.Length,
                        fromEmail = fromEmail,
                        senderName = senderName,
                        clientInitialized = _resend != null
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = "Failed to validate config",
                    message = ex.Message
                });
            }
        }

        [HttpPost("test-minimal")]
        public async Task<IActionResult> TestMinimal([FromBody] TestEmailRequest request)
        {
            try
            {
              
                var resp = await _resend.EmailSendAsync(new EmailMessage()
                {
                    From = "Acme <onboarding@resend.dev>",
                    To = { request.ToEmail },
                    Subject = "hello world",
                    HtmlBody = "<strong>it works!</strong>",
                });

                return Ok(new { success = true, id = resp.Success });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Minimal test failed");
                return BadRequest(new { error = ex.Message, type = ex.GetType().Name });
            }
        }
    }

    public class TestEmailRequest
    {
        public string ToEmail { get; set; } = string.Empty;
    }
}