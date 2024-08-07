using Platform.Api.Insight.Expenditure;
using Xunit;
namespace Platform.Tests.Insight.Expenditure.ExpenditureResponseFactoryCreate;

public static class Assertions
{
    internal static void AssertTotalExpenditure(ExpenditureBaseResponse expected, ExpenditureBaseResponse response)
    {
        AssertEqual(nameof(ExpenditureBaseResponse.TotalExpenditure), expected.TotalExpenditure, response.TotalExpenditure);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolTotalExpenditure), expected.SchoolTotalExpenditure, response.SchoolTotalExpenditure);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralTotalExpenditure), expected.CentralTotalExpenditure, response.CentralTotalExpenditure);
    }

    internal static void AssertTeachingTeachingSupportStaff(ExpenditureBaseResponse expected, ExpenditureBaseResponse response)
    {
        AssertEqual(nameof(ExpenditureBaseResponse.TotalTeachingSupportStaffCosts), expected.TotalTeachingSupportStaffCosts, response.TotalTeachingSupportStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolTotalTeachingSupportStaffCosts), expected.SchoolTotalTeachingSupportStaffCosts, response.SchoolTotalTeachingSupportStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralTotalTeachingSupportStaffCosts), expected.CentralTotalTeachingSupportStaffCosts, response.CentralTotalTeachingSupportStaffCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.TeachingStaffCosts), expected.TeachingStaffCosts, response.TeachingStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolTeachingStaffCosts), expected.SchoolTeachingStaffCosts, response.SchoolTeachingStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralTeachingStaffCosts), expected.CentralTeachingStaffCosts, response.CentralTeachingStaffCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.SupplyTeachingStaffCosts), expected.SupplyTeachingStaffCosts, response.SupplyTeachingStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolSupplyTeachingStaffCosts), expected.SchoolSupplyTeachingStaffCosts, response.SchoolSupplyTeachingStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralSupplyTeachingStaffCosts), expected.CentralSupplyTeachingStaffCosts, response.CentralSupplyTeachingStaffCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.EducationalConsultancyCosts), expected.EducationalConsultancyCosts, response.EducationalConsultancyCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolEducationalConsultancyCosts), expected.SchoolEducationalConsultancyCosts, response.SchoolEducationalConsultancyCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralEducationalConsultancyCosts), expected.CentralEducationalConsultancyCosts, response.CentralEducationalConsultancyCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.EducationSupportStaffCosts), expected.EducationSupportStaffCosts, response.EducationSupportStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolEducationSupportStaffCosts), expected.SchoolEducationSupportStaffCosts, response.SchoolEducationSupportStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralEducationSupportStaffCosts), expected.CentralEducationSupportStaffCosts, response.CentralEducationSupportStaffCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.AgencySupplyTeachingStaffCosts), expected.AgencySupplyTeachingStaffCosts, response.AgencySupplyTeachingStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolAgencySupplyTeachingStaffCosts), expected.SchoolAgencySupplyTeachingStaffCosts, response.SchoolAgencySupplyTeachingStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralAgencySupplyTeachingStaffCosts), expected.CentralAgencySupplyTeachingStaffCosts, response.CentralAgencySupplyTeachingStaffCosts);
    }

    internal static void AssertNonEducationalSupportStaff(ExpenditureBaseResponse expected, ExpenditureBaseResponse response)
    {
        AssertEqual(nameof(ExpenditureBaseResponse.TotalNonEducationalSupportStaffCosts), expected.TotalNonEducationalSupportStaffCosts, response.TotalNonEducationalSupportStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolTotalNonEducationalSupportStaffCosts), expected.SchoolTotalNonEducationalSupportStaffCosts, response.SchoolTotalNonEducationalSupportStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralTotalNonEducationalSupportStaffCosts), expected.CentralTotalNonEducationalSupportStaffCosts, response.CentralTotalNonEducationalSupportStaffCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.AdministrativeClericalStaffCosts), expected.AdministrativeClericalStaffCosts, response.AdministrativeClericalStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolAdministrativeClericalStaffCosts), expected.SchoolAdministrativeClericalStaffCosts, response.SchoolAdministrativeClericalStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralAdministrativeClericalStaffCosts), expected.CentralAdministrativeClericalStaffCosts, response.CentralAdministrativeClericalStaffCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.AuditorsCosts), expected.AuditorsCosts, response.AuditorsCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolAuditorsCosts), expected.SchoolAuditorsCosts, response.SchoolAuditorsCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralAuditorsCosts), expected.CentralAuditorsCosts, response.CentralAuditorsCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.OtherStaffCosts), expected.OtherStaffCosts, response.OtherStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolOtherStaffCosts), expected.SchoolOtherStaffCosts, response.SchoolOtherStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralOtherStaffCosts), expected.CentralOtherStaffCosts, response.CentralOtherStaffCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.ProfessionalServicesNonCurriculumCosts), expected.ProfessionalServicesNonCurriculumCosts, response.ProfessionalServicesNonCurriculumCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolProfessionalServicesNonCurriculumCosts), expected.SchoolProfessionalServicesNonCurriculumCosts, response.SchoolProfessionalServicesNonCurriculumCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralProfessionalServicesNonCurriculumCosts), expected.CentralProfessionalServicesNonCurriculumCosts, response.CentralProfessionalServicesNonCurriculumCosts);
    }

    internal static void AssertEducationalSupplies(ExpenditureBaseResponse expected, ExpenditureBaseResponse response)
    {
        AssertEqual(nameof(ExpenditureBaseResponse.TotalEducationalSuppliesCosts), expected.TotalEducationalSuppliesCosts, response.TotalEducationalSuppliesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolTotalEducationalSuppliesCosts), expected.SchoolTotalEducationalSuppliesCosts, response.SchoolTotalEducationalSuppliesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralTotalEducationalSuppliesCosts), expected.CentralTotalEducationalSuppliesCosts, response.CentralTotalEducationalSuppliesCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.ExaminationFeesCosts), expected.ExaminationFeesCosts, response.ExaminationFeesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolExaminationFeesCosts), expected.SchoolExaminationFeesCosts, response.SchoolExaminationFeesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralExaminationFeesCosts), expected.CentralExaminationFeesCosts, response.CentralExaminationFeesCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.LearningResourcesNonIctCosts), expected.LearningResourcesNonIctCosts, response.LearningResourcesNonIctCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolLearningResourcesNonIctCosts), expected.SchoolLearningResourcesNonIctCosts, response.SchoolLearningResourcesNonIctCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralLearningResourcesNonIctCosts), expected.CentralLearningResourcesNonIctCosts, response.CentralLearningResourcesNonIctCosts);
    }

    internal static void AssertEducationalIct(ExpenditureBaseResponse expected, ExpenditureBaseResponse response)
    {
        AssertEqual(nameof(ExpenditureBaseResponse.LearningResourcesIctCosts), expected.LearningResourcesIctCosts, response.LearningResourcesIctCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolLearningResourcesIctCosts), expected.SchoolLearningResourcesIctCosts, response.SchoolLearningResourcesIctCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralLearningResourcesIctCosts), expected.CentralLearningResourcesIctCosts, response.CentralLearningResourcesIctCosts);
    }

    internal static void AssertPremisesStaffServices(ExpenditureBaseResponse expected, ExpenditureBaseResponse response)
    {
        AssertEqual(nameof(ExpenditureBaseResponse.TotalPremisesStaffServiceCosts), expected.TotalPremisesStaffServiceCosts, response.TotalPremisesStaffServiceCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolTotalPremisesStaffServiceCosts), expected.SchoolTotalPremisesStaffServiceCosts, response.SchoolTotalPremisesStaffServiceCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralTotalPremisesStaffServiceCosts), expected.CentralTotalPremisesStaffServiceCosts, response.CentralTotalPremisesStaffServiceCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.CleaningCaretakingCosts), expected.CleaningCaretakingCosts, response.CleaningCaretakingCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolCleaningCaretakingCosts), expected.SchoolCleaningCaretakingCosts, response.SchoolCleaningCaretakingCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralCleaningCaretakingCosts), expected.CentralCleaningCaretakingCosts, response.CentralCleaningCaretakingCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.MaintenancePremisesCosts), expected.MaintenancePremisesCosts, response.MaintenancePremisesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolMaintenancePremisesCosts), expected.SchoolMaintenancePremisesCosts, response.SchoolMaintenancePremisesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralMaintenancePremisesCosts), expected.CentralMaintenancePremisesCosts, response.CentralMaintenancePremisesCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.OtherOccupationCosts), expected.OtherOccupationCosts, response.OtherOccupationCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolOtherOccupationCosts), expected.SchoolOtherOccupationCosts, response.SchoolOtherOccupationCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralOtherOccupationCosts), expected.CentralOtherOccupationCosts, response.CentralOtherOccupationCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.PremisesStaffCosts), expected.PremisesStaffCosts, response.PremisesStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolPremisesStaffCosts), expected.SchoolPremisesStaffCosts, response.SchoolPremisesStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralPremisesStaffCosts), expected.CentralPremisesStaffCosts, response.CentralPremisesStaffCosts);
    }

    internal static void AssertUtilities(ExpenditureBaseResponse expected, ExpenditureBaseResponse response)
    {
        AssertEqual(nameof(ExpenditureBaseResponse.TotalUtilitiesCosts), expected.TotalUtilitiesCosts, response.TotalUtilitiesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolTotalUtilitiesCosts), expected.SchoolTotalUtilitiesCosts, response.SchoolTotalUtilitiesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralTotalUtilitiesCosts), expected.CentralTotalUtilitiesCosts, response.CentralTotalUtilitiesCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.EnergyCosts), expected.EnergyCosts, response.EnergyCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolEnergyCosts), expected.SchoolEnergyCosts, response.SchoolEnergyCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralEnergyCosts), expected.CentralEnergyCosts, response.CentralEnergyCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.WaterSewerageCosts), expected.WaterSewerageCosts, response.WaterSewerageCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolWaterSewerageCosts), expected.SchoolWaterSewerageCosts, response.SchoolWaterSewerageCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralWaterSewerageCosts), expected.CentralWaterSewerageCosts, response.CentralWaterSewerageCosts);
    }

    internal static void AssertAdministrationSupplies(ExpenditureBaseResponse expected, ExpenditureBaseResponse response)
    {
        AssertEqual(nameof(ExpenditureBaseResponse.AdministrativeSuppliesCosts), expected.AdministrativeSuppliesCosts, response.AdministrativeSuppliesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolAdministrativeSuppliesCosts), expected.SchoolAdministrativeSuppliesCosts, response.SchoolAdministrativeSuppliesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralAdministrativeSuppliesCosts), expected.CentralAdministrativeSuppliesCosts, response.CentralAdministrativeSuppliesCosts);
    }

    internal static void AssertCateringStaffServices(ExpenditureBaseResponse expected, ExpenditureBaseResponse response)
    {
        AssertEqual(nameof(ExpenditureBaseResponse.TotalGrossCateringCosts), expected.TotalGrossCateringCosts, response.TotalGrossCateringCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolTotalGrossCateringCosts), expected.SchoolTotalGrossCateringCosts, response.SchoolTotalGrossCateringCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralTotalGrossCateringCosts), expected.CentralTotalGrossCateringCosts, response.CentralTotalGrossCateringCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.CateringStaffCosts), expected.CateringStaffCosts, response.CateringStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolCateringStaffCosts), expected.SchoolCateringStaffCosts, response.SchoolCateringStaffCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralCateringStaffCosts), expected.CentralCateringStaffCosts, response.CentralCateringStaffCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.CateringSuppliesCosts), expected.CateringSuppliesCosts, response.CateringSuppliesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolCateringSuppliesCosts), expected.SchoolCateringSuppliesCosts, response.SchoolCateringSuppliesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralCateringSuppliesCosts), expected.CentralCateringSuppliesCosts, response.CentralCateringSuppliesCosts);
    }

    internal static void AssertOtherCosts(ExpenditureBaseResponse expected, ExpenditureBaseResponse response)
    {
        AssertEqual(nameof(ExpenditureBaseResponse.TotalOtherCosts), expected.TotalOtherCosts, response.TotalOtherCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolTotalOtherCosts), expected.SchoolTotalOtherCosts, response.SchoolTotalOtherCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralTotalOtherCosts), expected.CentralTotalOtherCosts, response.CentralTotalOtherCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.DirectRevenueFinancingCosts), expected.DirectRevenueFinancingCosts, response.DirectRevenueFinancingCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolDirectRevenueFinancingCosts), expected.SchoolDirectRevenueFinancingCosts, response.SchoolDirectRevenueFinancingCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralDirectRevenueFinancingCosts), expected.CentralDirectRevenueFinancingCosts, response.CentralDirectRevenueFinancingCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.GroundsMaintenanceCosts), expected.GroundsMaintenanceCosts, response.GroundsMaintenanceCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolGroundsMaintenanceCosts), expected.SchoolGroundsMaintenanceCosts, response.SchoolGroundsMaintenanceCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralGroundsMaintenanceCosts), expected.CentralGroundsMaintenanceCosts, response.CentralGroundsMaintenanceCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.IndirectEmployeeExpenses), expected.IndirectEmployeeExpenses, response.IndirectEmployeeExpenses);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolIndirectEmployeeExpenses), expected.SchoolIndirectEmployeeExpenses, response.SchoolIndirectEmployeeExpenses);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralIndirectEmployeeExpenses), expected.CentralIndirectEmployeeExpenses, response.CentralIndirectEmployeeExpenses);

        AssertEqual(nameof(ExpenditureBaseResponse.InterestChargesLoanBank), expected.InterestChargesLoanBank, response.InterestChargesLoanBank);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolInterestChargesLoanBank), expected.SchoolInterestChargesLoanBank, response.SchoolInterestChargesLoanBank);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralInterestChargesLoanBank), expected.CentralInterestChargesLoanBank, response.CentralInterestChargesLoanBank);

        AssertEqual(nameof(ExpenditureBaseResponse.OtherInsurancePremiumsCosts), expected.OtherInsurancePremiumsCosts, response.OtherInsurancePremiumsCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolOtherInsurancePremiumsCosts), expected.SchoolOtherInsurancePremiumsCosts, response.SchoolOtherInsurancePremiumsCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralOtherInsurancePremiumsCosts), expected.CentralOtherInsurancePremiumsCosts, response.CentralOtherInsurancePremiumsCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.PrivateFinanceInitiativeCharges), expected.PrivateFinanceInitiativeCharges, response.PrivateFinanceInitiativeCharges);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolPrivateFinanceInitiativeCharges), expected.SchoolPrivateFinanceInitiativeCharges, response.SchoolPrivateFinanceInitiativeCharges);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralPrivateFinanceInitiativeCharges), expected.CentralPrivateFinanceInitiativeCharges, response.CentralPrivateFinanceInitiativeCharges);

        AssertEqual(nameof(ExpenditureBaseResponse.RentRatesCosts), expected.RentRatesCosts, response.RentRatesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolRentRatesCosts), expected.SchoolRentRatesCosts, response.SchoolRentRatesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralRentRatesCosts), expected.CentralRentRatesCosts, response.CentralRentRatesCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.SpecialFacilitiesCosts), expected.SpecialFacilitiesCosts, response.SpecialFacilitiesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolSpecialFacilitiesCosts), expected.SchoolSpecialFacilitiesCosts, response.SchoolSpecialFacilitiesCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralSpecialFacilitiesCosts), expected.CentralSpecialFacilitiesCosts, response.CentralSpecialFacilitiesCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.StaffDevelopmentTrainingCosts), expected.StaffDevelopmentTrainingCosts, response.StaffDevelopmentTrainingCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolStaffDevelopmentTrainingCosts), expected.SchoolStaffDevelopmentTrainingCosts, response.SchoolStaffDevelopmentTrainingCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralStaffDevelopmentTrainingCosts), expected.CentralStaffDevelopmentTrainingCosts, response.CentralStaffDevelopmentTrainingCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.StaffRelatedInsuranceCosts), expected.StaffRelatedInsuranceCosts, response.StaffRelatedInsuranceCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolStaffRelatedInsuranceCosts), expected.SchoolStaffRelatedInsuranceCosts, response.SchoolStaffRelatedInsuranceCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralStaffRelatedInsuranceCosts), expected.CentralStaffRelatedInsuranceCosts, response.CentralStaffRelatedInsuranceCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.SupplyTeacherInsurableCosts), expected.SupplyTeacherInsurableCosts, response.SupplyTeacherInsurableCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolSupplyTeacherInsurableCosts), expected.SchoolSupplyTeacherInsurableCosts, response.SchoolSupplyTeacherInsurableCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralSupplyTeacherInsurableCosts), expected.CentralSupplyTeacherInsurableCosts, response.CentralSupplyTeacherInsurableCosts);

        AssertEqual(nameof(ExpenditureBaseResponse.CommunityFocusedSchoolStaff), expected.CommunityFocusedSchoolStaff, response.CommunityFocusedSchoolStaff);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolCommunityFocusedSchoolStaff), expected.SchoolCommunityFocusedSchoolStaff, response.SchoolCommunityFocusedSchoolStaff);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralCommunityFocusedSchoolStaff), expected.CentralCommunityFocusedSchoolStaff, response.CentralCommunityFocusedSchoolStaff);

        AssertEqual(nameof(ExpenditureBaseResponse.CommunityFocusedSchoolCosts), expected.CommunityFocusedSchoolCosts, response.CommunityFocusedSchoolCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.SchoolCommunityFocusedSchoolCosts), expected.SchoolCommunityFocusedSchoolCosts, response.SchoolCommunityFocusedSchoolCosts);
        AssertEqual(nameof(ExpenditureBaseResponse.CentralCommunityFocusedSchoolCosts), expected.CentralCommunityFocusedSchoolCosts, response.CentralCommunityFocusedSchoolCosts);
    }

    private static void AssertEqual(string field, decimal? expected, decimal? actual) =>
        Assert.True(
            Math.Abs(expected.GetValueOrDefault() - actual.GetValueOrDefault()) < 0.02m,
            $"Expected `{expected}` for {field} but got `{actual}`");
}