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
        HasText = "View full national data"
    });
    private ILocator ViewHistoricSpendingButton => page.Locator(Selectors.CtaButton, new PageLocatorOptions
    {
        HasText = "View full historical spending"
    });
    private ILocator BenchmarkHighNeedsCard => page.Locator(Selectors.GovSummaryCard, new PageLocatorOptions
    {
        Has = page.Locator(Selectors.GovSummaryCardTitle, new PageLocatorOptions
        {
            HasText = "Benchmark high needs data"
        })
    });
    private ILocator KeyInformationCard => page.Locator(Selectors.GovSummaryCard, new PageLocatorOptions
    {
        Has = page.Locator(Selectors.GovSummaryCardTitle, new PageLocatorOptions
        {
            HasText = "Key information"
        })
    });
    private ILocator NationalViewSection => page.Locator(Selectors.NationalView);
    private ILocator HistoricDataSection => page.Locator(Selectors.HistoricalSpending);
    private ILocator HistoricalFundingVsOutturnTab => page.Locator(Selectors.HistoricalFundingVsOutturnTab);
    private ILocator HistoricalFundingVsOutturnTabPanel => page.Locator(Selectors.HistoricalFundingVsOutturnTabPanel);

    private ILocator HistoricalExpenditureVsOutturnTab => page.Locator(Selectors.HistoricalExpenditureVsOutturnTab);
    private ILocator HistoricalExpenditureVsOutturnTabPanel => page.Locator(Selectors.HistoricalExpenditureVsOutturnTabPanel);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await BenchmarkHighNeedsCard.ShouldBeVisible();
        await KeyInformationCard.ShouldBeVisible();
        await NationalViewSection.ShouldBeVisible();
        await HistoricDataSection.ShouldBeVisible();
        await StartBenchmarkingButton.ShouldBeVisible();
        await ViewHistoricSpendingButton.First.ShouldBeVisible();
        await HistoricalFundingVsOutturnTab.ShouldBeVisible();
        await HistoricalFundingVsOutturnTabPanel.ShouldBeVisible();
        await ViewHistoricSpendingButton.Last.ShouldNotBeVisible();
        await HistoricalExpenditureVsOutturnTab.ShouldBeVisible();
        await HistoricalExpenditureVsOutturnTabPanel.ShouldNotBeVisible();
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
        await ViewHistoricSpendingButton.First.Click();
        return new HighNeedsHistoricDataPage(page);
    }

    public async Task<HighNeedsGlossaryPage> ClickOnHighNeedsGlossaryLink()
    {
        await page.Locator("#high-needs-glossary").ClickAsync();
        await page.BringToFrontAsync();
        return new HighNeedsGlossaryPage(page);
    }
}