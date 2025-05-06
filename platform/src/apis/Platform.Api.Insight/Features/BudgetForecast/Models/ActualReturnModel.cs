using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.BudgetForecast.Models;

[ExcludeFromCodeCoverage]
public record ActualReturnModel
{
    public int Year { get; set; }
    public decimal? Value { get; set; }
    public decimal? TotalPupils { get; set; }
}