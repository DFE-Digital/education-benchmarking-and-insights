using System.Collections.Specialized;
using Platform.Api.Insight.Features.Balance.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.Balance.Parameters;

public class BalanceParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "dimension", "PercentExpenditure" }
        };

        var parameters = new BalanceParameters();
        parameters.SetValues(values);

        Assert.Equal("PercentExpenditure", parameters.Dimension);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new BalanceParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
    }
}