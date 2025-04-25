using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Moq;
using Web.App.Middleware;
using Xunit;

namespace Web.Tests.Middleware;

public class WhenCanonicalHeaderMiddlewareInvokes
{
    public static TheoryData<string, string, string, Dictionary<string, StringValues>, string?> LinkResponseHeaderTestData = new()
    {
        {
            "", "https", "/", new Dictionary<string, StringValues>(), null
        },
        {
            "host.name", "https", "/", new Dictionary<string, StringValues>(), "<https://host.name/>; rel=\"canonical\""
        },
        {
            "host.name", "https", "/path", new Dictionary<string, StringValues>(), "<https://host.name/path>; rel=\"canonical\""
        },
        {
            "host.name",
            "https",
            "/path",
            new Dictionary<string, StringValues> { { "query", "value" } },
            "<https://host.name/path?query=value>; rel=\"canonical\""
        }
    };

    [Theory]
    [MemberData(nameof(LinkResponseHeaderTestData))]
    public async Task ShouldSetLinkResponseHeader(string canonicalHostName, string scheme, string path, Dictionary<string, StringValues> query, string? expected)
    {
        var options = new Mock<IOptions<MiddlewareOptions>>();
        options
            .SetupGet(o => o.Value)
            .Returns(new MiddlewareOptions { CanonicalHostName = canonicalHostName });

        var middleware = new CanonicalHeaderMiddleware(options.Object);

        var context = new DefaultHttpContext
        {
            Request =
            {
                Scheme = scheme,
                Path = path,
                Query = new QueryCollection(query)
            }
        };

        await middleware.InvokeAsync(context, _ => Task.CompletedTask);

        Assert.Equal(expected, context.Response.Headers.Link);
    }
}