using System.Collections.Generic;

namespace EducationBenchmarking.Platform.Api.Insight.Models;

public class Rating
{
    public string AssessmentArea { get; set; }
    public string Divisor { get; set; }
    public decimal? ScoreLow { get; set; }
    public decimal? ScoreHigh { get; set; }
    public string RatingText { get; set; }
    public string RatingColour { get; set; }
}