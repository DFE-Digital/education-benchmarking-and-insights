using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public record Rating
{
    public string? AssessmentArea { get; set; }
    public string? Divisor { get; set; }
    public decimal? ScoreLow { get; set; }
    public decimal? ScoreHigh { get; set; }
    public string? RatingText { get; set; }
    public string? RatingColour { get; set; }
}