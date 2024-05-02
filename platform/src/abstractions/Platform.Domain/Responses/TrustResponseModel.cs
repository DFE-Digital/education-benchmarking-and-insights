using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record TrustResponseModel
{
    public string? CompanyNumber { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Telephone { get; set; }
    public string? LocalAuthority { get; set; }
    public string? Website { get; set; }
    public string? Uid { get; set; }

    public static TrustResponseModel Create(EdubaseDataObject edubase)
    {
        return new TrustResponseModel
        {
            CompanyNumber = edubase.CompanyNumber.ToString(),
            Name = edubase.TrustOrCompanyName,
            Address = edubase.Address,
            Telephone = edubase.TelephoneNum,
            LocalAuthority = edubase.La,
            Website = edubase.SchoolWebsite,
            Uid = edubase.Uid.ToString(),
        };
    }
}