using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OTPService :IOTPService
    {
        public string GenerateOTP()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            // Convert to int and ensure it's positive
            var number = Math.Abs(BitConverter.ToInt32(bytes, 0));

            // Get 6-digit number (100000 to 999999)
            var otp = (number % 900000) + 100000;

            return otp.ToString();
        }

        public bool ValidateOTP(string providedOTP, string storedOTP, DateTimeOffset? expiryTime)
        {
            if (string.IsNullOrEmpty(providedOTP) || string.IsNullOrEmpty(storedOTP))
                return false;

            if (IsOTPExpired(expiryTime))
                return false;

            return string.Equals(providedOTP.Trim(), storedOTP.Trim(), StringComparison.Ordinal);
        }

        public bool IsOTPExpired(DateTimeOffset? expiryTime)
        {
            return !expiryTime.HasValue || DateTimeOffset.UtcNow > expiryTime.Value;
        }
    }
}
