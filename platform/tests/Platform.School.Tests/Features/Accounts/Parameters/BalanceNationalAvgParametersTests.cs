using System.Collections.Specialized;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.School.Tests.Features.Accounts.Parameters;

public class BalanceNationalAvgParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "dimension", "dimension" },
            { "financeType", "financeType" },
            { "phase", "phase" }
        };

        var parameters = new BalanceNationalAvgParameters();
        parameters.SetValues(values);

        Assert.Equal("dimension", parameters.Dimension);
        Assert.Equal("financeType", parameters.FinanceType);
        Assert.Equal("phase", parameters.OverallPhase);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new BalanceNationalAvgParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
    }
}