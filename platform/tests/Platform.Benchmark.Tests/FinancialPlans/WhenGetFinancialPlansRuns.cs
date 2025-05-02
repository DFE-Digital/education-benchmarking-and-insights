using System.Net;
using AutoFixture;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Benchmark.Features.FinancialPlans;
using Platform.Api.Benchmark.Features.FinancialPlans.Models;
using Platform.Api.Benchmark.Features.FinancialPlans.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.FinancialPlans;

public class WhenGetFinancialPlansRuns : FunctionsTestBase
{
    private readonly Fixture _fixture = new();
    private readonly GetFinancialPlansFunction _function;
    private readonly Mock<IFinancialPlansService> _service = new();

    public WhenGetFinancialPlansRuns()
    {
        _function = new GetFinancialPlansFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        string[] urns = ["urn1", "urn2", "urn3"];
        var query = new Dictionary<string, StringValues>
        {
            { "urns", urns }
        };

        var data = _fixture.Build<FinancialPlanSummary>().CreateMany();

        _service
            .Setup(d => d.QueryAsync(urns, It.IsAny<CancellationToken>()))
            .ReturnsAsync(data);

        var result = await _function.RunAsync(CreateHttpRequestData(query));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}