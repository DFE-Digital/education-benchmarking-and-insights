using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight.Census;

public class WhenFunctionReceivesQueryWorkforceRequest : CensusFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.Get(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<CensusResponseModel>());

        var result = await Functions.QueryCensusAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }


    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.Get(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.QueryCensusAsync(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}