using System.Collections.Specialized;
using Platform.Api.LocalAuthority.Features.Accounts.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Accounts.Parameters;

public class GivenHighNeedsParameters
{
    [Fact]
    public void ShouldSetDefaultValuesWhenQueryIsEmpty()
    {
        var query = new NameValueCollection();
        var parameters = new HighNeedsParametersV1();

        parameters.SetValues(query);

        Assert.Empty(parameters.Codes);
        Assert.Equal(Dimensions.HighNeeds.Actuals, parameters.Dimension);
    }

    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var query = new NameValueCollection
        {
            { "code", "LA1,LA2" },
            { "dimension", "PerHead" }
        };

        var parameters = new HighNeedsParametersV1();
        parameters.SetValues(query);

        Assert.Equal(new[] { "LA1", "LA2" }, parameters.Codes);
        Assert.Equal("PerHead", parameters.Dimension);
    }
}
