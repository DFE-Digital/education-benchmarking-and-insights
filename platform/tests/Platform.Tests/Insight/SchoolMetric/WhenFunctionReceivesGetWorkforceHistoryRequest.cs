using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight.SchoolMetric;

public class WhenFunctionReceivesGetFloorAreaRequest : SchoolMetricFunctionsTestBase
{
    private const string Urn = "URN";

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db.Setup(d => d.FloorArea(Urn)).ReturnsAsync(new FloorAreaResponseModel());

        var result = await Functions.MetricSingleAsync(CreateRequest(), Urn) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        Db.Setup(d => d.FloorArea(It.IsAny<string>())).ReturnsAsync((FloorAreaResponseModel?)null);

        var result = await Functions.MetricSingleAsync(CreateRequest(), Urn) as NotFoundResult;

        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db.Setup(d => d.FloorArea(Urn)).Throws(new Exception());

        var result = await Functions.MetricSingleAsync(CreateRequest(), Urn) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}