namespace Web.App.Domain;

public class CostCodes(bool isPartOfTrust)
{
    // TeachingStaffCostCodes
    private string TeachingStaffCostCode { get; } = isPartOfTrust ? "BAE010-T" : "E01";
    private string SupplyTeachingStaffCostCode { get; } = isPartOfTrust ? "BAE020-T" : "E02";
    private string AgencySupplyTeachingStaffCostCode { get; } = isPartOfTrust ? "BAE240-T" : "E26";
    private string EducationSupportStaffCostCode { get; } = isPartOfTrust ? "BAE030-T" : "E03";
    private string EducationalConsultancyCostCode { get; } = isPartOfTrust ? "BAE230-T" : "E27";

    // NonEducationalSupportStaffCostCodes
    private string AdministrativeClericalStaffCostCode { get; } = isPartOfTrust ? "BAE040-T" : "E05";
    private string AuditorsCostCode { get; } = isPartOfTrust ? "BAE260-T" : "";
    private string OtherStaffCostCode { get; } = isPartOfTrust ? "BAE070-T" : "E07";
    private string ProfessionalServicesNonCurriculumCostCode { get; } = isPartOfTrust ? "BAE300-T" : "E28a";

    // EducationalSuppliesCostCodes
    private string ExaminationFeesCostCode { get; } = isPartOfTrust ? "BAE220-T" : "E21";
    private string LearningResourcesNonIctCostsCode { get; } = isPartOfTrust ? "BAE200-T" : "E19";

    // EducationalIctCostCodes
    private string LearningResourcesIctCostCode { get; } = isPartOfTrust ? "BAE210-T" : "E20";

    // PremisesStaffServicesCostCodes
    private string CleaningCaretakingCostCode { get; } = isPartOfTrust ? "BAE130-T" : "E14";
    private string MaintenancePremisesCostCode { get; } = isPartOfTrust ? "BAE120-T" : "E12";
    private string OtherOccupationCostCode { get; } = isPartOfTrust ? "BAE180-T" : "E18";
    private string PremisesStaffCostCode { get; } = isPartOfTrust ? "BAE050-T" : "E04";

    // UtilitiesCostCodes
    private string EnergyCostCode { get; } = isPartOfTrust ? "BAE150-T" : "E16";
    private string WaterSewerageCostCode { get; } = isPartOfTrust ? "BAE140-T" : "E15";

    // AdministrativeSuppliesCostCodes
    private string AdministrativeSuppliesNonEducationalCostCode { get; } = isPartOfTrust ? "BAE280-T" : "E22";

    // CateringStaffServicesCostCodes
    private string CateringStaffCostCode { get; } = isPartOfTrust ? "BAE060-T" : "E06";
    private string CateringSuppliesCostCode { get; } = isPartOfTrust ? "BAE250-T" : "E25";

    // OtherCostCodes
    private string DirectRevenueFinancingCostCode { get; } = isPartOfTrust ? "BAE290-T" : "E30";
    private string GroundsMaintenanceCostCode { get; } = isPartOfTrust ? "BAE320-T" : "E13";
    private string IndirectEmployeeExpensesCostCode { get; } = isPartOfTrust ? "BAE080-T" : "E08";
    private string InterestChargesLoanBankCostCode { get; } = isPartOfTrust ? "BAE320-T" : "E29";
    private string OtherInsurancePremiumsCostCode { get; } = isPartOfTrust ? "BAE270-T" : "E23";
    private string PrivateFinanceInitiativeChargesCostCode { get; } = isPartOfTrust ? "BAE310-T" : "E28b";
    private string RentRatesCostCode { get; } = isPartOfTrust ? "BAE160-T" : "E17";
    private string SpecialFacilitiesCostCode { get; } = isPartOfTrust ? "BAE190-T" : "E24";
    private string StaffDevelopmentTrainingCostCode { get; } = isPartOfTrust ? "BAE090-T" : "E09";
    private string StaffRelatedInsuranceCostCode { get; } = isPartOfTrust ? "BAE110-T" : "E11";
    private string SupplyTeacherInsurableCostCode { get; } = isPartOfTrust ? "BAE100-T" : "E10";
    private string CommunityFocusedSchoolCostCode { get; } = isPartOfTrust ? "" : "E32";
    private string CommunityFocusedSchoolStaffCostCode { get; } = isPartOfTrust ? "" : "E31";

