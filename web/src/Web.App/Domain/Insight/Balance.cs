namespace Web.App.Domain;

public abstract record BalanceBase
{
    public decimal? SchoolInYearBalance { get; set; }
    public decimal? CentralInYearBalance { get; set; }
    public decimal? InYearBalance { get; set; }

    public decimal? SchoolRevenueReserve { get; set; }
    public decimal? CentralRevenueReserve { get; set; }
    public decimal? RevenueReserve { get; set; }
}

public record SchoolBalance : BalanceBase
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public decimal? TotalPupils { get; set; }
}

public record TrustBalance : BalanceBase
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}

public record BalanceHistory : BalanceBase
{
    public int? Year { get; set; }
    public string? Term { get; set; }
}

public record BudgetForecastReturn
{
    public string? RunType { get; set; }
    public string? RunId { get; set; }
    public int? Year { get; set; }
    public string? CompanyNumber { get; set; }
    public string? Category { get; set; }
    public decimal? Forecast { get; set; }
    public decimal? Actual { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? Slope { get; set; }
    public decimal? Variance { get; set; }
    public decimal? PercentVariance { get; set; }
}

public record BudgetForecastReturnMetric
{
    public string? RunType { get; set; }
    public string? RunId { get; set; }
    public int? Year { get; set; }
    public string? CompanyNumber { get; set; }
    public string? Metric { get; set; }
    public decimal? Value { get; set; }
}