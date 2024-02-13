using Deque.AxeCore.Commons;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages;

public class WhenViewingFindOrganisation(ITestOutputHelper outputHelper)
    : PageBase(outputHelper)
{
    //known issues conditionally reveal https://design-system.service.gov.uk/components/radios/
    private readonly AxeRunContext _context = new()
        { Exclude = [new AxeSelector("#school"), new AxeSelector("#trust")] };

    protected override string PageUrl => $"{TestConfiguration.ServiceUrl}/find-organisation";

    [Theory]
    [InlineData("school")]
    [InlineData("trust")]
    public async Task ThenThereAreNoAccessibilityIssues(string organisationType)
    {
        Page = await Driver.GetPage(PageUrl);
        await Page.Locator($"#{organisationType}").ClickAsync();
        await EvaluatePage(_context);
    }

    [Theory]
    [InlineData("school")]
    [InlineData("trust")]
    public async Task ValidationErrorThenThereAreNoAccessibilityIssues(string organisationType)
    {
        Page = await Driver.GetPage(PageUrl);
        await Page.Locator($"#{organisationType}").ClickAsync();
        await Page.Locator(":text('Continue')").ClickAsync();
        await EvaluatePage(_context);
    }
}