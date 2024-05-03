using Deque.AxeCore.Commons;
using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages;

public class WhenViewingFindOrganisation(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    private readonly AxeRunContext _context = new()
    {
        // known issues
        Exclude =
        [
            // conditionally reveal https://design-system.service.gov.uk/components/radios/
            new AxeSelector("#local-authority"),
            new AxeSelector("#school"),
            new AxeSelector("#trust"),
            // accessible-autocomplete component uses `aria-describedby` rather than an explicit label
            new AxeSelector("#la-input"),
            new AxeSelector("#school-input"),
            new AxeSelector("#trust-input")
        ]
    };

    protected override string PageUrl => "/find-organisation";

    [Theory]
    [InlineData("local-authority")]
    [InlineData("school")]
    [InlineData("trust")]
    public async Task ThenThereAreNoAccessibilityIssues(string organisationType)
    {
        await GoToPage();
        await Page.Locator($"#{organisationType}").ClickAsync();
        await EvaluatePage(_context);
    }

    [Theory]
    [InlineData("local-authority")]
    [InlineData("school")]
    [InlineData("trust")]
    public async Task ValidationErrorThenThereAreNoAccessibilityIssues(string organisationType)
    {
        await GoToPage();
        await Page.Locator($"#{organisationType}").ClickAsync();
        await Page.Locator(":text('Continue')").ClickAsync();
        await EvaluatePage(_context);
    }
}