using EducationBenchmarking.Web.E2ETests.Pages;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Steps;
[Binding]
public class ServiceLandingPageSteps
{
    private readonly IPage _page;
    private readonly ServiceLandingPage _serviceLandingPage;

    public ServiceLandingPageSteps(IPage page, ServiceLandingPage serviceLandingPage)
    {
        _page = page;
        _serviceLandingPage = serviceLandingPage;
    }

    [Given(@"I am on service landing page")]
    public async Task GivenIAmOnServiceLandingPage()
    {
        await _page.GotoAsync($"{Config.BaseUrl}");
        
    }

    [When(@"I click start now")]
    public async Task WhenIClickStartNow()
    {
      await _serviceLandingPage.ClickStartNow();
    }
}