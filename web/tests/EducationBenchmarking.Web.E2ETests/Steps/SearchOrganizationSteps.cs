using EducationBenchmarking.Web.E2ETests.Pages;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class SearchOrganizationSteps(SearchOrganizationPage searchOrganizationPage)
{
    [Then("I am on find organization page")]
    public async Task ThenIAmOnFindOrganizationPage()
    {
        await searchOrganizationPage.WaitForPage();
    }

    [When("I type '(.*)' in the search bar and click it")]
    public async Task WhenITypeInTheSearchBarAndClickIt(string searchText)
    {
        await searchOrganizationPage.TypeInSearchSearchBar(searchText);
        await searchOrganizationPage.ClickOnSuggestion(searchText);
    }

    [When("I click continue")]
    public async Task WhenIClickContinue()
    {
        await searchOrganizationPage.ClickContinueBtn();
    }
}