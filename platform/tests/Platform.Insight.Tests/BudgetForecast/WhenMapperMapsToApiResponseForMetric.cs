using AutoFixture;
using Platform.Api.Insight.Features.BudgetForecast;
using Platform.Api.Insight.Features.BudgetForecast.Models;
using Xunit;

namespace Platform.Insight.Tests.BudgetForecast;

public class WhenMapperMapsToApiResponseForMetric
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldBuildResponseModelForMetric()
    {
        // arrange
        var model = _fixture.Create<BudgetForecastReturnMetricModel>();

        // act
        var actual = Mapper.MapToApiResponse(model);

        // assert
        Assert.Equal(model.Year, actual.Year);
        Assert.Equal(model.Metric, actual.Metric);
        Assert.Equal(model.Value, actual.Value);
    }
}