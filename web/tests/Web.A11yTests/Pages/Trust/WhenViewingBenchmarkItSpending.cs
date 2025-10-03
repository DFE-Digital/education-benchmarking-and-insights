using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.Trust;

public class WhenViewingBenchmarkItSpending(ITestOutputHelper testOutputHelper, WebDriver webDriver) : AuthPageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/trust/{TestConfiguration.Trust}/comparators/create/by/name?redirectUri=/trust/{TestConfiguration.Trust}/benchmark-it-spending";

    [Theory]
    [InlineData("Chart")]
    [InlineData("Table")]
    public async Task ThenThereAreNoAccessibilityIssues(string viewAs)
    {
        await GoToPage();
        await Page.Locator("#trust-input").FillAsync("trust");
        await Page.Locator("#trust-input__listbox.autocomplete__menu--visible").WaitForAsync();
        await Page.Keyboard.DownAsync("ArrowDown");
        await Page.Keyboard.DownAsync("Enter");
        await Page.Locator("main button[type='submit']").ClickAsync();
        await Page.Locator("#create-set").WaitForAsync();
        await Page.Locator("#create-set").ClickAsync();
        await Page.WaitForURLAsync("**/benchmark-it-spending?comparator-generated=true");
        await Page.Locator($"input#view-{viewAs}").ClickAsync();
        await Page.Locator("form button[type='submit']").ClickAsync();
        await EvaluatePage();
    }
}