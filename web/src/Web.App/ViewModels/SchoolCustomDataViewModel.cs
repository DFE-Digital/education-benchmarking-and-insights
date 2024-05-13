using Web.App.Domain;

namespace Web.App.ViewModels;

public record SchoolCustomDataViewModel : IFinancialDataCustomDataViewModel, INonFinancialDataCustomDataViewModel,
    IWorkforceDataCustomDataViewModel
{
    // Administrative supplies
    public decimal? AdministrativeSuppliesCosts { get; init; }

    // Catering
    public decimal? CateringStaffCosts { get; init; }
    public decimal? CateringSuppliesCosts { get; init; }
    public decimal? CateringIncome { get; init; }

    // Educational supplies
    public decimal? ExaminationFeesCosts { get; init; }
    public decimal? LearningResourcesNonIctCosts { get; init; }

    // IT
    public decimal? LearningResourcesIctCosts { get; init; }

    // Non-educational support staff
    public decimal? AdministrativeClericalStaffCosts { get; init; }
    public decimal? AuditorsCosts { get; init; }
    public decimal? OtherStaffCosts { get; init; }
    public decimal? ProfessionalServicesNonCurriculumCosts { get; init; }

    // Premises and services
    public decimal? CleaningCaretakingCosts { get; init; }
    public decimal? MaintenancePremisesCosts { get; init; }
    public decimal? OtherOccupationCosts { get; init; }
    public decimal? PremisesStaffCosts { get; init; }

    // Teaching and teaching support
    public decimal? AgencySupplyTeachingStaffCosts { get; init; }
    public decimal? EducationSupportStaffCosts { get; init; }
    public decimal? EducationalConsultancyCosts { get; init; }
    public decimal? SupplyTeachingStaffCosts { get; init; }
    public decimal? TeachingStaffCosts { get; init; }

    // Utilities
    public decimal? EnergyCosts { get; init; }
    public decimal? WaterSewerageCosts { get; init; }

    // Other costs
    public decimal? DirectRevenueFinancingCosts { get; init; }
    public decimal? GroundsMaintenanceCosts { get; init; }
    public decimal? IndirectEmployeeExpenses { get; init; }
    public decimal? InterestChargesLoanBank { get; init; }
    public decimal? OtherInsurancePremiumsCosts { get; init; }
    public decimal? PrivateFinanceInitiativeCharges { get; init; }
    public decimal? RentRatesCosts { get; init; }
    public decimal? SpecialFacilitiesCosts { get; init; }
    public decimal? StaffDevelopmentTrainingCosts { get; init; }
    public decimal? StaffRelatedInsuranceCosts { get; init; }
    public decimal? SupplyTeacherInsurableCosts { get; init; }

    // Totals
    public decimal TotalIncome { get; init; }
    public decimal TotalExpenditure { get; init; }
    public decimal RevenueReserve { get; init; }

    // Non-financial data
    public decimal? TotalNumberOfTeachersFte { get; init; }
    public decimal? FreeSchoolMealPercent { get; init; }
    public decimal? SpecialEducationalNeedsPercent { get; init; }
    public int? FloorArea { get; init; }

    // Workforce data
    public decimal? WorkforceFte { get; init; }
    public decimal? TeachersFte { get; init; }
    public decimal? SeniorLeadershipFte { get; init; }

    public static SchoolCustomDataViewModel FromCustomData(CustomData customData)
    {
        return new SchoolCustomDataViewModel
        {
            // Administrative supplies
            AdministrativeSuppliesCosts = customData.AdministrativeSuppliesCosts,

            // Catering
            CateringStaffCosts = customData.CateringStaffCosts,
            CateringSuppliesCosts = customData.CateringSuppliesCosts,
            CateringIncome = customData.CateringIncome,

            // Educational supplies
            ExaminationFeesCosts = customData.ExaminationFeesCosts,
            LearningResourcesNonIctCosts = customData.LearningResourcesNonIctCosts,

            // IT
            LearningResourcesIctCosts = customData.LearningResourcesIctCosts,

            // Non-educational support staff
            AdministrativeClericalStaffCosts = customData.AdministrativeClericalStaffCosts,
            AuditorsCosts = customData.AuditorsCosts,
            OtherStaffCosts = customData.OtherStaffCosts,
            ProfessionalServicesNonCurriculumCosts = customData.ProfessionalServicesNonCurriculumCosts,

            // Premises and services
            CleaningCaretakingCosts = customData.CleaningCaretakingCosts,
            MaintenancePremisesCosts = customData.MaintenancePremisesCosts,
            OtherOccupationCosts = customData.OtherOccupationCosts,
            PremisesStaffCosts = customData.PremisesStaffCosts,

            // Teaching and teaching support
            AgencySupplyTeachingStaffCosts = customData.AgencySupplyTeachingStaffCosts,
            EducationSupportStaffCosts = customData.EducationSupportStaffCosts,
            EducationalConsultancyCosts = customData.EducationalConsultancyCosts,
            SupplyTeachingStaffCosts = customData.SupplyTeachingStaffCosts,
            TeachingStaffCosts = customData.TeachingStaffCosts,

            // Utilities
            EnergyCosts = customData.EnergyCosts,
            WaterSewerageCosts = customData.WaterSewerageCosts,

            // Other costs
            DirectRevenueFinancingCosts = customData.DirectRevenueFinancingCosts,
            GroundsMaintenanceCosts = customData.GroundsMaintenanceCosts,
            IndirectEmployeeExpenses = customData.IndirectEmployeeExpenses,
            InterestChargesLoanBank = customData.InterestChargesLoanBank,
            OtherInsurancePremiumsCosts = customData.OtherInsurancePremiumsCosts,
            PrivateFinanceInitiativeCharges = customData.PrivateFinanceInitiativeCharges,
            RentRatesCosts = customData.RentRatesCosts,
            SpecialFacilitiesCosts = customData.SpecialFacilitiesCosts,
            StaffDevelopmentTrainingCosts = customData.StaffDevelopmentTrainingCosts,
            StaffRelatedInsuranceCosts = customData.StaffRelatedInsuranceCosts,
            SupplyTeacherInsurableCosts = customData.SupplyTeacherInsurableCosts,

            // Totals
            TotalIncome = customData.TotalIncome,
            TotalExpenditure = customData.TotalExpenditure,
            RevenueReserve = customData.RevenueReserve,

            // Non-financial data
            TotalNumberOfTeachersFte = customData.TotalNumberOfTeachersFte,
            FreeSchoolMealPercent = customData.FreeSchoolMealPercent,
            SpecialEducationalNeedsPercent = customData.SpecialEducationalNeedsPercent,
            FloorArea = customData.FloorArea,

            // Workforce data
            WorkforceFte = customData.WorkforceFte,
            TeachersFte = customData.TeachersFte,
            SeniorLeadershipFte = customData.SeniorLeadershipFte
        };
    }
}

