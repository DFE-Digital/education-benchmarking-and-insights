using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Trust.Features.BudgetForecast.Models;

[ExcludeFromCodeCoverage]
// ReSharper disable once ClassNeverInstantiated.Global
public record BudgetForecastReturnMetricModelDto
{
    public int Year { get; set; }
    public string? Metric { get; set; }
    public decimal? Value { get; set; }
}