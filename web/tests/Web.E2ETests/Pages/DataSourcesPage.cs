using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Web.E2ETests.Assist;
namespace Web.E2ETests.Pages;

public partial class DataSourcesPage(IPage page) : BasePage(page)
{
    private ILocator CfrSourcesHeader =>
        page.Locator(Selectors.H4, new PageLocatorOptions
        {
            HasText = "LA Maintained Schools"
        });

    private ILocator AarSourcesHeader =>
        page.Locator(Selectors.H4, new PageLocatorOptions
        {
            HasText = "Academies"
        });

    private ILocator CfrSection => page.Locator("#data-sources-cfr");
    private ILocator AarSection => page.Locator("#data-sources-aar");

    public override async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await CfrSourcesHeader.ShouldBeVisible();
        await AarSourcesHeader.ShouldBeVisible();
    }

    public async Task AssertCfrDataSources(DataTable table)
    {
        var anchors = await CfrSection.Locator("ul > li > a").AllAsync();
        await AssertDataSources(table, anchors);
    }

    public async Task AssertAarDataSources(DataTable table)
    {
        var anchors = await AarSection.Locator("ul > li > a").AllAsync();
        await AssertDataSources(table, anchors);
    }

    private static async Task AssertDataSources(DataTable expected, IReadOnlyList<ILocator> actual)
    {
        var set = new List<dynamic>();
        foreach (var anchor in actual)
        {
            var text = await anchor.InnerTextAsync();
            var href = await anchor.GetAttributeAsync("href");
            set.Add(new
            {
                Label = text.Split("\n").FirstOrDefault(),
                FileName = FileNameFromUrlRegex().Match(href!).Value
            });
        }

        expected.CompareToDynamicSet(set, false);
    }

    [GeneratedRegex("[^/]+(?=\\?)")]
    private static partial Regex FileNameFromUrlRegex();
}