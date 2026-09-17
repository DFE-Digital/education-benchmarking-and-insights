using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School.Features.Risks.Models;

[ExcludeFromCodeCoverage]
public record SchoolRisksResponse
{
    public string Urn { get; init; } = string.Empty;
    public string SchoolName { get; init; } = string.Empty;
    public string OverallGrade { get; init; } = string.Empty;
    public decimal Overall { get; init; }
    public decimal OverallMax { get; init; }
    public decimal Financial { get; init; }
    public decimal FinancialMax { get; init; }
    public decimal SchoolAndPupil { get; init; }
    public decimal SchoolAndPupilMax { get; init; }
    public decimal EducationalPerformance { get; init; }
    public decimal EducationalPerformanceMax { get; init; }
}
