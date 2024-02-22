using EducationBenchmarking.Web.E2ETests.Pages;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class SchoolHomePageSteps(SchoolHomePage schoolHomePage)
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

    [Given(@"I am on school homepage for school with urn '(.*)'")]
    public async Task GivenIAmOnSchoolHomepageForSchoolWithUrn(string urn)
    {
        await schoolHomePage.GotToPage(urn);
        await schoolHomePage.AssertPage();
    }

    [When(@"I click on school details in resource section")]
    public async Task WhenIClickOnSchoolDetailsInResourceSection()
    {
        await schoolHomePage.ClickLink("school details");
        
    }
}