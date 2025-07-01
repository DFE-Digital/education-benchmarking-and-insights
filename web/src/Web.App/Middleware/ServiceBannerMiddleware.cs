using System.Diagnostics.CodeAnalysis;
using Web.App.Attributes;

namespace Web.App.Middleware;

public class ServiceBannerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

    public async Task InvokeAsync(HttpContext context)
    {
        var attribute = context.GetEndpoint()?.Metadata.OfType<ServiceBannerAttribute>().FirstOrDefault();
        var target = attribute?.Target;
        if (!string.IsNullOrWhiteSpace(target))
        {
            context.Items.Add(BannerTargets.Key, target);
        }

        await _next(context);
    }
}

[ExcludeFromCodeCoverage]
public static class ServiceBannerMiddlewareExtensions
{
    public static IApplicationBuilder UseServiceBanner(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ServiceBannerMiddleware>();
    }
}