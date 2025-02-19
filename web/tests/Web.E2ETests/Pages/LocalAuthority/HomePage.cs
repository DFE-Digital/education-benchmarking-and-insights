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

    public async Task<HighNeedsBenchmarkingPage> ClickHighNeedsBenchmarking()
    {
        await HighNeedsBenchmarkingLink.Click();
        return new HighNeedsBenchmarkingPage(page);
    }
}