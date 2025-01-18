using System.Collections.Specialized;
using Platform.Api.Insight.Features.Census.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.Census;

public class CensusParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "dimension", "PercentWorkforce" },
            { "category", "SeniorLeadershipFte" }
        };

        var parameters = new CensusParameters();
        parameters.SetValues(values);

        Assert.Equal("PercentWorkforce", parameters.Dimension);
        Assert.Equal("SeniorLeadershipFte", parameters.Category);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new CensusParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Census.Total, parameters.Dimension);
        Assert.Null(parameters.Category);
    }
}