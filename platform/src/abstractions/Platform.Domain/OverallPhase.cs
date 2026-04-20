namespace Platform.Domain;

//TODO: Consider converting these to enums
public static class OverallPhase
{
    /// <summary>Primary education phase (typically ages 5-11).</summary>
    public const string Primary = "Primary";
    /// <summary>Secondary education phase (typically ages 11-16 or 11-18).</summary>
    public const string Secondary = "Secondary";
    /// <summary>Special education phase for pupils with SEN.</summary>
    public const string Special = "Special";
    /// <summary>Pupil Referral Unit for children who are not able to attend a mainstream school.</summary>
    public const string PupilReferralUnit = "Pupil Referral Unit";
    /// <summary>All-through education phase covering both primary and secondary.</summary>
    public const string AllThrough = "All-through";
    /// <summary>Nursery education phase for early years.</summary>
    public const string Nursery = "Nursery";
    /// <summary>Post-16 education phase for further education.</summary>
    public const string PostSixteen = "Post-16";
    /// <summary>Alternative Provision for pupils who cannot attend mainstream schools.</summary>
    public const string AlternativeProvision = "Alternative Provision";
    /// <summary>University Technical College for technical and vocational education.</summary>
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