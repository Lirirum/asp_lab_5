using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

public class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorLoggingMiddleware> _logger;

    public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
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
            context.Response.ContentType = "text/plain; charset=utf-8";
            
            _logger.LogError($"Помилка: {ex.Message}");
            File.AppendAllText("error.log", $"{DateTime.UtcNow}: {ex.Message}\n");

            
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Сталася помилка. Будь ласка, спробуйте пізніше.");
        }
    }
}

public static class ErrorLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorLoggingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorLoggingMiddleware>();
    }
}
