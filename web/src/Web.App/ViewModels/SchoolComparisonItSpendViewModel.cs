using Web.App.Domain;
using Web.App.ViewModels.Components;

namespace Web.App.ViewModels;

public class SchoolComparisonItSpendViewModel(School school, SchoolComparisonSubCategoriesViewModel subCategories)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolComparisonSubCategoriesViewModel SubCategories => subCategories;
}