using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Income.Models;

[ExcludeFromCodeCoverage]
public record IncomeSchoolModel : IncomeModel
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
}