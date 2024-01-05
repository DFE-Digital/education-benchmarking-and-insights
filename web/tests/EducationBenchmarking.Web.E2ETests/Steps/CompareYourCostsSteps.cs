using EducationBenchmarking.Web.E2ETests.Pages;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Steps;
[Binding]
public class CompareYourCostsSteps
{
     private readonly IPage _page;
     private readonly CompareYourCostsPage _compareYourCostsPage;

     public CompareYourCostsSteps(IPage page, CompareYourCostsPage compareYourCostsPage)
     {
          _page = page;
          _compareYourCostsPage = compareYourCostsPage;
     }

     [Then(@"I am taken to compare your costs page")]
     public void ThenIAmTakenToCompareYourCostsPage()
     {
          _page.WaitForURLAsync(Config.BaseUrl + "/school/*/comparison");
     }

     [Given(@"I am on compare your costs page")]
     public async Task GivenIAmOnCompareYourCostsPage()
     {
        await  _compareYourCostsPage.AssertPage();
     }

     [When(@"i click on save as image for total expenditure")]
     public async Task WhenIClickOnSaveAsImageForTotalExpenditure()
     {
          await _compareYourCostsPage.ClickOnSaveImg();
     }

     [Then(@"chart image is downloaded")]
     public async Task ThenChartImageIsDownloaded()
     {
         await _compareYourCostsPage.AssertImageDownload();
     }

     [Given(@"the dimension in dimension dropdown is '(.*)'")]
     public async Task GivenTheDimensionInDimensionDropdownIs(string dimension)
     {
         await _compareYourCostsPage.AssertDimension(dimension);
     }

     [When(@"I change total expenditure dimension to '(.*)'")]
     public async Task WhenIChangeTotalExpenditureDimensionTo(string dimension)
     {
         await _compareYourCostsPage.ChangeDimension(dimension);
     }

     [Then(@"the chart should be updated")]
     public async Task ThenTheChartShouldBeUpdated()
     {
         await _compareYourCostsPage.AssertChartUpdate();
     }

     [Given(@"I am on compare your costs page for school with URN '(.*)'")]
     public async Task GivenIAmOnCompareYourCostsPageForSchoolWithUrn(string urn)
     {
         await _page.GotoAsync($"{Config.BaseUrl}/school/{urn}/comparison");
         await  _compareYourCostsPage.AssertPage();
     }

     [Then(@"all accordions are showing table view")]
     public void ThenAllAccordionsAreShowingTableView()
     {
         //assert utitlites and premises dimension dropdown 
         ScenarioContext.StepIsPending();
     }
}