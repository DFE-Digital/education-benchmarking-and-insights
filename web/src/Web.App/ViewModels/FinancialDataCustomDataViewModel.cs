using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;
using Web.App.Domain;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.ViewModels;

public interface IFinancialDataCustomDataViewModel : ICustomDataViewModel
{
    decimal? AdministrativeSuppliesNonEducationalCosts { get; }
    decimal? CateringStaffCosts { get; }
    decimal? CateringSuppliesCosts { get; }
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
    decimal? TotalIncome { get; }
    decimal? TotalExpenditure { get; }
    decimal? RevenueReserve { get; }
}

public record FinancialDataCustomDataViewModel : IFinancialDataCustomDataViewModel
{
    // Administrative supplies
    [PositiveNumericValue]
    [Display(Name = SubCostCategories.AdministrativeSupplies.AdministrativeSuppliesNonEducationalCosts)]
    public decimal? AdministrativeSuppliesNonEducationalCosts { get; init; }

    // Catering
    [PositiveNumericValue]
    [Display(Name = SubCostCategories.CateringStaffServices.CateringStaffCosts)]
    public decimal? CateringStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.CateringStaffServices.CateringSuppliesCosts)]
    public decimal? CateringSuppliesCosts { get; init; }

    // Educational supplies
    [PositiveNumericValue]
    [Display(Name = SubCostCategories.EducationalSupplies.ExaminationFeesCosts)]
    public decimal? ExaminationFeesCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.EducationalSupplies.LearningResourcesNonIctCosts)]
    public decimal? LearningResourcesNonIctCosts { get; init; }

    // IT
    [PositiveNumericValue]
    [Display(Name = SubCostCategories.EducationalIct.LearningResourcesIctCosts)]
    public decimal? LearningResourcesIctCosts { get; init; }

    // Non-educational support staff
    [PositiveNumericValue]
    [Display(Name = SubCostCategories.NonEducationalSupportStaff.AdministrativeClericalStaffCosts)]
    public decimal? AdministrativeClericalStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.NonEducationalSupportStaff.AuditorsCosts)]
    public decimal? AuditorsCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.NonEducationalSupportStaff.OtherStaffCosts)]
    public decimal? OtherStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.NonEducationalSupportStaff.ProfessionalServicesNonCurriculumCosts)]
    public decimal? ProfessionalServicesNonCurriculumCosts { get; init; }

    // Premises and services
    [PositiveNumericValue]
    [Display(Name = SubCostCategories.PremisesStaffServices.CleaningCaretakingCosts)]
    public decimal? CleaningCaretakingCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.PremisesStaffServices.MaintenancePremisesCosts)]
    public decimal? MaintenancePremisesCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.PremisesStaffServices.OtherOccupationCosts)]
    public decimal? OtherOccupationCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.PremisesStaffServices.PremisesStaffCosts)]
    public decimal? PremisesStaffCosts { get; init; }

    // Teaching and teaching support
    [PositiveNumericValue]
    [Display(Name = SubCostCategories.TeachingStaff.AgencySupplyTeachingStaffCosts)]
    public decimal? AgencySupplyTeachingStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.TeachingStaff.EducationSupportStaffCosts)]
    public decimal? EducationSupportStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.TeachingStaff.EducationalConsultancyCosts)]
    public decimal? EducationalConsultancyCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.TeachingStaff.SupplyTeachingStaffCosts)]
    public decimal? SupplyTeachingStaffCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.TeachingStaff.TeachingStaffCosts)]
    public decimal? TeachingStaffCosts { get; init; }

    // Utilities
    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Utilities.EnergyCosts)]
    public decimal? EnergyCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Utilities.WaterSewerageCosts)]
    public decimal? WaterSewerageCosts { get; init; }

    // Other costs
    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Other.GroundsMaintenanceCosts)]
    public decimal? GroundsMaintenanceCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Other.IndirectEmployeeExpenses)]
    public decimal? IndirectEmployeeExpenses { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Other.InterestChargesLoanBank)]
    public decimal? InterestChargesLoanBank { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Other.OtherInsurancePremiumsCosts)]
    public decimal? OtherInsurancePremiumsCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Other.PrivateFinanceInitiativeCharges)]
    public decimal? PrivateFinanceInitiativeCharges { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Other.RentRatesCosts)]
    public decimal? RentRatesCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Other.SpecialFacilitiesCosts)]
    public decimal? SpecialFacilitiesCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Other.StaffDevelopmentTrainingCosts)]
    public decimal? StaffDevelopmentTrainingCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Other.StaffRelatedInsuranceCosts)]
    public decimal? StaffRelatedInsuranceCosts { get; init; }

    [PositiveNumericValue]
    [Display(Name = SubCostCategories.Other.SupplyTeacherInsurableCosts)]
    public decimal? SupplyTeacherInsurableCosts { get; init; }

    // Totals
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.TotalIncome)]
    public decimal? TotalIncome { get; init; }

    public decimal? TotalExpenditure { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.RevenueReserve)]
    public decimal? RevenueReserve { get; init; }
}