namespace Web.App.Domain;

// todo: unit test
public static class OverallPhaseTypes
{
    public enum OverallPhaseTypeFilter
    {
        Primary = 0,
        Secondary = 1,
        Special = 2,
        PupilReferralUnit = 3,
        AllThrough = 4,
        Nursery = 5,
        PostSixteen = 6,
        AlternativeProvision = 7,
        UniversityTechnicalCollege = 8
    }

    public const string Primary = "Primary";
    public const string Secondary = "Secondary";
    public const string Special = "Special";
    public const string PupilReferralUnit = "Pupil Referral Unit";
    public const string AllThrough = "All-through";
    public const string Nursery = "Nursery";
    public const string PostSixteen = "Post-16";
    public const string AlternativeProvision = "Alternative Provision";
    public const string UniversityTechnicalCollege = "University Technical College";

    public static readonly OverallPhaseTypeFilter[] AllFilters =
    [
        OverallPhaseTypeFilter.Primary,
        OverallPhaseTypeFilter.Secondary,
        OverallPhaseTypeFilter.Special,
        OverallPhaseTypeFilter.PupilReferralUnit,
        OverallPhaseTypeFilter.AllThrough,
        OverallPhaseTypeFilter.Nursery,
        OverallPhaseTypeFilter.PostSixteen,
        OverallPhaseTypeFilter.AlternativeProvision,
        OverallPhaseTypeFilter.UniversityTechnicalCollege
    ];

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

    public static string GetFilterDescription(this OverallPhaseTypeFilter filter) => filter switch
    {
        OverallPhaseTypeFilter.Primary => Primary,
        OverallPhaseTypeFilter.Secondary => Secondary,
        OverallPhaseTypeFilter.Special => Special,
        OverallPhaseTypeFilter.PupilReferralUnit => PupilReferralUnit,
        OverallPhaseTypeFilter.AllThrough => AllThrough,
        OverallPhaseTypeFilter.Nursery => Nursery,
        OverallPhaseTypeFilter.PostSixteen => PostSixteen,
        OverallPhaseTypeFilter.AlternativeProvision => AlternativeProvision,
        OverallPhaseTypeFilter.UniversityTechnicalCollege => UniversityTechnicalCollege,
        _ => throw new ArgumentException(nameof(filter))
    };
}