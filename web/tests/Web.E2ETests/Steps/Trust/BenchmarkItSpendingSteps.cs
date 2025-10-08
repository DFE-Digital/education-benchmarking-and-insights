using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Web.E2ETests.Pages.Trust.Benchmarking;
using Web.E2ETests.Pages.Trust.Comparators;
using Xunit;

namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust benchmark IT spending")]
public class BenchmarkItSpendingSteps(PageDriver driver)
{
    private CreateComparatorsByNamePage? _createComparatorsByNamePage;
    private CreateComparatorsByPage? _createComparatorsByPage;
    private BenchmarkItSpendingPage? _benchmarkItSpendingPage;
    private ViewComparatorsPage? _viewComparatorsPage;
    private HomePage? _trustHomepage;
    private IDownload? _download;

    [Given("I have no previous comparators selected for company number '(.*)'")]
    public async Task GivenIHaveNoPreviousComparatorsSelectedForCompanyNumber(string companyNumber)
    {
        var url = RevertComparatorsUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        var revertComparatorsPage = new RevertComparatorsPage(page);
        await revertComparatorsPage.IsDisplayed();
        await revertComparatorsPage.ClickContinue();
    }

    [Given("I am on IT spend for trust with company number '(.*)'")]
    public async Task GivenIAmOnITSpendForTrustWithCompanyNumber(string companyNumber)
    {
        var url = BenchmarkItSpendingUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _createComparatorsByPage = new CreateComparatorsByPage(page);
        await _createComparatorsByPage.IsDisplayed();
    }

    [When("I select the option By Name and click continue")]
    public async Task WhenISelectTheOptionByNameAndClickContinue()
    {
        Assert.NotNull(_createComparatorsByPage);
        await _createComparatorsByPage.SelectComparatorsBy(ComparatorsByTypes.Name);
        _createComparatorsByNamePage = await _createComparatorsByPage.ClickContinue(ComparatorsByTypes.Name) as CreateComparatorsByNamePage;
    }

    [When("I select the trust with company number '(.*)' from suggester")]
    public async Task WhenISelectTheTrustWithCompanyNumberFromSuggester(string companyNumber)
    {
        Assert.NotNull(_createComparatorsByNamePage);
        await _createComparatorsByNamePage.TypeIntoTrustSearchBox(companyNumber);
        await _createComparatorsByNamePage.SelectItemFromSuggester();
    }

    [When("I click the choose trust button")]
    public async Task WhenIClickTheChooseTrustButton()
    {
        Assert.NotNull(_createComparatorsByNamePage);
        await _createComparatorsByNamePage.ClickChooseTrustButton();
    }

    [When("I click the create set button")]
    public async Task WhenIClickTheCreateSetButton()
    {
        Assert.NotNull(_createComparatorsByNamePage);
        _benchmarkItSpendingPage = await _createComparatorsByNamePage.ClickCreateSetButton(p => new BenchmarkItSpendingPage(p));
    }

    [When("I click on view comparator set")]
    public async Task WhenIClickOnViewComparatorSet()
    {
        Assert.NotNull(_benchmarkItSpendingPage);
        _viewComparatorsPage = await _benchmarkItSpendingPage.ClickViewComparatorSetLink();
    }

    [Then("the IT spend for trust page for company number '(.*)' is displayed")]
    public async Task ThenTheITSpendForTrustPageForCompanyNumberIsDisplayed(string companyNumber)
    {
        Assert.NotNull(_benchmarkItSpendingPage);
        await _benchmarkItSpendingPage.IsDisplayed();
    }

    [Then("I should see the following IT spend charts on the page:")]
    public async Task ThenIShouldSeeTheFollowingITSpendChartsOnThePage(Table table)
    {
        Assert.NotNull(_benchmarkItSpendingPage);

        var expectedTitles = table.Rows.Select(row => row["Chart Title"]);
        await _benchmarkItSpendingPage.AssertChartsVisible(expectedTitles);
    }

    [Then("I should see the following IT spend forecast charts on the page:")]
    public async Task ThenIShouldSeeTheFollowingITSpendForecastChartsOnThePage(Table table)
    {
        Assert.NotNull(_benchmarkItSpendingPage);

        var expectedTitles = table.Rows.Select(row => row["Chart Title"]);
        await _benchmarkItSpendingPage.AssertForecastChartsVisible(expectedTitles);
    }

