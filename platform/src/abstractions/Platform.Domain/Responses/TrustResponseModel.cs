using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record TrustResponseModel
{
    public string? CompanyNumber { get; set; }
    public string? Name { get; set; }
    public string? Uid { get; set; }

    public static TrustResponseModel Create(EdubaseDataObject edubase)
    {
        return new TrustResponseModel
        {
            CompanyNumber = edubase.CompanyNumber.ToString(),
            Name = edubase.TrustOrCompanyName,
            Uid = edubase.Uid.ToString(),
        };
    }
}