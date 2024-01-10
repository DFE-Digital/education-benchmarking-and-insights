using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public class SchoolWorkforce
{
    public string Urn { get; set; }
    public string Name { get; set; }
    public string FinanceType { get; set; }
    public string LocalAuthority { get; set; }
}


