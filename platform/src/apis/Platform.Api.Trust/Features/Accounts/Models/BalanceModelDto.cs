using System.Diagnostics.CodeAnalysis;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.Trust.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public abstract record BalanceModelBase
{
    public decimal? InYearBalance { get; set; }
    public decimal? RevenueReserve { get; set; }
}

[ExcludeFromCodeCoverage]
public record BalanceHistoryModelDto : BalanceModelBase
{
    public int? RunId { get; set; }
}

[ExcludeFromCodeCoverage]
public record BalanceModelDto : BalanceModelBase
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public decimal? InYearBalanceSchool { get; set; }
    public decimal? InYearBalanceCS { get; set; }
}