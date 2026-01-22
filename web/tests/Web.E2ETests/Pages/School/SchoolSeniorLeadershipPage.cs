using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;

public class SchoolSeniorLeadershipPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator ChartContainers => page.Locator(Selectors.SsrChartContainer);
    private ILocator SchoolLinksInCharts => page.Locator(Selectors.SsrOrgNamesLinksInCharts);
    private ILocator ChartTooltip => page.Locator(Selectors.EnhancementSchoolChartTooltip);
    private ILocator ActionControls => page.Locator(".actions-form .govuk-button");

    private ILocator ChartContainer(string chartName) => page.Locator($"[data-title=\"{chartName}\"]");
    private ILocator ChartBars(string urn) => page.Locator($"rect.chart-cell[data-key='{urn}']");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
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

    public async Task AssertChartVisible(string expectedTitle)
    {
        await AssertChartCount(1);
        await ChartContainer(expectedTitle).ShouldBeVisible();
    }

    public async Task HoverOnChartBar(string urn)
    {
        await ChartBars(urn).First.HoverAsync();
    }

    public async Task TooltipIsDisplayed(string name)
    {
        await ChartTooltip.ShouldBeVisible();
        await ChartTooltip.Locator("caption").ShouldContainText(name);
    }

    public async Task FocusLastActionControl()
    {
        await ActionControls.Last.FocusAsync();
    }

    public async Task PressTab()
    {
        await page.Keyboard.PressAsync("Tab");
    }

    private async Task AssertChartCount(int expectedCount)
    {
        var count = await ChartContainers.CountAsync();
        Assert.Equal(expectedCount, count);
    }
}