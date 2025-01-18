using System.Diagnostics.CodeAnalysis;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.Insight.Features.Balance.Models;

[ExcludeFromCodeCoverage]
public record BalanceTrustModel : BalanceModel
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public decimal? InYearBalanceSchool { get; set; }
    public decimal? InYearBalanceCS { get; set; }
}