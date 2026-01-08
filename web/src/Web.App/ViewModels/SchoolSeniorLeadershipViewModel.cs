using Web.App.Domain;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class SchoolSeniorLeadershipViewModel(School school)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;

    public FinanceToolsViewModel Tools => new(
        school.URN,
        FinanceTools.FinancialPlanning,
        FinanceTools.CompareYourCosts,
        FinanceTools.BenchmarkCensus);
}