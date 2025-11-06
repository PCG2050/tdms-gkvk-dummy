using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IOTPService
    {
        string GenerateOTP();
        bool ValidateOTP(string providedOTP, string storedOTP, DateTimeOffset? expiryTime);
        bool IsOTPExpired(DateTimeOffset? expiryTime);
    }
}
