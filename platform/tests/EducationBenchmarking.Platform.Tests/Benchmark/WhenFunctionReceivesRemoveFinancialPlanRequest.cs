using EducationBenchmarking.Platform.Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class WhenFunctionReceivesRemoveFinancialPlanRequest : FinancialPlanFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.FinancialPlan(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new FinancialPlan());

        Db.Setup(d => d.DeleteFinancialPlan(It.IsAny<FinancialPlan>()));

        var result = await Functions.RemoveFinancialPlanAsync(CreateRequest(), "1", 2021) as OkResult;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {

        Db
            .Setup(d => d.FinancialPlan(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync((FinancialPlan?)null);

        var result = await Functions.RemoveFinancialPlanAsync(CreateRequest(), "1", 2021) as NotFoundResult;

        Assert.NotNull(result);
        Assert.Equal(404, result?.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.FinancialPlan(It.IsAny<string>(), It.IsAny<int>()))
            .Throws(new Exception());

        var result = await Functions.RemoveFinancialPlanAsync(CreateRequest(), "1", 2021) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result?.StatusCode);
    }
}