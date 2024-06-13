using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Api.Insight.Census;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight.Census;

public class WhenFunctionReceivesGetWorkforceHistoryRequest : CensusFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.GetHistoryAsync(It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<CensusHistoryModel>());

        var result = await Functions.CensusHistoryAsync(CreateRequest(), "1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.GetHistoryAsync(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.CensusHistoryAsync(CreateRequest(), "1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}