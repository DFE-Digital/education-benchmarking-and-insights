using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Xunit;

namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust homepage")]
public class HomeSteps(PageDriver driver)
{
    private BenchmarkCensusPage? _benchmarkCensusPage;
    private CompareYourCostsPage? _compareYourCostsPage;
    private CurriculumFinancialPlanningPage? _curriculumFinancialPlanningPage;
    private SpendingCostsPage? _spendingCostsPage;
    private TrustForecastPage? _trustForecastPage;
    private HomePage? _trustHomePage;

    [Given("I am on trust homepage for trust with company number '(.*)'")]
    public async Task GivenIAmOnTrustHomepageForTrustWithCompanyNumber(string companyNumber)
    {
        var url = TrustHomeUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _trustHomePage = new HomePage(page);
        await _trustHomePage.IsDisplayed();
    }

    [When("I click on compare your costs")]
    public async Task WhenIClickOnCompareYourCosts()
    {
        Assert.NotNull(_trustHomePage);
        _compareYourCostsPage = await _trustHomePage.ClickCompareYourCosts();
    }

    [Then("the compare your costs page is displayed")]
    public async Task ThenTheCompareYourCostsPageIsDisplayed()
    {
        Assert.NotNull(_compareYourCostsPage);
        await _compareYourCostsPage.IsDisplayed();
    }

    [When("I click on view all spending priorities for this trust")]
    public async Task WhenIClickOnViewAllSpendingPrioritiesForThisTrust()
    {
        Assert.NotNull(_trustHomePage);
        _spendingCostsPage = await _trustHomePage.ClickSpendingPriorities();
    }

    [Then("the spending and costs page is displayed")]
    public async Task ThenTheSpendingAndCostsPageIsDisplayed()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsDisplayed();
    }

    [Then("I can see the following RAG ratings for cost categories in the trust:")]
    public async Task ThenICanSeeTheFollowingRagRatingsForCostCategoriesInTheTrust(DataTable table)
    {
        Assert.NotNull(_trustHomePage);

        var rags = table.Rows.ToDictionary(row => row["Name"], row => row["Status"]);
        foreach (var row in rags)
        {
            await _trustHomePage.AssertCategoryRags(row.Key, row.Value);
        }
    }

    [Then("I can see the following RAG ratings for schools in the trust:")]
    public async Task ThenICanSeeTheFollowingRagRatingsForSchoolsInTheTrust(DataTable table)
    {
        Assert.NotNull(_trustHomePage);

        var rags = table.Rows.ToDictionary(row => row["Name"], row => row["Status"]);
        foreach (var row in rags)
        {
            await _trustHomePage.AssertSchoolRags(row.Key, row.Value);
        }
    }

    [When("I click on benchmark census data")]
    public async Task WhenIClickOnBenchmarkCensusData()
    {
        Assert.NotNull(_trustHomePage);
        _benchmarkCensusPage = await _trustHomePage.ClickBenchmarkCensus();
    }

    [When("I click on forecast and risk")]
    public async Task WhenIClickOnForecastAndRisk()
    {
        Assert.NotNull(_trustHomePage);
        _trustForecastPage = await _trustHomePage.ClickTrustForecast();
    }

    [Then("the benchmark census page is displayed")]
    public async Task ThenTheBenchmarkCensusPageIsDisplayed()
    {
        Assert.NotNull(_benchmarkCensusPage);
        await _benchmarkCensusPage.IsDisplayed();
    }

    [When("I click on Curriculum and financial planning")]
    public async Task WhenIClickOnCurriculumAndFinancialPlanning()
    {
        Assert.NotNull(_trustHomePage);
        _curriculumFinancialPlanningPage = await _trustHomePage.ClickTrustCurriculumFinancialPlanning();
    }

    [Then("the Curriculum and financial page is displayed")]
    public async Task ThenTheCurriculumAndFinancialPageIsDisplayed()
    {
        Assert.NotNull(_curriculumFinancialPlanningPage);
        await _curriculumFinancialPlanningPage.IsDisplayed();
    }
    [Then("the trust forecast page is displayed")]
    public async Task ThenTheTrustForecastPageIsDisplayed()
    {
        Assert.NotNull(_trustForecastPage);
        await _trustForecastPage.IsDisplayed();
    }

    [Then("following table is displayed on the page")]
    public async Task ThenFollowingTableIsDisplayedOnThePage(Table table)
    {
        var expected = new List<List<string>>();
        {
            var headers = table.Header.ToList();

            expected.Add(headers);
            expected.AddRange(table.Rows.Select(row => row.Select(cell => cell.Value).ToList()));
        }
        Assert.NotNull(_trustForecastPage);
        await _trustForecastPage.IsTableDataDisplayed(expected);
    }

    [Then("the service banner displays the title '(.*)', heading '(.*)' and body '(.*)'")]
    public async Task ThenTheServiceBannerDisplaysTheTitleHeadingAndBody(string title, string heading, string body)
    {
        Assert.NotNull(_trustHomePage);
        await _trustHomePage.HasBanner(title, heading, body);
    }

    private static string TrustHomeUrl(string companyNumber)
    {
        return $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}";
    }
}