using EducationBenchmarking.Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages.Schools.FinancialPlanning;

[Collection(nameof(FinancialPlanCollection))]
public class WhenViewingFinancialPlanningTotalNumberTeachers(
    ITestOutputHelper outputHelper,
    WebDriver webDriver,
    FinancialPlanFixture plan)
    : PageBase(outputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{plan.Urn}/financial-planning/create?step=total-number-teachers&year={plan.Year}";

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