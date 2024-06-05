using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Api.Insight.Census;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Insight.Census;

public class WhenFunctionReceivesQueryWorkforceRequest : CensusFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>()))
            .ReturnsAsync(Array.Empty<CensusModel>());

        var result = await Functions.QueryCensusAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }


    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>()))
            .Throws(new Exception());

        var result = await Functions.QueryCensusAsync(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}