using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolResourcesViewModel(School school)
{
    public string? Urn => school.Urn;
    public string? Name => school.Name;
}