using Web.App;
using Web.App.Domain.LocalAuthorities;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenARiskMetricViewModel
{
    [Theory]
    [InlineData(RiskIndicatorValueFormatting.Percentage, "0.785", "78.5%")]
    [InlineData(RiskIndicatorValueFormatting.Percentage, "0.5", "50%")]
    [InlineData(RiskIndicatorValueFormatting.Percentage, "1", "100%")]
    [InlineData(RiskIndicatorValueFormatting.Percentage, "0", "0%")]
    [InlineData(RiskIndicatorValueFormatting.Decimal, "1234.5", "1234.5")]
    [InlineData(RiskIndicatorValueFormatting.Decimal, "1234.5678", "1234.57")]
    [InlineData(RiskIndicatorValueFormatting.Decimal, "0", "0")]
    [InlineData(RiskIndicatorValueFormatting.Boolean, "Yes", "Yes")]
    [InlineData(RiskIndicatorValueFormatting.Boolean, "No", "No")]
    [InlineData(RiskIndicatorValueFormatting.String, "foo", "foo")]
    public void WhenValueIsDisplayableShouldFormatValueCorrectly(string formatType, string rawValue, string expectedDisplay)
    {
        var metric = new RisksMetrics
        {
            RiskIndicator = "Test Indicator",
            RiskIndicatorValue = rawValue,
            RiskIndicatorValueFormatting = formatType,
            RiskIndicatorFlag = LocalAuthorityRiskFlags.Minor
        };

        var vm = new RiskMetricViewModel(metric);

        Assert.Equal(expectedDisplay, vm.ValueDisplay);
    }

    [Theory]
    [InlineData(RiskIndicatorValueFormatting.Percentage, "invalidNumber")]
    [InlineData(RiskIndicatorValueFormatting.Decimal, "notDecimal")]
    [InlineData("UnknownFormat", "SomeValue")]
    public void WhenValueFailsParsingOrFormatIsUnknownShouldFallbackToRawValue(string formatType, string rawValue)
    {
        var metric = new RisksMetrics
        {
            RiskIndicator = "Test Indicator",
            RiskIndicatorValue = rawValue,
            RiskIndicatorValueFormatting = formatType,
            RiskIndicatorFlag = LocalAuthorityRiskFlags.NoFlag
        };

        var vm = new RiskMetricViewModel(metric);

        Assert.Equal(rawValue, vm.ValueDisplay);
    }

    [Theory]
    [InlineData(RiskIndicatorValueFormatting.Percentage)]
    [InlineData(RiskIndicatorValueFormatting.Decimal)]
    [InlineData(RiskIndicatorValueFormatting.Boolean)]
    [InlineData(RiskIndicatorValueFormatting.String)]
    [InlineData("UnknownFormat")]
    public void WhenValueIsNullShouldDefaultToMissing(string formatType)
    {
        var metric = new RisksMetrics
        {
            RiskIndicator = "Test Indicator",
            RiskIndicatorValue = null,
            RiskIndicatorValueFormatting = formatType,
            RiskIndicatorFlag = LocalAuthorityRiskFlags.NoFlag
        };

        var vm = new RiskMetricViewModel(metric);

        Assert.Equal("Missing", vm.ValueDisplay);
    }

    [Theory]
    [InlineData(LocalAuthorityRiskFlags.Major, "govuk-tag--red", 1)]
    [InlineData(LocalAuthorityRiskFlags.Minor, "govuk-tag--yellow", 2)]
    [InlineData(LocalAuthorityRiskFlags.NoFlag, "govuk-tag--grey", 3)]
    [InlineData("UnknownFlag", "govuk-tag--grey", 3)]
    public void WhenMappingFlagsShouldAssignCorrectTagClassAndSortValue(string flag, string expectedTagClass, int expectedSortValue)
    {
        var metric = new RisksMetrics
        {
            RiskIndicator = "Test Indicator",
            RiskIndicatorValue = "100",
            RiskIndicatorValueFormatting = RiskIndicatorValueFormatting.Decimal,
            RiskIndicatorFlag = flag
        };

        var vm = new RiskMetricViewModel(metric);

        Assert.Equal(expectedTagClass, vm.TagColourClass);
        Assert.Equal(expectedSortValue, vm.SortValue);
    }
}
