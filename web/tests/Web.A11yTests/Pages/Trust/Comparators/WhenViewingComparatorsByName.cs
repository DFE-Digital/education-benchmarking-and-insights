using Deque.AxeCore.Commons;
using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Pages.Trust.Comparators;

[Trait("Category", "Comparators")]
public class WhenViewingComparatorsByName(ITestOutputHelper testOutputHelper, WebDriver webDriver) : AuthPageBase(testOutputHelper, webDriver)
{
    private readonly AxeRunContext _context = new()
    {
        // known issues
        Exclude =
        [
            // Ensures that every form element has a visible label and is not solely labeled using hidden labels, or the title or aria-describedby attributes
            // Ensures every form element has a label
            new AxeSelector("#trust-input")
        ]
    };

    protected override string PageUrl => $"/trust/{TestConfiguration.Trust}/comparators/create/by/name";

    [Fact]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await EvaluatePage(_context);
    }

    [Fact]
    public async Task ValidationErrorThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await Page.Locator("main button[type='submit']").ClickAsync();
        await EvaluatePage(_context);
    }

    [Fact]
    public async Task AddSchoolThenThereAreNoAccessibilityIssues()
    {
        await GoToPage();
        await Page.Locator("#trust-input").FillAsync("trust");
        await Page.Locator("#trust-input__listbox.autocomplete__menu--visible").WaitForAsync();
        await Page.Keyboard.DownAsync("ArrowDown");
        await Page.Keyboard.DownAsync("Enter");
        await Page.Locator("main button[type='submit']").ClickAsync();
        await EvaluatePage(_context);
    }
}