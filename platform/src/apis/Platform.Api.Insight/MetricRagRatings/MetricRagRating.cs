using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Insight.MetricRagRatings;

[ExcludeFromCodeCoverage]
public record MetricRagRating
{
    public string? URN { get; set; }
    public string? Category { get; set; }
    public string? SubCategory { get; set; }
    public decimal? Value { get; set; }
    public decimal? Mean { get; set; }
    public decimal? DiffMean { get; set; }
    public decimal? PercentDiff { get; set; }
    public decimal? Percentile { get; set; }
    public decimal? Decile { get; set; }
    public string? RAG { get; set; }
}