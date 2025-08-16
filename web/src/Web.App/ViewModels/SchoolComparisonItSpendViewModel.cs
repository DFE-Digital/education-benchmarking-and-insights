using Web.App.Domain;
using Web.App.Domain.Charts;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels;

public class SchoolComparisonItSpendViewModel(School school, SchoolComparisonSubCategoriesViewModel subCategories, SchoolItSpend[] expenditures)
{
    public enum ViewAsOptions
    {
        Chart = 0,
        Table = 1
    }

    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolComparisonSubCategoriesViewModel SubCategories => subCategories;
    public SchoolChartTooltipData[] TooltipData => expenditures.Select(x => new SchoolChartTooltipData
    {
        Urn = x.URN,
        SchoolName = x.SchoolName,
        LAName = x.LAName,
        SchoolType = x.SchoolType,
        TotalPupils = x.TotalPupils,
        PeriodCoveredByReturn = x.PeriodCoveredByReturn
    }).ToArray();

    public ViewAsOptions ViewAs { get; set; } = ViewAsOptions.Chart;
    public Dimensions.ResultAsOptions ResultAs { get; set; } = Dimensions.ResultAsOptions.SpendPerPupil;
    public ItSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; set; } = [];
}

public class SchoolComparisonItSpendTableViewModel(string? urn, Dimensions.ResultAsOptions resultAs, SchoolComparisonViewModelCostSubCategory<SchoolComparisonDatum> subCategory)
{
    public string? Urn => urn;
    public Dimensions.ResultAsOptions ResultAs => resultAs;
    public SchoolComparisonViewModelCostSubCategory<SchoolComparisonDatum> SubCategory => subCategory;
}

public class SchoolComparisonItSpendChartViewModel(Guid uuid, SchoolComparisonViewModelCostSubCategory<SchoolComparisonDatum> subCategory)
{
    public Guid Uuid => uuid;
    public SchoolComparisonViewModelCostSubCategory<SchoolComparisonDatum> SubCategory => subCategory;
}