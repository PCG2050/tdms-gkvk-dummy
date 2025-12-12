using Application.Interface.Services.Common;

namespace Infrastructure.Services
{
    /// <summary>
    /// Service for getting current time in Indian Standard Time (IST - UTC+5:30)
    /// </summary>
    public class IndianTimeService : IIndianTimeService
    {
        private static readonly TimeZoneInfo IndianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        /// <summary>
        /// Gets the current date and time in Indian Standard Time (IST - UTC+5:30)
        /// </summary>
        public DateTimeOffset Now
        {
            get
            {
                var utcNow = DateTimeOffset.UtcNow;
                return TimeZoneInfo.ConvertTime(utcNow, IndianTimeZone);
            }
        }

        /// <summary>
        /// Gets the current date in Indian Standard Time (IST - UTC+5:30)
        /// </summary>
        public DateOnly Today
        {
            get
            {
                var istNow = Now;
                return DateOnly.FromDateTime(istNow.DateTime);
            }
        }

        /// <summary>
        /// Converts UTC time to Indian Standard Time
        /// </summary>
        public DateTimeOffset ConvertUtcToIst(DateTimeOffset utcTime)
        {
            return TimeZoneInfo.ConvertTime(utcTime, IndianTimeZone);
        }

        /// <summary>
        /// Converts Indian Standard Time to UTC
        /// </summary>
        public DateTimeOffset ConvertIstToUtc(DateTimeOffset istTime)
        {
            return TimeZoneInfo.ConvertTime(istTime, TimeZoneInfo.Utc);
        }
    }
}
