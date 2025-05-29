using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustResourcesViewModel(Trust trust, Dictionary<string, CommercialResourceLink[]> resources)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;

    public Dictionary<string, CommercialResourceLink[]> Resources => resources;
}