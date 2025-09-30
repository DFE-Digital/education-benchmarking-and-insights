using System.Collections.Specialized;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Xunit;

namespace Platform.Insight.Tests.ItSpend.Parameters;

public class ItSpendTrustForecastParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "companyNumber", "12345678" },
            { "year", "2022" }
        };

        var parameters = new ItSpendTrustForecastParameters();
        parameters.SetValues(values);

        Assert.Equal("12345678", parameters.CompanyNumber);
        Assert.Equal("2022", parameters.Year);
    }
}