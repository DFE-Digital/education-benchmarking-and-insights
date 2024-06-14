namespace Platform.Api.Insight.Balance;

public abstract record BalanceBaseModel
{
    public decimal? TotalPupils { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? InYearBalance { get; private set; }
    public decimal? RevenueReserve { get; private set; }
    public decimal? TotalIncomeCS { get; set; }
    public decimal? TotalExpenditureCS { get; set; }
    public decimal? InYearBalanceCS { get; private set; }
    public decimal? RevenueReserveCS { get; private set; }
}

public record SchoolBalanceModel : BalanceBaseModel
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}

public record SchoolBalanceHistoryModel : BalanceBaseModel
{
    public string? URN { get; set; }
    public int? Year { get; set; }
}

public record TrustBalanceModel : BalanceBaseModel
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}

public record TrustBalanceHistoryModel : BalanceBaseModel
{
    public string? CompanyNumber { get; set; }
    public int? Year { get; set; }
}