using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolCustomDataLandingViewModel(School school)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public bool IsPartOfTrust => school.IsPartOfTrust;
}