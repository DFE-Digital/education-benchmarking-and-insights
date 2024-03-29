using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.Schools;

public class WhenViewingDetails(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{TestConfiguration.School}/details";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage();
    }
}