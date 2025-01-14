using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.Census;
using Xunit;

namespace Platform.Insight.Tests.Census;

public class CensusNationalAvgParametersSetsValues
{
    [Theory]
    [InlineData("Total", "Primary", "Academy", "Total", "Primary", "Academy")]
    [InlineData(null, null, null, "Total", "", "")]
    [InlineData("Invalid", "Invalid", "Invalid", "Invalid", "Invalid", "Invalid")]
    public void ShouldSetValuesFromIQueryCollection(
        string? dimension,
        string? phase,
        string? financeType,
        string expectedDimension,
        string expectedPhase,
        string expectedFinanceType)
    {
        var parameters = new CensusNationalAvgParameters();
        var query = new QueryCollection(new Dictionary<string, StringValues>
        {
            {
                "dimension", dimension
            },
            {
                "phase", phase
            },
            {
                "financeType", financeType
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedDimension, parameters.Dimension);
        Assert.Equal(expectedPhase, parameters.OverallPhase);
        Assert.Equal(expectedFinanceType, parameters.FinanceType);
    }
}