using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record TeachingPeriodDataObject
{
    [JsonProperty("periodName")] public string? PeriodName { get; set; }
    [JsonProperty("periodsPerTimetable")] public string? PeriodsPerTimetable { get; set; }
}