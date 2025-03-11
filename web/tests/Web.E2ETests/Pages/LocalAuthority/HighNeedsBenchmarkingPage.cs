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

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task ClickViewAsChart()
    {
        await ViewAsChartRadio.Click();
    }

    public async Task AreChartsDisplayed(int count)
    {
        var charts = await Charts.AllAsync();
        Assert.Equal(count, charts.Count);
        await charts.ShouldBeVisible();
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
                Name = await cells.ElementAt(0).InnerTextAsync(),
                Actual = await cells.ElementAt(1).InnerTextAsync(),
                Planned = await cells.ElementAt(2).InnerTextAsync()
            });
        }

        expected.CompareToDynamicSet(set);
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
                Amount = await cells.ElementAt(1).InnerTextAsync()
            });
        }

        expected.CompareToDynamicSet(set);
    }
}