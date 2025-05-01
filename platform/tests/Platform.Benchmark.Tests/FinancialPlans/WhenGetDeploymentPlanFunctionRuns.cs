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
    private readonly Mock<IFinancialPlansService> _service;

    public WhenGetDeploymentPlanFunctionRuns()
    {
        _service = new Mock<IFinancialPlansService>();
        _function = new GetDeploymentPlanFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.DeploymentPlanAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new FinancialPlanDeployment());

        var result = await _function.RunAsync(CreateHttpRequestData(), "1", 2021);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        _service
            .Setup(d => d.DeploymentPlanAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync((FinancialPlanDeployment?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), "1", 2021);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}