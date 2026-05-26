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

    public KeyValuePair<SchoolSpendingCategories.CategoryGroup, SchoolSpendingCategories.SubCategoryFilter[]>[] AllGroups =>
        SchoolSpendingCategories.Groups.ToArray();
    public List<SpendingComparisonGroup> Groups => subCategories?.Groups ?? [];
    public SchoolSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; init; } = [];
    public HashSet<int> SelectedIds =>
        SelectedSubCategories
            .Select(x => (int)x)
            .ToHashSet();

    public IEnumerable<SpendingComparisonGroup> SelectedGroups =>
        Groups.Where(g => g.SelectedCount(SelectedIds) > 0);

    // TODO: this should default to chart but until functionality to toggle view as options is implemented we default to table
    public Views.ViewAsOptions ViewAs { get; init; } = Views.ViewAsOptions.Table;

    public SchoolSpendingDimensions.ResultAsOptions ResultAs { get; init; } = SchoolSpendingDimensions.ResultAsOptions.SpendPerUnit;

    public FinanceToolsViewModel Tools => new(
        school.URN,
        FinanceTools.FinancialPlanning,
        FinanceTools.BenchmarkCensus);

    public FinanceToolsViewModel CustomTools => new(
        school.URN,
        FinanceTools.SpendingComparison,
        FinanceTools.Spending,
        FinanceTools.BenchmarkCensus);
}

public class SchoolSpendingDataViewModel(
    BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum> subCategory,
    SchoolSpendingDimensions.ResultAsOptions resultAs,
    string urn)
{
    public BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum> SubCategory => subCategory;
    public SchoolSpendingDimensions.ResultAsOptions ResultAs => resultAs;
    public string Urn => urn;
}
