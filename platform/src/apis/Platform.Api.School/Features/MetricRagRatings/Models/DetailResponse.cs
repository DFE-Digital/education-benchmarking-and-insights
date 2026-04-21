using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace Platform.Api.School.Features.MetricRagRatings.Models;

[ExcludeFromCodeCoverage]
public record DetailResponse
{
    /// <summary>The Unique Reference Number of the school.</summary>
    public string? URN { get; set; }
    /// <summary>The cost category for the RAG rating.</summary>
    public string? Category { get; set; }
    /// <summary>The specific sub-category for the RAG rating.</summary>
    public string? SubCategory { get; set; }
    /// <summary>The actual financial value for this metric.</summary>
    public decimal? Value { get; set; }
    /// <summary>The median value of the comparator set.</summary>
    public decimal? Median { get; set; }
    /// <summary>The difference between the school's value and the median.</summary>
    public decimal? DiffMedian { get; set; }
    /// <summary>The percentage difference from the median.</summary>
    public decimal? PercentDiff { get; set; }
    /// <summary>The percentile rank within the comparator set (1-100).</summary>
    public decimal? Percentile { get; set; }
    /// <summary>The decile rank within the comparator set (1-10).</summary>
    public decimal? Decile { get; set; }
    /// <summary>The Red-Amber-Green status string.</summary>
    public string? RAG { get; set; }
}