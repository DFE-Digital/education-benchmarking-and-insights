namespace EducationBenchmarking.Web.ViewModels.Components;

public class FinanceToolsViewModel(string identifier, IEnumerable<FinanceTools> tools)
{
    public IEnumerable<FinanceTools> Tools { get; } = tools;
    public string Identifier { get; } = identifier;
}

public enum FinanceTools
{
    CompareYourCosts,
    FinancialPlanning,
    BenchmarkWorkforce,
    AreasOfInvestigation
}