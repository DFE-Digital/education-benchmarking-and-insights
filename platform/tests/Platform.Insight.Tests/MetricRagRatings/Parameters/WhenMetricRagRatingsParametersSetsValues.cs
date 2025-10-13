using System.Collections.Specialized;
using Platform.Api.Insight.Features.MetricRagRatings.Parameters;
using Xunit;

namespace Platform.Insight.Tests.MetricRagRatings.Parameters;

public class WhenMetricRagRatingsParametersSetsValues
{
    [Theory]
    [InlineData("1,2,3", "4,5,6", "7,8,9", "companyNumber", "1|2|3", "4|5|6", "7|8|9", "companyNumber")]
    [InlineData(null, null, null, null, "", "", "", null)]
    public void ShouldSetValuesFromIQueryCollection(
        string? statuses,
        string? categories,
        string? urns,
        string? companyNumber,
        string expectedStatuses,
        string expectedCategories,
        string expectedUrns,
        string? expectedCompanyNumber)
    {
        var parameters = new MetricRagRatingsParameters();
        var query = new NameValueCollection
        {
            { "statuses", statuses },
            { "categories", categories },
            { "urns", urns },
            { "companyNumber", companyNumber }
        };

        parameters.SetValues(query);

        Assert.Equal(expectedStatuses, string.Join("|", parameters.Statuses));
        Assert.Equal(expectedCategories, string.Join("|", parameters.Categories));
        Assert.Equal(expectedUrns, string.Join("|", parameters.Urns));
        Assert.Equal(expectedCompanyNumber, parameters.CompanyNumber);
    }
}