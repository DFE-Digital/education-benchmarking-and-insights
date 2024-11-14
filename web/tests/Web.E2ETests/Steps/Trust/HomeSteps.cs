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
    private SpendingCostsPage? _spendingCostsPage;
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

    [Then("the benchmark census page is displayed")]
    public async Task ThenTheBenchmarkCensusPageIsDisplayed()
    {
        Assert.NotNull(_benchmarkCensusPage);
        await _benchmarkCensusPage.IsDisplayed();
    }

    private static string TrustHomeUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}";
}