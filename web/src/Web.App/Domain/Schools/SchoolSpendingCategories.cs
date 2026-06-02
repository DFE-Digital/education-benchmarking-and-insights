// ReSharper disable ConvertToExtensionBlock
namespace Web.App.Domain.Schools;

public static class SchoolSpendingCategories
{
    public enum CategoryGroup
    {
        TotalExpenditure,
        TeachingStaff,
        NonEducationalSupportStaff,
        EducationalSupplies,
        EducationalIct,
        PremisesStaffServices,
        Utilities,
        AdministrativeSupplies,
        CateringStaffServices,
        Other
    }

    public enum SubCategoryFilter
    {
        TotalExpenditure = 0,
        TotalTeachingSupportStaffCosts = 1,
        TeachingStaffCosts = 2,
        SupplyTeachingStaffCosts = 3,
        EducationalConsultancyCosts = 4,
        EducationSupportStaffCosts = 5,
        AgencySupplyTeachingStaffCosts = 6,
        TotalNonEducationalSupportStaffCosts = 7,
        AdministrativeClericalStaffCosts = 8,
        AuditorsCosts = 9,
        OtherStaffCosts = 10,
        ProfessionalServicesNonCurriculumCosts = 11,
        TotalEducationalSuppliesCosts = 12,
        ExaminationFeesCosts = 13,
        LearningResourcesNonIctCosts = 14,
        LearningResourcesIctCosts = 15,
        TotalPremisesStaffServiceCosts = 16,
        CleaningCaretakingCosts = 17,
        MaintenancePremisesCosts = 18,
        OtherOccupationCosts = 19,
        PremisesStaffCosts = 20,
        TotalUtilitiesCosts = 21,
        EnergyCosts = 22,
        WaterSewerageCosts = 23,
        AdministrativeSuppliesNonEducationalCosts = 24,
        TotalGrossCateringCosts = 25,
        TotalNetCateringCosts = 26,
        CateringStaffCosts = 27,
        CateringSuppliesCosts = 28,
        TotalOtherCosts = 29,
        OtherInsurancePremiumsCosts = 30,
        GroundsMaintenanceCosts = 31,
        IndirectEmployeeExpenses = 32,
        InterestChargesLoanBank = 33,
        PrivateFinanceInitiativeCharges = 34,
        RentRatesCosts = 35,
        SpecialFacilitiesCosts = 36,
        StaffDevelopmentTrainingCosts = 37,
        StaffRelatedInsuranceCosts = 38,
        SupplyTeacherInsurableCosts = 39,
        CommunityFocusedSchoolStaff = 40,
        CommunityFocusedSchoolCosts = 41,
    }

    public static readonly SubCategoryFilter[] All =
    [
        SubCategoryFilter.TotalExpenditure,
        SubCategoryFilter.TotalTeachingSupportStaffCosts,
        SubCategoryFilter.TeachingStaffCosts,
        SubCategoryFilter.SupplyTeachingStaffCosts,
        SubCategoryFilter.EducationalConsultancyCosts,
        SubCategoryFilter.EducationSupportStaffCosts,
        SubCategoryFilter.AgencySupplyTeachingStaffCosts,
        SubCategoryFilter.TotalNonEducationalSupportStaffCosts,
        SubCategoryFilter.AdministrativeClericalStaffCosts,
        SubCategoryFilter.AuditorsCosts,
        SubCategoryFilter.OtherStaffCosts,
        SubCategoryFilter.ProfessionalServicesNonCurriculumCosts,
        SubCategoryFilter.TotalEducationalSuppliesCosts,
        SubCategoryFilter.ExaminationFeesCosts,
        SubCategoryFilter.LearningResourcesNonIctCosts,
        SubCategoryFilter.LearningResourcesIctCosts,
        SubCategoryFilter.TotalPremisesStaffServiceCosts,
        SubCategoryFilter.CleaningCaretakingCosts,
        SubCategoryFilter.MaintenancePremisesCosts,
        SubCategoryFilter.OtherOccupationCosts,
        SubCategoryFilter.PremisesStaffCosts,
        SubCategoryFilter.TotalUtilitiesCosts,
        SubCategoryFilter.EnergyCosts,
        SubCategoryFilter.WaterSewerageCosts,
        SubCategoryFilter.AdministrativeSuppliesNonEducationalCosts,
        SubCategoryFilter.TotalGrossCateringCosts,
        SubCategoryFilter.TotalNetCateringCosts,
        SubCategoryFilter.CateringStaffCosts,
        SubCategoryFilter.CateringSuppliesCosts,
        SubCategoryFilter.TotalOtherCosts,
        SubCategoryFilter.OtherInsurancePremiumsCosts,
        SubCategoryFilter.GroundsMaintenanceCosts,
        SubCategoryFilter.IndirectEmployeeExpenses,
        SubCategoryFilter.InterestChargesLoanBank,
        SubCategoryFilter.PrivateFinanceInitiativeCharges,
        SubCategoryFilter.RentRatesCosts,
        SubCategoryFilter.SpecialFacilitiesCosts,
        SubCategoryFilter.StaffDevelopmentTrainingCosts,
        SubCategoryFilter.StaffRelatedInsuranceCosts,
        SubCategoryFilter.SupplyTeacherInsurableCosts,
        SubCategoryFilter.CommunityFocusedSchoolStaff,
        SubCategoryFilter.CommunityFocusedSchoolCosts
    ];

