using System.Net;
using Moq;
using Platform.Api.Benchmark.Features.FinancialPlans;
using Platform.Api.Benchmark.Features.FinancialPlans.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.FinancialPlans;

public class WhenGetFinancialPlansRuns : FunctionsTestBase
{
    private readonly GetFinancialPlansFunction _function;
    private readonly Mock<IFinancialPlansService> _service;

    public WhenGetFinancialPlansRuns()
    {
        _service = new Mock<IFinancialPlansService>();
        _function = new GetFinancialPlansFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>()))
            .ReturnsAsync([]);

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}