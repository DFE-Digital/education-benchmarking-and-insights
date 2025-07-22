using Microsoft.FeatureManagement;
using Moq;
using Web.App;
using Web.App.Domain;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Services;

public class WhenCostCodesServiceIsCalled
{
    private readonly Mock<IFeatureManager> _featureManager;
    private readonly CostCodesService _service;
    private const string SubCategory = SubCostCategories.EducationalIct.LearningResourcesIctCosts;

    public WhenCostCodesServiceIsCalled()
    {
        _featureManager = new Mock<IFeatureManager>();
        _service = new CostCodesService(_featureManager.Object);
    }

    [Fact]
    public async Task ShouldGetAcademyCostCodesIfPartOfTrust()
    {
        var actual = await _service.GetCostCodes(true);

        Assert.NotNull(actual);
        Assert.Equal("BAE210", actual.GetCostCodes(SubCategory));
        _featureManager.Verify(f => f.IsEnabledAsync(It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [InlineData(false, "E20")]
    [InlineData(true, "E20A,E20B,E20C,E20E,E20F,E20G")]
    public async Task ShouldGetMaintainedCostCodesIfNotPartOfTrust(bool cfrItSpendBreakdown, string expected)
    {
        _featureManager.Setup(f => f.IsEnabledAsync(FeatureFlags.CfrItSpendBreakdown)).ReturnsAsync(cfrItSpendBreakdown);

        var actual = await _service.GetCostCodes(false);

        Assert.NotNull(actual);
        Assert.Equal(expected, actual.GetCostCodes(SubCategory));
    }
}