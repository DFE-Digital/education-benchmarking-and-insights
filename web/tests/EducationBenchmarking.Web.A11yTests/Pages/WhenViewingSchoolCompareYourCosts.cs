using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages;

public class WhenViewingSchoolCompareYourCosts(WebDriver driver, ITestOutputHelper outputHelper) : PageBase(outputHelper), IClassFixture<WebDriver>
{
    private const string SchoolUrn = "139696";
    
    [Theory]
    [InlineData("table")]
    [InlineData("chart")]
    public async Task ThenThereAreNoAccessibilityIssues(string mode)
    {
        Page = await driver.GetPage(PageUrl);
        await Page.Locator($"#mode-{mode}").ClickAsync();
        await EvaluatePage();
    }
    
    protected override string PageUrl => $"{TestConfiguration.BaseUrl}/school/{SchoolUrn}/comparison";
}