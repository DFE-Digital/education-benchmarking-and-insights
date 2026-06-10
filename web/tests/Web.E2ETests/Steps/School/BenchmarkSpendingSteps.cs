using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Web.E2ETests.Pages.School.Comparators;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School benchmark spending")]
public class BenchmarkSpendingSteps(PageDriver driver)
{
    private BenchmarkSpendingPage? _benchmarkSpendingPage;
    private ComparatorsPage? _comparatorsPage;
    private IDownload? _download;
    private HomePage? _schoolHomePage;

    [Given("I am on benchmark spending page for school with URN '(.*)'")]
    public async Task GivenIAmOnBenchmarkSpendingPageForSchoolWithUrn(string urn)
    {
        var url = BenchmarkSpendingUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _benchmarkSpendingPage = new BenchmarkSpendingPage(page);
        await _benchmarkSpendingPage.IsDisplayed();
    }

    [Given("I am on benchmark spending page for part year school with URN '(.*)'")]
    public async Task GivenIAmOnBenchmarkSpendingPageForPartYearSchoolWithUrn(string urn)
    {
        var url = BenchmarkSpendingUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _benchmarkSpendingPage = new BenchmarkSpendingPage(page);
        await _benchmarkSpendingPage.IsDisplayed();
    }

    [Given("I am on benchmark spending page for missing comparator school with URN '(.*)'")]
    public async Task GivenIAmOnBenchmarkSpendingPageForMissingComparatorSchoolWithUrn(string urn)
    {
        var url = BenchmarkSpendingUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _benchmarkSpendingPage = new BenchmarkSpendingPage(page);
        await _benchmarkSpendingPage.IsDisplayed(true);
    }

    [Given("table view is selected for benchmark spending")]
    public async Task GivenTableViewIsSelectedForBenchmarkSpending()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.ClickViewAsTable();
    }

    [Given("the benchmark spending result is '(.*)'")]
    public async Task GivenTheBenchmarkSpendingResultIs(string resultValue)
    {
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.SelectResultAs(resultValue);
        await _benchmarkSpendingPage.ClickApplyButton();
    }

    [Then("the save chart images button is visible")]
    public async Task ThenTheSaveChartImagesButtonIsVisible()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.IsSaveImagesButtonDisplayed();
    }

    [When("I click the save chart images button")]
    public async Task WhenIClickTheSaveChartImagesButton()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        var page = await driver.Current;
        _download = await page.RunAndWaitForDownloadAsync(() => _benchmarkSpendingPage.ClickSaveImagesButton(), new TimeSpan(0, 2, 0));
    }

    [Then("the save chart images modal is visible")]
    public async Task ThenTheSaveChartImagesModalIsVisible()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.IsSaveImagesModalDisplayed();
    }

    [Then("the '(.*)' file is downloaded")]
    public void ThenTheFileIsDownloaded(string fileName)
    {
        Assert.NotNull(_benchmarkSpendingPage);
        Assert.NotNull(_download);
        var downloadedFilePath = _download.SuggestedFilename;
        Assert.Equal(fileName, downloadedFilePath);
    }

    [Then("the following benchmark spending table is shown for '(.*)'")]
    public async Task ThenTheFollowingBenchmarkSpendingTableIsShownFor(string chartName, Table table)
    {
        var expected = GetExpectedTableData(table);
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.IsTableDataForChartDisplayed(chartName, expected);
    }

    [Then("the benchmark spending comparison charts and tables are not displayed")]
    public async Task ThenTheBenchmarkSpendingComparisonChartsAndTablesAreNotDisplayed()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.AreComparisonChartsAndTablesDisplayed(false);
    }

    [When("I hover over a benchmark spending chart bar")]
    public async Task WhenIHoverOverABenchmarkSpendingChartBar()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.HoverOnGraphBar();
    }

    [Then("additional benchmark spending information is displayed")]
    public async Task ThenAdditionalBenchmarkSpendingInformationIsDisplayed()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.IsSchoolDetailsPopUpVisible();
    }

    [Then("additional benchmark spending information contains")]
    public async Task ThenAdditionalBenchmarkSpendingInformationContains(DataTable table)
    {
        var expected = new List<List<string>>();
        {
            var headers = table.Header.ToList();
            expected.Add(headers);
            expected.AddRange(table.Rows.Select(row => row.Select(cell => cell.Value).ToList()));
        }

        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.IsTableDataForTooltipDisplayed(expected);
    }

    [When("I select the school name on the benchmark spending chart")]
    public async Task WhenISelectTheSchoolNameOnTheBenchmarkSpendingChart()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        _schoolHomePage = await _benchmarkSpendingPage.ClickSchoolName();
    }

    [When("I tab to the school name on the benchmark spending chart")]
    public async Task WhenITabToTheSchoolNameOnTheBenchmarkSpendingChart()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.TabToSchoolName();
    }

    [When("I press the Enter key when focused on the benchmark spending school name")]
    public async Task WhenIPressTheEnterKeyWhenFocusedOnTheBenchmarkSpendingSchoolName()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.AssertSchoolNameFocused();
        _schoolHomePage = await _benchmarkSpendingPage.PressEnterKey();
    }

    [Then("I am navigated to selected school home page")]
    public async Task ThenIAmNavigatedToSelectedSchoolHomePage()
    {
        Assert.NotNull(_schoolHomePage);
        await _schoolHomePage.IsDisplayed();
    }

    [Then("I can view the benchmark spending tooltip")]
    public async Task ThenICanViewTheBenchmarkSpendingTooltip()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.TooltipIsDisplayed();
    }

    [When("I click on benchmark spending comparators link")]
    public async Task WhenIClickOnBenchmarkSpendingComparatorsLink()
    {
        Assert.NotNull(_benchmarkSpendingPage);
        _comparatorsPage = await _benchmarkSpendingPage.ClickComparatorSetDetails();
    }

    [Then("I am taken to comparators page")]
    public async Task ThenIAmTakenToComparatorsPage()
    {
        Assert.NotNull(_comparatorsPage);
        await _comparatorsPage.IsDisplayed();
    }

    [Then("pupil cost comparators are (.*)")]
    public async Task ThenPupilCostComparatorsAre(string pupilComparators)
    {
        Assert.NotNull(_comparatorsPage);
        await _comparatorsPage.CheckRunningCostComparators(pupilComparators == "not null");
    }

    [Then("building cost comparators are (.*)")]
    public async Task ThenBuildingCostComparatorsAre(string buildingComparators)
    {
        Assert.NotNull(_comparatorsPage);
        await _comparatorsPage.CheckBuildingCostComparators(buildingComparators == "not null");
    }

    [When("I click on benchmark spending '(.*)' school performance")]
    public async Task WhenIClickOnBenchmarkSpendingSchoolPerformance(string banding)
    {
        Assert.NotNull(_benchmarkSpendingPage);
        await _benchmarkSpendingPage.ClickSchoolPerformanceCheckbox(banding);
    }

    private List<List<string>> GetExpectedTableData(Table table)
    {
        var expected = new List<List<string>>();
        var headers = table.Header.ToList();
        expected.Add(headers);
        expected.AddRange(table.Rows.Select(row => row.Select(cell => cell.Value).ToList()));

        return expected;
    }

    private static string BenchmarkSpendingUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/comparison";
}
