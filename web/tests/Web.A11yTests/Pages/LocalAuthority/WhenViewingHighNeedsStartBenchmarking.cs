using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.LocalAuthority;

[Trait("Category", "HighNeedsFlagEnabled")]
public class WhenViewingHighNeedsStartBenchmarking(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/local-authority/{TestConfiguration.LocalAuthority}/high-needs/benchmarking/comparators";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage();
    }
}