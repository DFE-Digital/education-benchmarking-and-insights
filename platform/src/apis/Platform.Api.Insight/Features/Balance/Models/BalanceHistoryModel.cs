using System.Diagnostics.CodeAnalysis;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.Insight.Features.Balance.Models;

[ExcludeFromCodeCoverage]
public record BalanceHistoryModel : BalanceModel
{
    public int? RunId { get; set; }
}