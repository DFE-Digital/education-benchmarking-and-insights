using System.Diagnostics.CodeAnalysis;
namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public static class OverallPhaseTypes
{
    public const string Primary = "Primary";
    public const string Secondary = "Secondary";
    public const string Special = "Special";
    public const string PupilReferralUnit = "Pupil referral unit";
    public const string AllThrough = "All-through";
    public const string Nursery = "Nursery";
    public const string PostSixteen = "Post-16";
    public const string AlternativeProvision = "Alternative Provision";
    public const string UniversityTechnicalCollege = "University technical college";

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
}