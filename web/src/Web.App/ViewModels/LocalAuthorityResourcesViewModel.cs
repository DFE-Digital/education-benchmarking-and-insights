using Web.App.Domain;

namespace Web.App.ViewModels;

public class LocalAuthorityResourcesViewModel(LocalAuthority localAuthority)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
}
