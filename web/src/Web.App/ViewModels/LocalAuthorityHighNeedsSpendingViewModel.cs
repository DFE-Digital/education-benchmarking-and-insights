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

    public List<HighNeedsSpendingComparisonGroup> Groups => subCategories.Groups;

    public HighNeedsSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; set; } = [];

    public Views.ViewAsOptions ViewAs { get; set; } = Views.ViewAsOptions.Table;

    public HighNeedsDimensions.ResultAsOptions ResultAs { get; set; } = HighNeedsDimensions.ResultAsOptions.PerPupil;

    public HighNeedsDimensions.SubmissionTypeOptions Type { get; set; } = HighNeedsDimensions.SubmissionTypeOptions.Budget;
}

public class LocalAuthorityHighNeedsSpendingDataViewModel(
    BenchmarkingViewModelCostSubCategory<HighNeedsSpendingComparisonDatum> subCategory,
    HighNeedsDimensions.ResultAsOptions resultAs,
    HighNeedsDimensions.SubmissionTypeOptions type,
    string code)
{
    public BenchmarkingViewModelCostSubCategory<HighNeedsSpendingComparisonDatum> SubCategory => subCategory;
    public HighNeedsDimensions.ResultAsOptions ResultAs => resultAs;
    public HighNeedsDimensions.SubmissionTypeOptions Type => type;
    public string? Code => code;
}