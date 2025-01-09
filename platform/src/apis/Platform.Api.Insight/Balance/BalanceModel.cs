using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Balance;

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