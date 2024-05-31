namespace Platform.Api.Insight.Schools;

public record SchoolCharacteristic
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? AddressStreet { get; set; }
    public string? AddressLocality { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressTown { get; set; }
    public string? AddressCounty { get; set; }
    public string? AddressPostcode { get; set; }
    public string? OverallPhase { get; set; }
    public decimal? TotalPupils { get; set; }
    public string? FinanceType { get; set; }
    public string? LocalAuthority { get; set; }
    public decimal? PercentFreeSchoolMeals { get; set; }
    public decimal? PercentSpecialEducationNeeds { get; set; }
    public string? LondonWeighting { get; set; }
    public int? AverageBuildingAge { get; set; }
    public int? GrossInternalFloorArea { get; set; }
    public string? OfstedRating { get; set; }
    public int? NumberSchoolsInTrust { get; set; }
    public string? SchoolPosition { get; set; }
    public bool? PrivateFinanceInitiative { get; set; }
    public int? NumberOfPupilsSixthForm { get; set; }
    public decimal? KeyStage2Progress { get; set; }
    public decimal? KeyStage4Progress { get; set; }
}