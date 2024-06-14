namespace Web.App.Domain;

// public record Balance
// {
//     public int YearEnd { get; set; }
//     public string? Term { get; set; }
//     public string? Dimension { get; set; }
//     public decimal? InYearBalance { get; set; }
//     public decimal? RevenueReserve { get; set; }
// }

public abstract record BalanceBase
{
    public decimal? SchoolInYearBalance { get; set; }
    public decimal? CentralInYearBalance { get; set; }
    public decimal? TotalInYearBalance { get; set; }
    public decimal? SchoolRevenueReserve { get; set; }
    public decimal? CentralRevenueReserve { get; set; }
    public decimal? TotalRevenueReserve { get; set; }
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