using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.Expenditure;
using Xunit;
namespace Platform.Tests.Insight.Expenditure;

public class WhenExpenditureParametersSetsValues
{
    [Theory]
    [InlineData("TotalExpenditure", "PerUnit", "true", "1,2,3", "4,5,6", "TotalExpenditure", "PerUnit", true, "1|2|3", "4|5|6")]
    [InlineData(null, null, null, null, null, null, "Actuals", false, "", "")]
    [InlineData("Invalid", "Invalid", null, null, null, null, "Actuals", false, "", "")]
    public void ShouldSetValuesFromIQueryCollection(
        string? category,
        string? dimension,
        string? excludeCentralServices,
        string? urns,
        string? companyNumbers,
        string? expectedCategory,
        string expectedDimension,
        bool expectedExcludeCentralServices,
        string expectedSchools,
        string expectedTrusts)
    {
        var parameters = new ExpenditureParameters();
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
                "companyNumbers", companyNumbers
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedCategory, parameters.Category);
        Assert.Equal(expectedDimension, parameters.Dimension);
        Assert.Equal(expectedExcludeCentralServices, parameters.ExcludeCentralServices);
        Assert.Equal(expectedSchools, string.Join("|", parameters.Schools));
        Assert.Equal(expectedTrusts, string.Join("|", parameters.Trusts));
    }
}