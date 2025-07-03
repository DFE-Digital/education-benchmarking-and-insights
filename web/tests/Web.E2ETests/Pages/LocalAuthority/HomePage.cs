using Microsoft.Playwright;

namespace Web.E2ETests.Pages.LocalAuthority;

public class HomePage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator CompareYourCostsLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "View school spending"
    });
    private ILocator BenchmarkCensusDataLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "View pupil and workforce data"
    });
    private ILocator HighNeedsBenchmarkingLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "High needs benchmarking"
    });
    private ILocator CookieBanner => page.Locator(Selectors.CookieBanner);
    private ILocator Banner => page.Locator(Selectors.GovNotificationBanner);
    private ILocator BannerTitle => page.Locator(Selectors.GovNotificationBannerTitle);
    private ILocator BannerHeading => page.Locator(Selectors.GovNotificationBannerHeading);
    private ILocator BannerBody => page.Locator(Selectors.GovNotificationBannerBody);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await CompareYourCostsLink.ShouldBeVisible();
    }

    public async Task<CompareYourCostsPage> ClickCompareYourCosts()
    {
        await CompareYourCostsLink.Click();
        return new CompareYourCostsPage(page);
    }

    public async Task CookieBannerIsDisplayed()
    {
        await CookieBanner.ShouldBeVisible();
    }

    public async Task<BenchmarkCensusPage> ClickBenchmarkCensus()
    {
        await BenchmarkCensusDataLink.Click();
        return new BenchmarkCensusPage(page);
    }

    public async Task<HighNeedsDashboardPage> ClickHighNeedsBenchmarking()
    {
        await HighNeedsBenchmarkingLink.Click();
        return new HighNeedsDashboardPage(page);
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