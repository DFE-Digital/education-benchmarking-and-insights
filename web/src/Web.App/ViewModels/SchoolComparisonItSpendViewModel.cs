using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Extensions;

namespace Web.App.ViewModels;

public class SchoolComparisonItSpendViewModel(School school, SchoolComparisonSubCategoriesViewModel subCategories, SchoolItSpend[] expenditures)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolComparisonSubCategoriesViewModel SubCategories => subCategories;
    public SchoolChartTooltipData[] TooltipData => expenditures.Select(x => new SchoolChartTooltipData
    {
        Urn = x.URN,
        SchoolName = x.SchoolName,
        LAName = x.LAName,
        SchoolType = x.SchoolType,
        TotalPupils = x.TotalPupils
    }).ToArray();

    public ViewAsOptions ViewAs { get; set; } = ViewAsOptions.Chart;
    public Dimensions.ResultAsOptions ResultAs { get; set; } = Dimensions.ResultAsOptions.SpendPerPupil;
    public ItSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; set; } = [];

    public enum ViewAsOptions
    {
        Chart = 0,
        Table = 1
    }
}