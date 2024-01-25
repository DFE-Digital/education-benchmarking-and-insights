using EducationBenchmarking.Web.E2ETests.Pages;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class ServiceLandingPageSteps(ServiceLandingPage serviceLandingPage)
{
    [Given("I am on service landing page")]
    public async Task GivenIAmOnServiceLandingPage()
    {
        await serviceLandingPage.GoToPage();
    }

    [When("I click start now")]
    public async Task WhenIClickStartNow()
    {
        await serviceLandingPage.ClickStartNow();
    }
}