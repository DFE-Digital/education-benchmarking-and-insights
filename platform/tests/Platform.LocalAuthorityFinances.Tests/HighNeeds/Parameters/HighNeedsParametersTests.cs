using System.Collections.Specialized;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.HighNeeds.Parameters;

public class HighNeedsParametersTests
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

        var parameters = new HighNeedsParameters();
        parameters.SetValues(values);

        Assert.Equal(["code1", "code2", "code3"], parameters.Codes);
    }
}