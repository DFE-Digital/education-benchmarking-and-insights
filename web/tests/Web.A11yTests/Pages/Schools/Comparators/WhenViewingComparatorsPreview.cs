using Deque.AxeCore.Commons;
using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Pages.Schools.Comparators;

[Trait("Category", "Comparators")]
public class WhenViewingComparatorsPreview(ITestOutputHelper testOutputHelper, WebDriver webDriver) : AuthPageBase(testOutputHelper, webDriver)
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
        await Page.Locator("button[value='continue']").ClickAsync();
        await Page.WaitForURLAsync("**/preview");
        await EvaluatePage(_context);
    }
}