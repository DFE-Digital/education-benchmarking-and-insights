using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Pages.Trust.Comparators;

[Trait("Category", "Comparators")]
public class WhenViewingComparatorsByCharacteristic(ITestOutputHelper testOutputHelper, WebDriver webDriver) : AuthPageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/trust/{TestConfiguration.Trust}/comparators/create/by/characteristic";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage();
    }

    [Fact]
    public async Task WhenSelectingAllAdditionalCharacteristicsThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        foreach (var input in await Page.Locator("#additional-characteristics > div > div > input[type='checkbox']").AllAsync())
        {
            await input.ClickAsync();
        }

        await EvaluatePage();
    }

    [Fact]
    public async Task ValidationErrorThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await Page.Locator("#additional-characteristics > div > div > input[type='checkbox']").First.ClickAsync();
        await Page.Locator("button[value='continue']").ClickAsync();
        await EvaluatePage();
    }
}