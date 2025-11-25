using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolDetailsViewModel(School school, string? giasSchoolUrl)
{
    protected readonly School School = school;

    public string? Name => School.SchoolName;
    public string? Urn => School.URN;
    public bool IsPartOfTrust => School.IsPartOfTrust;
    public string? Address => School.Address;
    public string? Telephone => School.Telephone;
    public string? LocalAuthorityName => School.LAName;
    public string? TrustIdentifier => School.TrustCompanyNumber;
    public string? TrustName => School.TrustName;
    public string? GiasSchoolUrl => giasSchoolUrl;

    public string Website
    {
        get
        {
            var url = School.Website;
            if (!string.IsNullOrEmpty(url) && !url.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
            {
                url = "http://" + url;
            }

            return Uri.IsWellFormedUriString(url, UriKind.Absolute) ? url : "";
        }
    }
}