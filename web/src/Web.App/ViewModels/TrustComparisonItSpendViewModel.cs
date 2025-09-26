using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustComparisonItSpendViewModel(
    Trust trust,
    bool? comparatorGenerated,
    string? redirectUri,
    string[]? userDefinedSet)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public bool? ComparatorGenerated => comparatorGenerated;
    public string? RedirectUri => redirectUri;
    public string[]? UserDefinedSet => userDefinedSet;
}