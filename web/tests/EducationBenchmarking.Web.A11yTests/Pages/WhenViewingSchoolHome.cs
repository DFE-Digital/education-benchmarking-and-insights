using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages;

public class WhenViewingSchoolHome(WebDriver driver, ITestOutputHelper outputHelper) : PageBase(outputHelper), IClassFixture<WebDriver>
{
    private const string SchoolUrn = "139696";
    
    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        Page = await driver.GetPage(PageUrl);
        await EvaluatePage();
    }

    protected override string PageUrl => $"{TestConfiguration.ServiceUrl}/school/{SchoolUrn}";
}