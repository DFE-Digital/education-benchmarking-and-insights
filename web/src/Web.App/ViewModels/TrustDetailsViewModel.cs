using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustDetailsViewModel(Trust trust, IReadOnlyCollection<School> schools)
{
    public string? Name => trust.Name;
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Uid => trust.Uid;
    public IEnumerable<School> Schools => schools;
}
