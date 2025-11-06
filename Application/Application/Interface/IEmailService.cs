using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public  interface IEmailService
    {
        //Task SendPasswordResetEmailAsync(string toEmail, string resetLink);
        Task SendPasswordResetOTPAsync(string toEmail, string otp, string userName = ""); 

    }
}
