using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.Census;
using Xunit;
namespace Platform.Tests.Insight.Census;

public class WhenCensusParametersSetsValues
{
    [Theory]
    [InlineData("WorkforceFte", "HeadcountPerFte", "1,2,3", "WorkforceFte", "HeadcountPerFte", "1|2|3")]
    [InlineData(null, null, null, null, "Total", "")]
    [InlineData("Invalid", "Invalid", null, null, "Total", "")]
    public void ShouldSetValuesFromIQueryCollection(string? category, string? dimension, string? urns, string? expectedCategory, string expectedDimension, string expectedSchools)
    {
        var parameters = new CensusParameters();
        var query = new QueryCollection(new Dictionary<string, StringValues>
        {
            {
                "category", category
            },
            {
                "dimension", dimension
            },
            {
                "urns", urns
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedCategory, parameters.Category);
        Assert.Equal(expectedDimension, parameters.Dimension);
        Assert.Equal(expectedSchools, string.Join("|", parameters.Schools));
    }
}