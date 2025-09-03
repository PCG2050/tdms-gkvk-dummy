using Application.Interface;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using Resend;

namespace Infrastructure.Services
{
    public class ResendEmailService : IEmailService
    {
        private readonly IResend _resend;
        private readonly ResendSettings _settings;
        private readonly ILogger<ResendEmailService> _logger;


        public ResendEmailService(
            IResend resend,
            IOptions<ResendSettings> options,
            ILogger<ResendEmailService> logger)
        {
            _resend = resend;
            _settings = options.Value;
            _logger = logger;
        }

        //public async Task SendPasswordResetEmailAsync(string toEmail, string resetLink)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Attempting to send password reset email to {Email}", toEmail);

        //        ValidateConfiguration();

        //        var message = new EmailMessage
        //        {
        //            From = GetFromAddress(),
        //            Subject = "Password Reset Request",
        //            HtmlBody = $"<p>Click the link to reset your password:</p><p><a href=\"{resetLink}\">Reset Password</a></p>"
        //        };

        //        message.To.Add(toEmail);

        //        var response = await _resend.EmailSendAsync(message);
        //        _logger.LogInformation("Email sent successfully. ID: {EmailId}", response.Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Failed to send password reset email to {Email}. Error: {Error}", toEmail, ex.Message);
        //        throw new InvalidOperationException($"Failed to send email: {ex.Message}", ex);
        //    }
        //}

        public async Task SendPasswordResetOTPAsync(string toEmail, string otp, string userName = "")
        {
            try
            {
                _logger.LogInformation("Attempting to send OTP email to {Email}", toEmail);

                // Validate configuration first
                ValidateConfiguration();

                var greeting = !string.IsNullOrEmpty(userName) ? $"Hello {userName}," : "Hello,";

                var htmlBody = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <div style='background-color: #f8f9fa; padding: 20px; border-radius: 10px; border-left: 4px solid #007bff;'>
                        <h2 style='color: #333; margin-bottom: 20px;'>Password Reset Request</h2>
                        
                        <p style='color: #555; line-height: 1.6;'>{greeting}</p>
                        
                        <p style='color: #555; line-height: 1.6;'>You have requested to reset your password. Please use the following One-Time Password (OTP) to proceed:</p>
                        
                        <div style='background-color: #fff; padding: 20px; border-radius: 8px; text-align: center; margin: 20px 0; border: 2px dashed #007bff;'>
                            <h1 style='color: #007bff; font-size: 36px; letter-spacing: 8px; margin: 0; font-family: monospace;'>{otp}</h1>
                            <p style='color: #666; font-size: 14px; margin-top: 10px;'>Your 6-digit OTP</p>
                        </div>
                        
                        <div style='background-color: #fff3cd; padding: 15px; border-radius: 5px; border: 1px solid #ffeaa7; margin: 20px 0;'>
                            <p style='color: #856404; margin: 0; font-size: 14px;'>
                                <strong>⚠️ Important:</strong> This OTP is valid for <strong>10 minutes only</strong> and can be used only once.
                            </p>
                        </div>
                        
                        <p style='color: #555; line-height: 1.6; font-size: 14px;'>
                            If you did not request this password reset, please ignore this email and your password will remain unchanged.
                        </p>
                        
                        <p style='color: #dc3545; font-size: 14px; font-weight: bold;'>
                            🔒 For security reasons, never share this OTP with anyone.
                        </p>
                        
                        <hr style='border: none; border-top: 1px solid #eee; margin: 30px 0;'>
                        
                        <p style='color: #666; font-size: 12px;'>
                            Best regards,<br>
                            GKVK Support Team
                        </p>
                    </div>
                </div>";

                var message = new EmailMessage
                {
                    From = GetFromAddress(),
                    Subject = "Password Reset OTP - GKVK",
                    HtmlBody = htmlBody
                };

                message.To.Add(toEmail);

                _logger.LogDebug("Sending email from {From} to {To}", message.From, toEmail);

                var response = await _resend.EmailSendAsync(message);

                _logger.LogInformation("OTP email sent successfully. ID: {EmailId}", response.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send OTP email to {Email}. Error: {Error}", toEmail, ex.Message);
                throw new InvalidOperationException($"Failed to send email: {ex.Message}", ex);
            }
        }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(_settings.ApiKey))
            {
                throw new InvalidOperationException("Resend API key is not configured");
            }

            if (string.IsNullOrEmpty(_settings.FromEmail))
            {
                throw new InvalidOperationException("From email address is not configured");
            }

            if (!IsValidEmail(_settings.FromEmail))
            {
                throw new InvalidOperationException("From email address format is invalid");
            }

            _logger.LogDebug("Email configuration validated. From: {FromEmail}", _settings.FromEmail);
        }

        private string GetFromAddress()
        {
            return string.IsNullOrEmpty(_settings.SenderName)
                ? _settings.FromEmail
                : $"{_settings.SenderName} <{_settings.FromEmail}>";
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
}