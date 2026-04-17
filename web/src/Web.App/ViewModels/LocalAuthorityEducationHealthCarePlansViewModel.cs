using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels;

public class LocalAuthorityEducationHealthCarePlansViewModel(
    LocalAuthority localAuthority,
    string[] comparators,
    EducationHealthCarePlansComparisonSubCategoriesViewModel subCategories)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;

    public string[] Comparators => comparators
        .Where(c => c != Code)
        .Distinct()
        .ToArray();
    public List<BenchmarkingViewModelCostSubCategory<EducationHealthCarePlansComparisonDatum>> SubCategories => subCategories.Items;
    public Views.ViewAsOptions ViewAs { get; set; } = Views.ViewAsOptions.Chart;
}

public class LocalAuthorityEducationHealthCarePlansTableViewModel(
    BenchmarkingViewModelCostSubCategory<EducationHealthCarePlansComparisonDatum> subCategory)
{
    public BenchmarkingViewModelCostSubCategory<EducationHealthCarePlansComparisonDatum> SubCategory => subCategory;
}