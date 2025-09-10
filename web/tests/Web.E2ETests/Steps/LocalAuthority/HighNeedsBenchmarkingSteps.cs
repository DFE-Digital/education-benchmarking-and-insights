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
        while (await _highNeedsStartBenchmarkingPage.HasComparators()) await _highNeedsStartBenchmarkingPage.ClickRemoveButton();
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

    [When("I click on view as table")]
    public async Task WhenIClickOnViewAsTable()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.ClickViewAsTable();
    }

    [Then("comparator commentary label is visible, showing local authority count of '(\\d+)'")]
    public async Task ThenComparatorCommentaryLabelIsVisibleShowingLocalAuthorityCountOf(string comparators)
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.IsComparatorCommentaryDisplayed(int.Parse(comparators));
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

    [Then("the table for 'High needs amount per head 2-18 population' contains the following S251 values:")]
    public async Task ThenTheTableForContainsTheFollowingSValues(DataTable table)
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.TableContainsSection251(0, table);
    }

    [Then("the table for 'Number aged up to 25 with SEN statement or EHC plan' contains the following SEND2 values:")]
    public async Task ThenTheTableForContainsTheFollowingSendValues(DataTable table)
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.TableContainsSend2(25, table);
    }

    [When("I click the Change comparators button")]
    public async Task WhenIClickTheChangeComparatorsButton()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        _highNeedsStartBenchmarkingPage = await _highNeedsBenchmarkingPage.ClickChangeComparatorsButton();
    }

    [Then("the local authority high needs start benchmarking page is displayed")]
    public async Task ThenTheLocalAuthorityHighNeedsStartBenchmarkingPageIsDisplayed()
    {
        Assert.NotNull(_highNeedsStartBenchmarkingPage);
        await _highNeedsStartBenchmarkingPage.IsDisplayed();
    }

    [Then("the line codes are present")]
    public async Task ThenTheLineCodesArePresent()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);

        await _highNeedsBenchmarkingPage.LineCodesArePresent();
    }

    private static string LocalAuthorityHighNeedsBenchmarkingUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/high-needs/benchmarking";

    private static string LocalAuthorityHighNeedsStartBenchmarkingUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/high-needs/benchmarking/comparators";
}