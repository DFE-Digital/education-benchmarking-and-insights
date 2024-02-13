using EducationBenchmarking.Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages.Schools.FinancialPlanning;

public class WhenViewingFinancialPlanningHelp(
    ITestOutputHelper testOutputHelper, 
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{TestConfiguration.School}/financial-planning/steps/help";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage();
    }
}