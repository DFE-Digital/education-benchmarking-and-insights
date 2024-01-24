using EducationBenchmarking.Web.E2ETests.Pages;

namespace EducationBenchmarking.Web.E2ETests.Steps;
[Binding]
public class SearchOrganizationSteps
{
    private readonly SearchOrganizationPage _searchOrganizationPage;

    public SearchOrganizationSteps(SearchOrganizationPage searchOrganizationPage)
    {
        _searchOrganizationPage = searchOrganizationPage;
    }

    [Then("I am on find organization page")]
    public async Task ThenIAmOnFindOrganizationPage()
    {
        await _searchOrganizationPage.WaitForPage();
    }

    [When("I type '(.*)' in the search bar and click it")]
    public async Task WhenITypeInTheSearchBarAndClickIt(string searchText)
    {
        await _searchOrganizationPage.TypeInSearchSearchBar(searchText);
        await _searchOrganizationPage.ClickOnSuggestion(searchText);
    }

    [When("I click continue")]
    public async Task WhenIClickContinue()
    {
        await _searchOrganizationPage.ClickContinueBtn();
    }
}