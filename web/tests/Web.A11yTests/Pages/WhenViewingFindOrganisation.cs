using Deque.AxeCore.Commons;
using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages;

public class WhenViewingFindOrganisation(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => "/find-organisation";

    [Theory]
    [InlineData("local-authority")]
    [InlineData("school")]
    [InlineData("trust")]
    public async Task ThenThereAreNoAccessibilityIssuesWithFilteredSearch(string organisationType)
    {
        await GoToPage();
        await Page.Locator($"#{organisationType}").ClickAsync();
        await EvaluatePage();
    }
}