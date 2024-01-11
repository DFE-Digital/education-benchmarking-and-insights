using EducationBenchmarking.Web.E2ETests.Pages;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Steps;
[Binding]
public class SearchOrganizationSteps
{
    private readonly SearchOrganizationPage _searchOrganizationPage;
    private readonly IPage _page;

    public SearchOrganizationSteps(SearchOrganizationPage searchOrganizationPage, IPage page)
    {
        _searchOrganizationPage = searchOrganizationPage;
        _page = page;
    }

    [Then(@"I am on find organization page")]
    public void ThenIAmOnFindOrganizationPage()
    {
        _page.WaitForURLAsync(Config.BaseUrl + "/find-organisation");
    }
    

    [When(@"I type '(.*)' in the search bar and click it")]
    public async Task WhenITypeInTheSearchBarAndClickIt(string searchText)
    {
        await _searchOrganizationPage.TypeInSearchSearchBar(searchText);
        await _searchOrganizationPage.ClickOnSuggestion(searchText);
    }

    [When(@"I click continue")]
    public async Task WhenIClickContinue()
    {
       await _searchOrganizationPage.ClickContinueBtn();
    }
}