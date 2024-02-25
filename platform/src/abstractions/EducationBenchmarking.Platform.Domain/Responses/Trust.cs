using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Domain.DataObjects;

namespace EducationBenchmarking.Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public record Trust
{
    public string? CompanyNumber { get; set; }
    public string? Name { get; set; }


    public static Trust Create(EdubaseDataObject edubase)
    {
        return new Trust
        {
            CompanyNumber = edubase.CompanyNumber.ToString(),
            Name = edubase.TrustOrCompanyName
        };
    }
}