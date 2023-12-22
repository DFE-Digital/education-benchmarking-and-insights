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
}