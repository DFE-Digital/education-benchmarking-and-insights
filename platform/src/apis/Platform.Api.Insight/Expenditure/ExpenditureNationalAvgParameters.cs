using Microsoft.AspNetCore.Http;
using Platform.Functions;

namespace Platform.Api.Insight.Expenditure;

public record ExpenditureNationalAvgParameters : QueryParameters
{
    public string Dimension { get; private set; } = ExpenditureDimensions.Actuals;
    public string FinanceType { get; private set; } = string.Empty;
    public string OverallPhase { get; private set; } = string.Empty;

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