using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Steps;
[Binding]
public class CompareYourCostsSteps
{
     private readonly IPage _page;

     public CompareYourCostsSteps(IPage page)
     {
          _page = page;
     }

     [Then(@"I am taken to compare your costs page")]
     public void ThenIAmTakenToCompareYourCostsPage()
     {
          _page.WaitForURLAsync(Config.BaseUrl + "/school/*/comparison");
     }

     [Given(@"I am on compare your costs page")]
     public void GivenIAmOnCompareYourCostsPage()
     {
          ScenarioContext.StepIsPending();
     }
}