    public static readonly Dictionary<CategoryGroup, SubCategoryFilter[]> Groups = new()
    {
        [CategoryGroup.TotalExpenditure] =
        [
            SubCategoryFilter.TotalExpenditure
        ],
        [CategoryGroup.TeachingStaff] =
        [
            SubCategoryFilter.TotalTeachingSupportStaffCosts,
            SubCategoryFilter.TeachingStaffCosts,
            SubCategoryFilter.SupplyTeachingStaffCosts,
            SubCategoryFilter.EducationalConsultancyCosts,
            SubCategoryFilter.EducationSupportStaffCosts,
            SubCategoryFilter.AgencySupplyTeachingStaffCosts
        ],
        [CategoryGroup.NonEducationalSupportStaff] =
        [
            SubCategoryFilter.TotalNonEducationalSupportStaffCosts,
            SubCategoryFilter.AdministrativeClericalStaffCosts,
            SubCategoryFilter.AuditorsCosts,
            SubCategoryFilter.OtherStaffCosts,
            SubCategoryFilter.ProfessionalServicesNonCurriculumCosts
        ],
        [CategoryGroup.EducationalSupplies] =
        [
            SubCategoryFilter.TotalEducationalSuppliesCosts,
            SubCategoryFilter.ExaminationFeesCosts,
            SubCategoryFilter.LearningResourcesNonIctCosts
        ],
        [CategoryGroup.EducationalIct] =
        [
            SubCategoryFilter.LearningResourcesIctCosts
        ],
        [CategoryGroup.PremisesStaffServices] =
        [
            SubCategoryFilter.TotalPremisesStaffServiceCosts,
            SubCategoryFilter.CleaningCaretakingCosts,
            SubCategoryFilter.MaintenancePremisesCosts,
            SubCategoryFilter.OtherOccupationCosts,
            SubCategoryFilter.PremisesStaffCosts
        ],
        [CategoryGroup.Utilities] =
        [
            SubCategoryFilter.TotalUtilitiesCosts,
            SubCategoryFilter.EnergyCosts,
            SubCategoryFilter.WaterSewerageCosts
        ],
        [CategoryGroup.AdministrativeSupplies] =
        [
            SubCategoryFilter.AdministrativeSuppliesNonEducationalCosts
        ],
        [CategoryGroup.CateringStaffServices] =
        [
            SubCategoryFilter.TotalGrossCateringCosts,
            SubCategoryFilter.TotalNetCateringCosts,
            SubCategoryFilter.CateringStaffCosts,
            SubCategoryFilter.CateringSuppliesCosts
        ],
        [CategoryGroup.Other] =
        [
            SubCategoryFilter.TotalOtherCosts,
            SubCategoryFilter.OtherInsurancePremiumsCosts,
            SubCategoryFilter.GroundsMaintenanceCosts,
            SubCategoryFilter.IndirectEmployeeExpenses,
            SubCategoryFilter.InterestChargesLoanBank,
            SubCategoryFilter.PrivateFinanceInitiativeCharges,
            SubCategoryFilter.RentRatesCosts,
            SubCategoryFilter.SpecialFacilitiesCosts,
            SubCategoryFilter.StaffDevelopmentTrainingCosts,
            SubCategoryFilter.StaffRelatedInsuranceCosts,
            SubCategoryFilter.SupplyTeacherInsurableCosts,
            SubCategoryFilter.CommunityFocusedSchoolStaff,
            SubCategoryFilter.CommunityFocusedSchoolCosts
        ]
    };

    public static string GetCategoryGroupDescription(this CategoryGroup group) => group switch
    {
        CategoryGroup.TotalExpenditure => "Total expenditure",
        CategoryGroup.TeachingStaff => "Teaching and Teaching support staff",
        CategoryGroup.NonEducationalSupportStaff => "Non-educational support staff and services",
        CategoryGroup.EducationalSupplies => "Educational supplies",
        CategoryGroup.EducationalIct => "Educational ICT",
        CategoryGroup.PremisesStaffServices => "Premises and staff services",
        CategoryGroup.Utilities => "Utilities",
        CategoryGroup.AdministrativeSupplies => "Administrative supplies",
        CategoryGroup.CateringStaffServices => "Catering staff services",
        CategoryGroup.Other => "Other costs",
        _ => ""
    };

