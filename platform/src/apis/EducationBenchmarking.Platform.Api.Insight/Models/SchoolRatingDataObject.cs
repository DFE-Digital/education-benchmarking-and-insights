namespace EducationBenchmarking.Platform.Api.Insight.Models;

public class SchoolRatingDataObject
{
    public string Term { get; set; }
    public string OverallPhase { get; set; }
    public string AssessmentArea { get; set; }
    public string Divisor { get; set; }
    public string Size { get; set; }
    public string FSM { get; set; }
    public decimal? ScoreLow { get; set; }
    public decimal? ScoreHigh { get; set; }
    public string RatingText { get; set; }
    public string RatingColour { get; set; }
}