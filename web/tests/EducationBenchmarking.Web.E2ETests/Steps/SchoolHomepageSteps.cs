using EducationBenchmarking.Web.E2ETests.Pages;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class SchoolHomepageSteps(SchoolHomepage schoolHomepage, SchoolDetailsPage schoolDetailsPage, CompareYourCostsPage compareYourCostsPage, CurriculumFinancialPlanningPages curriculumFinancialPlanningPages)
{
    [Then("I am taken to school '(.*)' home page")]
    public async Task ThenIAmTakenToSchoolHomePage(string urn)
    {
        await schoolHomepage.WaitForPage(urn);
    }

    [When("I click compare your costs CTA")]
    public async Task WhenIClickCompareYourCostsCta()
    {
        await schoolHomepage.ClickOnCompareYourCosts();
    }

    [Given(@"I am on school homepage for school with urn '(.*)'")]
    public async Task GivenIAmOnSchoolHomepageForSchoolWithUrn(string urn)
    {
        await schoolHomepage.GotToPage(urn);
        await schoolHomepage.AssertPage();
    }

    [When(@"I click on school details in resource section")]
    public async Task WhenIClickOnSchoolDetailsInResourceSection()
    {
        await schoolHomepage.ClickLink("school details");
        
    }

    [Then(@"I am navigated to school details page for school with urn '(.*)'")]
    public async Task ThenIAmNavigatedToSchoolDetailsPageForSchoolWithUrn(string urn)
    {
        await schoolDetailsPage.WaitForPage(urn);
        await schoolDetailsPage.AssertPage();
    }

    [When(@"I click on compare your costs in finance tools section")]
    public async Task WhenIClickOnCompareYourCostsInFinanceToolsSection()
    {
        await schoolHomepage.ClickLink("compare your costs");
    }

    [Then(@"I am navigated to compare your costs page for school with urn '(.*)'")]
    public async Task ThenIAmNavigatedToCompareYourCostsPageForSchoolWithUrn(string urn)
    {
        await compareYourCostsPage.WaitForPage(urn);
        await compareYourCostsPage.AssertPage();
    }

    [When(@"I click on curriculum and financial planning in finance tools section")]
    public async Task WhenIClickOnCurriculumAndFinancialPlanningInFinanceToolsSection()
    {
        await schoolHomepage.ClickLink("curriculum and financial planning");
    }

    [Then(@"I am navigated to curriculum and financial planning page for school with urn '(.*)'")]
    public async Task ThenIAmNavigatedToCurriculumAndFinancialPlanningPageForSchoolWithUrn(string urn)
    {
        await curriculumFinancialPlanningPages.Start.WaitForPage(urn);
    }
}