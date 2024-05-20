using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Pages.Schools.CustomData;

[Trait("Category", "CustomData")]
public class WhenViewingCustomDataWorkforceData(ITestOutputHelper testOutputHelper, WebDriver webDriver) : AuthPageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{TestConfiguration.School}/custom-data/workforce";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage();
    }
}