using Microsoft.Playwright;
using Web.E2ETests.Assist;
using Xunit;

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
        HasText = "Benchmark high needs"
    });

    private ILocator HighNeedsHistoryLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "View high needs historical data"
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

    public async Task<HighNeedsStartBenchmarkingPage> ClickBenchmarkHighNeeds()
    {
        await HighNeedsBenchmarkingLink.Click();
        return new HighNeedsStartBenchmarkingPage(page);
    }

    public async Task<HighNeedsHistoricDataPage> ClickHighNeedsHistory()
    {
        await HighNeedsHistoryLink.Click();
        return new HighNeedsHistoricDataPage(page);
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

    public async Task IsSchoolsAccordionDisplayed(bool displayed = true)
    {
        var locator = page.Locator("#accordion-schools");
        if (displayed)
        {
            await locator.ShouldBeVisible();
        }
        else
        {
            await locator.ShouldNotBeVisible();
        }
    }

    public async Task ContainsPriorityRagsForPhase(string overallPhase, DataTable table)
    {
        var grid = page.Locator($"#school-rag-{overallPhase.ToSlug()}");
        await grid.ShouldBeVisible();

        var rows = grid.Locator(".govuk-grid-row");
        var set = new List<dynamic>();
        var i = 0;
        foreach (var row in await rows.AllAsync())
        {
            var cols = row.Locator(".govuk-grid-column-one-half");
            if (i == 0)
            {
                Assert.Equal(overallPhase, await cols.First.InnerTextAsync());
            }
            else
            {
                set.Add(new
                {
                    School = await cols.First.InnerTextAsync(),
                    Status = await cols.Last.Locator("title").InnerHTMLAsync()
                });
            }

            i++;
        }

        table.CompareToDynamicSet(set, false);
    }
}