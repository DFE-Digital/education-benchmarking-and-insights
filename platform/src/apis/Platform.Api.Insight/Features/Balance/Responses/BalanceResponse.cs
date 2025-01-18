using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Api.Insight.Features.Balance.Responses;

[ExcludeFromCodeCoverage]
public abstract record BalanceResponse
{
    public decimal? InYearBalance { get; set; }
    public decimal? RevenueReserve { get; set; }
}