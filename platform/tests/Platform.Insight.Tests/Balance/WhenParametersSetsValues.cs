using System.Collections.Specialized;
using Platform.Api.Insight.Features.Balance;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.Balance;

public class WhenBalanceParametersSetsValues
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

public class WhenBalanceQueryTrustParametersSetsValues
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "companyNumbers", "1,2,3" }
        };

        var parameters = new BalanceQueryTrustsParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
        Assert.Equal(["1", "2", "3"], parameters.Trusts);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new BalanceQueryTrustsParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
        Assert.Equal([], parameters.Trusts);
    }
}