using Web.App.Domain;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class SchoolCensusViewModel(
    School school,
    string? userDefinedSetId = null,
    string? customDataId = null,
    Census? census = null,
    SchoolComparatorSet? defaultComparatorSet = null)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? UserDefinedSetId => userDefinedSetId;
    public string? CustomDataId => customDataId;
    public decimal? TotalPupils => census?.TotalPupils;
    public bool HasDefaultComparatorSet => defaultComparatorSet != null
                                           && defaultComparatorSet.Pupil.Any(p => !string.IsNullOrWhiteSpace(p));
    public FinanceToolsViewModel Tools => new(
        school.URN,
        FinanceTools.FinancialPlanning,
        FinanceTools.CompareYourCosts);

    public FinanceToolsViewModel CustomTools => new(
        school.URN,
        FinanceTools.SpendingComparison,
        FinanceTools.CompareYourCosts,
        FinanceTools.Spending);
}