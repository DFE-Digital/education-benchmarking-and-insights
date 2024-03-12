using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.Schools.FinancialPlanning;

[Collection(nameof(FinancialPlanMinimalDataCollection))]
public class WhenViewingFinancialPlanningTimetableCycle(
    ITestOutputHelper outputHelper,
    WebDriver webDriver,
    FinancialPlanMinimalDataFixture plan)
    : PageBase(outputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{plan.Urn}/financial-planning/create?step=timetable-cycle&year={plan.Year}";

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