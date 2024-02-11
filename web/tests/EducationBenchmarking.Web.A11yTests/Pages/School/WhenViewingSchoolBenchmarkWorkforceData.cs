using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages.School;

public class WhenViewingSchoolBenchmarkWorkforceData(WebDriver driver, ITestOutputHelper outputHelper) : PageBase(outputHelper), IClassFixture<WebDriver>
{
    [Theory]
    [InlineData("table")]
    [InlineData("chart")]
    public async Task ThenThereAreNoAccessibilityIssues(string mode)
    {
        Page = await driver.GetPage(PageUrl);
        await Page.Locator($"#mode-{mode}").ClickAsync();
        await EvaluatePage();
    }
    protected override string PageUrl => $"{TestConfiguration.ServiceUrl}/school/{TestConfiguration.School}/workforce";
}