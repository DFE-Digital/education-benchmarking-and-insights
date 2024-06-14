using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Api.Benchmark.FinancialPlans;
using Platform.Functions;
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

        var result = await Functions.QueryFinancialPlanAsync(CreateRequest()) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>()))
            .Throws(new Exception());

        var result = await Functions.QueryFinancialPlanAsync(CreateRequest()) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}