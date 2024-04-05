using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustResourcesViewModel(Trust trust)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.Name;
}