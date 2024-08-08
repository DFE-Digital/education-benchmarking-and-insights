using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.Trusts;
using Xunit;
namespace Platform.Tests.Insight.Trusts;

public class WhenTrustsParametersSetsValues
{
    [Theory]
    [InlineData("1,2,3", "1|2|3")]
    [InlineData(null, "")]
    public void ShouldSetValuesFromIQueryCollection(string? companyNumbers, string expectedTrusts)
    {
        var parameters = new TrustsParameters();
        var query = new QueryCollection(new Dictionary<string, StringValues>
        {
            {
                "companyNumbers", companyNumbers
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedTrusts, string.Join("|", parameters.Trusts));
    }
}