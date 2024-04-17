using System.Security.Cryptography;
using System.Text;

namespace Web.App.Middleware;

public class CustomResponseHeadersMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

    public async Task InvokeAsync(HttpContext context)
    {
        var bytes = RandomNumberGenerator.GetBytes(12);
        context.Items["csp-nonce"] = Convert.ToBase64String(bytes);

        var csp = new StringBuilder();
        csp.Append("default-src 'self';");
        csp.Append("img-src 'self';");
        csp.Append("style-src 'self' 'unsafe-inline';");
        csp.Append($"script-src 'self' 'nonce-{context.Items["csp-nonce"]}' https://js.monitor.azure.com/scripts/b/ai.3.gbl.min.js https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js;");
        csp.Append("object-src 'none';");
        csp.Append("worker-src 'none';");
        csp.Append("frame-ancestors 'self';");
        csp.Append("form-action 'self' https://*.signin.education.gov.uk;");
        csp.Append("connect-src dc.services.visualstudio.com 'self';");

        context.Response.Headers.ContentSecurityPolicy = csp.ToString();
        context.Response.Headers.XFrameOptions = "SAMEORIGIN";
        context.Response.Headers.XContentTypeOptions = "nosniff";
        context.Response.Headers.XXSSProtection = "0";

        await _next(context);
    }
}