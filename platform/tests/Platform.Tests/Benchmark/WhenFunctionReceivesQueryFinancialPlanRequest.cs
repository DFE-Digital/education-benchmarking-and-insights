using System.Net;
using Moq;
using Platform.Api.Benchmark.FinancialPlans;
using Xunit;
namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesQueryFinancialPlanRequest : FinancialPlansFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>()))
            .ReturnsAsync(Array.Empty<FinancialPlanSummary>());

        var result = await Functions.QueryFinancialPlanAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>()))
            .Throws(new Exception());

        var result = await Functions.QueryFinancialPlanAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}