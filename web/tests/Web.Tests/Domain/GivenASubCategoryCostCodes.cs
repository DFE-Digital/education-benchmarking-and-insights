using Web.App.Domain;
using Xunit;

namespace Web.Tests.Domain;

public class GivenASubCategoryCostCodes
{
    [Theory]
    [InlineData(SubCostCategories.TeachingStaff.TeachingStaffCosts, false, false, "E01")]
    [InlineData(SubCostCategories.TeachingStaff.TeachingStaffCosts, true, false, "BAE010")]
    [InlineData(SubCostCategories.TeachingStaff.SupplyTeachingStaffCosts, false, false, "E02")]
    [InlineData(SubCostCategories.TeachingStaff.SupplyTeachingStaffCosts, true, false, "BAE020")]
    [InlineData(SubCostCategories.TeachingStaff.AgencySupplyTeachingStaffCosts, false, false, "E26")]
    [InlineData(SubCostCategories.TeachingStaff.AgencySupplyTeachingStaffCosts, true, false, "BAE240")]
    [InlineData(SubCostCategories.TeachingStaff.EducationSupportStaffCosts, false, false, "E03")]
    [InlineData(SubCostCategories.TeachingStaff.EducationSupportStaffCosts, true, false, "BAE030")]
    [InlineData(SubCostCategories.TeachingStaff.EducationalConsultancyCosts, false, false, "E27")]
    [InlineData(SubCostCategories.TeachingStaff.EducationalConsultancyCosts, true, false, "BAE230")]
    public void GetsCorrectCostCodeForTeachingStaff(string subCategory, bool isPartOfTrust, bool cfrItSpendBreakdown, string? expectedCostCodes)
    {
        AssertCodesEqual(subCategory, isPartOfTrust, cfrItSpendBreakdown, expectedCostCodes);
    }

