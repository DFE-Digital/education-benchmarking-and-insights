using EducationBenchmarking.Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages.Schools.FinancialPlanning;

[Collection(nameof(FinancialPlanCollection))]
public class WhenViewingFinancialPlanningTotalIncome(
    ITestOutputHelper outputHelper,
    WebDriver webDriver,
    FinancialPlanFixture plan)
    : PageBase(outputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{plan.Urn}/financial-planning/steps/total-income?year={plan.Year}";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage();
    }

    [Fact]
    public async Task ValidationErrorThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await Page.Locator(":text('Continue')").ClickAsync();
        await EvaluatePage();
    }
}