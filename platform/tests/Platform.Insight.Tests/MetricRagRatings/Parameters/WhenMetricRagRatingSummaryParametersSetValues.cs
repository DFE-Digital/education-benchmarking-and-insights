using System.Collections.Specialized;
using Platform.Api.Insight.Features.MetricRagRatings.Parameters;
using Xunit;

namespace Platform.Insight.Tests.MetricRagRatings.Parameters;

public class WhenMetricRagRatingSummaryParametersSetValues
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "companyNumber", "12345678" },
            { "urns", "123456" },
            { "urns", "653456" },
            { "laCode", "123" },
            { "overallPhase", "Primary" },
        };

        var parameters = new MetricRagRatingSummaryParameters();
        parameters.SetValues(values);

        Assert.Equal("12345678", parameters.CompanyNumber);
        Assert.Equal(["123456", "653456"], parameters.Urns);
        Assert.Equal("123", parameters.LaCode);
        Assert.Equal("Primary", parameters.OverallPhase);
    }
}