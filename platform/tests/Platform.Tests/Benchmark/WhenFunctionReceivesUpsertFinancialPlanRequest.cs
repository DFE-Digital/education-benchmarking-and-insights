using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Api.Benchmark.FinancialPlans;
using Platform.Domain;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesUpsertFinancialPlanRequest : FinancialPlansFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn201OnValidRequest()
    {
        Service
            .Setup(d => d.UpsertAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FinancialPlanDetails>()))
            .ReturnsAsync(new CreatedResult<FinancialPlanDetails>(new FinancialPlanDetails(), ""));

        var result = await Functions.UpsertFinancialPlanAsync(CreateRequestWithBody(new FinancialPlanDetails()), "1", 2021) as CreatedResult;

        Assert.NotNull(result);
        Assert.Equal(201, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn204OnValidRequest()
    {
        Service
            .Setup(d => d.UpsertAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FinancialPlanDetails>()))
            .ReturnsAsync(new UpdatedResult());

        var result = await Functions.UpsertFinancialPlanAsync(CreateRequestWithBody(new FinancialPlanDetails()), "1", 2021) as NoContentResult;

        Assert.NotNull(result);
        Assert.Equal(204, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn409OnInvalidRequest()
    {

        Service
            .Setup(d => d.UpsertAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FinancialPlanDetails>()))
            .ReturnsAsync(new DataConflictResult());

        var result = await Functions.UpsertFinancialPlanAsync(CreateRequestWithBody(new FinancialPlanDetails()), "1", 2021) as ConflictObjectResult;

        Assert.NotNull(result);
        Assert.Equal(409, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.UpsertAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FinancialPlanDetails>()))
            .Throws(new Exception());

        var result = await Functions.UpsertFinancialPlanAsync(CreateRequestWithBody(new FinancialPlanDetails()), "1", 2021) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}