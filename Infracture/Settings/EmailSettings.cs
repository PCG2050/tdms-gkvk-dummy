namespace Infrastructure.Settings
{
    public class EmailSettings
    {
        public int OTPExpiryMinutes { get; set; }
        public int MaxOTPAttempts { get; set; }

        public int OTPResendDelayMinutes { get; set; }
   

    }
}
