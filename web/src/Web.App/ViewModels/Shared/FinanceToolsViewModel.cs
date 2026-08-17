namespace Web.App.ViewModels.Shared;

public class FinanceToolsViewModel(string? identifier, params FinanceTools[] tools)
{
    public IEnumerable<FinanceTools> Tools => tools;
    public string? Identifier => identifier;
    public string? ReferrerKey { get; init; }
}

public enum FinanceTools
{
    CompareYourCosts,
    FinancialPlanning,
    BenchmarkCensus,
    CentralServices,
    ForecastRisk,
    SpendingComparison,
    SpendingComparisonIt,
    Spending,
    Risks
}
