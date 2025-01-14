using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.Census;
using Xunit;

namespace Platform.Insight.Tests.Census;

public class WhenCensusParametersSetsValues
{
    [Theory]
    [InlineData("WorkforceFte", "HeadcountPerFte", "1,2,3", "companyNumber", "phase", "laCode", "WorkforceFte", "HeadcountPerFte", "1|2|3", "companyNumber", "phase", "laCode")]
    [InlineData(null, null, null, null, null, null, null, "Total", "", "", "", "")]
    [InlineData("Invalid", "Invalid", null, null, "Invalid", null, "Invalid", "Invalid", "", "", "Invalid", "")]
    public void ShouldSetValuesFromIQueryCollection(
        string? category,
        string? dimension,
        string? urns,
        string? companyNumber,
        string? phase,
        string? laCode,
        string? expectedCategory,
        string expectedDimension,
        string expectedUrns,
        string expectedCompanyNumber,
        string expectedPhase,
        string expectedLaCode)
    {
        var parameters = new QuerySchoolCensusParameters();
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
            },
            {
                "companyNumber", companyNumber
            },
            {
                "phase", phase
            },
            {
                "laCode", laCode
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedCategory, parameters.Category);
        Assert.Equal(expectedDimension, parameters.Dimension);
        Assert.Equal(expectedUrns, string.Join("|", parameters.Urns));
        Assert.Equal(expectedCompanyNumber, parameters.CompanyNumber);
        Assert.Equal(expectedPhase, parameters.Phase);
        Assert.Equal(expectedLaCode, parameters.LaCode);
    }
}