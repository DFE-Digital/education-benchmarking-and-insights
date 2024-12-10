using Microsoft.AspNetCore.Http;
using Platform.Functions;

namespace Platform.Api.Insight.Expenditure;

public record ExpenditureNationalAvgParameters : QueryParameters
{
    public string Dimension { get; internal set; } = ExpenditureDimensions.Actuals;
    public string FinanceType { get; internal set; } = string.Empty;
    public string OverallPhase { get; internal set; } = string.Empty;

    public override void SetValues(IQueryCollection query)
    {
        if (query.TryGetValue("dimension", out var dimension) && !string.IsNullOrWhiteSpace(dimension))
        {
            Dimension = dimension.ToString();
        }

        if (query.TryGetValue("financeType", out var financeType) && !string.IsNullOrWhiteSpace(financeType))
        {
            FinanceType = financeType.ToString();
        }

        if (query.TryGetValue("phase", out var overallPhase) && !string.IsNullOrWhiteSpace(overallPhase))
        {
            OverallPhase = overallPhase.ToString();
        }
    }
}