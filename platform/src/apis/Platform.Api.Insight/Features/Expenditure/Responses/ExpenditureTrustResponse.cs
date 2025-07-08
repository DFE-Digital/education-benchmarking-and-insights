using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Expenditure.Responses;

[ExcludeFromCodeCoverage]
public record ExpenditureTrustResponse : ExpenditureResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }

    public decimal? CentralTotalExpenditure { get; set; }
    public decimal? CentralTotalTeachingSupportStaffCosts { get; set; }
    public decimal? CentralTeachingStaffCosts { get; set; }
    public decimal? CentralSupplyTeachingStaffCosts { get; set; }
    public decimal? CentralEducationalConsultancyCosts { get; set; }
    public decimal? CentralEducationSupportStaffCosts { get; set; }
    public decimal? CentralAgencySupplyTeachingStaffCosts { get; set; }
    public decimal? CentralTotalNonEducationalSupportStaffCosts { get; set; }
    public decimal? CentralAdministrativeClericalStaffCosts { get; set; }
    public decimal? CentralAuditorsCosts { get; set; }
    public decimal? CentralOtherStaffCosts { get; set; }
    public decimal? CentralProfessionalServicesNonCurriculumCosts { get; set; }
    public decimal? CentralTotalEducationalSuppliesCosts { get; set; }
    public decimal? CentralExaminationFeesCosts { get; set; }
    public decimal? CentralLearningResourcesNonIctCosts { get; set; }
    public decimal? CentralLearningResourcesIctCosts { get; set; }
    public decimal? CentralTotalPremisesStaffServiceCosts { get; set; }
    public decimal? CentralCleaningCaretakingCosts { get; set; }
    public decimal? CentralMaintenancePremisesCosts { get; set; }
    public decimal? CentralOtherOccupationCosts { get; set; }
    public decimal? CentralPremisesStaffCosts { get; set; }
    public decimal? CentralTotalUtilitiesCosts { get; set; }
    public decimal? CentralEnergyCosts { get; set; }
    public decimal? CentralWaterSewerageCosts { get; set; }
    public decimal? CentralAdministrativeSuppliesNonEducationalCosts { get; set; }
    public decimal? CentralTotalGrossCateringCosts { get; set; }
    public decimal? CentralTotalNetCateringCosts { get; set; }
    public decimal? CentralCateringStaffCosts { get; set; }
    public decimal? CentralCateringSuppliesCosts { get; set; }
    public decimal? CentralTotalOtherCosts { get; set; }
    public decimal? CentralDirectRevenueFinancingCosts { get; set; }
    public decimal? CentralGroundsMaintenanceCosts { get; set; }
    public decimal? CentralIndirectEmployeeExpenses { get; set; }
    public decimal? CentralInterestChargesLoanBank { get; set; }
    public decimal? CentralOtherInsurancePremiumsCosts { get; set; }
    public decimal? CentralPrivateFinanceInitiativeCharges { get; set; }
    public decimal? CentralRentRatesCosts { get; set; }
    public decimal? CentralSpecialFacilitiesCosts { get; set; }
    public decimal? CentralStaffDevelopmentTrainingCosts { get; set; }
    public decimal? CentralStaffRelatedInsuranceCosts { get; set; }
    public decimal? CentralSupplyTeacherInsurableCosts { get; set; }

    public decimal? SchoolTotalExpenditure { get; set; }
    public decimal? SchoolTotalTeachingSupportStaffCosts { get; set; }
    public decimal? SchoolTeachingStaffCosts { get; set; }
    public decimal? SchoolSupplyTeachingStaffCosts { get; set; }
    public decimal? SchoolEducationalConsultancyCosts { get; set; }
    public decimal? SchoolEducationSupportStaffCosts { get; set; }
    public decimal? SchoolAgencySupplyTeachingStaffCosts { get; set; }
    public decimal? SchoolTotalNonEducationalSupportStaffCosts { get; set; }
    public decimal? SchoolAdministrativeClericalStaffCosts { get; set; }
    public decimal? SchoolAuditorsCosts { get; set; }
    public decimal? SchoolOtherStaffCosts { get; set; }
    public decimal? SchoolProfessionalServicesNonCurriculumCosts { get; set; }
    public decimal? SchoolTotalEducationalSuppliesCosts { get; set; }
    public decimal? SchoolExaminationFeesCosts { get; set; }
    public decimal? SchoolLearningResourcesNonIctCosts { get; set; }
    public decimal? SchoolLearningResourcesIctCosts { get; set; }
    public decimal? SchoolTotalPremisesStaffServiceCosts { get; set; }
    public decimal? SchoolCleaningCaretakingCosts { get; set; }
    public decimal? SchoolMaintenancePremisesCosts { get; set; }
    public decimal? SchoolOtherOccupationCosts { get; set; }
    public decimal? SchoolPremisesStaffCosts { get; set; }
    public decimal? SchoolTotalUtilitiesCosts { get; set; }
    public decimal? SchoolEnergyCosts { get; set; }
    public decimal? SchoolWaterSewerageCosts { get; set; }
    public decimal? SchoolAdministrativeSuppliesNonEducationalCosts { get; set; }
    public decimal? SchoolTotalGrossCateringCosts { get; set; }
    public decimal? SchoolTotalNetCateringCosts { get; set; }
    public decimal? SchoolCateringStaffCosts { get; set; }
    public decimal? SchoolCateringSuppliesCosts { get; set; }
    public decimal? SchoolTotalOtherCosts { get; set; }
    public decimal? SchoolDirectRevenueFinancingCosts { get; set; }
    public decimal? SchoolGroundsMaintenanceCosts { get; set; }
    public decimal? SchoolIndirectEmployeeExpenses { get; set; }
    public decimal? SchoolInterestChargesLoanBank { get; set; }
    public decimal? SchoolOtherInsurancePremiumsCosts { get; set; }
    public decimal? SchoolPrivateFinanceInitiativeCharges { get; set; }
    public decimal? SchoolRentRatesCosts { get; set; }
    public decimal? SchoolSpecialFacilitiesCosts { get; set; }
    public decimal? SchoolStaffDevelopmentTrainingCosts { get; set; }
    public decimal? SchoolStaffRelatedInsuranceCosts { get; set; }
    public decimal? SchoolSupplyTeacherInsurableCosts { get; set; }

    public int? HighestSalaryEmolumentBandValue { get; set; }
    public decimal? TotalPupils { get; set; }
}