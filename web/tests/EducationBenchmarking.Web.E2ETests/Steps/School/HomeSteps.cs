using EducationBenchmarking.Web.E2ETests.Drivers;
using EducationBenchmarking.Web.E2ETests.Pages.School;
using Xunit;

namespace EducationBenchmarking.Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School homepage")]
public class HomeSteps(PageDriver driver)
{
    private HomePage? _schoolHomePage;
    private DetailsPage? _schoolDetailsPage;
    private CompareYourCostsPage? _compareYourCostsPage;
    private CurriculumFinancialPlanningPage? _curriculumAndFinancialPlanningPage;
    private BenchmarkWorkforcePage? _benchmarkWorkforcePage;

    [Given("I am on school homepage for school with urn '(.*)'")]
    public async Task GivenIAmOnSchoolHomepageForSchoolWithUrn(string urn)
    {
        var url = SchoolHomeUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _schoolHomePage = new HomePage(page);
        await _schoolHomePage.IsDisplayed();
    }

    [When("I click on school details in resource section")]
    public async Task WhenIClickOnSchoolDetailsInResourceSection()
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
        Assert.NotNull(_curriculumAndFinancialPlanningPage);
        await _curriculumAndFinancialPlanningPage.IsDisplayed();
    }

    [When("I click on benchmark workforce data")]
    public async Task WhenIClickOnBenchmarkWorkforceData()
    {
        Assert.NotNull(_schoolHomePage);
        _benchmarkWorkforcePage = await _schoolHomePage.ClickBenchmarkWorkforce();
    }

    [Then("the benchmark workforce page is displayed")]
    public async Task ThenTheBenchmarkWorkforcePageIsDisplayed()
    {
        Assert.NotNull(_benchmarkWorkforcePage);
        await _benchmarkWorkforcePage.IsDisplayed();
    }

    private static string SchoolHomeUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}";
}