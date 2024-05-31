using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record School
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? FinanceType { get; set; }
    public string? OverallPhase { get; set; }
    public string? SchoolType { get; set; }
    public bool HasSixthForm { get; set; }
    public bool HasNursery { get; set; }
    public bool IsPFISchool { get; set; }
    public DateTime? OfstedDate { get; set; }
    public string? OfstedDescription { get; set; }
    public string? Telephone { get; set; }
    public string? Website { get; set; }
    public string? ContactEmail { get; set; }
    public string? HeadteacherName { get; set; }
    public string? HeadteacherEmail { get; set; }
    public string? TrustCompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public string? FederationLeadURN { get; set; }
    public string? FederationLeadName { get; set; }
    public string? LACode { get; set; }
    public string? LAName { get; set; }
    public string? LondonWeighting { get; set; }
    public string? AddressStreet { get; set; }
    public string? AddressLocality { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressTown { get; set; }
    public string? AddressCounty { get; set; }
    public string? AddressPostcode { get; set; }

    public bool IsPrimary => OverallPhase == OverallPhaseTypes.Primary;
    public bool IsPartOfTrust => !string.IsNullOrEmpty(TrustCompanyNumber);
}