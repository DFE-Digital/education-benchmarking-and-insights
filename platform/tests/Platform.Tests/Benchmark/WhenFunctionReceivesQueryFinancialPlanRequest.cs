using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesQueryFinancialPlanRequest : FinancialPlanFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.QueryFinancialPlan(It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<FinancialPlanResponseModel>());

        var result = await Functions.QueryFinancialPlanAsync(CreateRequest(), "1") as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.QueryFinancialPlan(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.QueryFinancialPlanAsync(CreateRequest(), "1") as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}