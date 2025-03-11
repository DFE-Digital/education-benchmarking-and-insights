using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority high needs benchmarking")]
public class HighNeedsBenchmarkingSteps(PageDriver driver)
{
    private HighNeedsBenchmarkingPage? _highNeedsBenchmarkingPage;
    private HighNeedsStartBenchmarkingPage? _highNeedsStartBenchmarkingPage;

    [Given("I am on local authority high needs start benchmarking for local authority with code '(.*)'")]
    public async Task GivenIAmOnLocalAuthorityHighNeedsStartBenchmarkingForLocalAuthorityWithCode(string laCode)
    {
        var url = LocalAuthorityHighNeedsStartBenchmarkingUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _highNeedsStartBenchmarkingPage = new HighNeedsStartBenchmarkingPage(page);
        await _highNeedsStartBenchmarkingPage.IsDisplayed();
    }

    [Given("I am on local authority high needs benchmarking for local authority with code '(.*)'")]
    public async Task GivenIAmOnLocalAuthorityHighNeedsBenchmarkingForLocalAuthorityWithCode(string laCode)
    {
        var url = LocalAuthorityHighNeedsBenchmarkingUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _highNeedsBenchmarkingPage = new HighNeedsBenchmarkingPage(page);
        await _highNeedsBenchmarkingPage.IsDisplayed();
    }

    [Given("I have no comparators selected")]
    public async Task GivenIHaveNoComparatorsSelected()
    {
        Assert.NotNull(_highNeedsStartBenchmarkingPage);
        while (await _highNeedsStartBenchmarkingPage.HasComparators())
        {
            await _highNeedsStartBenchmarkingPage.ClickRemoveButton();
        }
    }

    [Given("I add the comparator matching the value '(.*)'")]
    public async Task GivenIAddTheComparatorMatchingTheValue(string name)
    {
        Assert.NotNull(_highNeedsStartBenchmarkingPage);
        await _highNeedsStartBenchmarkingPage.TypeIntoInputField(name);
        await _highNeedsStartBenchmarkingPage.PressEnterKey();
        await _highNeedsStartBenchmarkingPage.PressEnterKey();
    }

    [Given("I click the Save and continue button")]
    public async Task GivenIClickTheSaveAndContinueButton()
    {
        Assert.NotNull(_highNeedsStartBenchmarkingPage);
        _highNeedsBenchmarkingPage = await _highNeedsStartBenchmarkingPage.ClickSaveAndContinueButton();
    }

    [When("I click on show all sections")]
    public async Task WhenIClickOnShowAllSections()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.ClickShowAllSections();
    }

    [When("I click on view as table")]
    public async Task WhenIClickOnViewAsTable()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.ClickViewAsTable();
    }

    [Then("all sections on the page are expanded")]
    public async Task ThenAllSectionsOnThePageAreExpanded()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.AreSectionsExpanded();
    }

    [Then("chart view is visible, showing '(\\d+)' charts")]
    public async Task ThenChartViewIsVisibleShowingCharts(string charts)
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.AreChartsDisplayed(int.Parse(charts));
    }

    [Then("table view is visible, showing '(\\d+)' tables")]
    public async Task ThenTableViewIsVisibleShowingTables(string tables)
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.AreTablesDisplayed(int.Parse(tables));
    }

    [Then("the table at index '(\\d+)' contains the following S251 values:")]
    public async Task ThenTheTableAtIndexContainsTheFollowingSValues(string index, DataTable table)
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.TableContainsSection251(int.Parse(index), table);
    }

    [Then("the table at index '(\\d+)' contains the following SEND2 values:")]
    public async Task ThenTheTableAtIndexContainsTheFollowingSendValues(string index, DataTable table)
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.TableContainsSend2(int.Parse(index), table);
    }

    private static string LocalAuthorityHighNeedsBenchmarkingUrl(string laCode)
    {
        return $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/high-needs/benchmarking";
    }

    private static string LocalAuthorityHighNeedsStartBenchmarkingUrl(string laCode)
    {
        return $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/high-needs/benchmarking/comparators";
    }
}