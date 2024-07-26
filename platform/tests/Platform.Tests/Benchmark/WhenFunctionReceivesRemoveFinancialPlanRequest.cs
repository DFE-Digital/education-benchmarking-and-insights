using System.Net;
using Moq;
using Platform.Api.Benchmark.FinancialPlans;
using Xunit;
namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesRemoveFinancialPlanRequest : FinancialPlansFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.DetailsAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new FinancialPlanDetails());

        Service.Setup(d => d.DeleteAsync(It.IsAny<string>(), It.IsAny<int>()));

        var result = await Functions.RemoveFinancialPlanAsync(CreateHttpRequestData(), "1", 2021);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.DeleteAsync(It.IsAny<string>(), It.IsAny<int>()))
            .Throws(new Exception());

        var result = await Functions.RemoveFinancialPlanAsync(CreateHttpRequestData(), "1", 2021);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}