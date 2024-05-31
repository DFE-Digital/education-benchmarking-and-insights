using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Functions;
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

        var result = await Functions.SingleLocalAuthorityAsync(CreateRequest(), "1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        Service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync((LocalAuthority?)null);

        var result = await Functions.SingleLocalAuthorityAsync(CreateRequest(), "1") as NotFoundResult;

        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.SingleLocalAuthorityAsync(CreateRequest(), "1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}