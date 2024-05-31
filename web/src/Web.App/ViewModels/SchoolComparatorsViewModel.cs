using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolComparatorsViewModel(School school)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
}