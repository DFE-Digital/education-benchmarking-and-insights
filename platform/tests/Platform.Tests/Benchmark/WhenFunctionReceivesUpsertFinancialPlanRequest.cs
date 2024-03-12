using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain.DataObjects;
using Platform.Domain.Requests;
using Platform.Functions;
using Xunit;

namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesUpsertFinancialPlanRequest : FinancialPlanFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn201OnValidRequest()
    {
        Db
            .Setup(d => d.UpsertFinancialPlan(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FinancialPlanRequest>()))
            .ReturnsAsync(new CreatedResult<FinancialPlanDataObject>(new FinancialPlanDataObject(), ""));

        var result = await Functions.UpsertFinancialPlanAsync(CreateRequestWithBody(new FinancialPlanRequest()), "1", 2021) as CreatedResult;

        Assert.NotNull(result);
        Assert.Equal(201, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn204OnValidRequest()
    {
        Db
            .Setup(d => d.UpsertFinancialPlan(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FinancialPlanRequest>()))
            .ReturnsAsync(new UpdatedResult());

        var result = await Functions.UpsertFinancialPlanAsync(CreateRequestWithBody(new FinancialPlanRequest()), "1", 2021) as NoContentResult;

        Assert.NotNull(result);
        Assert.Equal(204, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn409OnInvalidRequest()
    {

        Db
            .Setup(d => d.UpsertFinancialPlan(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FinancialPlanRequest>()))
            .ReturnsAsync(new DataConflictResult());

        var result = await Functions.UpsertFinancialPlanAsync(CreateRequestWithBody(new FinancialPlanRequest()), "1", 2021) as ConflictObjectResult;

        Assert.NotNull(result);
        Assert.Equal(409, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Db
            .Setup(d => d.UpsertFinancialPlan(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FinancialPlanRequest>()))
            .Throws(new Exception());

        var result = await Functions.UpsertFinancialPlanAsync(CreateRequestWithBody(new FinancialPlanRequest()), "1", 2021) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}