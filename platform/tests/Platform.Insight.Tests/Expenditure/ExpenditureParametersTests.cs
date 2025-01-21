using System.Collections.Specialized;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.Expenditure;

public class ExpenditureParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "dimension", "dimension" },
            { "category", "category" }
        };

        var parameters = new ExpenditureParameters();
        parameters.SetValues(values);

        Assert.Equal("dimension", parameters.Dimension);
        Assert.Equal("category", parameters.Category);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new ExpenditureParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
        Assert.Null(parameters.Category);
    }
}