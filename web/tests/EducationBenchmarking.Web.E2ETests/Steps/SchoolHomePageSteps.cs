using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Steps;
[Binding]
public class SchoolHomePageSteps
{
     private readonly IPage _page;

     public SchoolHomePageSteps(IPage page)
     {
          _page = page;
     }

     [Given(@"I search for a school on search page")]
     public void GivenISearchForASchoolOnSearchPage()
     {
          
     }

     [Then(@"the expenditure benchmarking page is displayed")]
     public void ThenTheExpenditureBenchmarkingPageIsDisplayed()
     {
          /*//add checks for the below 
          And the common header and footer are used as per the design
          And the URL is bookmarkable and contains the school's URN
          And the school name is displayed at the top of the page*/
          ScenarioContext.StepIsPending();
     }
}