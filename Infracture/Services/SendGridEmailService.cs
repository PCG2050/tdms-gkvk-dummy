using Application.Interface;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly SendGridSettings _settings;

        public SendGridEmailService(IOptions<SendGridSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string resetLink)
        {
            var client = new SendGridClient(_settings.ApiKey);
            var from = new EmailAddress(_settings.FromEmail, _settings.SenderName);
            var to = new EmailAddress(toEmail);
            var subject = "Password Reset Request";
            var plainText = $"Click the link to reset your password: {resetLink}";
            var html = $"<p>Click the link to reset your password:</p><p><a href=\"{resetLink}\">Reset Password</a></p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainText, html);
            await client.SendEmailAsync(msg);
        }
    }
}