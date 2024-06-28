using Web.App.Domain;
using Web.App.Infrastructure.Apis;

namespace Web.App.ViewModels;

public class SchoolCustomDataSubmittedViewModel(School school, string customData)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public string Identifier => customData;
}
