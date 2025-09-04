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

    private ILocator Banner => page.Locator(Selectors.GovNotificationBanner);
    private ILocator BannerTitle => page.Locator(Selectors.GovNotificationBannerTitle);
    private ILocator BannerHeading => page.Locator(Selectors.GovNotificationBannerHeading);
    private ILocator BannerBody => page.Locator(Selectors.GovNotificationBannerBody);

    private ILocator NewsLink =>
        page.Locator(Selectors.GovFooterLink, new PageLocatorOptions
        {
            HasText = "News"
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

    public async Task<NewsPage> ClickNewsLink()
    {
        await NewsLink.ClickAsync();
        return new NewsPage(page);
    }

    public async Task HasBanner(string title, string heading, string body)
    {
        await Banner.ShouldBeVisible();

        await BannerTitle.ShouldBeVisible();
        await BannerTitle.ShouldContainText(title);

        await BannerHeading.ShouldBeVisible();
        await BannerHeading.ShouldContainText(heading);

        await BannerBody.ShouldBeVisible();
        await BannerBody.ShouldContainText(body);
    }
}