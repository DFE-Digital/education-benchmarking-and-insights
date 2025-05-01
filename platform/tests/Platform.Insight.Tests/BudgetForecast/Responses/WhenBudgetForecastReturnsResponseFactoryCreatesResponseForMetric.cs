using AutoFixture;
using Platform.Api.Insight.Features.BudgetForecast.Models;
using Platform.Api.Insight.Features.BudgetForecast.Responses;
using Xunit;

namespace Platform.Insight.Tests.BudgetForecast.Responses;

public class WhenBudgetForecastReturnsResponseFactoryCreatesResponseForMetric
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldBuildResponseModelForMetric()
    {
        // arrange
        var model = _fixture.Create<BudgetForecastReturnMetricModel>();

        // act
        var actual = BudgetForecastReturnsResponseFactory.Create(model);

        // assert
        Assert.Equal(model.Year, actual.Year);
        Assert.Equal(model.Metric, actual.Metric);
        Assert.Equal(model.Value, actual.Value);
    }
}