using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Domain;

[ExcludeFromCodeCoverage]
public record Trust
{
    public string CompanyNumber { get; set; }
    public string Name { get; set; }
}