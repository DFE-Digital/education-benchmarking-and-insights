using System.Collections.Specialized;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.EducationHealthCarePlans.Parameters;

public class GivenEducationHealthCarePlansParameters
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var query = new NameValueCollection
        {
            { "code", "LA1,LA2" },
            { "dimension", "Per1000" }
        };

        var parameters = new EducationHealthCarePlansParameters();
        parameters.SetValues(query);

        Assert.Equal(new[] { "LA1", "LA2" }, parameters.Codes);
        Assert.Equal("Per1000", parameters.Dimension);
    }

    [Fact]
    public void ShouldSetDefaultValuesWhenQueryEmpty()
    {
        var query = new NameValueCollection();

        var parameters = new EducationHealthCarePlansParameters();
        parameters.SetValues(query);

        Assert.Empty(parameters.Codes);
        Assert.Equal(Dimensions.EducationHealthCarePlans.Actuals, parameters.Dimension);
    }
}
