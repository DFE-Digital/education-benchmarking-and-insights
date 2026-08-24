using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.LocalAuthority;

// Requires correct organisation assigned to DSI account

/*[Trait("Category", "LocalAuthorityRisksEnabled")]
public class WhenViewingLocalAuthorityRisks(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : AuthPageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/local-authority/{TestConfiguration.LocalAuthority}/risks";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssuesAcrossKeyInteractions()
    {
        // initial page
        await GoToPage();
        await EvaluatePage();

        // apply a phase
        await Page.Locator("#SelectedPhaseOption").SelectOptionAsync("Primary");
        await Page.Locator("button:has-text(\"Apply\")").ClickAsync();
        await EvaluatePage();

        // apply a sort (clicking the header button)
        await Page.Locator("th:has-text(\"URN\") button").ClickAsync();
        await EvaluatePage();
    }
}*/
