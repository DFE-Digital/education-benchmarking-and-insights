using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School.Features.Risks.Models;

[ExcludeFromCodeCoverage]
public abstract record RisksHistoryBase
{
    public string Urn { get; init; } = string.Empty;
    public string SchoolName { get; init; } = string.Empty;
    public string OverallGrade { get; init; } = string.Empty;
    public decimal Overall { get; init; }
    public decimal Financial { get; init; }
    public decimal SchoolAndPupil { get; init; }
    public decimal EducationalPerformance { get; init; }
}

public record RisksHistoryModelDto : RisksHistoryBase
{
    public int? RunId { get; init; }
}
