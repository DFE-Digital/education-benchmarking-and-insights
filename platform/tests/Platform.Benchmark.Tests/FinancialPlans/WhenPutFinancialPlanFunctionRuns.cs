using System.Net;
using Moq;
using Platform.Api.Benchmark.Features.FinancialPlans;
using Platform.Api.Benchmark.Features.FinancialPlans.Models;
using Platform.Api.Benchmark.Features.FinancialPlans.Services;
using Platform.Functions;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.FinancialPlans;

public class WhenPutFinancialPlanFunctionRuns : FunctionsTestBase
{
    private readonly PutFinancialPlanFunction _function;
    private readonly Mock<IFinancialPlansService> _service = new();

    public WhenPutFinancialPlanFunctionRuns()
    {
        _function = new PutFinancialPlanFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn201OnValidRequest()
    {
        const string urn = nameof(urn);
        const int year = 2021;
        var plan = new FinancialPlanDetails();

        _service
            .Setup(d => d.UpsertAsync(urn, year, It.IsAny<FinancialPlanDetails>()))
            .ReturnsAsync(new CreatedResult<FinancialPlanDetails>(new FinancialPlanDetails(), ""));

        var result = await _function.RunAsync(CreateHttpRequestDataWithBody(plan), urn, year);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn204OnValidRequest()
    {
        const string urn = nameof(urn);
        const int year = 2021;
        var plan = new FinancialPlanDetails();

        _service
            .Setup(d => d.UpsertAsync(urn, year, It.IsAny<FinancialPlanDetails>()))
            .ReturnsAsync(new UpdatedResult());

        var result = await _function.RunAsync(CreateHttpRequestDataWithBody(plan), urn, year);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn409OnInvalidRequest()
    {
        const string urn = nameof(urn);
        const int year = 2021;
        var plan = new FinancialPlanDetails();

        _service
            .Setup(d => d.UpsertAsync(urn, year, It.IsAny<FinancialPlanDetails>()))
            .ReturnsAsync(new DataConflictResult());

        var result = await _function.RunAsync(CreateHttpRequestDataWithBody(plan), urn, year);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
    }
}