using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages;

public class WhenViewingSchoolCompareYourCosts(WebDriver driver, ITestOutputHelper outputHelper) : PageBase(outputHelper), IClassFixture<WebDriver>
{
    [Theory]
    [InlineData("table")]
    [InlineData("chart")]
    public async Task ShowAllSectionsThenThereAreNoAccessibilityIssues(string mode)
    {
        Page = await driver.GetPage(PageUrl);
        await Page.Locator($"#mode-{mode}").ClickAsync();
        await Page.Locator(".govuk-accordion__show-all").ClickAsync();
        await EvaluatePage();
    }
    
    [Theory]
    [InlineData("table")]
    [InlineData("chart")]
    public async Task ThenThereAreNoAccessibilityIssues(string mode)
    {
        Page = await driver.GetPage(PageUrl);
        await Page.Locator($"#mode-{mode}").ClickAsync();
        await EvaluatePage();
    }
    
    protected override string PageUrl => $"{TestConfiguration.ServiceUrl}/school/{TestConfiguration.School}/comparison";
}