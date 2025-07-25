using Web.App.Domain;
using Web.App.ViewModels.Components;

namespace Web.App.ViewModels;

public class SchoolComparisonItSpendViewModel(School school, SchoolComparisonViewModelCostSubCategories subCategories)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolComparisonViewModelCostSubCategories SubCategories => subCategories;
}