using AutoFixture;
using Platform.Api.Insight.BudgetForecast;
using Xunit;
namespace Platform.Tests.Insight.BudgetForecast;

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
        Assert.Equal(model.RunType, actual.RunType);
        Assert.Equal(model.RunId, actual.RunId);
        Assert.Equal(model.Year, actual.Year);
        Assert.Equal(model.CompanyNumber, actual.CompanyNumber);
        Assert.Equal(model.Metric, actual.Metric);
        Assert.Equal(model.Value, actual.Value);
    }
}