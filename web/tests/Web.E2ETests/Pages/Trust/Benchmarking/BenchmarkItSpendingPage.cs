using Microsoft.Playwright;
using Web.E2ETests.Pages.Trust.Comparators;
using Xunit;

namespace Web.E2ETests.Pages.Trust.Benchmarking;

public class BenchmarkItSpendingPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Benchmark your IT spending"
        });
    private ILocator ViewComparatorSetLink => page.Locator("a[data-test-id='comparators-link']");
    private ILocator ChartContainer(string chartName) => page.Locator($"[data-title=\"{chartName}\"]");
    private ILocator ForecastChartContainer(string chartName) => page.Locator($"[data-title=\"{chartName} (forecast)\"]");
    private ILocator ChartBars(string urn) => page.Locator($"rect.chart-cell[data-key='{urn}']");
    private ILocator ChartContainers => page.Locator($"{Selectors.SsrChartContainer}:not([id$=forecast])");
    private ILocator ForecastChartContainers => page.Locator($"{Selectors.SsrChartContainer}[id$=forecast]");
    private ILocator TrustLinksInCharts => page.Locator(Selectors.SsrOrgNamesLinksInCharts);
    private ILocator SaveImagesButton =>
        page.Locator(Selectors.Button, new PageLocatorOptions
        {
            HasText = "Save chart images"
        });
    private ILocator SaveImagesModal =>
        page.Locator(Selectors.Modal, new PageLocatorOptions
        {
            HasText = "Save chart images"
        });
    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await ViewComparatorSetLink.ShouldBeVisible();
    }

    public async Task<ViewComparatorsPage> ClickViewComparatorSetLink()
    {
        await ViewComparatorSetLink.ClickAsync();
        return new ViewComparatorsPage(page);
    }

    public async Task AssertChartsVisible(IEnumerable<string> expectedTitles)
    {
        var titles = expectedTitles.ToArray();

        await AssertChartCount(titles.Length);
        await AssertVisibleCharts(titles);
    }

    public async Task AssertForecastChartsVisible(IEnumerable<string> expectedTitles)
    {
        var titles = expectedTitles.ToArray();

        await AssertForecastChartCount(titles.Length);
        await AssertForecastVisibleCharts(titles);
    }

    public async Task<HomePage> ClickOnTrustName()
    {
        await TrustLinksInCharts.First.Click();
        return new HomePage(page);
    }

    public async Task IsSaveImagesButtonDisplayed()
    {
        await SaveImagesButton.ShouldBeVisible();
    }

    public async Task ClickSaveImagesButton()
    {
        await SaveImagesButton.ClickAsync();
    }

    public async Task IsSaveImagesModalDisplayed()
    {
        await SaveImagesModal.ShouldBeVisible();
    }
    private async Task AssertVisibleCharts(IEnumerable<string> expectedTitles)
    {
        foreach (var title in expectedTitles)
        {
            await ChartContainer(title).ShouldBeVisible();
        }
    }

    private async Task AssertForecastVisibleCharts(IEnumerable<string> expectedTitles)
    {
        foreach (var title in expectedTitles)
        {
            await ForecastChartContainer(title).ShouldBeVisible();
        }
    }

    private async Task AssertChartCount(int expectedCount)
    {
        var count = await ChartContainers.CountAsync();
        Assert.Equal(expectedCount, count);
    }

    private async Task AssertForecastChartCount(int expectedCount)
    {
        var count = await ForecastChartContainers.CountAsync();
        Assert.Equal(expectedCount, count);
    }
}