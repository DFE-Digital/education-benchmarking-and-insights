using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages.School.FinancialPlanning;

[Collection(nameof(FinancialPlanCollection))]
public class WhenViewingSchoolFinancialPlanningTotalIncome(WebDriver driver, ITestOutputHelper outputHelper, FinancialPlanFixture plan) : PageBase(outputHelper), IClassFixture<WebDriver>
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

    protected override string PageUrl => $"{TestConfiguration.ServiceUrl}/school/{plan.Urn}/financial-planning/steps/total-income?year={plan.Year}";
}