using System.Collections.Specialized;
using Platform.Api.Insight.Features.BudgetForecast.Parameters;
using Xunit;

namespace Platform.Insight.Tests.BudgetForecast.Parameters;

public class WhenBudgetForecastReturnParametersSetsValues
{
    [Theory]
    [InlineData("runType", "category", "runId", "runType", "category", "runId")]
    public void ShouldSetValuesFromIQueryCollection(string? runType, string? category, string? runId, string expectedRunType, string expectedCategory, string? expectedRunId)
    {
        var parameters = new BudgetForecastReturnParameters();
        var query = new NameValueCollection
        {
            { "runType", runType },
            { "category", category },
            { "runId", runId }
        };

        parameters.SetValues(query);

        Assert.Equal(expectedRunType, parameters.RunType);
        Assert.Equal(expectedCategory, parameters.Category);
        Assert.Equal(expectedRunId, parameters.RunId);
    }
}