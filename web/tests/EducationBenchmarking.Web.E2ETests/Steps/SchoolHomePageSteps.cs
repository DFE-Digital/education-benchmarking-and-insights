using EducationBenchmarking.Web.E2ETests.Pages;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Steps;
[Binding]
public class SchoolHomePageSteps
{
    private IPage _page;
    private SchoolHomePage _schoolHomePage;

    public SchoolHomePageSteps(IPage page, SchoolHomePage schoolHomePage)
    {
        _page = page;
        _schoolHomePage = schoolHomePage;
    }

    [Then(@"I am taken to school home page")]
    public void ThenIAmTakenToSchoolHomePage()
    {
        _page.WaitForURLAsync(Config.BaseUrl + "/school*");
    }
    [When(@"I click compare your costs CTA")]
    public async Task WhenIClickCompareYourCostsCta()
    {
       await _schoolHomePage.ClickOnCompareYourCosts();
    }
}