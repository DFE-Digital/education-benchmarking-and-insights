using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Platform.Tests.Insight.Workforce;
using Xunit;

namespace Platform.Tests.Insight.Census;

public class WhenFunctionReceivesGetWorkforceHistoryRequest : CensusFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.GetHistory(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<CensusResponseModel>());

        var result = await Functions.CensusHistoryAsync(CreateRequest(), "1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.GetHistory(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.CensusHistoryAsync(CreateRequest(), "1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}