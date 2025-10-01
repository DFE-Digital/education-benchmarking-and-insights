using System.Collections.Specialized;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Xunit;

namespace Platform.Insight.Tests.ItSpend.Parameters;

public class ItSpendTrustsParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "companyNumbers", "12345678" },
            { "companyNumbers", "87654321" },
            { "companyNumbers", "12348765" },
        };

        var parameters = new ItSpendTrustsParameters();
        parameters.SetValues(values);

        Assert.Equal(["12345678", "87654321", "12348765"], parameters.CompanyNumbers);
    }
}