using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.BudgetForecast.Models;

[ExcludeFromCodeCoverage]
public record BudgetForecastReturnMetricModel
{
    public int Year { get; set; }
    public string? Metric { get; set; }
    public decimal? Value { get; set; }
}