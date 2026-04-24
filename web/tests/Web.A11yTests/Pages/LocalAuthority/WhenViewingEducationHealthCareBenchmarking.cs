using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.LocalAuthority;

[Trait("Category", "HighNeedsBenchmarkingFlagEnabled")]
public class WhenViewingEducationHealthCareBenchmarking(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver) : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/local-authority/{TestConfiguration.LocalAuthority}/comparators?type=EducationHealthCarePlans";
    [Theory]
    [InlineData("view-Chart")]
    [InlineData("view-Table")]
    public async Task ThenThereAreNoAccessibilityIssues(string mode)
    {
        await GoToPage();
        await Page.Locator("button[type=submit][name=action][value=continue]").ClickAsync();
        await Page.Locator($"#{mode}").ClickAsync();
        await EvaluatePage();
    }
}