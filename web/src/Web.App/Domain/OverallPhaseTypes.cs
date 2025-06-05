namespace Web.App.Domain;

public static class OverallPhaseTypes
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

    public static string[] All =>
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

    public static string[] AcademyPhases =>
    [
        Primary,
        Secondary,
        Special,
        AllThrough,
        PostSixteen,
        AlternativeProvision,
        UniversityTechnicalCollege
    ];

    public static string[] SendCharacteristicsPhases =>
    [
        Special,
        PupilReferralUnit,
        AlternativeProvision
    ];
}