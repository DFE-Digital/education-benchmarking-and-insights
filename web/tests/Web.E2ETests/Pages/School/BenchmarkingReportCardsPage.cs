using Microsoft.Playwright;
namespace Web.E2ETests.Pages.School;

public class BenchmarkingReportCardsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }
}