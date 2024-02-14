namespace EducationBenchmarking.Web.ViewModels.Components;

public class FinanceToolsViewModel(string identifier, IEnumerable<FinanceTools> tools)
{
    public IEnumerable<FinanceTools> Tools => tools;
    public string Identifier => identifier;
}

public enum FinanceTools
{
    CompareYourCosts,
    FinancialPlanning,
    BenchmarkWorkforce,
    AreasOfInvestigation
}