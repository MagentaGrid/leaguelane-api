using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using System.Diagnostics;
using System.Text;

namespace Leaguelane.ApiService.Middlewears;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    
    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, LoggingDbContext dbContext)
    {
        // Skip logging for certain paths (e.g. swagger, health checks) if needed
        if (context.Request.Path.StartsWithSegments("/swagger") || 
            context.Request.Path.StartsWithSegments("/health"))
        {
            await _next(context);
            return;
        }

        var stopwatch = Stopwatch.StartNew();
        var requestTime = DateTime.UtcNow;
        
        // Read Request
        var requestBody = await ReadRequestBody(context.Request);
        
        // Capture Response
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            // Read Response
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();

            // SANITIZATION: PostgreSQL cannot store the null character \0 (0x00) in text columns.
            // We strip it from both the request and response strings.
            var sanitizedRequest = requestBody?.Replace("\0", string.Empty);
            var sanitizedResponse = responseBodyText?.Replace("\0", string.Empty);

            // Log to DB
            var auditLog = new AuditLog
            {
                Timestamp = requestTime,
                Method = context.Request.Method,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                RequestBody = sanitizedRequest, // Use sanitized version
                ResponseBody = sanitizedResponse, // Use sanitized version
                StatusCode = context.Response.StatusCode,
                DurationMs = stopwatch.ElapsedMilliseconds,
                User = context.User?.Identity?.Name,
                IpAddress = context.Connection.RemoteIpAddress?.ToString()
            };

            dbContext.AuditLogs.Add(auditLog);
            await dbContext.SaveChangesAsync();

            // Reset and Copy back to original stream
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    private async Task<string?> ReadRequestBody(HttpRequest request)
    {
        if (request.ContentLength == null || request.ContentLength == 0)
        {
            return null;
        }

        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true);
        var body = await reader.ReadToEndAsync();
        
        // Reset the request body stream position so the next middleware can read it
        request.Body.Position = 0;

        return body;
    }
}
