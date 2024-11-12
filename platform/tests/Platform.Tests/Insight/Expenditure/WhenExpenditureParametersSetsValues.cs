using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.Expenditure;
using Xunit;
namespace Platform.Tests.Insight.Expenditure;

public class WhenExpenditureParametersSetsValues
{
    [Theory]
    [InlineData("TotalExpenditure", "PerUnit", "true", "1,2,3", "456", "phase", "laCode", "TotalExpenditure", "PerUnit", true, "1|2|3", "456", "phase", "laCode")]
    [InlineData(null, null, null, null, null, null, null, null, "Actuals", false, "", "", "", "")]
    [InlineData("Invalid", "Invalid", null, null, null, null, null, "Invalid", "Invalid", false, "", "", "", "")]
    public void ShouldSetValuesFromIQueryCollection(
        string? category,
        string? dimension,
        string? excludeCentralServices,
        string? urns,
        string? companyNumber,
        string? phase,
        string? laCode,
        string? expectedCategory,
        string expectedDimension,
        bool expectedExcludeCentralServices,
        string expectedUrns,
        string expectedCompanyNumber,
        string expectedPhase,
        string expectedLaCode)
    {
        var parameters = new QuerySchoolExpenditureParameters();
        var query = new QueryCollection(new Dictionary<string, StringValues>
        {
            {
                "category", category
            },
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
        Assert.Equal(expectedExcludeCentralServices, parameters.ExcludeCentralServices);
        Assert.Equal(expectedUrns, string.Join("|", parameters.Urns));
        Assert.Equal(expectedCompanyNumber, parameters.CompanyNumber);
        Assert.Equal(expectedPhase, parameters.Phase);
        Assert.Equal(expectedLaCode, parameters.LaCode);
    }
}