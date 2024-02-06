using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace EducationBenchmarking.Platform.Domain.DataObjects;

[ExcludeFromCodeCoverage]
public record SchoolRatingDataObject
{
    public string? Term { get; set; }
    public string? OverallPhase { get; set; }
    public string? AssessmentArea { get; set; }
    public string? Divisor { get; set; }
    public string? Size { get; set; }
    [JsonProperty("FSM")] public string? Fsm { get; set; }
    public decimal? ScoreLow { get; set; }
    public decimal? ScoreHigh { get; set; }
    public string? RatingText { get; set; }
    public string? RatingColour { get; set; }
}