    [Theory]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.AdministrativeClericalStaffCosts, false, false, "E05")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.AdministrativeClericalStaffCosts, true, false, "BAE040")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.AuditorsCosts, false, false, null)]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.AuditorsCosts, true, false, "BAE260")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.OtherStaffCosts, false, false, "E07")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.OtherStaffCosts, true, false, "BAE070")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.ProfessionalServicesNonCurriculumCosts, false, false, "E28a")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.ProfessionalServicesNonCurriculumCosts, true, false, "BAE300")]
    public void GetsCorrectCostCodeForNonEducationalSupportStaff(string subCategory, bool isPartOfTrust, bool cfrItSpendBreakdown, string? expectedCostCodes)
    {
        AssertCodesEqual(subCategory, isPartOfTrust, cfrItSpendBreakdown, expectedCostCodes);
    }

    [Theory]
    [InlineData(SubCostCategories.EducationalSupplies.ExaminationFeesCosts, false, false, "E21")]
    [InlineData(SubCostCategories.EducationalSupplies.ExaminationFeesCosts, true, false, "BAE220")]
    [InlineData(SubCostCategories.EducationalSupplies.LearningResourcesNonIctCosts, false, false, "E19")]
    [InlineData(SubCostCategories.EducationalSupplies.LearningResourcesNonIctCosts, true, false, "BAE200")]
    public void GetsCorrectCostCodeForEducationalSupplies(string subCategory, bool isPartOfTrust, bool cfrItSpendBreakdown, string? expectedCostCodes)
    {
        AssertCodesEqual(subCategory, isPartOfTrust, cfrItSpendBreakdown, expectedCostCodes);
    }

    [Theory]
    [InlineData(SubCostCategories.EducationalIct.LearningResourcesIctCosts, false, false, "E20")]
    [InlineData(SubCostCategories.EducationalIct.LearningResourcesIctCosts, false, true, "E20A,E20B,E20C,E20E,E20F,E20G")]
    [InlineData(SubCostCategories.EducationalIct.LearningResourcesIctCosts, true, false, "BAE210")]
    public void GetsCorrectCostCodeForEducationalIct(string subCategory, bool isPartOfTrust, bool cfrItSpendBreakdown, string? expectedCostCodes)
    {
        AssertCodesEqual(subCategory, isPartOfTrust, cfrItSpendBreakdown, expectedCostCodes);
    }

    [Theory]
    [InlineData(SubCostCategories.PremisesStaffServices.CleaningCaretakingCosts, false, false, "E14")]
    [InlineData(SubCostCategories.PremisesStaffServices.CleaningCaretakingCosts, true, false, "BAE130")]
    [InlineData(SubCostCategories.PremisesStaffServices.MaintenancePremisesCosts, false, false, "E12")]
    [InlineData(SubCostCategories.PremisesStaffServices.MaintenancePremisesCosts, true, false, "BAE120")]
    [InlineData(SubCostCategories.PremisesStaffServices.OtherOccupationCosts, false, false, "E18")]
    [InlineData(SubCostCategories.PremisesStaffServices.OtherOccupationCosts, true, false, "BAE180")]
    [InlineData(SubCostCategories.PremisesStaffServices.PremisesStaffCosts, false, false, "E04")]
    [InlineData(SubCostCategories.PremisesStaffServices.PremisesStaffCosts, true, false, "BAE050")]
    public void GetsCorrectCostCodeForPremisesStaffServices(string subCategory, bool isPartOfTrust, bool cfrItSpendBreakdown, string? expectedCostCodes)
    {
        AssertCodesEqual(subCategory, isPartOfTrust, cfrItSpendBreakdown, expectedCostCodes);
    }

    [Theory]
    [InlineData(SubCostCategories.Utilities.EnergyCosts, false, false, "E16")]
    [InlineData(SubCostCategories.Utilities.EnergyCosts, true, false, "BAE150")]
    [InlineData(SubCostCategories.Utilities.WaterSewerageCosts, false, false, "E15")]
    [InlineData(SubCostCategories.Utilities.WaterSewerageCosts, true, false, "BAE140")]
    public void GetsCorrectCostCodeForUtilities(string subCategory, bool isPartOfTrust, bool cfrItSpendBreakdown, string? expectedCostCodes)
    {
        AssertCodesEqual(subCategory, isPartOfTrust, cfrItSpendBreakdown, expectedCostCodes);
    }

    [Theory]
    [InlineData(SubCostCategories.AdministrativeSupplies.AdministrativeSuppliesNonEducationalCosts, false, false, "E22")]
    [InlineData(SubCostCategories.AdministrativeSupplies.AdministrativeSuppliesNonEducationalCosts, false, true, "E22,E20D")]
    [InlineData(SubCostCategories.AdministrativeSupplies.AdministrativeSuppliesNonEducationalCosts, true, false, "BAE280")]
    public void GetsCorrectCostCodeForAdministrativeSupplies(string subCategory, bool isPartOfTrust, bool cfrItSpendBreakdown, string? expectedCostCodes)
    {
        AssertCodesEqual(subCategory, isPartOfTrust, cfrItSpendBreakdown, expectedCostCodes);
    }

    [Theory]
    [InlineData(SubCostCategories.CateringStaffServices.CateringStaffCosts, false, false, "E06")]
    [InlineData(SubCostCategories.CateringStaffServices.CateringStaffCosts, true, false, "BAE060")]
    [InlineData(SubCostCategories.CateringStaffServices.CateringSuppliesCosts, false, false, "E25")]
    [InlineData(SubCostCategories.CateringStaffServices.CateringSuppliesCosts, true, false, "BAE250")]
    public void GetsCorrectCostCodeForCateringStaffServices(string subCategory, bool isPartOfTrust, bool cfrItSpendBreakdown, string? expectedCostCodes)
    {
        AssertCodesEqual(subCategory, isPartOfTrust, cfrItSpendBreakdown, expectedCostCodes);
    }

    [Theory]
    [InlineData(SubCostCategories.Other.DirectRevenueFinancingCosts, false, false, "E30")]
    [InlineData(SubCostCategories.Other.DirectRevenueFinancingCosts, true, false, "BAE290")]
    [InlineData(SubCostCategories.Other.GroundsMaintenanceCosts, false, false, "E13")]
    [InlineData(SubCostCategories.Other.GroundsMaintenanceCosts, true, false, "BAE170")]
    [InlineData(SubCostCategories.Other.IndirectEmployeeExpenses, false, false, "E08")]
    [InlineData(SubCostCategories.Other.IndirectEmployeeExpenses, true, false, "BAE080")]
    [InlineData(SubCostCategories.Other.InterestChargesLoanBank, false, false, "E29")]
    [InlineData(SubCostCategories.Other.InterestChargesLoanBank, true, false, "BAE320")]
    [InlineData(SubCostCategories.Other.OtherInsurancePremiumsCosts, false, false, "E23")]
    [InlineData(SubCostCategories.Other.OtherInsurancePremiumsCosts, true, false, "BAE270")]
    [InlineData(SubCostCategories.Other.PrivateFinanceInitiativeCharges, false, false, "E28b")]
    [InlineData(SubCostCategories.Other.PrivateFinanceInitiativeCharges, true, false, "BAE310")]
    [InlineData(SubCostCategories.Other.RentRatesCosts, false, false, "E17")]
    [InlineData(SubCostCategories.Other.RentRatesCosts, true, false, "BAE160")]
    [InlineData(SubCostCategories.Other.SpecialFacilitiesCosts, false, false, "E24")]
    [InlineData(SubCostCategories.Other.SpecialFacilitiesCosts, true, false, "BAE190")]
    [InlineData(SubCostCategories.Other.StaffDevelopmentTrainingCosts, false, false, "E09")]
    [InlineData(SubCostCategories.Other.StaffDevelopmentTrainingCosts, true, false, "BAE090")]
    [InlineData(SubCostCategories.Other.StaffRelatedInsuranceCosts, false, false, "E11")]
    [InlineData(SubCostCategories.Other.StaffRelatedInsuranceCosts, true, false, "BAE110")]
    [InlineData(SubCostCategories.Other.SupplyTeacherInsurableCosts, false, false, "E10")]
    [InlineData(SubCostCategories.Other.SupplyTeacherInsurableCosts, true, false, "BAE100")]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolStaff, false, false, "E31")]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolStaff, true, false, null)]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolCosts, false, false, "E32")]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolCosts, true, false, null)]
    public void GetsCorrectCostCodeForOther(string subCategory, bool isPartOfTrust, bool cfrItSpendBreakdown, string? expectedCostCodes)
    {
        AssertCodesEqual(subCategory, isPartOfTrust, cfrItSpendBreakdown, expectedCostCodes);
    }

    private static void AssertCodesEqual(string subCategory, bool isPartOfTrust, bool cfrItSpendBreakdown, string? expectedCostCodes)
    {
        var costCodes = new CostCodes(isPartOfTrust, cfrItSpendBreakdown);

        var actualCostCode = costCodes.GetCostCodes(subCategory);

        Assert.Equal(expectedCostCodes, actualCostCode);
    }
}