using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Pages.Trust;

public class WhenViewingHome(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/trust/{TestConfiguration.Trust}";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage();
    }
}