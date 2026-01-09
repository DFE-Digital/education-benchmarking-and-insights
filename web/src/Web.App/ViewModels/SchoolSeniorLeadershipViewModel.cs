using Web.App.Domain;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class SchoolSeniorLeadershipViewModel(School school, SeniorLeadershipGroup[] group)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SeniorLeadershipGroup[] Group => group;

    public FinanceToolsViewModel Tools => new(
        school.URN,
        FinanceTools.FinancialPlanning,
        FinanceTools.CompareYourCosts,
        FinanceTools.BenchmarkCensus);
}