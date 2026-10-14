using System.Diagnostics;

namespace PmServiceNetCode.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            var traceId = Activity.Current?.TraceId.ToString();
            var spanId = Activity.Current?.SpanId.ToString();

            var endpointName =
                context.GetEndpoint()?.DisplayName;

            var apiVersion =
                context.GetRequestedApiVersion()?.ToString();

            var clientIp =
                context.Connection.RemoteIpAddress?.ToString();

            var userAgent =
                context.Request.Headers.UserAgent.ToString();

            var userId =
                context.User.FindFirst("sub")?.Value;

            var userName =
                context.User.Identity?.Name;

            var scope = new List<KeyValuePair<string, object?>>
            {
                new("TraceId", traceId),
                new("SpanId", spanId),
                new("RequestId", context.TraceIdentifier),

                new("Method", context.Request.Method),
                new("Path", context.Request.Path.ToString()),

                new("Endpoint", endpointName),
                new("ApiVersion", apiVersion),

                new("ClientIp", clientIp),
                new("UserAgent", userAgent),

                new("UserId", userId),
                new("UserName", userName)
            };

            using (_logger.BeginScope(scope))
            {
                _logger.LogInformation(
                    "HTTP request started");

                try
                {
                    await _next(context);

                    stopwatch.Stop();

                    var statusCode = context.Response.StatusCode;
                    var elapsedMs = stopwatch.ElapsedMilliseconds;
           
                    _logger.LogInformation(
                        "HTTP request completed. " +
                        "StatusCode: {StatusCode}, " +
                        "ElapsedMs: {ElapsedMs}",
                        statusCode,
                        elapsedMs);
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();

                    _logger.LogError(
                        ex,
                        "HTTP request failed. " +
                        "StatusCode: {StatusCode}, " +
                        "ElapsedMs: {ElapsedMs}",
                        context.Response.StatusCode,
                        stopwatch.ElapsedMilliseconds);

                    throw;
                }
            }
        }
    }
}