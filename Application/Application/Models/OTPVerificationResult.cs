using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{ 
    public class OTPVerificationResult
    {
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }
        public int RemainingAttempts { get; set; }
        public bool IsBlocked { get; set; }

        public static OTPVerificationResult Success() => new() { IsValid = true };
        public static OTPVerificationResult Failure(string error, int remaining = 0, bool blocked = false)
            => new() { IsValid = false, ErrorMessage = error, RemainingAttempts = remaining, IsBlocked = blocked };
    }
}
