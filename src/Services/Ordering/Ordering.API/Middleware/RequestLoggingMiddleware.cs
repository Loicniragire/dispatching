namespace Ordering.API.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        // Log the request here
        _logger.LogInformation($"Handling request: {context.Request.Method} {context.Request.Path}");

        await _next(context); // Call the next delegate/middleware in the pipeline

        // Log the response here
        _logger.LogInformation($"Finished handling request.");
    }
}
