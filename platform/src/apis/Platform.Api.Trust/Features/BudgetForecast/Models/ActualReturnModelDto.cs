using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Trust.Features.BudgetForecast.Models;

[ExcludeFromCodeCoverage]
// ReSharper disable once ClassNeverInstantiated.Global
public record ActualReturnModelDto
{
    public int Year { get; set; }
    public decimal? Value { get; set; }
    public decimal? TotalPupils { get; set; }
}