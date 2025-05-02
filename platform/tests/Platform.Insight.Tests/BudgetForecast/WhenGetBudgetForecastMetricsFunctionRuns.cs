using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.BudgetForecast;
using Platform.Api.Insight.Features.BudgetForecast.Models;
using Platform.Api.Insight.Features.BudgetForecast.Responses;
using Platform.Api.Insight.Features.BudgetForecast.Services;
using Platform.Domain;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.BudgetForecast;

public class WhenGetBudgetForecastMetricsFunctionRuns : FunctionsTestBase
{
    private static readonly Fixture Fixture = new();
    private readonly GetBudgetForecastMetricsFunction _function;
    private readonly Mock<IBudgetForecastService> _service = new();

    public WhenGetBudgetForecastMetricsFunctionRuns()
    {
        _function = new GetBudgetForecastMetricsFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        const string companyNumber = nameof(companyNumber);
        var results = Fixture.Build<BudgetForecastReturnMetricModel>().CreateMany().ToArray();

        _service
            .Setup(d => d.GetBudgetForecastReturnMetricsAsync(companyNumber, Pipeline.RunType.Default, It.IsAny<CancellationToken>()))
            .ReturnsAsync(results);

        var result = await _function.RunAsync(CreateHttpRequestData(), companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<BudgetForecastReturnMetricResponse[]>();
        Assert.NotNull(body);
        Assert.Equal(results.Length, body.Length);
    }
}