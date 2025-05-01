using System.Net;
using Moq;
using Platform.Api.Benchmark.Features.FinancialPlans;
using Platform.Api.Benchmark.Features.FinancialPlans.Models;
using Platform.Api.Benchmark.Features.FinancialPlans.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.FinancialPlans;

public class WhenGetDeploymentPlanFunctionRuns : FunctionsTestBase
{
    private readonly GetDeploymentPlanFunction _function;
    private readonly Mock<IFinancialPlansService> _service = new();

    public WhenGetDeploymentPlanFunctionRuns()
    {
        _function = new GetDeploymentPlanFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        const string urn = nameof(urn);
        const int year = 2021;

        _service
            .Setup(d => d.DeploymentPlanAsync(urn, year))
            .ReturnsAsync(new FinancialPlanDeployment());

        var result = await _function.RunAsync(CreateHttpRequestData(), urn, year);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        const string urn = nameof(urn);
        const int year = 2021;

        _service
            .Setup(d => d.DeploymentPlanAsync(urn, year))
            .ReturnsAsync((FinancialPlanDeployment?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), urn, year);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}