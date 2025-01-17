using System.Diagnostics.CodeAnalysis;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.Insight.Features.Balance;

[ExcludeFromCodeCoverage]
public record BalanceYearsModel
{
    public int StartYear { get; set; }
    public int EndYear { get; set; }
}

[ExcludeFromCodeCoverage]
public abstract record BalanceModel
{
    public decimal? InYearBalance { get; set; }
    public decimal? RevenueReserve { get; set; }
}

[ExcludeFromCodeCoverage]
public record BalanceSchoolModel : BalanceModel
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
}

[ExcludeFromCodeCoverage]
public record BalanceHistoryModel : BalanceModel
{
    public int? RunId { get; set; }
}

[ExcludeFromCodeCoverage]
public record BalanceTrustModel : BalanceModel
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public decimal? InYearBalanceSchool { get; set; }
    public decimal? InYearBalanceCS { get; set; }
}