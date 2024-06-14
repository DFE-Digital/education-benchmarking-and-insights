namespace Web.App.Domain.Benchmark;

public class FinancialPlan
{
    public int Year { get; set; }
    public string? Urn { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsComplete { get; set; }
    public decimal? TeacherContactRatio { get; set; }
    public string? ContactRatioRating { get; set; }
    public decimal? InYearBalance { get; set; }
    public string? InYearBalancePercentIncomeRating { get; set; }
    public decimal? AverageClassSize { get; set; }
    public string? AverageClassSizeRating { get; set; }
}