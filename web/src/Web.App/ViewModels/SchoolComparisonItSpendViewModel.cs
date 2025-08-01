using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolComparisonItSpendViewModel(School school, SchoolComparisonSubCategoriesViewModel subCategories)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolComparisonSubCategoriesViewModel SubCategories => subCategories;

    public ViewAsOptions ViewAs { get; set; } = ViewAsOptions.Chart;
    public ResultAsOptions ResultAs { get; set; } = ResultAsOptions.SpendPerPupil;
    public ItSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; set; } = [];


    public enum ViewAsOptions
    {
        Chart = 0,
        Table = 1
    }

    public enum ResultAsOptions
    {
        SpendPerPupil = 0,
        Actuals = 1,
        PercentExpenditure = 2,
        PercentIncome = 3
    }
}