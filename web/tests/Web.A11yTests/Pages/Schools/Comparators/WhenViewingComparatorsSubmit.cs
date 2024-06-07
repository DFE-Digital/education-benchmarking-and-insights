using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Pages.Schools.Comparators;

[Trait("Category", "Comparators")]
public class WhenViewingComparatorsSubmit(ITestOutputHelper testOutputHelper, WebDriver webDriver) : AuthPageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{TestConfiguration.School}/comparators/create/by/name";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await Page.Locator("#school-input").FillAsync("school");
        await Page.Locator("#school-input__listbox.autocomplete__menu--visible").WaitForAsync();
        await Page.Keyboard.DownAsync("ArrowDown");
        await Page.Keyboard.DownAsync("Enter");
        await Page.Locator("button[type='submit']").ClickAsync();
        await Page.Locator("#create-set").WaitForAsync();
        await Page.Locator("#create-set").ClickAsync();
        await Page.WaitForURLAsync("**/submit");
        await EvaluatePage();
    }
}