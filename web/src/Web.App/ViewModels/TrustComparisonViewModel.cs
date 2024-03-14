using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustComparisonViewModel(Trust trust)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.Name;
}