using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels;

public class SchoolComparisonItSpendViewModel(School school, SchoolComparisonSubCategoriesViewModel subCategories)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolComparisonSubCategoriesViewModel SubCategories => subCategories;

    public ViewAsOptions ViewAs { get; set; } = ViewAsOptions.Chart;
    public ChartDimensions.ResultAsOptions ResultAs { get; set; } = ChartDimensions.ResultAsOptions.SpendPerPupil;
    public ItSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; set; } = [];


    public enum ViewAsOptions
    {
        Chart = 0,
        Table = 1
    }
}