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

        _historicDataPage = new HistoricDataPage(page);
        await _historicDataPage.IsDisplayed(TabNamesFromFriendlyNames(tab));
    }

    [Given(@"all sections are shown on '(.*)' tab")]
    [When(@"I click on show all sections on '(.*)' tab")]
    public async Task WhenIClickOnShowAllSections(string tab)
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

    [Then(@"all sections on '(.*)' tab are expanded")]
    public async Task ThenAllSectionsOnThePageAreExpanded(string tab)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.AreSectionsExpanded(TabNamesFromFriendlyNames(tab));
    }

    [Then(@"the show all text changes to hide all sections on '(.*)' tab")]
    public async Task ThenTheShowAllTextChangesToHideAllSectionsOnTab(string tab)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.IsShowHideAllSectionsText(TabNamesFromFriendlyNames(tab), "Hide all sections");
    }

    [Then(@"all '(.*)' sub categories are displayed on the page")]
    public async Task ThenAllSubCategoriesAreDisplayedOnThePage(string tab)
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.AreSubCategoriesVisible(TabNamesFromFriendlyNames(tab));

    }

    [Then(@"are showing table view on '(.*)' tab")]
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
            "workforce" => HistoryTabs.Workforce,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
    }

}