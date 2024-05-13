using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;

namespace Web.App.ViewModels;

public interface IFinancialDataCustomDataViewModel
{
    decimal? AdministrativeSuppliesCosts { get; }
    decimal? CateringStaffCosts { get; }
    decimal? CateringSuppliesCosts { get; }
    decimal? CateringIncome { get; }
    decimal? ExaminationFeesCosts { get; }
    decimal? LearningResourcesNonIctCosts { get; }
    decimal? LearningResourcesIctCosts { get; }
    decimal? AdministrativeClericalStaffCosts { get; }
    decimal? AuditorsCosts { get; }
    decimal? OtherStaffCosts { get; }
    decimal? ProfessionalServicesNonCurriculumCosts { get; }
    decimal? CleaningCaretakingCosts { get; }
    decimal? MaintenancePremisesCosts { get; }
    decimal? OtherOccupationCosts { get; }
    decimal? PremisesStaffCosts { get; }
    decimal? AgencySupplyTeachingStaffCosts { get; }
    decimal? EducationSupportStaffCosts { get; }
    decimal? EducationalConsultancyCosts { get; }
    decimal? SupplyTeachingStaffCosts { get; }
    decimal? TeachingStaffCosts { get; }
    decimal? EnergyCosts { get; }
    decimal? WaterSewerageCosts { get; }
    decimal? DirectRevenueFinancingCosts { get; }
    decimal? GroundsMaintenanceCosts { get; }
    decimal? IndirectEmployeeExpenses { get; }
    decimal? InterestChargesLoanBank { get; }
    decimal? OtherInsurancePremiumsCosts { get; }
    decimal? PrivateFinanceInitiativeCharges { get; }
    decimal? RentRatesCosts { get; }
    decimal? SpecialFacilitiesCosts { get; }
    decimal? StaffDevelopmentTrainingCosts { get; }
    decimal? StaffRelatedInsuranceCosts { get; }
    decimal? SupplyTeacherInsurableCosts { get; }
    decimal TotalIncome { get; }
    decimal TotalExpenditure { get; }
    decimal RevenueReserve { get; }
}

public record FinancialDataCustomDataViewModel : IFinancialDataCustomDataViewModel
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
    [Display(Name = SchoolCustomDataViewModelTitles.CateringSuppliesCosts)]
    public decimal? CateringSuppliesCosts { get; init; }

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
}