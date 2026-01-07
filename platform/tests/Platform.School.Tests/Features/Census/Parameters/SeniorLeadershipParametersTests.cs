using System.Collections.Specialized;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.School.Tests.Features.Census.Parameters;

public class SeniorLeadershipParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "urns", "123456" },
            { "urns", "234567" },
            { "urns", "345678" },
            { "dimension", "dimension" }
        };

        var parameters = new SeniorLeadershipParameters();
        parameters.SetValues(values);

        Assert.Equal("dimension", parameters.Dimension);
        Assert.Equal(["123456", "234567", "345678"], parameters.Urns);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new SeniorLeadershipParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Census.Total, parameters.Dimension);
        Assert.Equal([], parameters.Urns);
    }
}