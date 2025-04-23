using Web.App.Domain;
using Xunit;

namespace Web.Tests.Domain;

public class GivenASubCategoryCostCodes
{
    [Theory]
    [InlineData(SubCostCategories.TeachingStaff.TeachingStaffCosts, false, "E01")]
    [InlineData(SubCostCategories.TeachingStaff.TeachingStaffCosts, true, "BAE010")]
    [InlineData(SubCostCategories.TeachingStaff.SupplyTeachingStaffCosts, false, "E02")]
    [InlineData(SubCostCategories.TeachingStaff.SupplyTeachingStaffCosts, true, "BAE020")]
    [InlineData(SubCostCategories.TeachingStaff.AgencySupplyTeachingStaffCosts, false, "E26")]
    [InlineData(SubCostCategories.TeachingStaff.AgencySupplyTeachingStaffCosts, true, "BAE240")]
    [InlineData(SubCostCategories.TeachingStaff.EducationSupportStaffCosts, false, "E03")]
    [InlineData(SubCostCategories.TeachingStaff.EducationSupportStaffCosts, true, "BAE030")]
    [InlineData(SubCostCategories.TeachingStaff.EducationalConsultancyCosts, false, "E27")]
    [InlineData(SubCostCategories.TeachingStaff.EducationalConsultancyCosts, true, "BAE230")]
    public void GetsCorrectCostCodeForTeachingStaff(string subCategory, bool isPartOfTrust, string? expectedCostCode)
    {
        AssertCodeEqual(subCategory, isPartOfTrust, expectedCostCode);
    }

