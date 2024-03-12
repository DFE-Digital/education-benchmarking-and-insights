using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain.Responses;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight;

public class WhenFunctionReceivesGetAcademyRequest : AcademyFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.Get(It.IsAny<string>()))
            .ReturnsAsync(new Finances());

        var result = await Functions.SingleAcademyAsync(CreateRequest(), "1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {

        Db
            .Setup(d => d.Get(It.IsAny<string>()))
            .ReturnsAsync((Finances?)null);

        var result = await Functions.SingleAcademyAsync(CreateRequest(), "1") as NotFoundResult;

        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.Get(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.SingleAcademyAsync(CreateRequest(), "1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}