using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.Trust;

public class HomePage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");

    private ILocator CompareYourCostsLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "View school spending"
    });

    private ILocator TrustBenchmarking => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "Trust to trust benchmarking"
    });

    private ILocator SpendingPrioritiesLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "View all spending priorities for this trust"
    });

    private ILocator BenchmarkCensusDataLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "View pupil and workforce data"
    });

    private ILocator TrustForecastLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "Forecast and risk"
    });

    private ILocator CurriculumFinancialPlanningLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "Curriculum and financial planning"
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

    public async Task<SpendingCostsPage> ClickSpendingPriorities()
    {
        await SpendingPrioritiesLink.Click();
        return new SpendingCostsPage(page);
    }

    public async Task CookieBannerIsDisplayed()
    {
        await CookieBanner.ShouldBeVisible();
    }

    public async Task AssertCategoryRags(string name, string status)
    {
        var cell = page.Locator($".table-cost-category-rag tbody > tr > td:has-text('{name}'):first-of-type");
        await cell.ShouldBeVisible();
        var content = await cell.Locator("~ td").TextContentAsync();
        Assert.Equal(status, content?.Trim());
    }

    public async Task AssertSchoolRags(string name, string status)
    {
        var cell = page.Locator($".table-school-rag tbody > tr > td:has-text('{name}'):first-of-type");
        await cell.ShouldBeVisible();
        var content = await cell.Locator("~ td").TextContentAsync();
        Assert.Equal(status, content?.Trim());
    }

    public async Task<BenchmarkCensusPage> ClickBenchmarkCensus()
    {
        await BenchmarkCensusDataLink.Click();
        return new BenchmarkCensusPage(page);
    }

    public async Task<TrustForecastPage> ClickTrustForecast()
    {
        await TrustForecastLink.Click();
        return new TrustForecastPage(page);
    }

    public async Task<CurriculumFinancialPlanningPage> ClickTrustCurriculumFinancialPlanning()
    {
        await CurriculumFinancialPlanningLink.Click();
        return new CurriculumFinancialPlanningPage(page);
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