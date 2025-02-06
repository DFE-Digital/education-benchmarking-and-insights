using Web.App.Domain;
using Xunit;
namespace Web.Tests.Domain;

public class GivenASubCategoryCostCodes
{
    [Theory]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.AuditorsCosts, false, null)]
    [InlineData(SubCostCategories.NonEducationalSupportStaff.AuditorsCosts, true, "BAE260-T")]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolCosts, false, "E32")]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolCosts, true, null)]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolStaff, false, "E31")]
    [InlineData(SubCostCategories.Other.CommunityFocusedSchoolStaff, true, null)]
    public void GetsCorrectCostCode(string subCategory, bool isPartOfTrust, string? expectedCostCode)
    {
        var costCodes = new CostCodes(isPartOfTrust);

        var actualCostCode = costCodes.GetCostCode(subCategory);

        Assert.Equal(expectedCostCode, actualCostCode);
    }
}