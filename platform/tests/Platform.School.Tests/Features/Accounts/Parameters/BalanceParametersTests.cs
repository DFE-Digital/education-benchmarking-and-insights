using System.Collections.Specialized;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.School.Tests.Features.Accounts.Parameters;

public class BalanceParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "dimension", "dimension" }
        };

        var parameters = new BalanceParameters();
        parameters.SetValues(values);

        Assert.Equal("dimension", parameters.Dimension);
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