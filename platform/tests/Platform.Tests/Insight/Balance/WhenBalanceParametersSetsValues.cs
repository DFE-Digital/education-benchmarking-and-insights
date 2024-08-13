using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.Balance;
using Xunit;
namespace Platform.Tests.Insight.Balance;

public class WhenBalanceParametersSetsValues
{
    [Theory]
    [InlineData("PerUnit", "true", "1,2,3", "4,5,6", "PerUnit", true, "1|2|3", "4|5|6")]
    [InlineData(null, null, null, null, "Actuals", false, "", "")]
    [InlineData("Invalid", null, null, null, "Actuals", false, "", "")]
    public void ShouldSetValuesFromIQueryCollection(
        string? dimension,
        string? excludeCentralServices,
        string? urns,
        string? companyNumbers,
        string expectedDimension,
        bool expectedExcludeCentralServices,
        string expectedSchools,
        string expectedTrusts)
    {
        var parameters = new BalanceParameters();
        var query = new QueryCollection(new Dictionary<string, StringValues>
        {
            {
                "dimension", dimension
            },
            {
                "excludeCentralServices", excludeCentralServices
            },
            {
                "urns", urns
            },
            {
                "companyNumbers", companyNumbers
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedDimension, parameters.Dimension);
        Assert.Equal(expectedExcludeCentralServices, parameters.ExcludeCentralServices);
        Assert.Equal(expectedSchools, string.Join("|", parameters.Schools));
        Assert.Equal(expectedTrusts, string.Join("|", parameters.Trusts));
    }
}