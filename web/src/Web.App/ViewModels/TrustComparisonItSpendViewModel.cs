using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels;

public class TrustComparisonItSpendViewModel(
    Trust trust,
    bool? comparatorGenerated,
    string? redirectUri,
    string[]? userDefinedSet,
    TrustComparisonSubCategoriesViewModel subCategories)
{
    public static readonly Dimensions.ResultAsOptions[] FilterDimensions =
    [
        Dimensions.ResultAsOptions.Actuals,
    ];

    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;

    public bool? ComparatorGenerated => comparatorGenerated;
    public string? RedirectUri => redirectUri;
    public string[]? UserDefinedSet => userDefinedSet;
    public List<BenchmarkingViewModelCostSubCategory<TrustComparisonDatum>> SubCategories => subCategories.Items;

    public Views.ViewAsOptions ViewAs { get; init; } = Views.ViewAsOptions.Chart;
    public Dimensions.ResultAsOptions ResultAs { get; init; } = Dimensions.ResultAsOptions.Actuals;
    public ItSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; init; } = [];
}