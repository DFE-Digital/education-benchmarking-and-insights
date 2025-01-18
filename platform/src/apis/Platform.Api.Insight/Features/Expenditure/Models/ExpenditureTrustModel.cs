using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Expenditure.Models;

[ExcludeFromCodeCoverage]
public record ExpenditureTrustModel : ExpenditureModel
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }

    public decimal? TotalExpenditureCS { get; set; }
    public decimal? TotalTeachingSupportStaffCostsCS { get; set; }
    public decimal? TeachingStaffCostsCS { get; set; }
    public decimal? SupplyTeachingStaffCostsCS { get; set; }
    public decimal? EducationalConsultancyCostsCS { get; set; }
    public decimal? EducationSupportStaffCostsCS { get; set; }
    public decimal? AgencySupplyTeachingStaffCostsCS { get; set; }
    public decimal? TotalNonEducationalSupportStaffCostsCS { get; set; }
    public decimal? AdministrativeClericalStaffCostsCS { get; set; }
    public decimal? AuditorsCostsCS { get; set; }
    public decimal? OtherStaffCostsCS { get; set; }
    public decimal? ProfessionalServicesNonCurriculumCostsCS { get; set; }
    public decimal? TotalEducationalSuppliesCostsCS { get; set; }
    public decimal? ExaminationFeesCostsCS { get; set; }
    public decimal? LearningResourcesNonIctCostsCS { get; set; }
    public decimal? LearningResourcesIctCostsCS { get; set; }
    public decimal? TotalPremisesStaffServiceCostsCS { get; set; }
    public decimal? CleaningCaretakingCostsCS { get; set; }
    public decimal? MaintenancePremisesCostsCS { get; set; }
    public decimal? OtherOccupationCostsCS { get; set; }
    public decimal? PremisesStaffCostsCS { get; set; }
    public decimal? TotalUtilitiesCostsCS { get; set; }
    public decimal? EnergyCostsCS { get; set; }
    public decimal? WaterSewerageCostsCS { get; set; }
    public decimal? AdministrativeSuppliesNonEducationalCostsCS { get; set; }
    public decimal? TotalGrossCateringCostsCS { get; set; }
    public decimal? TotalNetCateringCostsCS { get; set; }
    public decimal? CateringStaffCostsCS { get; set; }
    public decimal? CateringSuppliesCostsCS { get; set; }
    public decimal? TotalOtherCostsCS { get; set; }
    public decimal? DirectRevenueFinancingCostsCS { get; set; }
    public decimal? GroundsMaintenanceCostsCS { get; set; }
    public decimal? IndirectEmployeeExpensesCS { get; set; }
    public decimal? InterestChargesLoanBankCS { get; set; }
    public decimal? OtherInsurancePremiumsCostsCS { get; set; }
    public decimal? PrivateFinanceInitiativeChargesCS { get; set; }
    public decimal? RentRatesCostsCS { get; set; }
    public decimal? SpecialFacilitiesCostsCS { get; set; }
    public decimal? StaffDevelopmentTrainingCostsCS { get; set; }
    public decimal? StaffRelatedInsuranceCostsCS { get; set; }
    public decimal? SupplyTeacherInsurableCostsCS { get; set; }

    public decimal? TotalExpenditureSchool { get; set; }
    public decimal? TotalTeachingSupportStaffCostsSchool { get; set; }
    public decimal? TeachingStaffCostsSchool { get; set; }
    public decimal? SupplyTeachingStaffCostsSchool { get; set; }
    public decimal? EducationalConsultancyCostsSchool { get; set; }
    public decimal? EducationSupportStaffCostsSchool { get; set; }
    public decimal? AgencySupplyTeachingStaffCostsSchool { get; set; }
    public decimal? TotalNonEducationalSupportStaffCostsSchool { get; set; }
    public decimal? AdministrativeClericalStaffCostsSchool { get; set; }
    public decimal? AuditorsCostsSchool { get; set; }
    public decimal? OtherStaffCostsSchool { get; set; }
    public decimal? ProfessionalServicesNonCurriculumCostsSchool { get; set; }
    public decimal? TotalEducationalSuppliesCostsSchool { get; set; }
    public decimal? ExaminationFeesCostsSchool { get; set; }
    public decimal? LearningResourcesNonIctCostsSchool { get; set; }
    public decimal? LearningResourcesIctCostsSchool { get; set; }
    public decimal? TotalPremisesStaffServiceCostsSchool { get; set; }
    public decimal? CleaningCaretakingCostsSchool { get; set; }
    public decimal? MaintenancePremisesCostsSchool { get; set; }
    public decimal? OtherOccupationCostsSchool { get; set; }
    public decimal? PremisesStaffCostsSchool { get; set; }
    public decimal? TotalUtilitiesCostsSchool { get; set; }
    public decimal? EnergyCostsSchool { get; set; }
    public decimal? WaterSewerageCostsSchool { get; set; }
    public decimal? AdministrativeSuppliesNonEducationalCostsSchool { get; set; }
    public decimal? TotalGrossCateringCostsSchool { get; set; }
    public decimal? TotalNetCateringCostsSchool { get; set; }
    public decimal? CateringStaffCostsSchool { get; set; }
    public decimal? CateringSuppliesCostsSchool { get; set; }
    public decimal? TotalOtherCostsSchool { get; set; }
    public decimal? DirectRevenueFinancingCostsSchool { get; set; }
    public decimal? GroundsMaintenanceCostsSchool { get; set; }
    public decimal? IndirectEmployeeExpensesSchool { get; set; }
    public decimal? InterestChargesLoanBankSchool { get; set; }
    public decimal? OtherInsurancePremiumsCostsSchool { get; set; }
    public decimal? PrivateFinanceInitiativeChargesSchool { get; set; }
    public decimal? RentRatesCostsSchool { get; set; }
    public decimal? SpecialFacilitiesCostsSchool { get; set; }
    public decimal? StaffDevelopmentTrainingCostsSchool { get; set; }
    public decimal? StaffRelatedInsuranceCostsSchool { get; set; }
    public decimal? SupplyTeacherInsurableCostsSchool { get; set; }
}