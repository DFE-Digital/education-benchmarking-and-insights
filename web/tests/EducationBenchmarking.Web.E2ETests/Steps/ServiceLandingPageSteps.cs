using EducationBenchmarking.Web.E2ETests.Pages;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class ServiceLandingPageSteps
{
    private readonly ServiceLandingPage _serviceLandingPage;

    public ServiceLandingPageSteps(ServiceLandingPage serviceLandingPage)
    {
        _serviceLandingPage = serviceLandingPage;
    }

    [Given("I am on service landing page")]
    public async Task GivenIAmOnServiceLandingPage()
    {
        await _serviceLandingPage.GoToPage();
    }

    [When("I click start now")]
    public async Task WhenIClickStartNow()
    {
      await _serviceLandingPage.ClickStartNow();
    }
}