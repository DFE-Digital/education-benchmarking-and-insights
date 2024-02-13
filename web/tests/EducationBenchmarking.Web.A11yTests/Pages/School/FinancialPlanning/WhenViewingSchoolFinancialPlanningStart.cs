using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages.School.FinancialPlanning;

public class WhenViewingSchoolFinancialPlanningStart(ITestOutputHelper outputHelper) : PageBase(outputHelper)
{
    protected override string PageUrl =>
        $"{TestConfiguration.ServiceUrl}/school/{TestConfiguration.School}/financial-planning/steps/start";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        Page = await Driver.GetPage(PageUrl);
        await EvaluatePage();
    }
}