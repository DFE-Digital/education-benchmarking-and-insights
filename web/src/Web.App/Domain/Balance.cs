namespace Web.App.Domain;

public record Balance
{
    public int YearEnd { get; set; }
    public string? Urn { get; set; }
    public string? Name { get; set; }
    public string? SchoolType { get; set; }
    public string? LocalAuthority { get; set; }
    public decimal NumberPupils { get; set; }
    public BalancePayload? Payload { get; set; }

    public record BalancePayload
    {
        public string? Dimension { get; set; }
        public decimal InYearBalance { get; set; }
        public decimal RevenueReserve { get; set; }
    }
}