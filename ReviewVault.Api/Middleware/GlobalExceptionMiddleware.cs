using System.Net;
using System.Text.Json;

namespace ReviewVault.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, response) = exception switch
            {
                UnauthorizedAccessException => (
                    (int)HttpStatusCode.Unauthorized,
                    new { error = "Unauthorized" }
                ),

                KeyNotFoundException => (
                    (int)HttpStatusCode.NotFound,
                    new { error = exception.Message }
                ),

                _ when exception.Message.Contains("not found", StringComparison.OrdinalIgnoreCase) => (
                    (int)HttpStatusCode.NotFound,
                    new { error = exception.Message }
                ),

                _ when exception.Message.Contains("already", StringComparison.OrdinalIgnoreCase) => (
                    (int)HttpStatusCode.Conflict,
                    new { error = exception.Message }
                ),

                _ when exception.Message.Contains("Invalid", StringComparison.OrdinalIgnoreCase) => (
                    (int)HttpStatusCode.BadRequest,
                    new { error = exception.Message }
                ),

                _ when exception.Message.Contains("expired", StringComparison.OrdinalIgnoreCase) => (
                    (int)HttpStatusCode.Unauthorized,
                    new { error = exception.Message }
                ),

                _ => (
                    (int)HttpStatusCode.InternalServerError,
                    new { error = "Something went wrong. Please try again later." }
                )
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
