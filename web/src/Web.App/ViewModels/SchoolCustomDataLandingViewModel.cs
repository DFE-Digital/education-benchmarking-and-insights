using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolCustomDataLandingViewModel(School school)
{
    public string? Urn => school.Urn;
    public string? Name => school.Name;
    public bool IsPartOfTrust => school.IsPartOfTrust;
}