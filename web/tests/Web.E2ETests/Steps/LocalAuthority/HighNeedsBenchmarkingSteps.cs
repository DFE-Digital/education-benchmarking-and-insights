using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority high needs benchmarking")]
public class HighNeedsBenchmarkingSteps(PageDriver driver)
{
    private HighNeedsBenchmarkingPage? _highNeedsBenchmarkingPage;
    private ChooseLocalAuthoritiesToComparePage? _highNeedsStartBenchmarkingPage;
    private IDownload? _download;


    [Given("I am on local authority high needs start benchmarking for local authority with code '(.*)'")]
    public async Task GivenIAmOnLocalAuthorityHighNeedsStartBenchmarkingForLocalAuthorityWithCode(string laCode)
    {
        var url = LocalAuthorityHighNeedsStartBenchmarkingUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _highNeedsStartBenchmarkingPage = new ChooseLocalAuthoritiesToComparePage(page);
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
        await _highNeedsBenchmarkingPage.ClickViewAsTableAndApply();
    }

    [Then("chart view is visible, showing '(\\d+)' charts")]
    public async Task ThenChartViewIsVisibleShowingCharts(string charts)
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.AreChartsDisplayed(int.Parse(charts));
    }

    [Then("the legend is visible on all s251 charts")]
    public async Task ThenTheLegendIsVisibleOnAllSCharts()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.AreS251ChartLegendsDisplayed(24, "Outturn", "Planned expenditure");
    }

    [Then("table view is visible, showing '(\\d+)' tables")]
    public async Task ThenTableViewIsVisibleShowingTables(string tables)
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.AreTablesDisplayed(int.Parse(tables));
    }

    [Then(@"the following is shown for '(.*)'")]
    public async Task ThenTheFollowingIisShownFor(string chartName, Table table)
    {
        var expected = new List<List<string>>();
        {
            var headers = table.Header.ToList();
            expected.Add(headers);
            expected.AddRange(table.Rows.Select(row => row.Select(cell => cell.Value).ToList()));
        }

        Assert.NotNull(_highNeedsBenchmarkingPage);
        await _highNeedsBenchmarkingPage.IsTableDataForChartDisplayed(chartName, expected);
    }

    [When("I click the Change comparators link")]
    public async Task WhenIClickTheChangeComparatorsLink()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        _highNeedsStartBenchmarkingPage = await _highNeedsBenchmarkingPage.ClickChangeComparatorsLink();
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
    [When("I click on download data")]
    public async Task WhenIClickOnDownloadData()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        var page = await driver.Current;
        var downloadTask = page.WaitForDownloadAsync();

        await _highNeedsBenchmarkingPage.ClickDownloadDataButton();
        _download = await downloadTask;
    }

    [When("I click on save chart images")]
    public async Task WhenIClickOnSaveChartImages()
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        var page = await driver.Current;
        var downloadTask = page.WaitForDownloadAsync();

        await _highNeedsBenchmarkingPage.ClickSaveChartImagesButton();
        _download = await downloadTask;
    }

    [Then(@"the file '(.*)' is downloaded")]
    public void ThenTheFileIsDownloaded(string fileName)
    {
        Assert.NotNull(_highNeedsBenchmarkingPage);
        Assert.NotNull(_download);
        var downloadedFilePath = _download.SuggestedFilename;
        Assert.Equal(fileName, downloadedFilePath);
    }

    private static string LocalAuthorityHighNeedsBenchmarkingUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/high-needs-spending";

    private static string LocalAuthorityHighNeedsStartBenchmarkingUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/comparators";

}