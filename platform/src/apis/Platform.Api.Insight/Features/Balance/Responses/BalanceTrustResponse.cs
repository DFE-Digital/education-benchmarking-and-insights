using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Api.Insight.Features.Balance.Responses;

[ExcludeFromCodeCoverage]
public record BalanceTrustResponse : BalanceResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public decimal? SchoolInYearBalance { get; set; }
    public decimal? CentralInYearBalance { get; set; }
}