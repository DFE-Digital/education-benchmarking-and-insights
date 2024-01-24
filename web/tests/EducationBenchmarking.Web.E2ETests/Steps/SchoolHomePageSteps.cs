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
}