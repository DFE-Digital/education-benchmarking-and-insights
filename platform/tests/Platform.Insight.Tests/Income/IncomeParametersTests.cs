using System.Collections.Specialized;
using Platform.Api.Insight.Features.Income.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.Income;

public class IncomeParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "dimension", "dimension" }
        };

        var parameters = new IncomeParameters();
        parameters.SetValues(values);

        Assert.Equal("dimension", parameters.Dimension);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new IncomeParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
    }
}