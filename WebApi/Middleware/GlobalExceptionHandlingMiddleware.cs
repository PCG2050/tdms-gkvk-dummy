using System.Net;
using System.Text.Json;

namespace WebApi.Middleware
{
    /// <summary>
    /// Global exception handling middleware to catch unhandled exceptions
    /// and return consistent error responses without exposing sensitive details in production
    /// </summary>
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlingMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                ArgumentException => HttpStatusCode.BadRequest,
                InvalidOperationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            var response = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = GetErrorMessage(exception, statusCode),
                // Only include details in development environment
                Details = _environment.IsDevelopment() ? exception.ToString() : null,
                Timestamp = DateTime.UtcNow
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }

        private string GetErrorMessage(Exception exception, HttpStatusCode statusCode)
        {
            // In production, use generic messages to avoid information disclosure
            if (!_environment.IsDevelopment())
            {
                return statusCode switch
                {
                    HttpStatusCode.Unauthorized => "You are not authorized to access this resource.",
                    HttpStatusCode.NotFound => "The requested resource was not found.",
                    HttpStatusCode.BadRequest => "The request was invalid.",
                    _ => "An error occurred while processing your request."
                };
            }

            // In development, show actual exception message
            return exception.Message;
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
