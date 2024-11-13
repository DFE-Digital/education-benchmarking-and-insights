using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages;

public class WhenViewingContact(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => "/contact";

    [Theory]
    [InlineData("contact-us")]
    [InlineData("send-us-feedback")]
    public async Task ThenThereAreNoAccessibilityIssues(string resource)
    {
        await GoToPage();
        await Page.Locator($"#tab_{resource}").ClickAsync();
        await EvaluatePage();
    }
}