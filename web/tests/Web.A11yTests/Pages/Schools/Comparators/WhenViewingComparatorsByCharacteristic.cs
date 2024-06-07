using Deque.AxeCore.Commons;
using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Pages.Schools.Comparators;

[Trait("Category", "Comparators")]
public class WhenViewingComparatorsByCharacteristic(ITestOutputHelper testOutputHelper, WebDriver webDriver) : AuthPageBase(testOutputHelper, webDriver)
{
    private readonly AxeRunContext _context = new()
    {
        // known issues
        Exclude =
        [
            // Ensures an element's role supports its ARIA attributes
            new AxeSelector("#LaSelection-Choose")
        ]
    };

    protected override string PageUrl => $"/school/{TestConfiguration.School}/comparators/create/by/characteristic";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage(_context);
    }

    [Fact]
    public async Task WhenSelectingAllAdditionalCharacteristicsThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        foreach (var input in await Page.Locator("#additional-characteristics > div > div > input[type='checkbox']").AllAsync())
        {
            await input.ClickAsync();
        }

        await EvaluatePage(_context);
    }

    [Fact]
    public async Task ValidationErrorThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await Page.Locator("#additional-characteristics > div > div > input[type='checkbox']").First.ClickAsync();
        await Page.Locator("button[value='continue']").ClickAsync();
        await EvaluatePage(_context);
    }
}