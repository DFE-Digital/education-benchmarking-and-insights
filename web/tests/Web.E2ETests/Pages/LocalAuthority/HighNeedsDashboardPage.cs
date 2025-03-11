using Microsoft.Playwright;

namespace Web.E2ETests.Pages.LocalAuthority;

public class HighNeedsDashboardPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator StartBenchmarkingButton => page.Locator(Selectors.CtaButton, new PageLocatorOptions
    {
        HasText = "Start benchmarking"
    });
    private ILocator ViewNationalRankingsButton => page.Locator(Selectors.CtaButton, new PageLocatorOptions
    {
        HasText = "View full national rankings"
    });
    private ILocator ViewHistoricDataButton => page.Locator(Selectors.CtaButton, new PageLocatorOptions
    {
        HasText = "View full historic data"
    });
    private ILocator BenchmarkHighNeedsCard => page.Locator(Selectors.GovSummaryCard, new PageLocatorOptions
    {
        Has = page.Locator(Selectors.GovSummaryCardTitle, new PageLocatorOptions
        {
            HasText = "Benchmark high needs data"
        })
    });
    private ILocator HeadlineFiguresCard => page.Locator(Selectors.GovSummaryCard, new PageLocatorOptions
    {
        Has = page.Locator(Selectors.GovSummaryCardTitle, new PageLocatorOptions
        {
            HasText = "Total number of EHC plans and cost"
        })
    });
    private ILocator NationalRankingsCard => page.Locator(Selectors.GovSummaryCard, new PageLocatorOptions
    {
        Has = page.Locator(Selectors.GovSummaryCardTitle, new PageLocatorOptions
        {
            HasText = "National Ranking"
        })
    });
    private ILocator HistoricDataCard => page.Locator(Selectors.GovSummaryCard, new PageLocatorOptions
    {
        Has = page.Locator(Selectors.GovSummaryCardTitle, new PageLocatorOptions
        {
            HasText = "Historical spending"
        })
    });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await BenchmarkHighNeedsCard.ShouldBeVisible();
        await HeadlineFiguresCard.ShouldBeVisible();
        await NationalRankingsCard.ShouldBeVisible();
        await HistoricDataCard.ShouldBeVisible();
        await StartBenchmarkingButton.ShouldBeVisible();
        await ViewHistoricDataButton.ShouldBeVisible();
        await ViewNationalRankingsButton.ShouldBeVisible();
    }

    public async Task<HighNeedsStartBenchmarkingPage> ClickStartBenchmarking()
    {
        await StartBenchmarkingButton.Click();
        return new HighNeedsStartBenchmarkingPage(page);
    }

    public async Task<HighNeedsNationalRankingsPage> ClickViewNationalRankings()
    {
        await ViewNationalRankingsButton.Click();
        return new HighNeedsNationalRankingsPage(page);
    }

    public async Task<HighNeedsHistoricDataPage> ClickViewHistoricData()
    {
        await ViewHistoricDataButton.Click();
        return new HighNeedsHistoricDataPage(page);
    }
}