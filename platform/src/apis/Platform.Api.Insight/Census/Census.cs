using System.Linq;

namespace Platform.Api.Insight.Census;

public static class CensusCategories
{
    public const string WorkforceFte = nameof(WorkforceFte);
    public const string TeachersFte = nameof(TeachersFte);
    public const string SeniorLeadershipFte = nameof(SeniorLeadershipFte);
    public const string TeachingAssistantsFte = nameof(TeachingAssistantsFte);
    public const string NonClassroomSupportStaffFte = nameof(NonClassroomSupportStaffFte);
    public const string AuxiliaryStaffFte = nameof(AuxiliaryStaffFte);
    public const string WorkforceHeadcount = nameof(WorkforceHeadcount);
    public const string TeachersQualified = nameof(TeachersQualified);

    public static readonly string[] All =
    {
        WorkforceFte,
        TeachersFte,
        SeniorLeadershipFte,
        TeachingAssistantsFte,
        NonClassroomSupportStaffFte,
        AuxiliaryStaffFte,
        WorkforceHeadcount,
        TeachersQualified
    };

    public static bool IsValid(string? category) => All.Any(a => a == category);
}

public static class CensusDimensions
{
    public const string HeadcountPerFte = nameof(HeadcountPerFte);
    public const string Total = nameof(Total);
    public const string PercentWorkforce = nameof(PercentWorkforce);
    public const string PupilsPerStaffRole = nameof(PupilsPerStaffRole);

    public static readonly string[] All =
    {
        HeadcountPerFte,
        Total,
        PercentWorkforce,
        PupilsPerStaffRole
    };

    public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
}