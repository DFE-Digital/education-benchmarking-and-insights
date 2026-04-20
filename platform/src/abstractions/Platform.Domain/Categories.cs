using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

//TODO: Consider converting these to enums
[ExcludeFromCodeCoverage]
public static class Categories
{
    public static class Census
    {
        /// <summary>Total full-time equivalent workforce.</summary>
        public const string WorkforceFte = nameof(WorkforceFte);
        /// <summary>Total full-time equivalent teachers.</summary>
        public const string TeachersFte = nameof(TeachersFte);
        /// <summary>Total full-time equivalent senior leadership staff.</summary>
        public const string SeniorLeadershipFte = nameof(SeniorLeadershipFte);    
        /// <summary>Total full-time equivalent teaching assistants.</summary>
        public const string TeachingAssistantsFte = nameof(TeachingAssistantsFte);
        /// <summary>Total full-time equivalent non-classroom support staff.</summary>
        public const string NonClassroomSupportStaffFte = nameof(NonClassroomSupportStaffFte);
        /// <summary>Total full-time equivalent auxiliary staff.</summary>
        public const string AuxiliaryStaffFte = nameof(AuxiliaryStaffFte);        
        /// <summary>Absolute headcount of the total workforce.</summary>
        public const string WorkforceHeadcount = nameof(WorkforceHeadcount);      
        /// <summary>Total number of qualified teachers.</summary>
        public const string TeachersQualified = nameof(TeachersQualified);        

        public static readonly string[] All =        {
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

    public static class Cost
    {
        /// <summary>The total amount of expenditure across all categories.</summary>
        public const string TotalExpenditure = nameof(TotalExpenditure);
        /// <summary>Costs related to teaching staff and direct educational support.</summary>
        public const string TeachingTeachingSupportStaff = nameof(TeachingTeachingSupportStaff);
        /// <summary>Costs for staff providing non-educational support (e.g., administration, welfare).</summary>
        public const string NonEducationalSupportStaff = nameof(NonEducationalSupportStaff);
        /// <summary>Costs of resources directly used for teaching and learning.</summary>
        public const string EducationalSupplies = nameof(EducationalSupplies);    
        /// <summary>Costs associated with educational Information and Communication Technology hardware and software.</summary>
        public const string EducationalIct = nameof(EducationalIct);
        /// <summary>Costs related to building maintenance, premises staff, and related services.</summary>
        public const string PremisesStaffServices = nameof(PremisesStaffServices);
        /// <summary>Costs for energy and water utilities.</summary>
        public const string Utilities = nameof(Utilities);
        /// <summary>Costs for office supplies, printing, and general administration.</summary>
        public const string AdministrationSupplies = nameof(AdministrationSupplies);
        /// <summary>Costs related to providing school meals and catering staff.</summary>
        public const string CateringStaffServices = nameof(CateringStaffServices);
        /// <summary>Miscellaneous costs not covered by other specific categories.</summary>
        public const string Other = nameof(Other);

        public static readonly string[] All =        [
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