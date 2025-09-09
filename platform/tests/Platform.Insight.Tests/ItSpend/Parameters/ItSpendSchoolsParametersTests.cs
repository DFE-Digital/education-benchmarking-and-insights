using System.Collections.Specialized;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.ItSpend.Parameters;

public class ItSpendSchoolsParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "urns", "123456" },
            { "urns", "234567" },
            { "urns", "345678" },
            { "dimension", "dimension" }
        };

        var parameters = new ItSpendSchoolsParameters();
        parameters.SetValues(values);

        Assert.Equal("dimension", parameters.Dimension);
        Assert.Equal(["123456", "234567", "345678"], parameters.Urns);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new ItSpendSchoolsParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
        Assert.Equal([], parameters.Urns);
    }
}