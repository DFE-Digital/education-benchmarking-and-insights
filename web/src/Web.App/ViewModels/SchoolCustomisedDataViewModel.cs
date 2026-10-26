using Web.App.Domain;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class SchoolCustomisedDataViewModel(
    School school, bool? customDataGenerated = false)
{
    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public bool? CustomDataGenerated => customDataGenerated;

    public FinanceToolsViewModel CustomTools => new(
        school.URN,
        FinanceTools.SpendingComparison,
        FinanceTools.CompareYourCosts,
        FinanceTools.Spending,
        FinanceTools.BenchmarkCensus);
}
