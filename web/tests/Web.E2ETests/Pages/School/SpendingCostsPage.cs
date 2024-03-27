using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School;

public class SpendingCostsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator Breadcrumbs => page.Locator(Selectors.GovBreadcrumbs);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await Breadcrumbs.ShouldBeVisible();

        //further assertions will be added here once the page is dev is completed.
    }
}