using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.Schools;
using Xunit;
namespace Platform.Tests.Insight.Schools;

public class WhenSchoolsParametersSetsValues
{
    [Theory]
    [InlineData("1,2,3", "1|2|3")]
    [InlineData(null, "")]
    public void ShouldSetValuesFromIQueryCollection(string? urns, string expectedSchools)
    {
        var parameters = new SchoolsParameters();
        var query = new QueryCollection(new Dictionary<string, StringValues>
        {
            {
                "urns", urns
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedSchools, string.Join("|", parameters.Schools));
    }
}