using Leaguelane.Models.Dtos;
using System;
using System.Net;
using System.Text.Json;

namespace Leaguelane.ApiService.Middlewears
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
                _logger.LogError(ex, "An unhandled exception occurred");
                var code = HttpStatusCode.InternalServerError;
                var result = string.Empty;

                switch (ex)
                {
                    case ArgumentNullException:
                    case ArgumentException:
                        code = HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException:
                    case FileNotFoundException:
                        code = HttpStatusCode.NotFound;
                        break;
                    case UnauthorizedAccessException:
                        code = HttpStatusCode.Unauthorized;
                        break;
                    case InvalidOperationException:
                        code = HttpStatusCode.Conflict;
                        break;
                }

                var errorResponse = new BaseResponse(false, ex.Message, null);

                result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;

                await context.Response.WriteAsync(result);
            }
        }
    }
}
