using Web.App.Domain;

namespace Web.App.ViewModels;

public class LocalAuthorityResourcesViewModel(LocalAuthority localAuthority, IEnumerable<CommercialResources> resources)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;

    public IEnumerable<GroupedResources> GroupedResources => CommercialResourcesBuilder.GroupByValidCategory(resources);
    public (string? Title, string? Url)? AllResourcesFramework => CommercialResourcesBuilder.GetFindAFrameworkLink(resources);
}