using System.Text.Json;

namespace SuperTaskTracking.Middleware;

public class UserIdMiddleware
{
    private readonly RequestDelegate _next;

    public UserIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var raw = context.Request.Headers["X-User-Id"].FirstOrDefault();
        
        if (!Guid.TryParse(raw, out var userId))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            var body = JsonSerializer.Serialize(new { error = "X-User-Id header is required" });
            await context.Response.WriteAsync(body);
            return;
        }

        context.Items["UserId"] = userId;
        await _next(context);
    }
}