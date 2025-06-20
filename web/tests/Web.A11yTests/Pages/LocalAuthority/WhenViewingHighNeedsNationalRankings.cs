using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.LocalAuthority;

[Trait("Category", "HighNeedsFlagEnabled")]
public class WhenViewingHighNeedsNationalRankings(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/local-authority/{TestConfiguration.LocalAuthority}/high-needs/national-rank";

    [Theory]
    [InlineData("funding", "funding-mode-chart")]
    [InlineData("funding", "funding-mode-table")]
    [InlineData("planned-expenditure", "planned-expenditure-mode-chart")]
    [InlineData("planned-expenditure", "planned-expenditure-mode-table")]
    public async Task ThenThereAreNoAccessibilityIssues(string tab, string chartTableMode)
    {
        await GoToPage();
        await Page.Locator($"#tab_{tab}").ClickAsync();
        await Page.Locator($"#{chartTableMode}").ClickAsync();
        await EvaluatePage();
    }
}