using System.Collections.Specialized;
using Platform.Api.Insight.Features.Schools.Parameters;
using Xunit;

namespace Platform.Insight.Tests.Schools;

public class WhenSchoolsParametersSetsValues
{
    [Theory]
    [InlineData("1,2,3", "1|2|3")]
    [InlineData(null, "")]
    public void ShouldSetValuesFromIQueryCollection(string? urns, string expectedSchools)
    {
        var parameters = new SchoolsParameters();
        var query = new NameValueCollection
        {
            { "urns", urns }
        };

        parameters.SetValues(query);

        Assert.Equal(expectedSchools, string.Join("|", parameters.Schools));
    }
}