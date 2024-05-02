using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesSingleFinancialPlanRequest : FinancialPlanFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.SingleFinancialPlanInput(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new FinancialPlanInputResponseModel());

        var result = await Functions.SingleFinancialPlanAsync(CreateRequest(), "1", 2021) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {

        Db
            .Setup(d => d.SingleFinancialPlanInput(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync((FinancialPlanInputResponseModel?)null);

        var result = await Functions.SingleFinancialPlanAsync(CreateRequest(), "1", 2021) as NotFoundResult;

        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.SingleFinancialPlanInput(It.IsAny<string>(), It.IsAny<int>()))
            .Throws(new Exception());

        var result = await Functions.SingleFinancialPlanAsync(CreateRequest(), "1", 2021) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}