using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Extensions;

namespace Web.App.ViewModels;

public class SchoolComparisonItSpendViewModel(School school, SchoolComparisonSubCategoriesViewModel subCategories)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolComparisonSubCategoriesViewModel SubCategories => subCategories;

    public ViewAsOptions ViewAs { get; set; } = ViewAsOptions.Chart;
    public Dimensions.ResultAsOptions ResultAs { get; set; } = Dimensions.ResultAsOptions.SpendPerPupil;
    public ItSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; set; } = [];

    public enum ViewAsOptions
    {
        Chart = 0,
        Table = 1
    }
}