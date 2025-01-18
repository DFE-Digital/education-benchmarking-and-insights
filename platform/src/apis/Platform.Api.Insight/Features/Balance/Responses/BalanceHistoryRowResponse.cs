using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Api.Insight.Features.Balance.Responses;

[ExcludeFromCodeCoverage]
public record BalanceHistoryRowResponse : BalanceResponse
{
    public int? Year { get; set; }
}