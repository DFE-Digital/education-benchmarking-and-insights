using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.LocalAuthority;

public class WhenViewingSearch(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => "/local-authority";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage();
    }

    [Fact]
    public async Task ValidationErrorThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await Page.Locator("button[type='submit'][value='reset']").ClickAsync();
        await EvaluatePage();
    }
}