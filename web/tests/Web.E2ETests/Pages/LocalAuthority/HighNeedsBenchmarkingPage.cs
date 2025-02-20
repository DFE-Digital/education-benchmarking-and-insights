using Microsoft.Playwright;

namespace Web.E2ETests.Pages.LocalAuthority;

public class HighNeedsBenchmarkingPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator StartBenchmarkingButton => page.Locator(Selectors.CtaButton, new PageLocatorOptions
    {
        HasText = "Start benchmarking"
    });
    private ILocator ViewNationalRankingsButton => page.Locator(Selectors.CtaButton, new PageLocatorOptions
    {
        HasText = "View national rankings"
    });
    private ILocator ViewHistoricDataButton => page.Locator(Selectors.CtaButton, new PageLocatorOptions
    {
        HasText = "View historic data"
    });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
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