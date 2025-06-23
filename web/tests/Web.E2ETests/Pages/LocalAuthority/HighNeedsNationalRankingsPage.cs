using Microsoft.Playwright;
using Web.E2ETests.Assist;

namespace Web.E2ETests.Pages.LocalAuthority;

public class HighNeedsNationalRankingsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator ViewAsTableRadio(string prefix) => page.Locator($"input[id='{prefix}-mode-table']");
    private ILocator Table(string chartName) => page.Locator($"div.costs-chart-wrapper[data-title='{chartName}'] {Selectors.GovTable}");
    private ILocator SaveImageButton(string chartName) =>
        page.Locator($"button[data-custom-event-chart-name='{chartName}'][data-custom-event-id='save-chart-as-image']");
    private ILocator CopyImageButton(string chartName) =>
        page.Locator($"button[data-custom-event-chart-name='{chartName}'][data-custom-event-id='copy-chart-as-image']");

    private ILocator WarningMessage(string tab) => page.Locator($"div.govuk-tabs__panel[id='{tab}'] {Selectors.GovWarning}");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task ClickViewAsTable(string prefix)
    {
        await ViewAsTableRadio(prefix).ShouldBeVisible();
        await ViewAsTableRadio(prefix).ClickAsync();
    }

    public async Task TableContainsValues(string chartName, DataTable table)
    {
        await Table(chartName).ShouldBeVisible();
        var rows = await Table(chartName).Locator("> tbody > tr").AllAsync();
        var set = new List<dynamic>();

        foreach (var row in rows)
        {
            set.Add(new
            {
                Name = await row.Locator("> td").Nth(0).InnerTextAsync(),
                Value = await row.Locator("> td").Nth(1).InnerTextAsync()
            });
        }

        table.CompareToDynamicSet(set, false);
    }

    public async Task ClickSaveAsImage(string chartName)
    {
        await SaveImageButton(chartName).Click();
    }

    public async Task ClickCopyImage(string chartName)
    {
        await CopyImageButton(chartName).Click();
    }

    public async Task DoesNotContainWarningMessage(string chartName)
    {
        await WarningMessage(chartName).ShouldNotBeVisible();
    }

    public async Task ContainsWarningMessage(string chartName, string message)
    {
        await WarningMessage(chartName).ShouldBeVisible();
        await WarningMessage(chartName).ShouldContainText(message);
    }

    public async Task ClickTab(string tab)
    {
        var tabButton = page.Locator($"a.govuk-tabs__tab[href='#{tab}']");
        await tabButton.ClickAsync();
    }
}