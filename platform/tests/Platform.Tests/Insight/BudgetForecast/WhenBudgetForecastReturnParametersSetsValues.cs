using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Api.Insight.BudgetForecast;
using Xunit;
namespace Platform.Tests.Insight.BudgetForecast;

public class WhenBudgetForecastReturnParametersSetsValues
{
    [Theory]
    [InlineData("runType", "category", "runId", "runType", "category", "runId")]
    [InlineData(null, null, null, "default", "Revenue reserve", null)]
    public void ShouldSetValuesFromIQueryCollection(string? runType, string? category, string? runId, string expectedRunType, string expectedCategory, string? expectedRunId)
    {
        var parameters = new BudgetForecastReturnParameters();
        var query = new QueryCollection(new Dictionary<string, StringValues>
        {
            {
                "runType", runType
            },
            {
                "category", category
            },
            {
                "runId", runId
            }
        });

        parameters.SetValues(query);

        Assert.Equal(expectedRunType, parameters.RunType);
        Assert.Equal(expectedCategory, parameters.Category);
        Assert.Equal(expectedRunId, parameters.RunId);
    }
}