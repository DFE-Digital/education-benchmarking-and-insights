using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages;

public class WhenViewingFindOrganisation(WebDriver driver, ITestOutputHelper outputHelper) : PageBase(outputHelper), IClassFixture<WebDriver>
{
    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        Page = await driver.GetPage(PageUrl);
        await EvaluatePage();
    }

    protected override string PageUrl => $"{TestConfiguration.ServiceUrl}/find-organisation";
}