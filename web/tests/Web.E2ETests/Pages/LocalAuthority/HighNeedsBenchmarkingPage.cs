using Microsoft.Playwright;
using Web.E2ETests.Assist;
using Xunit;

namespace Web.E2ETests.Pages.LocalAuthority;

public class HighNeedsBenchmarkingPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator ViewAsTableRadio => page.Locator(Selectors.ModeTable);
    private ILocator ViewAsChartRadio => page.Locator(Selectors.ModeChart);
    private ILocator Charts => page.Locator(Selectors.Charts);
    private ILocator Tables => page.Locator(Selectors.GovTable);
    private ILocator ChangeComparatorsLink => page.Locator(".govuk-link", new PageLocatorOptions
    {
        HasText = "Change local authorities to benchmark against"
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

    public async Task<HighNeedsStartBenchmarkingPage> ClickChangeComparatorsLink()
    {
        await ChangeComparatorsLink.ClickAsync();
        return new HighNeedsStartBenchmarkingPage(page);
    }

    public async Task AreChartsDisplayed(int count)
    {
        var charts = await Charts.AllAsync();
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

    public async Task ClickViewAsTable()
    {
        await ViewAsTableRadio.Click();
    }

    public async Task AreTablesDisplayed(int count)
    {
        var tables = await Tables.AllAsync();
        Assert.Equal(count, tables.Count);
        await tables.ShouldBeVisible();
    }

    public async Task TableContainsSection251(int index, DataTable expected)
    {
        var table = Tables.Nth(index);
        await table.ShouldBeVisible();
        var rows = await table.Locator("tbody > tr").AllAsync();

        var set = new List<dynamic>();

        foreach (var row in rows)
        {
            var cells = await row.Locator("td").AllAsync();

            set.Add(new
            {
                Name = await Get(0),
                Actual = await Get(1),
                Planned = await Get(2),
                NumberPupils = await Get(3)
            });
            continue;

            // S251 rows render only three <td> elements when values are missing.
            // This helper safely returns an empty string instead of throwing.
            async Task<string> Get(int i) =>
                i < cells.Count ? (await cells[i].InnerTextAsync()).Trim() : "";
        }

        expected.CompareToDynamicSet(set, false);
    }

    public async Task TableContainsSend2(int index, DataTable expected)
    {
        var table = Tables.Nth(index);
        await table.ShouldBeVisible();
        var rows = await table.Locator("tbody > tr").AllAsync();

        var set = new List<dynamic>();
        foreach (var row in rows)
        {
            var cells = await row.Locator("td").AllAsync();
            set.Add(new
            {
                Name = await cells.ElementAt(0).InnerTextAsync(),
                Amount = await cells.ElementAt(1).InnerTextAsync(),
                NumberPupils = await cells.ElementAt(2).InnerTextAsync()
            });
        }

        expected.CompareToDynamicSet(set, false);
    }

    public async Task LineCodesArePresent()
    {
        var lineCodeItems = await page.Locator("[data-test-id='line-code-source']").AllAsync();
        Assert.Equal(25, lineCodeItems.Count);

        Assert.NotEmpty(lineCodeItems);

        foreach (var item in lineCodeItems)
        {
            Assert.True(await item.IsVisibleAsync());
        }
    }
}