using Microsoft.Playwright;

namespace Web.E2ETests.Pages.Trust.Comparators;

public class RevertComparatorsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Remove all your trusts?"
        });

    private ILocator ContinueButton => page.Locator(Selectors.GovButton,
        new PageLocatorOptions
        {
            HasText = "Remove all trusts"
        });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await ContinueButton.ShouldBeVisible();
    }

    public async Task<HomePage> ClickContinue()
    {
        await ContinueButton.ClickAsync();
        return new HomePage(page);
    }
}