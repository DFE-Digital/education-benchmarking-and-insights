using Web.App.Domain;
using Web.App.Domain.Content;

namespace Web.App.ViewModels;

public class LocalAuthorityResourcesViewModel(LocalAuthority localAuthority, Dictionary<string, CommercialResourceLink[]> resources)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;

    public Dictionary<string, CommercialResourceLink[]> Resources => resources;
}