using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority high needs historic data")]
public class HighNeedsHistoricDataSteps(PageDriver driver)
{
    private HighNeedsHistoricDataPage? _highNeedsHistoricDataPage;

    [Given("I am on '(.*)' high needs history page for local authority with code '(.*)'")]
    public async Task GivenIAmOnHighNeedsHistoryPageForLocalAuthorityWithCode(string tab, string code)
    {
        var url = HighNeedsHistoricDataUrl(tab, code);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);
        await page.ReloadAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        _highNeedsHistoricDataPage = new HighNeedsHistoricDataPage(page);
        await _highNeedsHistoricDataPage.IsDisplayed(TabNamesFromFriendlyNames(tab));
    }

    [Then("the expected categories should be displayed on '(.*)':")]
    public async Task ThenTheExpectedCategoriesShouldBeDisplayedOn(string tab, DataTable table)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        var categories = table.Rows.Select(r => r["Category"]).ToArray();
        await _highNeedsHistoricDataPage.HasCategoryNames(TabNamesFromFriendlyNames(tab), categories);
    }

    [Given("all sections are shown on '(.*)'")]
    [When("I click on show all sections on '(.*)'")]
    public async Task WhenIClickOnShowAllSectionsOn(string tab)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        await _highNeedsHistoricDataPage.ClickShowAllSections(TabNamesFromFriendlyNames(tab));
    }

    [Then("all sections on '(.*)' tab are expanded")]
    public async Task ThenAllSectionsOnTabAreExpanded(string tab)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        await _highNeedsHistoricDataPage.AreSectionsExpanded(TabNamesFromFriendlyNames(tab));
    }

    [Then("the show all text changes to hide all sections on '(.*)'")]
    public async Task ThenTheShowAllTextChangesToHideAllSectionsOn(string tab)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        await _highNeedsHistoricDataPage.IsShowHideAllSectionsText(TabNamesFromFriendlyNames(tab), "Hide all sections");
    }

    [Then("the expected sub categories should be displayed on '(.*)':")]
    public async Task ThenTheExpectedSubCategoriesShouldBeDisplayedOn(string tab, DataTable table)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);

        var subCategories = table.Rows.Select(r => r["Sub category"]).ToArray();
        await _highNeedsHistoricDataPage.AreSubCategoriesVisible(TabNamesFromFriendlyNames(tab), subCategories);
    }

    [Then("there should be '(.*)' charts displayed on '(.*)'")]
    public async Task ThenThereShouldBeChartsDisplayedOn(string count, string tab)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        await _highNeedsHistoricDataPage.HasChartCount(TabNamesFromFriendlyNames(tab), int.Parse(count));
    }

    [Then("are showing table view on '(.*)' tab")]
    public async Task ThenAreShowingTableViewOnTab(string tab)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        await _highNeedsHistoricDataPage.AreTablesShown(TabNamesFromFriendlyNames(tab));
    }

    [When("I click on view as table on '(.*)' tab")]
    public async Task WhenIClickOnViewAsTableOnTab(string tab)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        await _highNeedsHistoricDataPage.ClickViewAsTable(TabNamesFromFriendlyNames(tab));
    }

    [When("I click section link for '(.*)'")]
    public async Task WhenIClickSectionLinkFor(string chartName)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        await _highNeedsHistoricDataPage.ClickSectionLink(Section251CategoryFromFriendlyName(chartName));
    }

    [Then("the section '(.*)' is hidden")]
    public async Task ThenTheSectionIsHidden(string chartName)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        await _highNeedsHistoricDataPage.IsSectionVisible(Section251CategoryFromFriendlyName(chartName), false, "Show", "chart");
    }

    [Then("the '(.*)' tab '(.*)' chart shows the legend '(.*)' using separator '(.*)'")]
    public async Task ThenTheTabChartShowsTheLegendUsingSeparator(string tab, string chartName, string legend, string separator)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        await _highNeedsHistoricDataPage.ChartLegendContains(chartName, legend, separator);
    }

    [Then("the table on the '(.*)' tab '(.*)' chart contains:")]
    public async Task ThenTheTableOnTheTabChartContains(string tab, string chartName, DataTable table)
    {
        Assert.NotNull(_highNeedsHistoricDataPage);
        await _highNeedsHistoricDataPage.ChartTableContains(chartName, table);
    }

    private static Section251CategoriesNames Section251CategoryFromFriendlyName(string chartName)
    {
        return chartName switch
        {
            "place funding" => Section251CategoriesNames.PlaceFunding,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };
    }

    private static string HighNeedsHistoricDataUrl(string tab, string code)
    {
        return $"{TestConfiguration.ServiceUrl}/local-authority/{code}/high-needs/history#{tab.Replace(" ", "-")}";
    }

    private static HighNeedsHistoryTabs TabNamesFromFriendlyNames(string tab)
    {
        return tab switch
        {
            "section 251" => HighNeedsHistoryTabs.Section251,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
    }
}