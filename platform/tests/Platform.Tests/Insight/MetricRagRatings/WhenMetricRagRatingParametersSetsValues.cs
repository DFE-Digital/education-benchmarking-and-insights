using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.MetricRagRatings;
using Xunit;
namespace Platform.Tests.Insight.MetricRagRatings;

public class WhenMetricRagRatingParametersSetsValues
{
    [Theory]
    [InlineData("mixed", "true", "mixed", "custom")]
    [InlineData("unmixed", "true", "unmixed", "custom")]
    [InlineData("mixed", "false", "mixed", "default")]
    [InlineData(null, null, "unmixed", "default")]
    public void ShouldSetValuesFromIQueryCollection(string? setType, string? useCustomData, string expectedSetType, string expectedDataContext)
    {
        var parameters = new MetricRagRatingParameters();
        var query = new QueryCollection(new Dictionary<string, StringValues>
        {
            {
                "setType", setType
            },
            {
                "useCustomData", useCustomData
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedSetType, parameters.SetType);
        Assert.Equal(expectedDataContext, parameters.DataContext);
    }
}