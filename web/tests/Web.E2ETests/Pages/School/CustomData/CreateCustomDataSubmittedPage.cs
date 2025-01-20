using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School.CustomData;

public class CreateCustomDataSubmittedPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Generating custom data"
        });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }
}