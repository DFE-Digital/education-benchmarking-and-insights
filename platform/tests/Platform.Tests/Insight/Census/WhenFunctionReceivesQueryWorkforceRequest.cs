using System.Net;
using Moq;
using Platform.Api.Insight.Census;
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

        var result = await Functions.QueryCensusAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }


    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>()))
            .Throws(new Exception());

        var result = await Functions.QueryCensusAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}