using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;

public class BenchmarkItSpendPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator ChartContainers => page.Locator(Selectors.SsrChartContainer);
    private ILocator ChartContainer(string chartName) => page.Locator($"[data-title=\"{chartName}\"]");
    private ILocator SchoolLinksInCharts => page.Locator(Selectors.SsrSchoolNamesLinksInCharts);
    private ILocator ComparatorSetDetails =>
        page.Locator(Selectors.GovLink,
            new PageLocatorOptions
            {
                HasText = "We've chosen 2 sets of similar schools"
            });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await ComparatorSetDetails.ShouldBeVisible();
    }

    public async Task<HomePage> ClickOnSchoolName()
    {
        await SchoolLinksInCharts.First.Click();
        return new HomePage(page);
    }

    public async Task<HomePage> EnterOnSchoolName()
    {
        await SchoolLinksInCharts.First.FocusAsync();
        await page.Keyboard.PressAsync("Enter");

        return new HomePage(page);
    }

    public async Task AssertChartsVisible(IEnumerable<string> expectedTitles)
    {
        var titles = expectedTitles.ToArray();

        await AssertChartCount(titles.Length);
        await AssertVisibleCharts(titles);
    }

    private async Task AssertVisibleCharts(IEnumerable<string> expectedTitles)
    {
        foreach (var title in expectedTitles)
        {
            await ChartContainer(title).ShouldBeVisible();
        }
    }

    private async Task AssertChartCount(int expectedCount)
    {
        var count = await ChartContainers.CountAsync();
        Assert.Equal(expectedCount, count);
    }
}