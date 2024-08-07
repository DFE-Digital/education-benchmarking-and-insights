using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Insight.Balance;

[ExcludeFromCodeCoverage]
public abstract record BalanceBaseModel
{
    public decimal? TotalPupils { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? InYearBalance { get; }
    public decimal? RevenueReserve { get; }
    public decimal? TotalIncomeCS { get; set; }
    public decimal? TotalExpenditureCS { get; set; }
    public decimal? InYearBalanceCS { get; }
    public decimal? RevenueReserveCS { get; }
}

[ExcludeFromCodeCoverage]
public record SchoolBalanceModel : BalanceBaseModel
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}

[ExcludeFromCodeCoverage]
public record SchoolBalanceHistoryModel : BalanceBaseModel
{
    public string? URN { get; set; }
    public int? Year { get; set; }
}

[ExcludeFromCodeCoverage]
public record TrustBalanceModel : BalanceBaseModel
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}

[ExcludeFromCodeCoverage]
public record TrustBalanceHistoryModel : BalanceBaseModel
{
    public string? CompanyNumber { get; set; }
    public int? Year { get; set; }
}