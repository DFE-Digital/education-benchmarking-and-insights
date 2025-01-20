using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School.CustomData;

public class CreateCustomDataPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Change data used to compare this school"
        });

    private ILocator StartNowButton => page.Locator(Selectors.GovButton,
        new PageLocatorOptions
        {
            HasText = "Start now"
        });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task<ChangeFinancialDataPage> ClickStartNow()
    {
        await StartNowButton.ClickAsync();
        return new ChangeFinancialDataPage(page);
    }
}