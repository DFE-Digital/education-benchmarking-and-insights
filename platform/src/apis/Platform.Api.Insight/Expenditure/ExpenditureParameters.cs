using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;
namespace Platform.Api.Insight.Expenditure;

public record ExpenditureParameters : QueryParameters
{
    public bool ExcludeCentralServices { get; internal set; }
    public string? Category { get; internal set; }
    public string Dimension { get; internal set; } = ExpenditureDimensions.Actuals;
    public string? FinanceType { get; private set; }
    public string? OverallPhase { get; private set; }

    public override void SetValues(IQueryCollection query)
    {
        ExcludeCentralServices = query.ToBool("excludeCentralServices");

        if (query.TryGetValue("category", out var category) && !string.IsNullOrWhiteSpace(category))
        {
            Category = category.ToString();
        }

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