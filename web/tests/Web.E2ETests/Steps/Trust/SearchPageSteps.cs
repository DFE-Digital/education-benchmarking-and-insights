using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Xunit;
using HomePage = Web.E2ETests.Pages.Trust.HomePage;

namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust search page")]
public class SearchPageSteps(PageDriver driver)
{
    private HomePage? _trustHomePage;
    private SearchPage? _trustSearchPage;
    private SearchResultsPage? _trustSearchResultsPage;

    [Given(@"I am on trust search page")]
    public async Task GivenIAmOnTrustSearchPage()
    {
        var url = TrustSearchUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _trustSearchPage = new SearchPage(page);
        await _trustSearchPage.IsDisplayed();
    }

    [When("I press the enter key after selecting a result")]
    public async Task WhenIPressTheEnterKeyAfterSelectingAResult()
    {
        Assert.NotNull(_trustSearchPage);
        await _trustSearchPage.SelectItemFromSuggesterWithKeyboard();
        _trustHomePage = await _trustSearchPage.PressEnterKey(p => new HomePage(p));
    }

    [When("I press the enter key without selecting a result")]
    public async Task WhenIPressTheEnterKeyWithoutSelectingAResult()
    {
        Assert.NotNull(_trustSearchPage);
        _trustSearchResultsPage = await _trustSearchPage.PressEnterKey(p => new SearchResultsPage(p));
    }

    [When("I type '(.*)' into the search bar")]
    public async Task WhenITypeIntoTheSearchBar(string keyword)
    {
        Assert.NotNull(_trustSearchPage);
        await _trustSearchPage.TypeIntoSearchBox(keyword);
    }

    [When("I click Search after selecting a result")]
    public async Task WhenIClickSearchAfterSelectingAResult()
    {
        Assert.NotNull(_trustSearchPage);
        await _trustSearchPage.SelectItemFromSuggesterWithMouse();
        _trustHomePage = await _trustSearchPage.ClickSearch(p => new HomePage(p));
    }

    [When("I click Search without selecting a result")]
    public async Task WhenIClickSearchWithoutSelectingAResult()
    {
        Assert.NotNull(_trustSearchPage);
        _trustSearchResultsPage = await _trustSearchPage.ClickSearch(p => new SearchResultsPage(p));
    }

    [Then("the trust homepage is displayed")]
    public async Task ThenTheTrustHomepageIsDisplayed()
    {
        Assert.NotNull(_trustHomePage);
        await _trustHomePage.IsDisplayed();
    }

    [Then("each suggester result contains '(.*)'")]
    public async Task ThenEachSuggesterResultContains(string keyword)
    {
        Assert.NotNull(_trustSearchPage);
        await _trustSearchPage.AssertSearchResults(keyword);
    }

    [Then("the trust search results page is displayed")]
    public async Task ThenTheTrustSearchResultsPageIsDisplayed()
    {
        Assert.NotNull(_trustSearchResultsPage);
        await _trustSearchResultsPage.IsDisplayed();
    }

    private static string TrustSearchUrl() => $"{TestConfiguration.ServiceUrl}/trust";
}