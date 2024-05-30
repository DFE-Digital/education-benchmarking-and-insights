using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolHistoryViewModel(School school)
{
    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public bool IsPartOfTrust => school.IsPartOfTrust;
}