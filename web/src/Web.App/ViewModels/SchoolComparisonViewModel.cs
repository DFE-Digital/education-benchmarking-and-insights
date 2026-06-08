using Microsoft.Extensions.Primitives;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Domain.Schools;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class SchoolComparisonViewModel(
    School school,
    CostCodes costCodes,
    string? userDefinedSetId = null,
    string? customDataId = null,
    SchoolExpenditure? expenditure = null,
    SchoolComparatorSet? defaultComparatorSet = null,
    KS4ProgressBandings? ks4ProgressBandings = null,
    SpendingComparisonSubCategoriesViewModel? subCategories = null
    )
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? UserDefinedSetId => userDefinedSetId;
    public string? CustomDataId => customDataId;
    public int? PeriodCoveredByReturn => expenditure?.PeriodCoveredByReturn;

    public bool HasDefaultComparatorSet => defaultComparatorSet != null
                                           && (defaultComparatorSet.Building.Any(b => !string.IsNullOrWhiteSpace(b))
                                               || defaultComparatorSet.Pupil.Any(p => !string.IsNullOrWhiteSpace(p)));

    public Dictionary<string, StringValues> CostCodeMap => costCodes.SubCategoryToCostCodeMap;

    public KS4ProgressBanding[] WellOrAboveAverageKS4ProgressBandingsInComparatorSet => ks4ProgressBandings?.Items
        .Where(i => i.Banding is KS4ProgressBandings.Banding.WellAboveAverage or KS4ProgressBandings.Banding.AboveAverage)
        .ToArray() ?? [];
    public bool HasProgressIndicators => WellOrAboveAverageKS4ProgressBandingsInComparatorSet.Length > 0;

    public SchoolSpendingDimensions.BandingsAsOptions[] AvailableBandingsAs =>
        WellOrAboveAverageKS4ProgressBandingsInComparatorSet
            .Select(x => ToBandingsAsOption(x.Banding))
            .Where(x => x.HasValue)
            .Select(x => x!.Value)
            .Distinct()
            .OrderBy(x => x)
            .ToArray();

    public KeyValuePair<SchoolSpendingCategories.CategoryGroup, SchoolSpendingCategories.SubCategoryFilter[]>[] AllGroups =>
        SchoolSpendingCategories.Groups.ToArray();
    public List<SchoolSpendingComparisonGroup> Groups => subCategories?.Groups ?? [];
    public SchoolSpendingCategories.CategoryGroup? ExpandFilterGroup { get; init; }
    public SchoolSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; init; } = [];
    public HashSet<int> SelectedIds =>
        SelectedSubCategories
            .Select(x => (int)x)
            .ToHashSet();

    public IEnumerable<SchoolSpendingComparisonGroup> SelectedGroups =>
        Groups.Where(g => g.SelectedCount(SelectedIds) > 0);

    public Views.ViewAsOptions ViewAs { get; init; } = Views.ViewAsOptions.Chart;

    public SchoolSpendingDimensions.ResultAsOptions ResultAs { get; init; } = SchoolSpendingDimensions.ResultAsOptions.SpendPerUnit;

    public SchoolSpendingDimensions.BandingsAsOptions[] BandingsAs { get; init; } = [];

    public FinanceToolsViewModel Tools => new(
        school.URN,
        FinanceTools.FinancialPlanning,
        FinanceTools.BenchmarkCensus);

    public FinanceToolsViewModel CustomTools => new(
        school.URN,
        FinanceTools.SpendingComparison,
        FinanceTools.Spending,
        FinanceTools.BenchmarkCensus);

    private static SchoolSpendingDimensions.BandingsAsOptions? ToBandingsAsOption(KS4ProgressBandings.Banding banding)
    {
        return banding switch
        {
            KS4ProgressBandings.Banding.WellAboveAverage => SchoolSpendingDimensions.BandingsAsOptions.WellAbove,
            KS4ProgressBandings.Banding.AboveAverage => SchoolSpendingDimensions.BandingsAsOptions.Above,
            _ => null
        };
    }
}

public class SchoolSpendingDataViewModel(
    BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum> subCategory,
    SchoolSpendingDimensions.ResultAsOptions resultAs,
    string urn,
    SchoolSpendingDimensions.BandingsAsOptions[]? bandingsAs = null)
{
    public BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum> SubCategory => subCategory;
    public SchoolSpendingDimensions.ResultAsOptions ResultAs => resultAs;
    public SchoolSpendingDimensions.BandingsAsOptions[]? BandingsAs => bandingsAs;
    public string Urn => urn;
}

public class ProgressBandingCellViewModel(
    SchoolSpendingDimensions.BandingsAsOptions[] selectedBandings,
    string? progressBanding
    )
{
    public KS4ProgressBandings.Banding ProgressBanding { get; init; } = progressBanding.ToBanding() ?? KS4ProgressBandings.Banding.Unknown;
    public SchoolSpendingDimensions.BandingsAsOptions[] SelectedBandings => selectedBandings;

    public bool ShouldShowTag =>
        ProgressBanding switch
        {
            KS4ProgressBandings.Banding.WellAboveAverage =>
                SelectedBandings.Contains(SchoolSpendingDimensions.BandingsAsOptions.WellAbove),
            KS4ProgressBandings.Banding.AboveAverage =>
                SelectedBandings.Contains(SchoolSpendingDimensions.BandingsAsOptions.Above),
            _ => false
        };
}
