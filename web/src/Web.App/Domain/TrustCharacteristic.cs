namespace Web.App.Domain;

public record TrustCharacteristic
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public string? Address { get; set; }

    public double? TotalPupils { get; set; }
    public double? SchoolsInTrust { get; set; }
    public double? TotalIncome { get; set; }
    public string[]? PhasesCovered { get; set; }
    public DateTime? OpenDate { get; set; }
    public double? PercentFreeSchoolMeals { get; set; }
    public double? PercentSpecialEducationNeeds { get; set; }
    public double? TotalInternalFloorArea { get; set; }
}

public record TrustCharacteristicUserDefined
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public string? Address { get; set; }
}