using Web.App.Domain;

namespace Web.App.ViewModels
{
    public class SchoolViewModel(School school)
    {
        public string? Name => school.Name;
        public string? Urn => school.Urn;
        public bool IsPartOfTrust => school.IsPartOfTrust;
        public string? TrustIdentifier => school.CompanyNumber;
        public string? TrustName => school.TrustOrCompanyName;
    }
}