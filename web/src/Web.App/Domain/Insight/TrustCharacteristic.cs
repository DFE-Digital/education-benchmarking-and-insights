namespace Web.App.Domain;

public record TrustCharacteristic
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? SchoolsInTrust { get; set; }
    public DateTime? OpenDate { get; set; }
    public decimal? PercentFreeSchoolMeals { get; set; }
    public decimal? PercentSpecialEducationNeeds { get; set; }
    public decimal? TotalInternalFloorArea { get; set; }
    public TrustPhase[] Phases { get; set; } = [];
}

public record TrustCharacteristicUserDefined
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? SchoolsInTrust { get; set; }
    public TrustPhase[] Phases { get; set; } = [];
}

public record TrustPhase
{
    public string? Phase { get; set; }
    public int? Count { get; set; }
}