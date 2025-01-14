namespace Platform.Domain;

public static class OverallPhase
{
    public const string Primary = "Primary";
    public const string Secondary = "Secondary";
    public const string Special = "Special";
    public const string PupilReferralUnit = "Pupil Referral Unit";
    public const string AllThrough = "All-through";
    public const string Nursery = "Nursery";
    public const string PostSixteen = "Post-16";
    public const string AlternativeProvision = "Alternative Provision";
    public const string UniversityTechnicalCollege = "University Technical College";

    public static readonly string[] All =
    [
        Primary,
        Secondary,
        Special,
        PupilReferralUnit,
        AllThrough,
        Nursery,
        PostSixteen,
        AlternativeProvision,
        UniversityTechnicalCollege
    ];


    //TODO: Add unit test coverage
    public static bool IsValid(string? overallPhase) => All.Any(a => a.Equals(overallPhase, StringComparison.OrdinalIgnoreCase));
}