using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Api.Insight.Features.Balance;

[ExcludeFromCodeCoverage]
public abstract record BalanceResponse
{
    public decimal? InYearBalance { get; set; }
    public decimal? RevenueReserve { get; set; }
}

[ExcludeFromCodeCoverage]
public record BalanceSchoolResponse : BalanceResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
}

[ExcludeFromCodeCoverage]
public record BalanceTrustResponse : BalanceResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public decimal? SchoolInYearBalance { get; set; }
    public decimal? CentralInYearBalance { get; set; }
}

[ExcludeFromCodeCoverage]
public record BalanceHistoryResponse
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public IEnumerable<BalanceHistoryRowResponse> Rows { get; set; } = [];
}


[ExcludeFromCodeCoverage]
public record BalanceHistoryRowResponse : BalanceResponse
{
    public int? Year { get; set; }
}