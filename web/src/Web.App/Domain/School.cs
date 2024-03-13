using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record School
{
    public string? Urn { get; set; }
    public string? Name { get; set; }
    public string? FinanceType { get; set; }
    public string? Kind { get; set; }
    public string? LaEstab { get; set; }
    public string? Address { get; set; }
    public string? Street { get; set; }
    public string? Locality { get; set; }
    public string? Address3 { get; set; }
    public string? Town { get; set; }
    public string? County { get; set; }
    public string? Postcode { get; set; }
    public string? LocalAuthorityName { get; set; }
    public string? CompanyNumber { get; set; }
    public string? TrustOrCompanyName { get; set; }
    public string? Telephone { get; set; }
    public string? Website { get; set; }
    public string? OverallPhase { get; set; }
    public string? OfstedRating { get; set; }
    public bool HasSixthForm { get; set; }

    public bool IsPrimary => OverallPhase == OverallPhaseTypes.Primary;
    public bool IsPartOfTrust => !string.IsNullOrEmpty(CompanyNumber);
}