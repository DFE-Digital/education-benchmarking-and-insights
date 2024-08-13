using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.MetricRagRatings;
using Xunit;
namespace Platform.Tests.Insight.MetricRagRatings;

public class WhenMetricRagRatingsParametersSetsValues
{
    [Theory]
    [InlineData("1,2,3", "4,5,6", "7,8,9", "1|2|3", "4|5|6", "7|8|9")]
    [InlineData(null, null, null, "", "", "")]
    public void ShouldSetValuesFromIQueryCollection(string? statuses, string? categories, string? urns, string expectedStatuses, string expectedCategories, string expectedSchools)
    {
        var parameters = new MetricRagRatingsParameters();
        var query = new QueryCollection(new Dictionary<string, StringValues>
        {
            {
                "statuses", statuses
            },
            {
                "categories", categories
            },
            {
                "urns", urns
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedStatuses, string.Join("|", parameters.Statuses));
        Assert.Equal(expectedCategories, string.Join("|", parameters.Categories));
        Assert.Equal(expectedSchools, string.Join("|", parameters.Schools));
    }
}