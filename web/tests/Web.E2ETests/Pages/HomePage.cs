using Microsoft.Playwright;

namespace Web.E2ETests.Pages;

public class HomePage(IPage page) : BasePage(page)
{
    private ILocator DataSourcesLink =>
        page.Locator(Selectors.GovFooterLink, new PageLocatorOptions
        {
            HasText = "Data Sources"
        });

    private ILocator StartNowButton =>
        page.Locator(Selectors.GovButton, new PageLocatorOptions
        {
            HasText = "Start now"
        });

    public override async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task<DataSourcesPage> ClickDataSourcesLink()
    {
        await DataSourcesLink.ClickAsync();
        return new DataSourcesPage(page);
    }

    public async Task<FindOrganisationPage> ClickStartNowButton()
    {
        await StartNowButton.ClickAsync();
        return new FindOrganisationPage(page);
    }
}