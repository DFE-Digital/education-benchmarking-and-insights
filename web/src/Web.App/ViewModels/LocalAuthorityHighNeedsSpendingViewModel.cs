using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;
using LocalAuthority = Web.App.Domain.LocalAuthority;

namespace Web.App.ViewModels;

public class LocalAuthorityHighNeedsSpendingViewModel(
    LocalAuthority localAuthority,
    string[] comparators,
    HighNeedsSpendingComparisonSubCategoriesViewModel subCategories)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;

    public string[] Comparators => comparators
        .Where(c => c != Code)
        .Distinct()
        .ToArray();

    public KeyValuePair<HighNeedsSpendingCategories.CategoryGroup, HighNeedsSpendingCategories.SubCategoryFilter[]>[] AllGroups =>
        HighNeedsSpendingCategories.Groups.ToArray();
    public List<HighNeedsSpendingComparisonGroup> Groups => subCategories.Groups;
    public HighNeedsSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; init; } = [];
    public HashSet<int> SelectedIds =>
        SelectedSubCategories
            .Select(x => (int)x)
            .ToHashSet();

    public IEnumerable<HighNeedsSpendingComparisonGroup> SelectedGroups =>
        Groups.Where(g => g.SelectedCount(SelectedIds) > 0);

    public Views.ViewAsOptions ViewAs { get; init; } = Views.ViewAsOptions.Chart;

    public HighNeedsDimensions.ResultAsOptions ResultAs { get; init; } = HighNeedsDimensions.ResultAsOptions.PerPupil;

    public HighNeedsDimensions.SubmissionTypeOptions Type { get; init; } = HighNeedsDimensions.SubmissionTypeOptions.Outturn;
    public int? Year { get; init; }
}

public class LocalAuthorityHighNeedsSpendingDataViewModel(
    BenchmarkingViewModelCostSubCategory<HighNeedsSpendingComparisonDatum> subCategory,
    HighNeedsDimensions.ResultAsOptions resultAs,
    HighNeedsDimensions.SubmissionTypeOptions type,
    string code,
    int? year = null)
{
    public BenchmarkingViewModelCostSubCategory<HighNeedsSpendingComparisonDatum> SubCategory => subCategory;
    public HighNeedsDimensions.ResultAsOptions ResultAs => resultAs;
    public HighNeedsDimensions.SubmissionTypeOptions Type => type;
    public string Code => code;
    public int? Year => year;
}
