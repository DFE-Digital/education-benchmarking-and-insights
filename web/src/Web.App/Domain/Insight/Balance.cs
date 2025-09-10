// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Web.App.Domain;

public abstract record BalanceBase
{
    public decimal? InYearBalance { get; set; }
    public decimal? RevenueReserve { get; set; }
}

public record SchoolBalance : BalanceBase
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
}

public record TrustBalance : BalanceBase
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public decimal? SchoolInYearBalance { get; set; }
    public decimal? CentralInYearBalance { get; set; }
}

public record BalanceHistory : BalanceBase
{
    public int? Year { get; set; }
}

public record BalanceHistoryRows
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public IEnumerable<BalanceHistory> Rows { get; set; } = [];
}