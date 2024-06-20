using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolSpendingComparisonViewModel(School school)
{
    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
}
