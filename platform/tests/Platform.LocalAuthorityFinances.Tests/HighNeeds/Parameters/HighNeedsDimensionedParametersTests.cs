using System.Collections.Specialized;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.HighNeeds.Parameters;

public class HighNeedsDimensionedParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        const string dimension = nameof(dimension);
        var values = new NameValueCollection
        {
            { "code", "code1" },
            { "code", "code2" },
            { "code", "code3" },
            { "dimension", dimension }
        };

        var parameters = new HighNeedsDimensionedParameters();
        parameters.SetValues(values);

        Assert.Equal(["code1", "code2", "code3"], parameters.Codes);
        Assert.Equal(dimension, parameters.Dimension);
    }
}