using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustResourcesViewModel(Trust trust, IEnumerable<CommercialResources> resources)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;

    public IEnumerable<GroupedResources> GroupedResources => CommercialResourcesBuilder.GroupByValidCategory(resources);
    public (string? Title, string? Url)? AllResourcesFramework => CommercialResourcesBuilder.GetFindAFrameworkLink(resources);
}