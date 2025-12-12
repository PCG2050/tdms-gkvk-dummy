namespace Application.Interface.Services.Common
{
    /// <summary>
    /// Service for getting current time in Indian Standard Time (IST)
    /// Use this instead of DateTimeOffset.UtcNow throughout the application
    /// </summary>
    public interface IIndianTimeService
    {
        /// <summary>
        /// Gets the current date and time in Indian Standard Time (IST - UTC+5:30)
        /// </summary>
        DateTimeOffset Now { get; }

        /// <summary>
        /// Gets the current date in Indian Standard Time (IST - UTC+5:30)
        /// </summary>
        DateOnly Today { get; }

        /// <summary>
        /// Converts UTC time to Indian Standard Time
        /// </summary>
        DateTimeOffset ConvertUtcToIst(DateTimeOffset utcTime);

        /// <summary>
        /// Converts Indian Standard Time to UTC
        /// </summary>
        DateTimeOffset ConvertIstToUtc(DateTimeOffset istTime);
    }
}
