using System.Collections.Specialized;
using Platform.Api.Insight.Features.MetricRagRatings.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.MetricRagRatings.Parameters;

public class WhenMetricRagRatingParametersSetsValues
{
    [Theory]
    [InlineData("true", Pipeline.RunType.Custom)]
    [InlineData("false", Pipeline.RunType.Default)]
    [InlineData(null, Pipeline.RunType.Default)]
    public void ShouldSetValuesFromIQueryCollection(string? useCustomData, string expectedDataContext)
    {
        var parameters = new MetricRagRatingParameters();
        var query = new NameValueCollection
        {
            { "useCustomData", useCustomData }
        };

        parameters.SetValues(query);

        Assert.Equal(expectedDataContext, parameters.DataContext);
    }
}