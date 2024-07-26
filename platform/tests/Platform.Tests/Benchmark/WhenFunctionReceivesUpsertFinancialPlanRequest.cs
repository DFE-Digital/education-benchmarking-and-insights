using System.Net;
using Moq;
using Platform.Api.Benchmark.FinancialPlans;
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

        var result = await Functions.UpsertFinancialPlanAsync(CreateHttpRequestDataWithBody(new FinancialPlanDetails()), "1", 2021);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn204OnValidRequest()
    {
        Service
            .Setup(d => d.UpsertAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FinancialPlanDetails>()))
            .ReturnsAsync(new UpdatedResult());

        var result = await Functions.UpsertFinancialPlanAsync(CreateHttpRequestDataWithBody(new FinancialPlanDetails()), "1", 2021);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn409OnInvalidRequest()
    {

        Service
            .Setup(d => d.UpsertAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FinancialPlanDetails>()))
            .ReturnsAsync(new DataConflictResult());

        var result = await Functions.UpsertFinancialPlanAsync(CreateHttpRequestDataWithBody(new FinancialPlanDetails()), "1", 2021);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.UpsertAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FinancialPlanDetails>()))
            .Throws(new Exception());

        var result = await Functions.UpsertFinancialPlanAsync(CreateHttpRequestDataWithBody(new FinancialPlanDetails()), "1", 2021);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}