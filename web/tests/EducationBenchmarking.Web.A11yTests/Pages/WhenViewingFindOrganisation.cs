using Deque.AxeCore.Commons;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Pages;

public class WhenViewingFindOrganisation(WebDriver driver, ITestOutputHelper outputHelper)
    : PageBase(outputHelper), IClassFixture<WebDriver>
{
    //known issues conditionally reveal https://design-system.service.gov.uk/components/radios/
    private readonly AxeRunContext _context = new() { Exclude = [new AxeSelector("#school"), new AxeSelector("#trust")] };

    [Theory]
    [InlineData(null)]
    [InlineData("school")]
    [InlineData("trust")]
    public async Task ThenThereAreNoAccessibilityIssues(string? organisationType)
    {
        Page = await driver.GetPage(PageUrl);
        if (!string.IsNullOrEmpty(organisationType))
        {
            await Page.Locator($"#{organisationType}").ClickAsync();    
        }
        
        await EvaluatePage(_context);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("school")]
    [InlineData("trust")]
    public async Task ValidationErrorThenThereAreNoAccessibilityIssues(string? organisationType)
    {
        Page = await driver.GetPage(PageUrl);
        if (!string.IsNullOrEmpty(organisationType))
        {
            await Page.Locator($"#{organisationType}").ClickAsync();    
        }
        await Page.Locator(":text('Continue')").ClickAsync();
        await EvaluatePage(_context);
    }

    protected override string PageUrl => $"{TestConfiguration.ServiceUrl}/find-organisation";
}