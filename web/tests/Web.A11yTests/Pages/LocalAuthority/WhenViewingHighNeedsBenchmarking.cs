using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.LocalAuthority;

[Trait("Category", "HighNeedsFlagEnabled")]
public class WhenViewingHighNeedsBenchmarking(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/local-authority/{TestConfiguration.LocalAuthority}/high-needs/benchmarking/comparators";

    [Theory]
    [InlineData("mode-chart")]
    [InlineData("mode-table")]
    public async Task ThenThereAreNoAccessibilityIssues(string mode)
    {
        await GoToPage();
        await Page.Locator("#LaInput").FillAsync("Hackney");
        await Page.Locator("button[type=submit][name=action][value=add]").ClickAsync();
        await Page.Locator("button[type=submit][name=action][value=continue]").ClickAsync();
        await Page.Locator($"#{mode}").ClickAsync();
        await EvaluatePage();
    }
}