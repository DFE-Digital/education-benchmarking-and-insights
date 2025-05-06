using System.Collections.Specialized;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.Expenditure.Parameters;

public class ExpenditureQueryTrustParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "companyNumbers", "1,2,3" },
            { "excludeCentralServices", "true" }
        };

        var parameters = new ExpenditureQueryTrustParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
        Assert.Null(parameters.Category);
        Assert.Equal(["1", "2", "3"], parameters.CompanyNumbers);
        Assert.True(parameters.ExcludeCentralServices);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new ExpenditureQueryTrustParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
        Assert.Null(parameters.Category);
        Assert.Equal([], parameters.CompanyNumbers);
        Assert.False(parameters.ExcludeCentralServices);
    }
}