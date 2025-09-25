using Microsoft.Playwright;

namespace Web.E2ETests.Pages.Trust.Comparators;

public class ViewComparatorsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Your set of trusts"
        });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }
}