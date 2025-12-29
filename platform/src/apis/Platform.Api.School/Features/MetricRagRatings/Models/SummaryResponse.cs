using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedMember.Global

namespace Platform.Api.School.Features.MetricRagRatings.Models;

[ExcludeFromCodeCoverage]
public record SummaryResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
    public int RedCount { get; set; }
    public int AmberCount { get; set; }
    public int GreenCount { get; set; }
}