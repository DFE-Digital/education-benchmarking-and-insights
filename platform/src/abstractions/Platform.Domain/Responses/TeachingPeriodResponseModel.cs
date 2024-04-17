using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record TeachingPeriodResponseModel
{
    public string? PeriodName { get; set; }
    public string? PeriodsPerTimetable { get; set; }
}