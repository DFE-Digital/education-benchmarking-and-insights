using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.Schools;

[Trait("Category", "SeniorLeadershipFlagEnabled")]
public class WhenViewingBenchmarkSeniorLeadership(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{TestConfiguration.School}/census/senior-leadership";

    [Theory]
    [InlineData("Table")]
    [InlineData("Chart")]
    public async Task ThenThereAreNoAccessibilityIssues(string mode)
    {
        await GoToPage();
        await Page.Locator($"#view-{mode}").ClickAsync();
        await Page.Locator("form .actions-form__button button.govuk-button").ClickAsync();
        await EvaluatePage();
    }
}