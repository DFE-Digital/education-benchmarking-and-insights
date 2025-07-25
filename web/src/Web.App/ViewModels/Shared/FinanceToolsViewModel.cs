namespace Web.App.ViewModels.Shared;

public class FinanceToolsViewModel(string? identifier, params FinanceTools[] tools)
{
    public IEnumerable<FinanceTools> Tools => tools;
    public string? Identifier => identifier;
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
    HighNeeds
}