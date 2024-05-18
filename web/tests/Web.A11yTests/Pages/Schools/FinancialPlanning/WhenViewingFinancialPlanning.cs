using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Pages.Schools.FinancialPlanning;

/*[Trait("Category", "FinancialPlanning")]
public class WhenViewingFinancialPlanning(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : AuthPageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{TestConfiguration.School}/financial-planning";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage();
    }
}*/