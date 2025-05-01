using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;

namespace Platform.Api.Insight.Features.BudgetForecast.Parameters;

public record BudgetForecastReturnParameters : QueryParameters
{
    public string RunType { get; private set; } = Pipeline.RunType.Default;
    public string Category { get; private set; } = "Revenue reserve";
    public string RunId { get; private set; } = string.Empty;

    public override void SetValues(NameValueCollection query)
    {
        var runType = query["runType"];
        if (!string.IsNullOrWhiteSpace(runType))
        {
            RunType = runType;
        }

        var category = query["category"];
        if (!string.IsNullOrWhiteSpace(category))
        {
            Category = category;
        }

        var runId = query["runId"];
        if (string.IsNullOrWhiteSpace(runId))
        {
            runId = string.Empty;
        }

        RunId = runId;
    }
}