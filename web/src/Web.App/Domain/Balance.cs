namespace Web.App.Domain;

public record Balance
{
    public int YearEnd { get; set; }
    public string? Term { get; set; }
    public string? Dimension { get; set; }
    public decimal? InYearBalance { get; set; }
    public decimal? RevenueReserve { get; set; }
}