using System.Collections.Specialized;
using Platform.Api.Insight.Features.Trusts.Parameters;
using Xunit;

namespace Platform.Insight.Tests.Trusts;

public class TrustsParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "companyNumbers", "1,2,3" }
        };

        var parameters = new TrustsParameters();
        parameters.SetValues(values);
        Assert.Equal(["1", "2", "3"], parameters.Trusts);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new TrustsParameters();
        parameters.SetValues(values);

        Assert.Equal([], parameters.Trusts);
    }
}