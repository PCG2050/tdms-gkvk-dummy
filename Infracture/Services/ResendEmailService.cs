using Application.Interface;
using Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Resend;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ResendEmailService : IEmailService
    {
        private readonly IResend _resend;
        private readonly ResendSettings _resendSettings;
        private readonly ILogger<ResendEmailService> _logger;

        public ResendEmailService(
            IResend resend,
            IOptions<ResendSettings> resendSettings,
            ILogger<ResendEmailService> logger)
        {
            _resend = resend;
            _resendSettings = resendSettings.Value;
            _logger = logger;
        }

        public async Task SendPasswordResetOTPAsync(string toEmail, string otp, string userName = "")
        {
            try
            {
                _logger.LogInformation("Attempting to send OTP email to {Email}", toEmail);

                // Validate configuration
                if (string.IsNullOrEmpty(_resendSettings.ApiKey))
                {
                    _logger.LogError("Resend API Key is not configured");
                    throw new InvalidOperationException("Email service is not properly configured");
                }

                if (string.IsNullOrEmpty(_resendSettings.FromEmail))
                {
                    _logger.LogError("From email is not configured");
                    throw new InvalidOperationException("From email is not configured");
                }

                // Create email message
                var message = new EmailMessage
                {
                    From = $"{_resendSettings.SenderName} <{_resendSettings.FromEmail}>",
                    Subject = "Password Reset OTP - GKVK",
                    HtmlBody = GenerateOTPEmailBody(otp, userName),
                    TextBody = $"Your password reset OTP is: {otp}. This OTP will expire in 10 minutes."
                };

                // Add recipient
                message.To.Add(toEmail);

                _logger.LogInformation("Sending email from {From} to {To} with subject: {Subject}",
                    message.From, toEmail, message.Subject);

                // Send email
                var response = await _resend.EmailSendAsync(message);

                if (response.Success)
                {
                    _logger.LogInformation("Successfully sent OTP email to {Email}. Email ID: {EmailId}",
                        toEmail, response.Success);
                }
                else
                {
                    _logger.LogError("Failed to send email. Response: {Response}", response);
                    throw new InvalidOperationException("Failed to send email");
                }
            }
            catch (ResendException ex)
            {
                _logger.LogError(ex, "Resend API error when sending email to {Email}. StatusCode: {StatusCode}, Message: {Message}",
                    toEmail, ex.StatusCode, ex.Message);

               
                if (ex.StatusCode == (HttpStatusCode?)422)
                {
                    _logger.LogError("Validation error (422) - check email format and configuration");
                }
                else if (ex.StatusCode == (HttpStatusCode?)401)
                {
                    _logger.LogError("Authentication error (401) - check API key");
                }
                else if (ex.StatusCode == (HttpStatusCode?)403)
                {
                    _logger.LogError("Permission error (403) - check domain verification");
                }

                throw new InvalidOperationException($"Failed to send email: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when sending OTP email to {Email}", toEmail);
                throw new InvalidOperationException("Failed to send email due to unexpected error", ex);
            }
        }

        private string GenerateOTPEmailBody(string otp, string userName)
        {
            var displayName = string.IsNullOrEmpty(userName) ? "User" : userName;

            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Password Reset OTP</title>
</head>
<body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px;'>
    <div style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); padding: 30px; text-align: center; border-radius: 10px 10px 0 0;'>
        <h1 style='color: white; margin: 0; font-size: 28px;'>GKVK Password Reset</h1>
    </div>
    
    <div style='background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; border: 1px solid #ddd;'>
        <h2 style='color: #333; margin-top: 0;'>Hello {displayName},</h2>
        
        <p style='font-size: 16px; margin-bottom: 25px;'>
            We received a request to reset your password. Use the OTP below to reset your password:
        </p>
        
        <div style='background: white; border: 2px dashed #667eea; border-radius: 10px; padding: 25px; text-align: center; margin: 25px 0;'>
            <p style='margin: 0; font-size: 14px; color: #666; text-transform: uppercase; letter-spacing: 1px;'>Your OTP Code</p>
            <h1 style='margin: 10px 0; font-size: 36px; color: #667eea; letter-spacing: 8px; font-weight: bold;'>{otp}</h1>
        </div>
        
        <div style='background: #fff3cd; border: 1px solid #ffeaa7; border-radius: 5px; padding: 15px; margin: 20px 0;'>
            <p style='margin: 0; color: #856404; font-size: 14px;'>
                <strong>⚠️ Important:</strong> This OTP will expire in 10 minutes and can only be used once.
            </p>
        </div>
        
        <p style='font-size: 14px; color: #666; margin-top: 30px;'>
            If you didn't request this password reset, please ignore this email or contact support if you have concerns.
        </p>
        
        <hr style='border: none; border-top: 1px solid #eee; margin: 30px 0;'>
        
        <p style='font-size: 12px; color: #888; text-align: center; margin: 0;'>
            This is an automated email from GKVK System. Please do not reply to this email.
        </p>
    </div>
</body>
</html>";
        }
    }
}