    public static string GetCategoryGroupSetType(this CategoryGroup group) => group switch
    {
        CategoryGroup.PremisesStaffServices => ComparatorSetTypes.Building,
        CategoryGroup.Utilities => ComparatorSetTypes.Building,
        _ => ComparatorSetTypes.Pupil
    };

    public static bool GetOpenByDefault(this CategoryGroup group) => group switch
    {
        CategoryGroup.TotalExpenditure => true,
        CategoryGroup.EducationalIct => true,
        CategoryGroup.AdministrativeSupplies => true,
        _ => false
    };

    public static string GetHeading(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.TotalExpenditure => "Total expenditure",
        SubCategoryFilter.TotalTeachingSupportStaffCosts => "Total teaching and teaching support staff costs",
        SubCategoryFilter.TeachingStaffCosts => "Teaching staff costs",
        SubCategoryFilter.SupplyTeachingStaffCosts => "Supply teaching staff costs",
        SubCategoryFilter.EducationalConsultancyCosts => "Educational consultancy costs",
        SubCategoryFilter.EducationSupportStaffCosts => "Educational support staff costs",
        SubCategoryFilter.AgencySupplyTeachingStaffCosts => "Agency supply teaching staff costs",
        SubCategoryFilter.TotalNonEducationalSupportStaffCosts => "Total non-educational support staff costs",
        SubCategoryFilter.AdministrativeClericalStaffCosts => "Administrative and clerical staff costs",
        SubCategoryFilter.AuditorsCosts => "Auditors costs",
        SubCategoryFilter.OtherStaffCosts => "Other staff costs",
        SubCategoryFilter.ProfessionalServicesNonCurriculumCosts => "Professional services (non-curriculum) costs",
        SubCategoryFilter.TotalEducationalSuppliesCosts => "Total educational supplies costs",
        SubCategoryFilter.ExaminationFeesCosts => "Examination fees costs",
        SubCategoryFilter.LearningResourcesNonIctCosts => "Learning resources (not ICT equipment) costs",
        SubCategoryFilter.LearningResourcesIctCosts => "Educational learning resources costs",
        SubCategoryFilter.TotalPremisesStaffServiceCosts => "Total premises staff and service costs",
        SubCategoryFilter.CleaningCaretakingCosts => "Cleaning and caretaking costs",
        SubCategoryFilter.MaintenancePremisesCosts => "Maintenance of premises costs",
        SubCategoryFilter.OtherOccupationCosts => "Other occupation costs",
        SubCategoryFilter.PremisesStaffCosts => "Premises staff costs",
        SubCategoryFilter.TotalUtilitiesCosts => "Total utilities costs",
        SubCategoryFilter.EnergyCosts => "Energy costs",
        SubCategoryFilter.WaterSewerageCosts => "Water and sewerage costs",
        SubCategoryFilter.AdministrativeSuppliesNonEducationalCosts => "Administrative supplies (Non-educational)",
        SubCategoryFilter.TotalGrossCateringCosts => "Total catering costs (gross)",
        SubCategoryFilter.TotalNetCateringCosts => "Total catering costs (net)",
        SubCategoryFilter.CateringStaffCosts => "Catering staff costs",
        SubCategoryFilter.CateringSuppliesCosts => "Catering supplies costs",
        SubCategoryFilter.TotalOtherCosts => "Total other costs",
        SubCategoryFilter.OtherInsurancePremiumsCosts => "Other insurance premiums costs",
        SubCategoryFilter.GroundsMaintenanceCosts => "Grounds maintenance costs",
        SubCategoryFilter.IndirectEmployeeExpenses => "Indirect employee expenses",
        SubCategoryFilter.InterestChargesLoanBank => "Interest charges for loan and bank",
        SubCategoryFilter.PrivateFinanceInitiativeCharges => "PFI charges",
        SubCategoryFilter.RentRatesCosts => "Rent and rates costs",
        SubCategoryFilter.SpecialFacilitiesCosts => "Special facilities costs",
        SubCategoryFilter.StaffDevelopmentTrainingCosts => "Staff development and training costs",
        SubCategoryFilter.StaffRelatedInsuranceCosts => "Staff-related insurance costs",
        SubCategoryFilter.SupplyTeacherInsurableCosts => "Supply teacher insurance costs",
        SubCategoryFilter.CommunityFocusedSchoolStaff => "Community focused school staff (maintained schools only)",
        SubCategoryFilter.CommunityFocusedSchoolCosts => "Community focused school costs (maintained schools only)",
        _ => ""
    };

