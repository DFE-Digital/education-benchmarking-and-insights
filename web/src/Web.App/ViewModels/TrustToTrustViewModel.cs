using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustToTrustViewModel(
    Trust trust,
    bool? comparatorGenerated)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public bool? ComparatorGenerated => comparatorGenerated;
}