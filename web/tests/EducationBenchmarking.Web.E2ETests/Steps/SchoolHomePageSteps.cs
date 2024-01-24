using EducationBenchmarking.Web.E2ETests.Pages;

namespace EducationBenchmarking.Web.E2ETests.Steps;
[Binding]
public class SchoolHomePageSteps
{
    private readonly SchoolHomePage _schoolHomePage;

    public SchoolHomePageSteps(SchoolHomePage schoolHomePage)
    {
        _schoolHomePage = schoolHomePage;
    }

    [Then("I am taken to school '(.*)' home page")]
    public async Task ThenIAmTakenToSchoolHomePage(string urn)
    {
        await _schoolHomePage.WaitForPage(urn);
    }
    
    [When("I click compare your costs CTA")]
    public async Task WhenIClickCompareYourCostsCta()
    {
        await _schoolHomePage.ClickOnCompareYourCosts();
    }
}