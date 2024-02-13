using EducationBenchmarking.Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages.Schools;

public class WhenViewingCompareYourCosts(
    ITestOutputHelper testOutputHelper, 
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{TestConfiguration.School}/comparison";

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