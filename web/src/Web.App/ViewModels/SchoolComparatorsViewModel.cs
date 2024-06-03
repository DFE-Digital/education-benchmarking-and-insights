using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolComparatorsViewModel(School school, string? by = null)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public string? By => by;
}