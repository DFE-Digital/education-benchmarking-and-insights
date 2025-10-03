using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.Schools;

public class WhenViewingBenchmarkItSpending(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{TestConfiguration.School}/benchmark-it-spending";

    [Theory]
    [InlineData("Chart")]
    [InlineData("Table")]
    public async Task ThenThereAreNoAccessibilityIssues(string viewAs)
    {
        await GoToPage();
        await Page.Locator($"input#view-{viewAs}").ClickAsync();
        await Page.Locator("form button[type='submit']").ClickAsync();
        await EvaluatePage();
    }
}