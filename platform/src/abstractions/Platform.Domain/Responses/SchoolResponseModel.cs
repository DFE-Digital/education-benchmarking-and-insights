using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record SchoolResponseModel
{
    public string? Urn { get; set; }
    public string? Name { get; set; }
    public string? FinanceType { get; set; }
    public string? OverallPhase { get; set; }
    public string? OfstedRating { get; set; }
    public string? Kind { get; set; }
    public string? LaEstab { get; set; }
    public string? Street { get; set; }
    public string? Locality { get; set; }
    public string? Address3 { get; set; }
    public string? Town { get; set; }
    public string? County { get; set; }
    public string? Postcode { get; set; }
    public string? CompanyNumber { get; set; }
    public string? TrustOrCompanyName { get; set; }
    public string? Telephone { get; set; }
    public string? Address { get; set; }
    public string? Website { get; set; }
    public string? LocalAuthorityName { get; set; }
    public bool HasSixthForm { get; set; }

    public static SchoolResponseModel Create(EdubaseDataObject edubase)
    {
        return new SchoolResponseModel
        {
            Urn = edubase.Urn.ToString(),
            Kind = edubase.TypeOfEstablishment,
            FinanceType = edubase.FinanceType,
            Name = edubase.EstablishmentName,
            CompanyNumber = edubase.CompanyNumber.ToString(),
            TrustOrCompanyName = edubase.TrustOrCompanyName,
            OverallPhase = edubase.OverallPhase,
            OfstedRating = edubase.OfstedRating,
            Address = edubase.Address,
            Telephone = edubase.TelephoneNum,
            Website = edubase.SchoolWebsite,
            LocalAuthorityName = edubase.La,
            HasSixthForm = edubase.OfficialSixthFormCode is 1
        };
    }
}