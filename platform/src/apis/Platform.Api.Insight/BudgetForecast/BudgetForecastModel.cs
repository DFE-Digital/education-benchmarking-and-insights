using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Insight.BudgetForecast;

[ExcludeFromCodeCoverage]
public record BudgetForecastReturnModel
{
    public int Year { get; set; }
    public decimal? Value { get; set; }
    public decimal? TotalPupils { get; set; }
}

[ExcludeFromCodeCoverage]
public record BudgetForecastReturnMetricModel
{
    public int Year { get; set; }
    public string? Metric { get; set; }
    public decimal? Value { get; set; }
}

[ExcludeFromCodeCoverage]
public record ActualReturnModel
{
    public int Year { get; set; }
    public decimal? Value { get; set; }
    public decimal? TotalPupils { get; set; }
}
