using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Api.Establishment.Schools;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesGetSchoolRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        SchoolService
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new School());

        var result = await Functions.SingleSchoolAsync(CreateRequest(), "1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {

        SchoolService
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync((School?)null);

        var result = await Functions.SingleSchoolAsync(CreateRequest(), "1") as NotFoundResult;

        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        SchoolService
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.SingleSchoolAsync(CreateRequest(), "1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}