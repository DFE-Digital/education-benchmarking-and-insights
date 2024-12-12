using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;
namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School historic data")]
public class HistoricDataSteps(PageDriver driver)
{
    private HistoricDataPage? _historicDataPage;

    [Given("I am on '(.*)' history page for school with URN '(.*)'")]
    public async Task GivenIAmOnHistoryPageForSchoolWithUrn(string tab, string urn)
    {
        var url = FindWaysToSpendLessUrl(tab, urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);
        await page.ReloadAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        _historicDataPage = new HistoricDataPage(page);
        await _historicDataPage.IsDisplayed(TabNamesFromFriendlyNames(tab));
    }

    [Given("all sections are shown on '(.*)'")]
    [When("I click on show all sections on '(.*)'")]
    public async Task WhenIClickOnShowAllSectionsOn(string tab)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.ClickShowAllSections(TabNamesFromFriendlyNames(tab));
    }

    [When("I change '(.*)' dimension to '(.*)'")]
    public async Task WhenIChangeDimensionTo(string tab, string dimensionValue)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.SelectDimension(TabNamesFromFriendlyNames(tab), dimensionValue);
    }

    [Then("the '(.*)' dimension is '(.*)'")]
    public async Task ThenTheDimensionIs(string tab, string value)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.IsDimensionSelected(TabNamesFromFriendlyNames(tab), value);
    }

    [Then("all sections on '(.*)' tab are expanded")]
    public async Task ThenAllSectionsOnTabAreExpanded(string tab)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.AreSectionsExpanded(TabNamesFromFriendlyNames(tab));
    }

    [Then("the show all text changes to hide all sections on '(.*)'")]
    public async Task ThenTheShowAllTextChangesToHideAllSectionsOn(string tab)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.IsShowHideAllSectionsText(TabNamesFromFriendlyNames(tab), "Hide all sections");
    }

    [Then("all '(.*)' sub categories are displayed on the page")]
    public async Task ThenAllSubCategoriesAreDisplayedOnThePage(string tab)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.AreSubCategoriesVisible(TabNamesFromFriendlyNames(tab));
    }

    [Then("there should be '(.*)' charts displayed on '(.*)'")]
    public async Task ThenThereShouldBeChartsDisplayedOn(string count, string tab)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.HasChartCount(TabNamesFromFriendlyNames(tab), int.Parse(count));
    }

    [Then("are showing table view on '(.*)' tab")]
    public async Task ThenAreShowingTableViewOnTab(string tab)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.AreTablesShown(TabNamesFromFriendlyNames(tab));
    }

    [When("I click on view as table on '(.*)' tab")]
    public async Task WhenIClickOnViewAsTableOnTab(string tab)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.ClickViewAsTable(TabNamesFromFriendlyNames(tab));
    }

    [When("I click section link for '(.*)'")]
    public async Task WhenIClickSectionLinkFor(string chartName)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.ClickSectionLink(ExpenditureCategoryFromFriendlyName(chartName));
    }

    [Then("the section '(.*)' is hidden")]
    public async Task ThenTheSectionIsHidden(string chartName)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.IsSectionVisible(ExpenditureCategoryFromFriendlyName(chartName), false, "Show", "chart");
    }

    [Then("the '(.*)' tab '(.*)' chart shows the legend '(.*)' using separator '(.*)'")]
    public async Task ThenTheTabChartShowsTheLegendUsingSeparator(string tab, string chartName, string legend, string separator)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.ChartLegendContains(chartName, legend, separator);
    }

    [Then("the table on the '(.*)' tab '(.*)' chart contains:")]
    public async Task ThenTheTableOnTheTabChartContains(string tab, string chartName, DataTable table)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.ChartTableContains(chartName, table);
    }

    private static SpendingCategoriesNames ExpenditureCategoryFromFriendlyName(string chartName)
    {
        return chartName switch
        {
            "non educational support staff" => SpendingCategoriesNames.NonEducationalSupportStaff,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };
    }

    private static string FindWaysToSpendLessUrl(string tab, string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/history#{tab}";

    private static HistoryTabs TabNamesFromFriendlyNames(string tab)
    {
        return tab switch
        {
            "spending" => HistoryTabs.Spending,
            "income" => HistoryTabs.Income,
            "balance" => HistoryTabs.Balance,
            "census" => HistoryTabs.Census,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
    }
}