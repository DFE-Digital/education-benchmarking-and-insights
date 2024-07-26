using System.Net;
using Moq;
using Platform.Api.Insight.Census;
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

        var result = await Functions.CensusHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.GetHistoryAsync(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.CensusHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}