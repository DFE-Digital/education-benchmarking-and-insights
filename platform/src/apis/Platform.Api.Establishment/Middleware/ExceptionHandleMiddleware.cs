using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using Platform.Functions.Extensions;

namespace Platform.Api.Establishment.Middleware;

public class ExceptionHandleMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ILogger<ExceptionHandleMiddleware> logger)
    {
        var correlationId = context.Request.GetCorrelationId();
        using (logger.BeginScope(State(correlationId)))
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred with request: {DisplayUrl}", context.Request.GetDisplayUrl());
                
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(new
                {
                    context.Response.StatusCode,
                    Message = "Internal Server Error."
                }.ToJson());
            }
        }
    }

    private static Dictionary<string, object> State(Guid correlationId) => new()
        { { "Application", Constants.ApplicationName }, { "CorrelationID", correlationId } };
}

public static class ExceptionHandleMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandleMiddleware>();
    }
}