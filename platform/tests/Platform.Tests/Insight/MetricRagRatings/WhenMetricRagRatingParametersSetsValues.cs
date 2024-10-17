using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.MetricRagRatings;
using Xunit;
namespace Platform.Tests.Insight.MetricRagRatings;

public class WhenMetricRagRatingParametersSetsValues
{
    [Theory]
    [InlineData("true", "custom")]
    [InlineData("false", "default")]
    [InlineData(null, "default")]
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