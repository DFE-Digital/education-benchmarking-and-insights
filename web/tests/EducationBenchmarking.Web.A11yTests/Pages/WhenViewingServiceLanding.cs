using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages;

public class WhenViewingServiceLanding(ITestOutputHelper outputHelper) : PageBase(outputHelper)
{
    protected override string PageUrl => $"{TestConfiguration.ServiceUrl}/";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        Page = await Driver.GetPage(PageUrl);
        await EvaluatePage();
    }
}