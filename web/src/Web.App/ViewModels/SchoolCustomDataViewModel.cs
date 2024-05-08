using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolCustomDataViewModel(School school)
{
    public string? Urn => school.Urn;
    public string? Name => school.Name;
    public bool IsPartOfTrust => school.IsPartOfTrust;
}