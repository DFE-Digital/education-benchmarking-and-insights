using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School;

public class DetailsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator BackLink => page.Locator(Selectors.GovBackLink);
    private ILocator GiasPageLink => page.Locator(Selectors.SchoolGiasPageLink);
    private ILocator EmailAddressField => page.Locator(Selectors.SchoolDetailsEmailAddress);


    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await BackLink.ShouldBeVisible();
        await GiasPageLink.ShouldBeVisible().ShouldHaveAttribute("target", "_blank");
    }
}