using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages.School;

public class WhenViewingSchoolHome(ITestOutputHelper outputHelper) : PageBase(outputHelper)
{
    protected override string PageUrl => $"{TestConfiguration.ServiceUrl}/school/{TestConfiguration.School}";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        Page = await Driver.GetPage(PageUrl);
        await EvaluatePage();
    }
}