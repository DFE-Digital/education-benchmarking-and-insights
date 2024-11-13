using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.Trust;

public class WhenViewingSchoolSpending(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/trust/{TestConfiguration.Trust}/comparison";

    [Theory]
    [InlineData("table")]
    [InlineData("chart")]
    public async Task ShowAllSectionsThenThereAreNoAccessibilityIssues(string mode)
    {
        await GoToPage();
        await Page.Locator($"#mode-{mode}").ClickAsync();
        await Page.Locator(".govuk-accordion__show-all").ClickAsync();
        await EvaluatePage();
    }

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