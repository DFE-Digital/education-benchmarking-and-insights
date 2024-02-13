using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolViewModel(School school)
{
    public string? Name => school.Name;
    public string? Urn => school.Urn;
    public bool IsPartOfTrust => !string.IsNullOrEmpty(school.CompanyNumber);
    public string? TrustIdentifier => school.CompanyNumber;
    public string? TrustName => school.TrustOrCompanyName;
}