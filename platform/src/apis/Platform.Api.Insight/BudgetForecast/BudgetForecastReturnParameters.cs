using Microsoft.AspNetCore.Http;
using Platform.Functions;
namespace Platform.Api.Insight.BudgetForecast;

public record BudgetForecastReturnParameters : QueryParameters
{
    public string RunType { get; private set; } = "default";
    public string Category { get; private set; } = "Revenue reserve";
    public string? RunId { get; private set; }

    public override void SetValues(IQueryCollection query)
    {
        var runType = query["runType"].ToString();
        if (!string.IsNullOrWhiteSpace(runType))
        {
            RunType = runType;
        }

        var category = query["category"].ToString();
        if (!string.IsNullOrWhiteSpace(category))
        {
            Category = category;
        }

        var runId = query["runId"].ToString();
        if (string.IsNullOrWhiteSpace(runId))
        {
            runId = null;
        }

        RunId = runId;
    }
}