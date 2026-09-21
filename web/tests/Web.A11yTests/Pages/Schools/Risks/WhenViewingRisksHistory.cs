using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.Schools.Risks;

// Requires correct organisation assigned to DSI account
// the below is just a template for the required test for this page
// updates will be required for DSI
// updates will be required for data seeding and/or Test config for TestConfiguration.School

/*[Trait("Category", "LocalAuthorityRisksEnabled")]
public class WhenViewingRisksHistory(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : AuthPageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/local-authority/{TestConfiguration.LocalAuthority}/risks/school/{TestConfiguration.School}/history";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssuesAcrossKeyInteractions()
    {
        // initial page
        await GoToPage();
        await EvaluatePage();

        // apply a view option
        await Page.Locator("#view-Table").CheckAsync();
        await Page.Locator("button:has-text(\"Apply\")").ClickAsync();
        await EvaluatePage();
    }
}*/
