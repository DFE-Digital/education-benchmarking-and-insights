using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;

namespace Web.App.ViewModels;

public record SchoolCustomDataViewModel
{
    // Administrative supplies
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.AdministrativeSuppliesCosts)]
    public decimal? AdministrativeSuppliesCosts { get; init; }

    // Catering
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.CateringStaffCosts)]
    public decimal? CateringStaffCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.CateringSupplies)]
    public decimal? CateringSupplies { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.CateringIncome)]
    public decimal? CateringIncome { get; init; }

    // Educational supplies
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.ExaminationFeesCosts)]
    public decimal? ExaminationFeesCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.LearningResourcesNonIctCosts)]
    public decimal? LearningResourcesNonIctCosts { get; init; }

    // IT
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.LearningResourcesIctCosts)]
    public decimal? LearningResourcesIctCosts { get; init; }

    // Non-educational support staff
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.AdministrativeClericalStaffCosts)]
    public decimal? AdministrativeClericalStaffCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.AuditorsCosts)]
    public decimal? AuditorsCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.OtherStaffCosts)]
    public decimal? OtherStaffCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.ProfessionalServicesNonCurriculumCosts)]
    public decimal? ProfessionalServicesNonCurriculumCosts { get; init; }

    // Premises and services
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.CleaningCaretakingCosts)]
    public decimal? CleaningCaretakingCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.MaintenancePremisesCosts)]
    public decimal? MaintenancePremisesCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.OtherOccupationCosts)]
    public decimal? OtherOccupationCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.PremisesStaffCosts)]
    public decimal? PremisesStaffCosts { get; init; }

    // Teaching and teaching support
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.AgencySupplyTeachingStaffCosts)]
    public decimal? AgencySupplyTeachingStaffCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.EducationSupportStaffCosts)]
    public decimal? EducationSupportStaffCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.EducationalConsultancyCosts)]
    public decimal? EducationalConsultancyCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.SupplyTeachingStaffCosts)]
    public decimal? SupplyTeachingStaffCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.TeachingStaffCosts)]
    public decimal? TeachingStaffCosts { get; init; }

    // Utilities
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.EnergyCosts)]
    public decimal? EnergyCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.WaterSewerageCosts)]
    public decimal? WaterSewerageCosts { get; init; }

    // Other costs
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.DirectRevenueFinancingCosts)]
    public decimal? DirectRevenueFinancingCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.GroundsMaintenanceCosts)]
    public decimal? GroundsMaintenanceCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.IndirectEmployeeExpenses)]
    public decimal? IndirectEmployeeExpenses { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.InterestChargesLoanBank)]
    public decimal? InterestChargesLoanBank { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.OtherInsurancePremiumsCosts)]
    public decimal? OtherInsurancePremiumsCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.PrivateFinanceInitiativeCharges)]
    public decimal? PrivateFinanceInitiativeCharges { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.RentRatesCosts)]
    public decimal? RentRatesCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.SpecialFacilitiesCosts)]
    public decimal? SpecialFacilitiesCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.StaffDevelopmentTrainingCosts)]
    public decimal? StaffDevelopmentTrainingCosts { get; init; }
    
    [CustomDataPositiveValue]
    [Display(Name = SchoolCustomDataViewModelTitles.StaffRelatedInsuranceCosts)]
    public decimal? StaffRelatedInsuranceCosts { get; init; }

    [CustomDataPositiveValue]
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