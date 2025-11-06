using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority homepage")]
public class HomeSteps(PageDriver driver)
{
    private BenchmarkCensusPage? _benchmarkCensusPage;
    private CompareYourCostsPage? _compareYourCostsPage;
    private HomePage? _localAuthorityHomePage;
    private HighNeedsStartBenchmarkingPage? _benchmarkHighNeedsPage;
    private HighNeedsHistoricDataPage? _highNeedsHistoryPage;

    [Given("I am on local authority homepage for local authority with code '(.*)'")]
    public async Task GivenIAmOnLocalAuthorityHomepageForLocalAuthorityWithCode(string laCode)
    {
        var url = LocalAuthorityHomeUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _localAuthorityHomePage = new HomePage(page);
        await _localAuthorityHomePage.IsDisplayed();
    }

    [When("I click on compare your costs")]
    public async Task WhenIClickOnCompareYourCosts()
    {
        Assert.NotNull(_localAuthorityHomePage);
        _compareYourCostsPage = await _localAuthorityHomePage.ClickCompareYourCosts();
    }

    [Then("the compare your costs page is displayed")]
    public async Task ThenTheCompareYourCostsPageIsDisplayed()
    {
        Assert.NotNull(_compareYourCostsPage);
        await _compareYourCostsPage.IsDisplayed();
    }

    [When("I click on benchmark census data")]
    public async Task WhenIClickOnBenchmarkCensusData()
    {
        Assert.NotNull(_localAuthorityHomePage);
        _benchmarkCensusPage = await _localAuthorityHomePage.ClickBenchmarkCensus();
    }

    [When("I click on benchmark high needs")]
    public async Task WhenIClickOnBenchmarkHighNeeds()
    {
        Assert.NotNull(_localAuthorityHomePage);
        _benchmarkHighNeedsPage = await _localAuthorityHomePage.ClickBenchmarkHighNeeds();
    }

    [When("I click on high needs historic data")]
    public async Task WhenIClickOnHighNeedsHistoricData()
    {
        Assert.NotNull(_localAuthorityHomePage);
        _highNeedsHistoryPage = await _localAuthorityHomePage.ClickHighNeedsHistory();
    }

    [Then("the benchmark census page is displayed")]
    public async Task ThenTheBenchmarkCensusPageIsDisplayed()
    {
        Assert.NotNull(_benchmarkCensusPage);
        await _benchmarkCensusPage.IsDisplayed();
    }

    [Then("the service banner displays the title '(.*)', heading '(.*)' and body '(.*)'")]
    public async Task ThenTheServiceBannerDisplaysTheTitleHeadingAndBody(string title, string heading, string body)
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.HasBanner(title, heading, body);
    }

    [Then("the High needs benchmarking page is displayed")]
    public async Task ThenTheHighNeedsBenchmarkingPageIsDisplayed()
    {
        Assert.NotNull(_benchmarkHighNeedsPage);
        await _benchmarkHighNeedsPage.IsDisplayed();
    }

    [Then("the High needs historic data page is displayed")]
    public async Task ThenTheHighNeedsHistoricDataPageIsDisplayed()
    {
        Assert.NotNull(_highNeedsHistoryPage);
        await _highNeedsHistoryPage.IsDisplayed();
    }

    [Then("the schools accordion should be displayed")]
    public async Task ThenTheSchoolsAccordionShouldBeDisplayed()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.IsSchoolsAccordionDisplayed();
    }

    [Then("the schools accordion should not be displayed")]
    public async Task ThenTheSchoolsAccordionShouldNotBeDisplayed()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.IsSchoolsAccordionDisplayed(false);
    }

    [Then("the priority school RAGs section should be displayed for '(.*)' containing the following rows:")]
    public async Task ThenThePrioritySchoolRaGsSectionShouldBeDisplayedForContainingTheFollowingRows(string overallPhase, DataTable table)
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.ContainsPriorityRagsForPhase(overallPhase, table);
    }

    [Given("I should see the following table data in financial tab")]
    [Then("I should see the following table data in financial tab")]
    public async Task GivenIShouldSeeTheFollowingTableDataInFinancialTab(Table table)
    {
        var expected = GetExpectedTableData(table);
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.IsFinancialTableDataDisplayed(expected);
    }

    [When("I click on show filters on the financial tab")]
    public async Task WhenIClickOnShowFiltersOnTheFinancialTab()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.ClickToggleFinancialFiltersBtn();
    }

    [When("I apply has nursery classes filter on the financial tab")]
    public async Task WhenIApplyHasNurseryClassesFilterOnTheFinancialTab()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.ClickHasNurseryClassesFinancialCheckBox();
    }

    [When("I click Apply filters on the financial tab")]
    public async Task WhenIClickApplyFiltersOnTheFinancialTab()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.ClickFinancialApplyFilters();
    }

    [Given("I should see the following table data in workforce tab")]
    [Then("I should see the following table data in workforce tab")]
    public async Task GivenIShouldSeeTheFollowingTableDataInWorkforceTab(Table table)
    {
        var expected = GetExpectedTableData(table);
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.IsWorkforceTableDataDisplayed(expected);
    }

    [Given("I click on the workforce tab")]
    public async Task GivenIClickOnTheWorkforceTab()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.ClickWorkforceTab();
    }

    [When("I click on show filters on the workforce tab")]
    public async Task WhenIClickOnShowFiltersOnTheWorkforceTab()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.ClickToggleWorkforceFiltersBtn();
    }

    [When("I apply has nursery classes filter on the workforce tab")]
    public async Task WhenIApplyHasNurseryClassesFilterOnTheWorkforceTab()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.ClickHasNurseryClassesWorkforceCheckBox();
    }

    [When("I click Apply filters on the workforce tab")]
    public async Task WhenIClickApplyFiltersOnTheWorkforceTab()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.ClickWorkforceApplyFilters();
    }

    private static string LocalAuthorityHomeUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}";

    private List<List<string>> GetExpectedTableData(Table table)
    {
        var expected = new List<List<string>>();
        var headers = table.Header.ToList();
        expected.Add(headers);
        expected.AddRange(table.Rows.Select(row => row.Select(cell => cell.Value).ToList()));

        return expected;
    }
}