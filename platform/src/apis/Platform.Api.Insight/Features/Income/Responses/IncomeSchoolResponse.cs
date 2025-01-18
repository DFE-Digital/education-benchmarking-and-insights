using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Income.Responses;

[ExcludeFromCodeCoverage]
public record IncomeSchoolResponse : IncomeResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
}