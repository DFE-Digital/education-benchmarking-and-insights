using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages.School;

public class WhenViewingSchoolBenchmarkWorkforceData(ITestOutputHelper outputHelper) : PageBase(outputHelper)
{
    protected override string PageUrl => $"{TestConfiguration.ServiceUrl}/school/{TestConfiguration.School}/workforce";

    [Theory]
    [InlineData("table")]
    [InlineData("chart")]
    public async Task ThenThereAreNoAccessibilityIssues(string mode)
    {
        Page = await Driver.GetPage(PageUrl);
        await Page.Locator($"#mode-{mode}").ClickAsync();
        await EvaluatePage();
    }
}