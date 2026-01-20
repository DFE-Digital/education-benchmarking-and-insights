using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class SchoolSeniorLeadershipViewModel(
    School school,
    SeniorLeadershipGroup[] group,
    string? userDefinedSetId = null,
    SchoolComparatorSet? defaultComparatorSet = null)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SeniorLeadershipGroup[] Group => group.OrderByDescending(g => g.SeniorLeadership).ToArray();
    public string? ChartSvg { get; set; }
    public bool HasUserDefinedSet => !string.IsNullOrEmpty(userDefinedSetId);
    public bool HasDefaultComparatorSet => defaultComparatorSet != null
                                           && defaultComparatorSet.Pupil.Any(p => !string.IsNullOrWhiteSpace(p));
    public bool HasMissingComparatorSet => !HasUserDefinedSet && !HasDefaultComparatorSet;

    public Views.ViewAsOptions ViewAs { get; set; } = Views.ViewAsOptions.Chart;
    public CensusDimensions.ResultAsOptions ResultAs { get; set; } = CensusDimensions.ResultAsOptions.Total;

    public FinanceToolsViewModel Tools => new(
        school.URN,
        FinanceTools.FinancialPlanning,
        FinanceTools.CompareYourCosts,
        FinanceTools.BenchmarkCensus);
}