using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Api.School.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public abstract record BalanceResponseBase
{
    public decimal? InYearBalance { get; set; }
    public decimal? RevenueReserve { get; set; }
}

public record BalanceResponse : BalanceResponseBase
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
}

public record BalanceHistoryRowResponse : BalanceResponse
{
    public int? Year { get; set; }
}

public record BalanceHistoryResponse
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public IEnumerable<BalanceHistoryRowResponse> Rows { get; set; } = [];
}