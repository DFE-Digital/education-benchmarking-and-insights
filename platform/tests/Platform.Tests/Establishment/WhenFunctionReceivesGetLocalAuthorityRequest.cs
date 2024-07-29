using System.Net;
using Moq;
using Platform.Api.Establishment.LocalAuthorities;
using Xunit;
namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesGetLocalAuthorityRequest : LocalAuthoritiesFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new LocalAuthority());

        var result = await Functions.SingleLocalAuthorityAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        Service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync((LocalAuthority?)null);

        var result = await Functions.SingleLocalAuthorityAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.SingleLocalAuthorityAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}