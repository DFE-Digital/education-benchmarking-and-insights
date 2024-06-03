using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolDetailsViewModel(School school)
{
    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? TrustIdentifier => school.TrustCompanyNumber;
    public string? TrustName => school.TrustName;
    public string? Address => school.Address;
    public string? Telephone => school.Telephone;
    public string? LocalAuthorityName => school.LAName;
    public string Website
    {
        get
        {
            var url = school.Website;
            if (!string.IsNullOrEmpty(url) && !url.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
            {
                url = "http://" + url;
            }


            return Uri.IsWellFormedUriString(url, UriKind.Absolute) ? url : "";
        }
    }
}