    public static decimal? GetValue(this SubCategoryFilter filter, SchoolExpenditure e) =>
        filter switch
        {
            SubCategoryFilter.TotalExpenditure => e.TotalExpenditure,
            SubCategoryFilter.TotalTeachingSupportStaffCosts => e.TotalTeachingSupportStaffCosts,
            SubCategoryFilter.TeachingStaffCosts => e.TeachingStaffCosts,
            SubCategoryFilter.SupplyTeachingStaffCosts => e.SupplyTeachingStaffCosts,
            SubCategoryFilter.EducationalConsultancyCosts => e.EducationalConsultancyCosts,
            SubCategoryFilter.EducationSupportStaffCosts => e.EducationSupportStaffCosts,
            SubCategoryFilter.AgencySupplyTeachingStaffCosts => e.AgencySupplyTeachingStaffCosts,
            SubCategoryFilter.TotalNonEducationalSupportStaffCosts => e.TotalNonEducationalSupportStaffCosts,
            SubCategoryFilter.AdministrativeClericalStaffCosts => e.AdministrativeClericalStaffCosts,
            SubCategoryFilter.AuditorsCosts => e.AuditorsCosts,
            SubCategoryFilter.OtherStaffCosts => e.OtherStaffCosts,
            SubCategoryFilter.ProfessionalServicesNonCurriculumCosts => e.ProfessionalServicesNonCurriculumCosts,
            SubCategoryFilter.TotalEducationalSuppliesCosts => e.TotalEducationalSuppliesCosts,
            SubCategoryFilter.ExaminationFeesCosts => e.ExaminationFeesCosts,
            SubCategoryFilter.LearningResourcesNonIctCosts => e.LearningResourcesNonIctCosts,
            SubCategoryFilter.LearningResourcesIctCosts => e.LearningResourcesIctCosts,
            SubCategoryFilter.TotalPremisesStaffServiceCosts => e.TotalPremisesStaffServiceCosts,
            SubCategoryFilter.CleaningCaretakingCosts => e.CleaningCaretakingCosts,
            SubCategoryFilter.MaintenancePremisesCosts => e.MaintenancePremisesCosts,
            SubCategoryFilter.OtherOccupationCosts => e.OtherOccupationCosts,
            SubCategoryFilter.PremisesStaffCosts => e.PremisesStaffCosts,
            SubCategoryFilter.TotalUtilitiesCosts => e.TotalUtilitiesCosts,
            SubCategoryFilter.EnergyCosts => e.EnergyCosts,
            SubCategoryFilter.WaterSewerageCosts => e.WaterSewerageCosts,
            SubCategoryFilter.AdministrativeSuppliesNonEducationalCosts => e.AdministrativeSuppliesNonEducationalCosts,
            SubCategoryFilter.TotalGrossCateringCosts => e.TotalGrossCateringCosts,
            SubCategoryFilter.TotalNetCateringCosts => e.TotalNetCateringCosts,
            SubCategoryFilter.CateringStaffCosts => e.CateringStaffCosts,
            SubCategoryFilter.CateringSuppliesCosts => e.CateringSuppliesCosts,
            SubCategoryFilter.TotalOtherCosts => e.TotalOtherCosts,
            SubCategoryFilter.OtherInsurancePremiumsCosts => e.OtherInsurancePremiumsCosts,
            SubCategoryFilter.GroundsMaintenanceCosts => e.GroundsMaintenanceCosts,
            SubCategoryFilter.IndirectEmployeeExpenses => e.IndirectEmployeeExpenses,
            SubCategoryFilter.InterestChargesLoanBank => e.InterestChargesLoanBank,
            SubCategoryFilter.PrivateFinanceInitiativeCharges => e.PrivateFinanceInitiativeCharges,
            SubCategoryFilter.RentRatesCosts => e.RentRatesCosts,
            SubCategoryFilter.SpecialFacilitiesCosts => e.SpecialFacilitiesCosts,
            SubCategoryFilter.StaffDevelopmentTrainingCosts => e.StaffDevelopmentTrainingCosts,
            SubCategoryFilter.StaffRelatedInsuranceCosts => e.StaffRelatedInsuranceCosts,
            SubCategoryFilter.SupplyTeacherInsurableCosts => e.SupplyTeacherInsurableCosts,
            SubCategoryFilter.CommunityFocusedSchoolStaff => e.CommunityFocusedSchoolStaff,
            SubCategoryFilter.CommunityFocusedSchoolCosts => e.CommunityFocusedSchoolCosts,
            _ => null
        };
}
