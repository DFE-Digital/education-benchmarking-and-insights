using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School.CustomData;

public class RevertCustomDataPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Change back to the original data?"
        });

    private ILocator RemoveCustomDataButton => page.Locator(Selectors.GovButton,
        new PageLocatorOptions
        {
            HasText = "Remove custom data"
        });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task<HomePage> ClickRemoveCustomData()
    {
        await RemoveCustomDataButton.ClickAsync();
        return new HomePage(page);
    }
}