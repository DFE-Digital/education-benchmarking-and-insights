using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.MetricRagRatings;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.MetricRagRatings;

public class WhenMetricRagRatingParametersSetsValues
{
    [Theory]
    [InlineData("true", Pipeline.RunType.Custom)]
    [InlineData("false", Pipeline.RunType.Default)]
    [InlineData(null, Pipeline.RunType.Default)]
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