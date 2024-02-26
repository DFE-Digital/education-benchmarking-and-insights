using EducationBenchmarking.Web.E2ETests.Drivers;
using EducationBenchmarking.Web.E2ETests.Pages.School;
using EducationBenchmarking.Web.E2ETests.Pages.School.CurriculumFinancialPlanningSteps;
using Xunit;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "School Homepage")]
public class SchoolHomeSteps(PageDriver driver)
{
    private SchoolHomePage? _schoolHomePage;
    private SchoolDetailsPage? _schoolDetailsPage;
    private CompareYourCostsPage? _compareYourCostsPage;
    private CreateNewFinancialPlanPage? _createNewFinancialPlanPage;
    private BenchmarkWorkforcePage? _benchmarkWorkforcePage;

    [Given("I am on school homepage for school with urn '(.*)'")]
    public async Task GivenIAmOnSchoolHomepageForSchoolWithUrn(string urn)
    {
        var url = SchoolHomeUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _schoolHomePage = new SchoolHomePage(page);
        await _schoolHomePage.IsDisplayed();
    }

    private static string SchoolHomeUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}";

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

    [When("I click on compare your costs in finance tools section")]
    public async Task WhenIClickOnCompareYourCostsInFinanceToolsSection()
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

    [When("I click on curriculum and financial planning in finance tools section")]
    public async Task WhenIClickOnCurriculumAndFinancialPlanningInFinanceToolsSection()
    {
        Assert.NotNull(_schoolHomePage);
        _createNewFinancialPlanPage = await _schoolHomePage.ClickFinancialPlanning();
    }

    [Then("the curriculum and financial planning start page is displayed")]
    public async Task ThenTheCurriculumAndFinancialPlanningStartPageIsDisplayed()
    {
        Assert.NotNull(_createNewFinancialPlanPage);
        await _createNewFinancialPlanPage.IsDisplayed();
    }

    [When("I click on benchmark workforce data in finance tools section")]
    public async Task WhenIClickOnBenchmarkWorkforceDataInFinanceToolsSection()
    {
        Assert.NotNull(_schoolHomePage);
        _benchmarkWorkforcePage = await _schoolHomePage.ClickBenchmarkWorkforce();
    }

    [Then(@"the benchmark workforce page is displayed")]
    public async Task ThenTheBenchmarkWorkforcePageIsDisplayed()
    {
        Assert.NotNull(_benchmarkWorkforcePage);
        await _benchmarkWorkforcePage.IsDisplayed();
    }
}