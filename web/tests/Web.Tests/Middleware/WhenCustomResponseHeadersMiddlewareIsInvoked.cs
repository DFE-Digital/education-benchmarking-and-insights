using Microsoft.AspNetCore.Http;
using Moq;
using Web.App.Middleware;
using Xunit;

namespace Web.Tests.Middleware;

public class WhenCustomResponseHeadersMiddlewareIsInvoked
{
    private readonly Mock<RequestDelegate> _next;
    private readonly DefaultHttpContext _context;
    private readonly CustomResponseHeadersMiddleware _middleware;

    public WhenCustomResponseHeadersMiddlewareIsInvoked()
    {
        _next = new Mock<RequestDelegate>();
        _middleware = new CustomResponseHeadersMiddleware(_next.Object);
        _context = new DefaultHttpContext();
    }

    [Fact]
    public async Task ShouldSetNonceItem()
    {
        await _middleware.InvokeAsync(_context);

        var hasValue = _context.Items.TryGetValue("csp-nonce", out var nonce);

        Assert.True(hasValue);
        Assert.NotNull(nonce);
    }

    [Fact]
    public async Task ShouldSetContentSecurityPolicyResponseHeader()
    {
        await _middleware.InvokeAsync(_context);

        string[] expected =
        [
            "default-src 'self'",
            "img-src 'self' data:",
            "style-src 'self'",
            $"script-src 'self' 'nonce-{_context.Items["csp-nonce"]}' https://js.monitor.azure.com/scripts/b/ai.3.gbl.min.js https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js",
            "object-src 'none'",
            "worker-src 'none'",
            "frame-ancestors 'self'",
            "form-action 'self' https://*.signin.education.gov.uk",
            "connect-src dc.services.visualstudio.com *.in.applicationinsights.azure.com js.monitor.azure.com 'self'"
        ];
        var actual = _context.Response.Headers.ContentSecurityPolicy.ToString().Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ShouldSetXFrameOptionsResponseHeader()
    {
        await _middleware.InvokeAsync(_context);

        Assert.Equal("SAMEORIGIN", _context.Response.Headers.XFrameOptions);
    }

    [Fact]
    public async Task ShouldSetXContentTypeOptionsResponseHeader()
    {
        await _middleware.InvokeAsync(_context);

        Assert.Equal("nosniff", _context.Response.Headers.XContentTypeOptions);
    }

    [Fact]
    public async Task ShouldSetXxssProtectionResponseHeader()
    {
        await _middleware.InvokeAsync(_context);

        Assert.Equal("0", _context.Response.Headers.XXSSProtection);
    }

    [Fact]
    public async Task ShouldCallNextRequestDelegate()
    {
        await _middleware.InvokeAsync(_context);

        _next.Verify(n => n.Invoke(_context));
    }
}