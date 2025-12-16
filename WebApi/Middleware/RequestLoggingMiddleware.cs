using System.Diagnostics;

namespace WebApi.Middleware
{
    /// <summary>
    /// Middleware to log HTTP requests and responses with performance metrics
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip logging for health check and static files
            if (context.Request.Path.StartsWithSegments("/health") ||
                context.Request.Path.StartsWithSegments("/scalar"))
            {
                await _next(context);
                return;
            }

            var stopwatch = Stopwatch.StartNew();
            var requestId = context.TraceIdentifier;

            try
            {
                // Log incoming request
                _logger.LogInformation(
                    "[{RequestId}] HTTP {Method} {Path} started",
                    requestId,
                    context.Request.Method,
                    context.Request.Path);

                await _next(context);

                stopwatch.Stop();

                // Log completed request with timing
                var logLevel = context.Response.StatusCode >= 400 ? LogLevel.Warning : LogLevel.Information;

                _logger.Log(
                    logLevel,
                    "[{RequestId}] HTTP {Method} {Path} completed with {StatusCode} in {ElapsedMs}ms",
                    requestId,
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(
                    ex,
                    "[{RequestId}] HTTP {Method} {Path} failed after {ElapsedMs}ms",
                    requestId,
                    context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds);

                throw;
            }
        }
    }
}
