using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.Schools;

public class WhenViewingCommercialResources(
        ITestOutputHelper testOutputHelper,
        WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{TestConfiguration.School}/find-ways-to-spend-less";

    [Theory]
    [InlineData("all")]
    [InlineData("recommended")]
    public async Task ThenThereAreNoAccessibilityIssues(string resource)
    {
        await GoToPage();
        await Page.Locator($"#tab_{resource}").ClickAsync();
        await EvaluatePage();
    }
}