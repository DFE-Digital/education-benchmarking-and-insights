using Microsoft.AspNetCore.Mvc;
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

        var result = await Functions.RemoveFinancialPlanAsync(CreateRequest(), "1", 2021) as OkResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.DeleteAsync(It.IsAny<string>(), It.IsAny<int>()))
            .Throws(new Exception());

        var result = await Functions.RemoveFinancialPlanAsync(CreateRequest(), "1", 2021) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}