using System.Collections.Specialized;
using Platform.Api.Insight.Features.Census.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.Census.Parameters;

public class CensusQuerySchoolsParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "urns", "1,2,3" },
            { "companyNumber", "12345678" },
            { "phase", "phase" },
            { "laCode", "822" }
        };

        var parameters = new CensusQuerySchoolsParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Census.Total, parameters.Dimension);
        Assert.Null(parameters.Category);
        Assert.Equal(["1", "2", "3"], parameters.Urns);
        Assert.Equal("12345678", parameters.CompanyNumber);
        Assert.Equal("phase", parameters.Phase);
        Assert.Equal("822", parameters.LaCode);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new CensusQuerySchoolsParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Census.Total, parameters.Dimension);
        Assert.Null(parameters.Category);
        Assert.Equal([], parameters.Urns);
        Assert.Null(parameters.CompanyNumber);
        Assert.Null(parameters.Phase);
        Assert.Null(parameters.LaCode);
    }
}