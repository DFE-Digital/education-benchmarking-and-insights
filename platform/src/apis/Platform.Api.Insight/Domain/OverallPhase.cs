using System;
using System.Linq;
namespace Platform.Api.Insight.Domain;

public static class OverallPhase
{
    internal const string Primary = "Primary";
    private const string Secondary = "Secondary";
    private const string Special = "Special";
    private const string PupilReferralUnit = "Pupil Referral Unit";
    private const string AllThrough = "All-through";
    private const string Nursery = "Nursery";
    private const string PostSixteen = "Post-16";
    private const string AlternativeProvision = "Alternative Provision";
    private const string UniversityTechnicalCollege = "University Technical College";

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

    public static bool IsValid(string? overallPhase) => All.Any(a => a.Equals(overallPhase, StringComparison.OrdinalIgnoreCase));
}