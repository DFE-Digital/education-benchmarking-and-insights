using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Trust.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public abstract record BalanceResponseBase
{
    public decimal? InYearBalance { get; set; }
    public decimal? RevenueReserve { get; set; }
}


[ExcludeFromCodeCoverage]
public record BalanceHistoryResponse
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public IEnumerable<BalanceHistoryRowResponse> Rows { get; set; } = [];
}

[ExcludeFromCodeCoverage]
public record BalanceHistoryRowResponse : BalanceResponseBase
{
    public int? Year { get; set; }
}

[ExcludeFromCodeCoverage]
public record BalanceResponse : BalanceResponseBase
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public decimal? SchoolInYearBalance { get; set; }
    public decimal? CentralInYearBalance { get; set; }
}