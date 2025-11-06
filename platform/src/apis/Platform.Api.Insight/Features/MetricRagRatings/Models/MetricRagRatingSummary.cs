using System.Diagnostics.CodeAnalysis;
// ReSharper disable UnusedMember.Global

namespace Platform.Api.Insight.Features.MetricRagRatings.Models;

[ExcludeFromCodeCoverage]
public record MetricRagRatingSummary
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
    public int RedCount { get; set; }
    public int AmberCount { get; set; }
    public int GreenCount { get; set; }
}