using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.BudgetForecast.Responses;

[ExcludeFromCodeCoverage]
public record BudgetForecastReturnMetricResponse
{
    public string? RunType { get; set; }
    public string? RunId { get; set; }
    public int? Year { get; set; }
    public string? CompanyNumber { get; set; }
    public string? Metric { get; set; }
    public decimal? Value { get; set; }
}