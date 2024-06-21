namespace Platform.Api.Insight.BudgetForecast;

public record BudgetForecastReturnModel
{
    public string? RunType { get; set; }
    public string? RunId { get; set; }
    public int? Year { get; set; }
    public string? CompanyNumber { get; set; }
    public string? Category { get; set; }
    public decimal? Forecast { get; set; }
    public decimal? Actual { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? Slope { get; set; }
    public decimal? Variance { get; set; }
    public decimal? PercentVariance { get; set; }
}

public record BudgetForecastReturnMetricModel
{
    public string? RunType { get; set; }
    public string? RunId { get; set; }
    public int? Year { get; set; }
    public string? CompanyNumber { get; set; }
    public string? Metric { get; set; }
    public decimal? Value { get; set; }
}