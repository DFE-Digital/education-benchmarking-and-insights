using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public static class Categories
{
    public static class Census
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
}