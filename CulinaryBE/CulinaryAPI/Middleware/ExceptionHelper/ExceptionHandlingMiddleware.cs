using BusinessObject.Models;
using System.Net;
using System.Text.Json;

namespace CulinaryAPI.Middleware.ExceptionHelper
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

            var (statusCode, message, error) = exception switch
            {
                NotFoundException => (HttpStatusCode.NotFound, exception.Message, "ERR_NOT_FOUND"),
                ValidationException => (HttpStatusCode.BadRequest, exception.Message, "ERR_VALIDATION"),
                DatabaseException => (HttpStatusCode.InternalServerError, "Database error occurred", "ERR_DATABASE"),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred", "ERR_UNEXPECTED")
            };

            var apiResponse = new ApiResponse
            {
                IsSuccess = false,
                Message = message,
                Error = $"{error}: {message}",
                Result = null
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var json = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
