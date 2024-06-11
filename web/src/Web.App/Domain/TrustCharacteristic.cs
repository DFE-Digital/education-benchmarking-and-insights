namespace Web.App.Domain;

public record TrustCharacteristic
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }

    public decimal? TotalPupils { get; set; }
    public decimal? SchoolsInTrust { get; set; }
    public decimal? TotalIncome { get; set; }
}

public record TrustCharacteristicUserDefined
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? SchoolsInTrust { get; set; }
    public decimal? TotalIncome { get; set; }
}