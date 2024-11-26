using Microsoft.Playwright;
namespace Web.E2ETests.Pages;

public class HomePage(IPage page) : BasePage(page)
{
    private ILocator DataSourcesLink =>
        page.Locator(Selectors.GovFooterLink, new PageLocatorOptions
        {
            HasText = "Data Sources"
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
}