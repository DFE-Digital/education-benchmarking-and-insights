using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels;

public class TrustComparisonItSpendViewModel(
    Trust trust,
    bool? comparatorGenerated,
    string? redirectUri,
    string[]? userDefinedSet,
    TrustComparisonSubCategoriesViewModel subCategories,
    int currentBfrYear)
{
    public static readonly Dimensions.ResultAsOptions[] FilterDimensions =
    [
        Dimensions.ResultAsOptions.Actuals
    ];

    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;

    public bool? ComparatorGenerated => comparatorGenerated;
    public string? RedirectUri => redirectUri;
    public string[]? UserDefinedSet => userDefinedSet;
    public List<BenchmarkingViewModelCostSubCategory<TrustComparisonDatum>> SubCategories => subCategories.Items;
    public int CurrentBfrYear => currentBfrYear;

    public Views.ViewAsOptions ViewAs { get; init; } = Views.ViewAsOptions.Chart;
    public Dimensions.ResultAsOptions ResultAs { get; init; } = Dimensions.ResultAsOptions.Actuals;
    public ItSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; init; } = [];
}

public class TrustComparisonItSpendTableViewModel(
    string? companyNumber,
    Dimensions.ResultAsOptions resultAs,
    BenchmarkingViewModelCostSubCategory<TrustComparisonDatum> subCategory,
    int currentBfrYear)
{
    public string? CompanyNumber => companyNumber;
    public Dimensions.ResultAsOptions ResultAs => resultAs;
    public BenchmarkingViewModelCostSubCategory<TrustComparisonDatum> SubCategory => subCategory;
    public int CurrentBfrYear => currentBfrYear;
}

public class TrustComparisonItSpendChartViewModel(
    Guid uuid,
    BenchmarkingViewModelCostSubCategory<TrustComparisonDatum> subCategory,
    int currentBfrYear)
{
    public Guid Uuid => uuid;
    public BenchmarkingViewModelCostSubCategory<TrustComparisonDatum> SubCategory => subCategory;
    public int CurrentBfrYear => currentBfrYear;
}