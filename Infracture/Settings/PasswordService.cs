using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Settings
{
    public class PasswordService
    {
        private readonly EmailSettings _emailSettings;

        public PasswordService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public string GenerateResetLink(string token)
        {
            return $"{_emailSettings.ResetPasswordUrlBase}{Uri.EscapeDataString(token)}";
        }
    }
}