    public Dictionary<string, string> SubCategoryToCostCodeMap => new Dictionary<string, string>
        {
            { SubCostCategories.TeachingStaff.TeachingStaffCosts, TeachingStaffCostCode },
            { SubCostCategories.TeachingStaff.SupplyTeachingStaffCosts, SupplyTeachingStaffCostCode },
            { SubCostCategories.TeachingStaff.AgencySupplyTeachingStaffCosts, AgencySupplyTeachingStaffCostCode },
            { SubCostCategories.TeachingStaff.EducationSupportStaffCosts, EducationSupportStaffCostCode },
            { SubCostCategories.TeachingStaff.EducationalConsultancyCosts, EducationalConsultancyCostCode },
            {
                SubCostCategories.NonEducationalSupportStaff.AdministrativeClericalStaffCosts,
                AdministrativeClericalStaffCostCode
            },
            { SubCostCategories.NonEducationalSupportStaff.AuditorsCosts, AuditorsCostCode },
            { SubCostCategories.NonEducationalSupportStaff.OtherStaffCosts, OtherStaffCostCode },
            {
                SubCostCategories.NonEducationalSupportStaff.ProfessionalServicesNonCurriculumCosts,
                ProfessionalServicesNonCurriculumCostCode
            },
            { SubCostCategories.EducationalSupplies.ExaminationFeesCosts, ExaminationFeesCostCode },
            { SubCostCategories.EducationalSupplies.LearningResourcesNonIctCosts, LearningResourcesNonIctCostsCode },
            { SubCostCategories.EducationalIct.LearningResourcesIctCosts, LearningResourcesIctCostCode },
            { SubCostCategories.PremisesStaffServices.CleaningCaretakingCosts, CleaningCaretakingCostCode },
            { SubCostCategories.PremisesStaffServices.MaintenancePremisesCosts, MaintenancePremisesCostCode },
            { SubCostCategories.PremisesStaffServices.OtherOccupationCosts, OtherOccupationCostCode },
            { SubCostCategories.PremisesStaffServices.PremisesStaffCosts, PremisesStaffCostCode },
            { SubCostCategories.Utilities.EnergyCosts, EnergyCostCode },
            { SubCostCategories.Utilities.WaterSewerageCosts, WaterSewerageCostCode },
            {
                SubCostCategories.AdministrativeSupplies.AdministrativeSuppliesNonEducationalCosts,
                AdministrativeSuppliesNonEducationalCostCode
            },
            { SubCostCategories.CateringStaffServices.CateringStaffCosts, CateringStaffCostCode },
            { SubCostCategories.CateringStaffServices.CateringSuppliesCosts, CateringSuppliesCostCode },
            { SubCostCategories.Other.DirectRevenueFinancingCosts, DirectRevenueFinancingCostCode },
            { SubCostCategories.Other.GroundsMaintenanceCosts, GroundsMaintenanceCostCode },
            { SubCostCategories.Other.IndirectEmployeeExpenses, IndirectEmployeeExpensesCostCode },
            { SubCostCategories.Other.InterestChargesLoanBank, InterestChargesLoanBankCostCode },
            { SubCostCategories.Other.OtherInsurancePremiumsCosts, OtherInsurancePremiumsCostCode },
            { SubCostCategories.Other.PrivateFinanceInitiativeCharges, PrivateFinanceInitiativeChargesCostCode },
            { SubCostCategories.Other.RentRatesCosts, RentRatesCostCode },
            { SubCostCategories.Other.SpecialFacilitiesCosts, SpecialFacilitiesCostCode },
            { SubCostCategories.Other.StaffDevelopmentTrainingCosts, StaffDevelopmentTrainingCostCode },
            { SubCostCategories.Other.StaffRelatedInsuranceCosts, StaffRelatedInsuranceCostCode },
            { SubCostCategories.Other.SupplyTeacherInsurableCosts, SupplyTeacherInsurableCostCode },
            { SubCostCategories.Other.CommunityFocusedSchoolCosts, CommunityFocusedSchoolCostCode },
            { SubCostCategories.Other.CommunityFocusedSchoolStaff, CommunityFocusedSchoolStaffCostCode }
        }
        .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Value))
        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

    public string? GetCostCode(string subCategory) =>
        SubCategoryToCostCodeMap.GetValueOrDefault(subCategory);

}