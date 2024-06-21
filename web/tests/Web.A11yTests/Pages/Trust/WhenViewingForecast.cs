using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Pages.Trust;

public class WhenViewingForecast(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : AuthPageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/trust/{TestConfiguration.Trust}/forecast";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage();
    }
}