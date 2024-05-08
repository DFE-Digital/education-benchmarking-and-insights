using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustDetailsViewModel(Trust trust, IReadOnlyCollection<School> schools)
{
    public string? Name => trust.Name;
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Address => trust.Address;
    public string? Telephone => trust.Telephone;
    public string? LocalAuthority => trust.LocalAuthority;
    public string? Uid => trust.Uid;
    public IEnumerable<School> Schools => schools;
    public string? Website
    {
        get
        {
            var url = trust.Website;
            if (!string.IsNullOrEmpty(url) && !url.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
            {
                url = "http://" + url;
            }


            return Uri.IsWellFormedUriString(url, UriKind.Absolute) ? url : "";
        }
    }
}
