using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.LocalAuthority;

public class WhenViewingPupilWorkforceData(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/local-authority/{TestConfiguration.LocalAuthority}/census";

    [Theory]
    [InlineData("table")]
    [InlineData("chart")]
    public async Task ThenThereAreNoAccessibilityIssues(string mode)
    {
        await GoToPage();
        await Page.Locator($"#mode-{mode}").ClickAsync();
        await EvaluatePage();
    }
}