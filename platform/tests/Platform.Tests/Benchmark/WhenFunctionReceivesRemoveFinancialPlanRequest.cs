using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Xunit;

namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesRemoveFinancialPlanRequest : FinancialPlanFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.SingleFinancialPlanInput(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new FinancialPlanInputResponseModel());

        Db.Setup(d => d.DeleteFinancialPlan(It.IsAny<string>(), It.IsAny<int>()));

        var result = await Functions.RemoveFinancialPlanAsync(CreateRequest(), "1", 2021) as OkResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.DeleteFinancialPlan(It.IsAny<string>(), It.IsAny<int>()))
            .Throws(new Exception());

        var result = await Functions.RemoveFinancialPlanAsync(CreateRequest(), "1", 2021) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}