using EducationBenchmarking.Web.E2ETests.Pages;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Steps;
[Binding]
public class SearchOrganizationSteps
{
    private SearchOrganizationPage _searchOrganizationPage;
    private IPage _page;

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

    [When(@"I type '(.*)' in the search bar")]
    public async Task WhenITypeInTheSearchBar(string searchText )
    {
       await _searchOrganizationPage.TypeInSearchSearchBar(searchText);
    }
}