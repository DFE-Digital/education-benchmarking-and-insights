using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.MetricRagRatings;
using Platform.Functions;
using Xunit;
namespace Platform.Tests.Insight.MetricRagRatings;

public class WhenMetricRagRatingParametersSetsValues
{
    [Theory]
    [InlineData("true", PipelineRunType.Custom)]
    [InlineData("false", PipelineRunType.Default)]
    [InlineData(null, PipelineRunType.Default)]
    public void ShouldSetValuesFromIQueryCollection(string? useCustomData, string expectedDataContext)
    {
        var parameters = new MetricRagRatingParameters();
        var query = new QueryCollection(new Dictionary<string, StringValues>
        {
            {
                "useCustomData", useCustomData
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedDataContext, parameters.DataContext);
    }
}