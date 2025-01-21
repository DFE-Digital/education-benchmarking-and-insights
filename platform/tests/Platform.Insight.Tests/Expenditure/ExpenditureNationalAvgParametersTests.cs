using System.Collections.Specialized;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.Expenditure;

public class ExpenditureNationalAvgParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "financeType", "financeType" },
            { "phase", "phase" }
        };

        var parameters = new ExpenditureNationalAvgParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
        Assert.Null(parameters.Category);
        Assert.Equal("financeType", parameters.FinanceType);
        Assert.Equal("phase", parameters.OverallPhase);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new ExpenditureNationalAvgParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
        Assert.Null(parameters.Category);
        Assert.Equal(string.Empty, parameters.FinanceType);
        Assert.Equal(string.Empty, parameters.OverallPhase);
    }
}