    [Theory]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.AdministrativeClericalStaffCosts, false, "E05")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.AdministrativeClericalStaffCosts, true, "BAE040")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.AuditorsCosts, false, null)]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.AuditorsCosts, true, "BAE260")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.OtherStaffCosts, false, "E07")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.OtherStaffCosts, true, "BAE070")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.ProfessionalServicesNonCurriculumCosts, false, "E28a")]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.ProfessionalServicesNonCurriculumCosts, true, "BAE300")]
    public void GetsCorrectCostCodeForNonEducationalSupportStaff(string subCategory, bool isPartOfTrust, string? expectedCostCode)
    {
        AssertCodeEqual(subCategory, isPartOfTrust, expectedCostCode);
    }

    [Theory]
    [InlineData(SubCostCategories.EducationalSupplies.ExaminationFeesCosts, false, "E21")]
    [InlineData(SubCostCategories.EducationalSupplies.ExaminationFeesCosts, true, "BAE220")]
    [InlineData(SubCostCategories.EducationalSupplies.LearningResourcesNonIctCosts, false, "E19")]
    [InlineData(SubCostCategories.EducationalSupplies.LearningResourcesNonIctCosts, true, "BAE200")]
    public void GetsCorrectCostCodeForEducationalSupplies(string subCategory, bool isPartOfTrust, string? expectedCostCode)
    {
        AssertCodeEqual(subCategory, isPartOfTrust, expectedCostCode);
    }

    [Theory]
    [InlineData(SubCostCategories.EducationalIct.LearningResourcesIctCosts, false, "E20")]
    [InlineData(SubCostCategories.EducationalIct.LearningResourcesIctCosts, true, "BAE210")]
    public void GetsCorrectCostCodeFoEducationalIct(string subCategory, bool isPartOfTrust, string? expectedCostCode)
    {
        AssertCodeEqual(subCategory, isPartOfTrust, expectedCostCode);
    }

    [Theory]
    [InlineData(SubCostCategories.PremisesStaffServices.CleaningCaretakingCosts, false, "E14")]
    [InlineData(SubCostCategories.PremisesStaffServices.CleaningCaretakingCosts, true, "BAE130")]
    [InlineData(SubCostCategories.PremisesStaffServices.MaintenancePremisesCosts, false, "E12")]
    [InlineData(SubCostCategories.PremisesStaffServices.MaintenancePremisesCosts, true, "BAE120")]
    [InlineData(SubCostCategories.PremisesStaffServices.OtherOccupationCosts, false, "E18")]
    [InlineData(SubCostCategories.PremisesStaffServices.OtherOccupationCosts, true, "BAE180")]
    [InlineData(SubCostCategories.PremisesStaffServices.PremisesStaffCosts, false, "E04")]
    [InlineData(SubCostCategories.PremisesStaffServices.PremisesStaffCosts, true, "BAE050")]
    public void GetsCorrectCostCodeForPremisesStaffServices(string subCategory, bool isPartOfTrust, string? expectedCostCode)
    {
        AssertCodeEqual(subCategory, isPartOfTrust, expectedCostCode);
    }

    [Theory]
    [InlineData(SubCostCategories.Utilities.EnergyCosts, false, "E16")]
    [InlineData(SubCostCategories.Utilities.EnergyCosts, true, "BAE150")]
    [InlineData(SubCostCategories.Utilities.WaterSewerageCosts, false, "E15")]
    [InlineData(SubCostCategories.Utilities.WaterSewerageCosts, true, "BAE140")]
    public void GetsCorrectCostCodeForUtilities(string subCategory, bool isPartOfTrust, string? expectedCostCode)
    {
        AssertCodeEqual(subCategory, isPartOfTrust, expectedCostCode);
    }

    [Theory]
    [InlineData(SubCostCategories.AdministrativeSupplies.AdministrativeSuppliesNonEducationalCosts, false, "E22")]
    [InlineData(SubCostCategories.AdministrativeSupplies.AdministrativeSuppliesNonEducationalCosts, true, "BAE280")]
    public void GetsCorrectCostCodeForAdministrativeSupplies(string subCategory, bool isPartOfTrust, string? expectedCostCode)
    {
        AssertCodeEqual(subCategory, isPartOfTrust, expectedCostCode);
    }

    [Theory]
    [InlineData(SubCostCategories.CateringStaffServices.CateringStaffCosts, false, "E06")]
    [InlineData(SubCostCategories.CateringStaffServices.CateringStaffCosts, true, "BAE060")]
    [InlineData(SubCostCategories.CateringStaffServices.CateringSuppliesCosts, false, "E25")]
    [InlineData(SubCostCategories.CateringStaffServices.CateringSuppliesCosts, true, "BAE250")]
    public void GetsCorrectCostCodeForCateringStaffServices(string subCategory, bool isPartOfTrust, string? expectedCostCode)
    {
        AssertCodeEqual(subCategory, isPartOfTrust, expectedCostCode);
    }

    [Theory]
    [InlineData(SubCostCategories.Other.DirectRevenueFinancingCosts, false, "E30")]
    [InlineData(SubCostCategories.Other.DirectRevenueFinancingCosts, true, "BAE290")]
    [InlineData(SubCostCategories.Other.GroundsMaintenanceCosts, false, "E13")]
    [InlineData(SubCostCategories.Other.GroundsMaintenanceCosts, true, "BAE320")]
    [InlineData(SubCostCategories.Other.IndirectEmployeeExpenses, false, "E08")]
    [InlineData(SubCostCategories.Other.IndirectEmployeeExpenses, true, "BAE080")]
    [InlineData(SubCostCategories.Other.InterestChargesLoanBank, false, "E29")]
    [InlineData(SubCostCategories.Other.InterestChargesLoanBank, true, "BAE320")]
    [InlineData(SubCostCategories.Other.OtherInsurancePremiumsCosts, false, "E23")]
    [InlineData(SubCostCategories.Other.OtherInsurancePremiumsCosts, true, "BAE270")]
    [InlineData(SubCostCategories.Other.PrivateFinanceInitiativeCharges, false, "E28b")]
    [InlineData(SubCostCategories.Other.PrivateFinanceInitiativeCharges, true, "BAE310")]
    [InlineData(SubCostCategories.Other.RentRatesCosts, false, "E17")]
    [InlineData(SubCostCategories.Other.RentRatesCosts, true, "BAE160")]
    [InlineData(SubCostCategories.Other.SpecialFacilitiesCosts, false, "E24")]
    [InlineData(SubCostCategories.Other.SpecialFacilitiesCosts, true, "BAE190")]
    [InlineData(SubCostCategories.Other.StaffDevelopmentTrainingCosts, false, "E09")]
    [InlineData(SubCostCategories.Other.StaffDevelopmentTrainingCosts, true, "BAE090")]
    [InlineData(SubCostCategories.Other.StaffRelatedInsuranceCosts, false, "E11")]
    [InlineData(SubCostCategories.Other.StaffRelatedInsuranceCosts, true, "BAE110")]
    [InlineData(SubCostCategories.Other.SupplyTeacherInsurableCosts, false, "E10")]
    [InlineData(SubCostCategories.Other.SupplyTeacherInsurableCosts, true, "BAE100")]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolStaff, false, "E31")]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolStaff, true, null)]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolCosts, false, "E32")]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolCosts, true, null)]
    public void GetsCorrectCostCodeForOther(string subCategory, bool isPartOfTrust, string? expectedCostCode)
    {
        AssertCodeEqual(subCategory, isPartOfTrust, expectedCostCode);
    }

    private static void AssertCodeEqual(string subCategory, bool isPartOfTrust, string? expectedCostCode)
    {
        var costCodes = new CostCodes(isPartOfTrust);

        var actualCostCode = costCodes.GetCostCode(subCategory);

        Assert.Equal(expectedCostCode, actualCostCode);
    }
}