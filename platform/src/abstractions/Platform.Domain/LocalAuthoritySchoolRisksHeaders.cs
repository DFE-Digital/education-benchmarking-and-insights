namespace Platform.Domain;

public static class LocalAuthoritySchoolRisksHeaders
{
    public const string SchoolName = nameof(SchoolName);
    public const string Urn = nameof(Urn);
    public const string Overall = nameof(Overall);
    public const string Financial = nameof(Financial);
    public const string SchoolAndPupil = nameof(SchoolAndPupil);
    public const string EducationalPerformance = nameof(EducationalPerformance);

    public static readonly string[] All =
    [
        SchoolName,
        Urn,
        Overall,
        Financial,
        SchoolAndPupil,
        EducationalPerformance
    ];
}
