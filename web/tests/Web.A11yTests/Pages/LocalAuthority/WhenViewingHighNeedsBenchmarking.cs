using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.LocalAuthority;

[Trait("Category", "HighNeedsBenchmarkingFlagEnabled")]
public class WhenViewingHighNeedsBenchmarking(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/local-authority/{TestConfiguration.LocalAuthority}/comparators?type=HighNeedsSpending";
    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage();
        await Page.Locator("#LaInput").FillAsync("Hackney");
        await Page.Locator("button[type=submit][name=action][value=add]").ClickAsync();
        await EvaluatePage();
        await Page.Locator("button[type=submit][name=action][value=continue]").ClickAsync();
        //evaluating the table view of the page
        await EvaluatePage();
        await Page.Locator("#view-Table").ClickAsync();
        await Page.Locator("button:text(\"Apply\")").Nth(1).ClickAsync();
        await EvaluatePage();
    }

}