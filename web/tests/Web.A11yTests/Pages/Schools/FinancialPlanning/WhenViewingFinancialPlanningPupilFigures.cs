using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.Schools.FinancialPlanning;

[Collection(nameof(FinancialPlanMinimalDataCollection))]
public class WhenViewingFinancialPlanningPupilFigures(
    ITestOutputHelper outputHelper,
    WebDriver webDriver,
    FinancialPlanMinimalDataFixture plan)
    : PageBase(outputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{plan.Urn}/financial-planning/create/pupil-figures?year={plan.Year}";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await plan.Initialize;
        await GoToPage();
        await EvaluatePage();
    }

    [Fact]
    public async Task ValidationErrorThenThereAreNoAccessibilityIssues()
    {
        await plan.Initialize;
        await GoToPage();
        await Page.Locator(":text('Continue')").ClickAsync();
        await EvaluatePage();
    }
}