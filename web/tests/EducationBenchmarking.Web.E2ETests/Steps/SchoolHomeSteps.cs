using EducationBenchmarking.Web.E2ETests.Pages;
using EducationBenchmarking.Web.E2ETests.Pages.CurriculumFinancialPlanning;

namespace EducationBenchmarking.Web.E2ETests.Steps;
[Binding, Scope(Tag = "SchoolHome.feature")]
public class SchoolHomeSteps(SchoolHomePage schoolHomePage, SchoolDetailsPage schoolDetailsPage, CompareYourCostsPage compareYourCostsPage, CreateNewFinancialPlanPage createNewFinancialPlan)
{
    [Then("I am taken to school '(.*)' home page")]
    public async Task ThenIAmTakenToSchoolHomePage(string urn)
    {
        await schoolHomePage.WaitForPage(urn);
    }

    [When("I click compare your costs CTA")]
    public async Task WhenIClickCompareYourCostsCta()
    {
        await schoolHomePage.ClickOnCompareYourCosts();
    }

    [Given("I am on school homepage for school with urn '(.*)'")]
    public async Task GivenIAmOnSchoolHomepageForSchoolWithUrn(string urn)
    {
        await schoolHomePage.GotToPage(urn);
        await schoolHomePage.AssertPage();
    }

    [When("I click on school details in resource section")]
    public async Task WhenIClickOnSchoolDetailsInResourceSection()
    {
        await schoolHomePage.ClickLink("school details");
    }

    [Then("I am navigated to school details page for school with urn '(.*)'")]
    public async Task ThenIAmNavigatedToSchoolDetailsPageForSchoolWithUrn(string urn)
    {
        await schoolDetailsPage.WaitForPage(urn);
    }

    [When("I click on compare your costs in finance tools section")]
    public async Task WhenIClickOnCompareYourCostsInFinanceToolsSection()
    {
        await schoolHomePage.ClickLink("compare your costs");
    }

    [Then("I am navigated to compare your costs page for school with urn '(.*)'")]
    public async Task ThenIAmNavigatedToCompareYourCostsPageForSchoolWithUrn(string urn)
    {
        await compareYourCostsPage.WaitForPage(urn);
    }

    [When("I click on curriculum and financial planning in finance tools section")]
    public async Task WhenIClickOnCurriculumAndFinancialPlanningInFinanceToolsSection()
    {
        await schoolHomePage.ClickLink("curriculum and financial planning");
    }

    [Then("I am navigated to curriculum and financial planning page for school with urn '(.*)'")]
    public async Task ThenIAmNavigatedToCurriculumAndFinancialPlanningPageForSchoolWithUrn(string urn)
    {
        await createNewFinancialPlan.WaitForPage(urn);
    }
}