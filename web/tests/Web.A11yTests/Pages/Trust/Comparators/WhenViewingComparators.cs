using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Pages.Trust.Comparators;

[Trait("Category", "Comparators")]
public class WhenViewingComparators(ITestOutputHelper testOutputHelper, WebDriver webDriver) : AuthPageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/trust/{TestConfiguration.Trust}/comparators/create/by/name";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await Page.Locator("#trust-input").FillAsync("trust");
        await Page.Locator("#trust-input__listbox.autocomplete__menu--visible").WaitForAsync();
        await Page.Keyboard.DownAsync("ArrowDown");
        await Page.Keyboard.DownAsync("Enter");
        await Page.Locator("button[type='submit']").ClickAsync();
        await Page.Locator("#create-set").WaitForAsync();
        await Page.Locator("#create-set").ClickAsync();
        await Page.WaitForURLAsync("**/comparators?comparator-generated=true");
        await Page.Locator("#tab_balance").WaitForAsync();
        await EvaluatePage();
    }
}