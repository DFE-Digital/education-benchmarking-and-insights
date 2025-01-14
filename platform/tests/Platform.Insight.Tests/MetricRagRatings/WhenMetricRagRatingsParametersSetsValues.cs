using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.MetricRagRatings;
using Xunit;

namespace Platform.Insight.Tests.MetricRagRatings;

public class WhenMetricRagRatingsParametersSetsValues
{
    [Theory]
    [InlineData("1,2,3", "4,5,6", "7,8,9", "companyNumber", "phase", "laCode", "1|2|3", "4|5|6", "7|8|9", "companyNumber", "phase", "laCode")]
    [InlineData(null, null, null, null, null, null, "", "", "", "", "", "")]
    public void ShouldSetValuesFromIQueryCollection(
        string? statuses,
        string? categories,
        string? urns,
        string? companyNumber,
        string? phase,
        string? laCode,
        string expectedStatuses,
        string expectedCategories,
        string expectedUrns,
        string expectedCompanyNumber,
        string expectedPhase,
        string expectedLaCode)
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
            },
            {
                "companyNumber", companyNumber
            },
            {
                "phase", phase
            },
            {
                "laCode", laCode
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedStatuses, string.Join("|", parameters.Statuses));
        Assert.Equal(expectedCategories, string.Join("|", parameters.Categories));
        Assert.Equal(expectedUrns, string.Join("|", parameters.Urns));
        Assert.Equal(expectedCompanyNumber, parameters.CompanyNumber);
        Assert.Equal(expectedPhase, parameters.Phase);
        Assert.Equal(expectedLaCode, parameters.LaCode);
    }
}