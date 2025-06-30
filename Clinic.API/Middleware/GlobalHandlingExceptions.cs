using System.Net;
using System.Text.Json;

namespace Clinic.API.Middleware
{
    public class GlobalHandlingExceptions
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalHandlingExceptions> _logger;

        public GlobalHandlingExceptions(RequestDelegate next, ILogger<GlobalHandlingExceptions> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var problemDetails = new
                {
                    type = "Holded Error",
                    title = "Internal Server Error",
                    status = StatusCodes.Status500InternalServerError,
                    detail = ex.Message,
                    instance = context.Request.Path.ToString()
                };

                var json = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
