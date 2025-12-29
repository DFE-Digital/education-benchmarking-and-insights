using System.Diagnostics.CodeAnalysis;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.School.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public abstract record BalanceModelBase
{
    public decimal? InYearBalance { get; set; }
    public decimal? RevenueReserve { get; set; }
}

public record BalanceHistoryModelDto : BalanceModelBase
{
    public int? RunId { get; set; }
}

public record BalanceModelDto : BalanceModelBase
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
}