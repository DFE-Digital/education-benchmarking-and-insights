using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School homepage")]
public class HomeSteps(PageDriver driver)
{
    private HomePage? _schoolHomePage;
    private DetailsPage? _schoolDetailsPage;
    private CompareYourCostsPage? _compareYourCostsPage;
    private CurriculumFinancialPlanningPage? _curriculumAndFinancialPlanningPage;
    private BenchmarkCensusPage? _benchmarkCensusPage;
    private SpendingCostsPage? _spendingCostsPage;
    private CommercialResourcesPage? _commercialResourcesPage;
    private HistoricDataPage? _historicDataPage;

    [Given("I am on school homepage for school with urn '(.*)'")]
    public async Task GivenIAmOnSchoolHomepageForSchoolWithUrn(string urn)
    {
        var url = SchoolHomeUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _schoolHomePage = new HomePage(page);
        await _schoolHomePage.IsDisplayed();
    }

    [When("I click on school details")]
    public async Task WhenIClickOnSchoolDetails()
    {
        Assert.NotNull(_schoolHomePage);
        _schoolDetailsPage = await _schoolHomePage.ClickSchoolDetails();
    }

    [Then("the school details page is displayed")]
    public async Task ThenTheSchoolDetailsPageIsDisplayed()
    {
        Assert.NotNull(_schoolDetailsPage);
        await _schoolDetailsPage.IsDisplayed();
    }

    [When("I click on compare your costs")]
    public async Task WhenIClickOnCompareYourCosts()
    {
        Assert.NotNull(_schoolHomePage);
        _compareYourCostsPage = await _schoolHomePage.ClickCompareYourCosts();
    }

    [Then("the compare your costs page is displayed")]
    public async Task ThenTheCompareYourCostsPageIsDisplayed()
    {
        Assert.NotNull(_compareYourCostsPage);
        await _compareYourCostsPage.IsDisplayed();
    }

    [When("I click on curriculum and financial planning")]
    public async Task WhenIClickOnCurriculumAndFinancialPlanning()
    {
        Assert.NotNull(_schoolHomePage);
        _curriculumAndFinancialPlanningPage = await _schoolHomePage.ClickFinancialPlanning();
    }

    [Then("the curriculum and financial planning page is displayed")]
    public async Task ThenTheCurriculumAndFinancialPlanningPageIsDisplayed()
    {
        var page = await driver.Current;
        if (await page.Locator("h1:text-is('Curriculum and financial planning (CFP)')").CheckVisible())
        {
            // already logged in
        }
        else if (await page.Locator("h1:text-is('Department for Education Sign-in')").CheckVisible())
        {
            // Login required
            await page.Locator("input[id='username']").Fill(TestConfiguration.LoginEmail);
            await page.Locator("input[id='password']").Fill(TestConfiguration.LoginPassword);
            await page.Locator("button[type='submit']").Click();
            await page.Locator("label", new PageLocatorOptions { HasTextString = "01: FBIT TEST - Community School (Open)" }).Check();
            await page.Locator("input[type='submit']").Click();
        }
        else
        {
            throw new Exception("Unexpected page state: Unable to determine login requirement.");
        }
        Assert.NotNull(_curriculumAndFinancialPlanningPage);
        await _curriculumAndFinancialPlanningPage.IsDisplayed();

    }

    [When("I click on benchmark census data")]
    public async Task WhenIClickOnBenchmarkCensusData()
    {
        Assert.NotNull(_schoolHomePage);
        _benchmarkCensusPage = await _schoolHomePage.ClickBenchmarkCensus();
    }

    [Then("the benchmark census page is displayed")]
    public async Task ThenTheBenchmarkCensusPageIsDisplayed()
    {
        Assert.NotNull(_benchmarkCensusPage);
        await _benchmarkCensusPage.IsDisplayed();
    }

    [When("I click on view all spending and costs")]
    public async Task WhenIClickOnViewAllSpendingAndCosts()
    {
        Assert.NotNull(_schoolHomePage);
        _spendingCostsPage = await _schoolHomePage.ClickSpendingAndCosts();
    }


    [Then("the spending and costs page is displayed")]
    public async Task ThenTheSpendingAndCostsPageIsDisplayed()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsDisplayed();
    }
    private static string SchoolHomeUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}";

    [When("I click on find ways to spend less")]
    public async Task WhenIClickOnFindWaysToSpendLess()
    {
        Assert.NotNull(_schoolHomePage);
        _commercialResourcesPage = await _schoolHomePage.ClickFindWaysToSpendLess();

    }

    [Then("the commercial resources page is displayed")]
    public async Task ThenTheCommercialResourcesPageIsDisplayed()
    {
        Assert.NotNull(_commercialResourcesPage);
        await _commercialResourcesPage.IsDisplayed();
    }

    [When("I click on view historic data")]
    public async Task WhenIClickOnViewHistoricData()
    {
        Assert.NotNull(_schoolHomePage);
        _historicDataPage = await _schoolHomePage.ClickHistoricData();
    }

    [Then("the historic data page is displayed")]
    public async Task ThenTheHistoricDataPageIsDisplayed()
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.IsDisplayed();
    }
}