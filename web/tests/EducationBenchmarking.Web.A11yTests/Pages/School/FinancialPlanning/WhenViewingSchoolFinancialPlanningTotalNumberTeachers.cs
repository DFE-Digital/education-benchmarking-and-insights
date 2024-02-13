using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages.School.FinancialPlanning;

[Collection(nameof(FinancialPlanCollection))]
public class WhenViewingSchoolFinancialPlanningTotalNumberTeachers(
    ITestOutputHelper outputHelper,
    FinancialPlanFixture plan) : PageBase(outputHelper)
{
    protected override string PageUrl =>
        $"{TestConfiguration.ServiceUrl}/school/{plan.Urn}/financial-planning/steps/total-number-teachers?year={plan.Year}";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        Page = await Driver.GetPage(PageUrl);
        await EvaluatePage();
    }

    [Fact]
    public async Task ValidationErrorThenThereAreNoAccessibilityIssues()
    {
        Page = await Driver.GetPage(PageUrl);
        await Page.Locator(":text('Continue')").ClickAsync();
        await EvaluatePage();
    }
}