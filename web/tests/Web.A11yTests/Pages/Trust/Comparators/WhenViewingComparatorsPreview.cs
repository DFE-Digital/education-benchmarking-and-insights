using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Pages.Trust.Comparators;

[Trait("Category", "Comparators")]
public class WhenViewingComparatorsPreview(ITestOutputHelper testOutputHelper, WebDriver webDriver) : AuthPageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/trust/{TestConfiguration.Trust}/comparators/create/by/characteristic";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await Page.Locator("#additional-characteristics > div > div > input[type='checkbox']").First.ClickAsync();
        await Page.Locator("#additional-characteristics > div > div > div > fieldset input[type='text']").Nth(0).FillAsync("123");
        await Page.Locator("#additional-characteristics > div > div > div > fieldset input[type='text']").Nth(1).FillAsync("456");
        await Page.Locator("button[value='continue']").ClickAsync();
        await Page.WaitForURLAsync("**/preview");
        await EvaluatePage();
    }
}