namespace Web.A11yTests.Pages.Schools.FinancialPlanning;

/*[Collection(nameof(FinancialPlanCollection))]
public class WhenViewingFinancialPlanningTotalNumberTeachers(
    ITestOutputHelper outputHelper,
    WebDriver webDriver,
    FinancialPlanFixture plan)
    : PageBase(outputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{plan.Urn}/financial-planning/create/total-number-teachers?year={plan.Year}";

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
}*/