    [Then("the comparator set page is displayed")]
    public async Task ThenTheComparatorSetPageIsDisplayed()
    {
        Assert.NotNull(_viewComparatorsPage);
        await _viewComparatorsPage.IsDisplayed();
    }

    [When("When I navigate to the trust Benchmark IT spending URL with company number '(.*)'")]
    public async Task WhenWhenINavigateToTheTrustBenchmarkITSpendingUrlWithCompanyNumber(string companyNumber)
    {
        await NavigateToBenchmarkItSpendPage(companyNumber, verify: false);
    }

    [Given("I am on it spend page for trust with company number '(.*)'")]
    [When("I am on it spend page for trust with company number '(.*)'")]
    public async Task GivenIAmOnItSpendPageForTrustWithCompanyNumber(string companyNumber)
    {
        await NavigateToBenchmarkItSpendPage(companyNumber);
    }

    [When("I click on the trust name on the chart")]
    public async Task WhenIClickOnTheTrustNameOnTheChart()
    {
        Assert.NotNull(_benchmarkItSpendingPage);
        _trustHomepage = await _benchmarkItSpendingPage.ClickOnTrustName();
    }

    [Then("I am navigated to selected trust home page")]
    public async Task ThenIAmNavigatedToSelectedTrustHomePage()
    {
        Assert.NotNull(_trustHomepage);
        await _trustHomepage.IsDisplayed();
    }

    [Then("the save chart images button is visible")]
    public async Task ThenTheSaveChartImagesButtonIsVisible()
    {
        Assert.NotNull(_benchmarkItSpendingPage);
        await _benchmarkItSpendingPage.IsSaveImagesButtonDisplayed();
    }

    [When("I click the save chart images button")]
    public async Task WhenIClickTheSaveChartImagesButton()
    {
        Assert.NotNull(_benchmarkItSpendingPage);

        var page = await driver.Current;
        _download = await page.RunAndWaitForDownloadAsync(() => _benchmarkItSpendingPage.ClickSaveImagesButton(), new TimeSpan(0, 2, 0));
    }

    [Then("the save chart images modal is visible")]
    public async Task ThenTheSaveChartImagesModalIsVisible()
    {
        Assert.NotNull(_benchmarkItSpendingPage);
        await _benchmarkItSpendingPage.IsSaveImagesModalDisplayed();
    }

    [Then("the '(.*)' file is downloaded")]
    public void ThenTheFileIsDownloaded(string fileName)
    {
        Assert.NotNull(_benchmarkItSpendingPage);
        var downloadedFilePath = _download?.SuggestedFilename;
        Assert.Equal(fileName, downloadedFilePath);
    }

    [When("I click to view results as '(.*)'")]
    public async Task WhenIClickToViewResultsAs(string viewAs)
    {
        Assert.NotNull(_benchmarkItSpendingPage);
        await _benchmarkItSpendingPage.ClickViewAs(viewAs);
    }

    [When("I click Apply filters")]
    public async Task WhenIClickApplyFilters()
    {
        Assert.NotNull(_benchmarkItSpendingPage);
        _benchmarkItSpendingPage = await _benchmarkItSpendingPage.ClickApplyFilters();
    }

    [Then("I should see the following IT spend tables on the page:")]
    public async Task ThenIShouldSeeTheFollowingITSpendTablesOnThePage(Table table)
    {
        Assert.NotNull(_benchmarkItSpendingPage);

        var expectedTitles = table.Rows.Select(row => row["Table Title"]);
        await _benchmarkItSpendingPage.AssertTablesVisible(expectedTitles);
    }

    private async Task NavigateToBenchmarkItSpendPage(string companyNumber, bool verify = true)
    {
        var url = BenchmarkItSpendingUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);
        await driver.WaitForPendingRequests(500);

        if (verify)
        {
            Assert.NotNull(_benchmarkItSpendingPage);
            await _benchmarkItSpendingPage.IsDisplayed();
        }
    }

    private static string RevertComparatorsUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}/comparators/revert";
    private static string BenchmarkItSpendingUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}/benchmark-it-spending";
}