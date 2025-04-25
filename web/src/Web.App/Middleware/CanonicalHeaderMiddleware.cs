using Microsoft.Extensions.Options;

namespace Web.App.Middleware;

public class CanonicalHeaderMiddleware(IOptions<MiddlewareOptions> options) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var canonicalHostName = options.Value.CanonicalHostName;
        if (!string.IsNullOrWhiteSpace(canonicalHostName))
        {
            var scheme = context.Request.Scheme;
            var path = context.Request.Path;
            var query = QueryString.Create(context.Request.Query).ToString();

            // https://developers.google.com/search/docs/crawling-indexing/consolidate-duplicate-urls#rel-canonical-header-method
            context.Response.Headers.Link = $"<{scheme}://{canonicalHostName}{path}{query}>; rel=\"canonical\"";
        }

        await next(context);
    }
}