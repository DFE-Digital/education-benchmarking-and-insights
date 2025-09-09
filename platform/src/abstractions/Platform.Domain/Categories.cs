using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

//TODO: Consider converting these to enums
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

        public static readonly string[] All = { WorkforceFte, TeachersFte, SeniorLeadershipFte, TeachingAssistantsFte, NonClassroomSupportStaffFte, AuxiliaryStaffFte, WorkforceHeadcount, TeachersQualified };

        public static bool IsValid(string? category) => All.Any(a => a == category);
    }

    public static class Cost
    {
        public const string TotalExpenditure = nameof(TotalExpenditure);
        public const string TeachingTeachingSupportStaff = nameof(TeachingTeachingSupportStaff);
        public const string NonEducationalSupportStaff = nameof(NonEducationalSupportStaff);
        public const string EducationalSupplies = nameof(EducationalSupplies);
        public const string EducationalIct = nameof(EducationalIct);
        public const string PremisesStaffServices = nameof(PremisesStaffServices);
        public const string Utilities = nameof(Utilities);
        public const string AdministrationSupplies = nameof(AdministrationSupplies);
        public const string CateringStaffServices = nameof(CateringStaffServices);
        public const string Other = nameof(Other);

        public static readonly string[] All =
        [
            TotalExpenditure,
            TeachingTeachingSupportStaff,
            NonEducationalSupportStaff,
            EducationalSupplies,
            EducationalIct,
            PremisesStaffServices,
            Utilities,
            AdministrationSupplies,
            CateringStaffServices,
            Other
        ];

        public static bool IsValid(string? category) => All.Any(a => a == category);
    }
}