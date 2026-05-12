using Microsoft.Playwright;
using Web.E2ETests.Assist;
using Xunit;

namespace Web.E2ETests.Pages.LocalAuthority;

public class HighNeedsBenchmarkingPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator ViewAsTableRadio => page.Locator(Selectors.ViewAsTable);
    private ILocator ViewAsChartRadio => page.Locator(Selectors.ModeChart);
    private ILocator Charts => page.Locator(Selectors.Charts);
    private ILocator ChartContainers => page.Locator(Selectors.SsrChartContainer);
    private ILocator Tables => page.Locator(Selectors.GovTable);
    private ILocator ChangeComparatorsLink => page.Locator(".govuk-link", new PageLocatorOptions
    {
        HasText = "Change local authorities to benchmark against"
    });
    private ILocator ApplyCta => page.Locator(Selectors.GovButton, new PageLocatorOptions
    {
        HasText = "Apply"

    });
    private ILocator DownloadDataButton =>
        page.GetByRole(AriaRole.Button, new PageGetByRoleOptions
        {
            Name = "Download page data"
        });

    private ILocator SaveChartImagesButton =>
        page.Locator(Selectors.Button, new PageLocatorOptions
        {
            HasText = "Save chart images"
        });

    private static ILocator ChartLegend(ILocator chart) => chart.Locator("//following-sibling::div[1]/ul");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task ClickViewAsChart()
    {
        await ViewAsChartRadio.Click();
    }

    public async Task<ChooseLocalAuthoritiesToComparePage> ClickChangeComparatorsLink()
    {
        await ChangeComparatorsLink.ClickAsync();
        return new ChooseLocalAuthoritiesToComparePage(page);
    }

    public async Task AreChartsDisplayed(int count)
    {
        var charts = await ChartContainers.AllAsync();
        Assert.Equal(count, charts.Count);
        await charts.ShouldBeVisible();
    }

    public async Task AreS251ChartLegendsDisplayed(int count, params string[] seriesNames)
    {
        var charts = await Charts.AllAsync();
        var i = 0;
        foreach (var chart in charts)
        {
            if (i < count)
            {
                var legend = await ChartLegend(chart).ShouldBeVisible();
                Assert.Equivalent(seriesNames, (await legend.AllInnerTextsAsync()).SelectMany(t => t.Split("\n")));
            }

            i++;
        }
    }

    public async Task ClickViewAsTableAndApply()
    {
        await ViewAsTableRadio.Click();
        await ApplyCta.Nth(1).Click();
        await Tables.First.ShouldBeVisible();
    }

    public async Task AreTablesDisplayed(int count)
    {
        var tables = await Tables.AllAsync();
        Assert.Equal(count, tables.Count);
        await tables.ShouldBeVisible();
    }


    public async Task LineCodesArePresent()
    {
        var lineCodeItems = await page.Locator(".app-source-info").AllAsync();
        Assert.Equal(31, lineCodeItems.Count);

        Assert.NotEmpty(lineCodeItems);

        foreach (var item in lineCodeItems)
        {
            Assert.True(await item.IsVisibleAsync());
        }
    }

    public async Task IsTableDataForChartDisplayed(string chartHeading, List<List<string>> expectedData)
    {
        var table = page.Locator("h3", new PageLocatorOptions { HasText = chartHeading })
            .Locator("../..")
            .Locator(Selectors.GovTable)
            .First;

        await table.ShouldBeVisible();
        await table.ShouldHaveTableContent(expectedData, true);
    }

    public async Task ClickDownloadDataButton()
    {
        await DownloadDataButton.Click();
    }

    public async Task ClickSaveChartImagesButton()
    {
        await SaveChartImagesButton.Click();
    }
}