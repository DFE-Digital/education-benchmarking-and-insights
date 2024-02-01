using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages;

public class WhenViewingSchoolFinancialPlanningPrePopulatedData(WebDriver driver, ITestOutputHelper outputHelper) : PageBase(outputHelper), IClassFixture<WebDriver>
{
    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        Page = await driver.GetPage(PageUrl);
        await EvaluatePage();
    }
    
    [Fact]
    public async Task ValidationErrorThenThereAreNoAccessibilityIssues()
    {
        Page = await driver.GetPage(PageUrl);
        await Page.Locator(":text('Continue')").ClickAsync();
        await EvaluatePage();
    }

    protected override string PageUrl => $"{TestConfiguration.ServiceUrl}/school/{TestConfiguration.School}/financial-planning/{TestConfiguration.PlanYear}";
}