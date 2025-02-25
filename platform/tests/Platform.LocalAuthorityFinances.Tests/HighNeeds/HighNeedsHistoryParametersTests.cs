using System.Collections.Specialized;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.HighNeeds;

public class HighNeedsHistoryParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "code", "code1" },
            { "code", "code2" },
            { "code", "code3" }
        };

        var parameters = new HighNeedsHistoryParameters();
        parameters.SetValues(values);

        Assert.Equal(["code1", "code2", "code3"], parameters.Codes);
    }
}