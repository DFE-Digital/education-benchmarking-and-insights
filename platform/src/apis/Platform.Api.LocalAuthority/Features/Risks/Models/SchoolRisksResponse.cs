using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Risks.Models;

[ExcludeFromCodeCoverage]
public record SchoolRiskResponse
{
    public string Urn { get; init; } = string.Empty;
    public string SchoolName { get; init; } = string.Empty;
    public string LaCode { get; init; } = string.Empty;
    public string OverallGrade { get; init; } = string.Empty;
    public decimal Overall { get; init; }
    public decimal Financial { get; init; }
    public decimal SchoolAndPupil { get; init; }
    public decimal EducationalPerformance { get; init; }
}
