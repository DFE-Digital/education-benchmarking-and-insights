using Deque.AxeCore.Commons;
using EducationBenchmarking.Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages;

public class WhenViewingFindOrganisation(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    //known issues conditionally reveal https://design-system.service.gov.uk/components/radios/
    private readonly AxeRunContext _context = new()
    { Exclude = [new AxeSelector("#school"), new AxeSelector("#trust")] };

    protected override string PageUrl => "/find-organisation";

    [Theory]
    [InlineData("school")]
    [InlineData("trust")]
    public async Task ThenThereAreNoAccessibilityIssues(string organisationType)
    {
        await GoToPage();
        await Page.Locator($"#{organisationType}").ClickAsync();
        await EvaluatePage(_context);
    }

    [Theory]
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