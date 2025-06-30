using Web.App.Attributes;

namespace Web.App.Middleware;

public class ServiceBannerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var attribute = context.GetEndpoint()?.Metadata.OfType<ServiceBannerAttribute>().FirstOrDefault();
        var target = attribute?.Target;
        if (!string.IsNullOrWhiteSpace(target))
        {
            context.Items.Add(BannerTargets.Key, target);
        }

        await next(context);
    }
}

public static class ServiceBannerMiddlewareExtensions
{
    public static IApplicationBuilder UseServiceBanner(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ServiceBannerMiddleware>();
    }
}