public static class SchoolCustomDataViewModelTitles
{
    public const string AdministrativeSuppliesCosts = "Administrative supplies (non-educational)";
    public const string CateringStaffCosts = "Catering staff";
    public const string CateringSuppliesCosts = "Catering supplies";
    public const string CateringIncome = "Income from catering";
    public const string ExaminationFeesCosts = "Examination fees";
    public const string LearningResourcesNonIctCosts = "Learning resources (not ICT equipment)";
    public const string LearningResourcesIctCosts = "ICT learning resources";
    public const string AdministrativeClericalStaffCosts = "Administrative and clerical staff";
    public const string AuditorsCosts = "Auditor costs";
    public const string OtherStaffCosts = "Other staff";
    public const string ProfessionalServicesNonCurriculumCosts = "Professional services (non-curriculum)";
    public const string CleaningCaretakingCosts = "Cleaning and caretaking";
    public const string MaintenancePremisesCosts = "Maintenance of premises";
    public const string OtherOccupationCosts = "Other occupation costs";
    public const string PremisesStaffCosts = "Premises staff";
    public const string AgencySupplyTeachingStaffCosts = "Agency supply teaching staff";
    public const string EducationSupportStaffCosts = "Education support staff";
    public const string EducationalConsultancyCosts = "Educational consultancy";
    public const string SupplyTeachingStaffCosts = "Supply teaching staff";
    public const string TeachingStaffCosts = "Teaching staff";
    public const string EnergyCosts = "Energy";
    public const string WaterSewerageCosts = "Water and sewerage";
    public const string DirectRevenueFinancingCosts = "Direct revenue financing";
    public const string GroundsMaintenanceCosts = "Grounds maintenance";
    public const string IndirectEmployeeExpenses = "Indirect employee expenses";
    public const string InterestChargesLoanBank = "Interest charges for loan and bank";
    public const string OtherInsurancePremiumsCosts = "Other insurance premiums";
    public const string PrivateFinanceInitiativeCharges = "Private Finance Initiative (PFI) charges";
    public const string RentRatesCosts = "Rent and rates";
    public const string SpecialFacilitiesCosts = "Special facilities";
    public const string StaffDevelopmentTrainingCosts = "Staff development and training";
    public const string StaffRelatedInsuranceCosts = "Staff-related insurance";
    public const string SupplyTeacherInsurableCosts = "Supply teacher insurance";
    public const string TotalIncome = "Total income";
    public const string TotalExpenditure = "Total spending";
    public const string RevenueReserve = "Revenue reserve";
    public const string FreeSchoolMealPercent = "Pupils eligible for free school meals (FSM)";
    public const string SpecialEducationalNeedsPercent = "Pupils with special educational needs (SEN)";
    public const string FloorArea = "Gross internal floor area";
}