using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;
using Web.App.Domain;

namespace Web.App.ViewModels;

public record SchoolCustomDataViewModel
{
    // Administrative supplies
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.AdministrativeSuppliesCosts)]
    public decimal? AdministrativeSuppliesCosts { get; init; }

    // Catering
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.CateringStaffCosts)]
    public decimal? CateringStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.CateringSupplies)]
    public decimal? CateringSupplies { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.CateringIncome)]
    public decimal? CateringIncome { get; init; }

    // Educational supplies
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.ExaminationFeesCosts)]
    public decimal? ExaminationFeesCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.LearningResourcesNonIctCosts)]
    public decimal? LearningResourcesNonIctCosts { get; init; }

    // IT
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.LearningResourcesIctCosts)]
    public decimal? LearningResourcesIctCosts { get; init; }

    // Non-educational support staff
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.AdministrativeClericalStaffCosts)]
    public decimal? AdministrativeClericalStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.AuditorsCosts)]
    public decimal? AuditorsCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.OtherStaffCosts)]
    public decimal? OtherStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.ProfessionalServicesNonCurriculumCosts)]
    public decimal? ProfessionalServicesNonCurriculumCosts { get; init; }

    // Premises and services
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.CleaningCaretakingCosts)]
    public decimal? CleaningCaretakingCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.MaintenancePremisesCosts)]
    public decimal? MaintenancePremisesCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.OtherOccupationCosts)]
    public decimal? OtherOccupationCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.PremisesStaffCosts)]
    public decimal? PremisesStaffCosts { get; init; }

    // Teaching and teaching support
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.AgencySupplyTeachingStaffCosts)]
    public decimal? AgencySupplyTeachingStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.EducationSupportStaffCosts)]
    public decimal? EducationSupportStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.EducationalConsultancyCosts)]
    public decimal? EducationalConsultancyCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.SupplyTeachingStaffCosts)]
    public decimal? SupplyTeachingStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.TeachingStaffCosts)]
    public decimal? TeachingStaffCosts { get; init; }

    // Utilities
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.EnergyCosts)]
    public decimal? EnergyCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.WaterSewerageCosts)]
    public decimal? WaterSewerageCosts { get; init; }

    // Other costs
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.DirectRevenueFinancingCosts)]
    public decimal? DirectRevenueFinancingCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.GroundsMaintenanceCosts)]
    public decimal? GroundsMaintenanceCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.IndirectEmployeeExpenses)]
    public decimal? IndirectEmployeeExpenses { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.InterestChargesLoanBank)]
    public decimal? InterestChargesLoanBank { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.OtherInsurancePremiumsCosts)]
    public decimal? OtherInsurancePremiumsCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.PrivateFinanceInitiativeCharges)]
    public decimal? PrivateFinanceInitiativeCharges { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.RentRatesCosts)]
    public decimal? RentRatesCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.SpecialFacilitiesCosts)]
    public decimal? SpecialFacilitiesCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.StaffDevelopmentTrainingCosts)]
    public decimal? StaffDevelopmentTrainingCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.StaffRelatedInsuranceCosts)]
    public decimal? StaffRelatedInsuranceCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.SupplyTeacherInsurableCosts)]
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
            CateringSupplies = customData.CateringSupplies,
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

    public CustomData ToCustomData()
    {
        return new CustomData
        {
            // Administrative supplies
            AdministrativeSuppliesCosts = AdministrativeSuppliesCosts,

            // Catering
            CateringStaffCosts = CateringStaffCosts,
            CateringSupplies = CateringSupplies,
            CateringIncome = CateringIncome,

            // Educational supplies
            ExaminationFeesCosts = ExaminationFeesCosts,
            LearningResourcesNonIctCosts = LearningResourcesNonIctCosts,

            // IT
            LearningResourcesIctCosts = LearningResourcesIctCosts,

            // Non-educational support staff
            AdministrativeClericalStaffCosts = AdministrativeClericalStaffCosts,
            AuditorsCosts = AuditorsCosts,
            OtherStaffCosts = OtherStaffCosts,
            ProfessionalServicesNonCurriculumCosts = ProfessionalServicesNonCurriculumCosts,

            // Premises and services
            CleaningCaretakingCosts = CleaningCaretakingCosts,
            MaintenancePremisesCosts = MaintenancePremisesCosts,
            OtherOccupationCosts = OtherOccupationCosts,
            PremisesStaffCosts = PremisesStaffCosts,

            // Teaching and teaching support
            AgencySupplyTeachingStaffCosts = AgencySupplyTeachingStaffCosts,
            EducationSupportStaffCosts = EducationSupportStaffCosts,
            EducationalConsultancyCosts = EducationalConsultancyCosts,
            SupplyTeachingStaffCosts = SupplyTeachingStaffCosts,
            TeachingStaffCosts = TeachingStaffCosts,

            // Utilities
            EnergyCosts = EnergyCosts,
            WaterSewerageCosts = WaterSewerageCosts,

            // Other costs
            DirectRevenueFinancingCosts = DirectRevenueFinancingCosts,
            GroundsMaintenanceCosts = GroundsMaintenanceCosts,
            IndirectEmployeeExpenses = IndirectEmployeeExpenses,
            InterestChargesLoanBank = InterestChargesLoanBank,
            OtherInsurancePremiumsCosts = OtherInsurancePremiumsCosts,
            PrivateFinanceInitiativeCharges = PrivateFinanceInitiativeCharges,
            RentRatesCosts = RentRatesCosts,
            SpecialFacilitiesCosts = SpecialFacilitiesCosts,
            StaffDevelopmentTrainingCosts = StaffDevelopmentTrainingCosts,
            StaffRelatedInsuranceCosts = StaffRelatedInsuranceCosts,
            SupplyTeacherInsurableCosts = SupplyTeacherInsurableCosts,

            // Totals
            TotalIncome = TotalIncome,
            TotalExpenditure = TotalExpenditure,
            RevenueReserve = RevenueReserve,

            // Non-financial data
            TotalNumberOfTeachersFte = TotalNumberOfTeachersFte,
            FreeSchoolMealPercent = FreeSchoolMealPercent,
            SpecialEducationalNeedsPercent = SpecialEducationalNeedsPercent,
            FloorArea = FloorArea,

            // Workforce data
            WorkforceFte = WorkforceFte,
            TeachersFte = TeachersFte,
            SeniorLeadershipFte = SeniorLeadershipFte,
        };
    }
}

public static class SchoolCustomDataViewModelTitles
{
    public const string AdministrativeSuppliesCosts = "Administrative supplies (non-educational)";
    public const string CateringStaffCosts = "Catering staff";
    public const string CateringSupplies = "Catering supplies";
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
}