using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedMember.Global

namespace Platform.Api.School.Features.MetricRagRatings.Models;

[ExcludeFromCodeCoverage]
public record SummaryResponse
{
    /// <summary>The Unique Reference Number of the school.</summary>
    public string? URN { get; set; }
    /// <summary>The name of the establishment.</summary>
    public string? SchoolName { get; set; }
    /// <summary>The educational phase of the school.</summary>
    public string? OverallPhase { get; set; }
    /// <summary>The number of metrics rated as Red.</summary>
    public int RedCount { get; set; }
    /// <summary>The number of metrics rated as Amber.</summary>
    public int AmberCount { get; set; }
    /// <summary>The number of metrics rated as Green.</summary>
    public int GreenCount { get; set; }
}