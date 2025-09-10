using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;
using HomePage = Web.E2ETests.Pages.LocalAuthority.HomePage;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local authority search page")]
public class SearchPageSteps(PageDriver driver)
{
    private HomePage? _localAuthorityHomePage;
    private SearchPage? _localAuthoritySearchPage;
    private SearchResultsPage? _localAuthoritySearchResultsPage;

    [Given(@"I am on local authority search page")]
    public async Task GivenIAmOnLocalAuthoritySearchPage()
    {
        var url = LocalAuthoritySearchUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _localAuthoritySearchPage = new SearchPage(page);
        await _localAuthoritySearchPage.IsDisplayed();
    }

    [When("I press the enter key after selecting a result")]
    public async Task WhenIPressTheEnterKeyAfterSelectingAResult()
    {
        Assert.NotNull(_localAuthoritySearchPage);
        await _localAuthoritySearchPage.SelectItemFromSuggesterWithKeyboard();
        _localAuthorityHomePage = await _localAuthoritySearchPage.PressEnterKey(p => new HomePage(p));
    }

    [When("I press the enter key without selecting a result")]
    public async Task WhenIPressTheEnterKeyWithoutSelectingAResult()
    {
        Assert.NotNull(_localAuthoritySearchPage);
        _localAuthoritySearchResultsPage = await _localAuthoritySearchPage.PressEnterKey(p => new SearchResultsPage(p));
    }

    [When("I type '(.*)' into the search bar")]
    public async Task WhenITypeIntoTheSearchBar(string keyword)
    {
        Assert.NotNull(_localAuthoritySearchPage);
        await _localAuthoritySearchPage.TypeIntoSearchBox(keyword);
    }

    [When("I click Search after selecting a result")]
    public async Task WhenIClickSearchAfterSelectingAResult()
    {
        Assert.NotNull(_localAuthoritySearchPage);
        await _localAuthoritySearchPage.SelectItemFromSuggesterWithMouse();
        _localAuthorityHomePage = await _localAuthoritySearchPage.ClickSearch(p => new HomePage(p));
    }

    [When("I click Search without selecting a result")]
    public async Task WhenIClickSearchWithoutSelectingAResult()
    {
        Assert.NotNull(_localAuthoritySearchPage);
        _localAuthoritySearchResultsPage = await _localAuthoritySearchPage.ClickSearch(p => new SearchResultsPage(p));
    }

    [Then("the local authority homepage is displayed")]
    public async Task ThenTheLocalAuthorityHomepageIsDisplayed()
    {
        Assert.NotNull(_localAuthorityHomePage);
        await _localAuthorityHomePage.IsDisplayed();
    }

    [Then("each suggester result contains '(.*)'")]
    public async Task ThenEachSuggesterResultContains(string keyword)
    {
        Assert.NotNull(_localAuthoritySearchPage);
        await _localAuthoritySearchPage.AssertSearchResults(keyword);
    }

    [Then("the local authority search results page is displayed")]
    public async Task ThenTheLocalAuthoritySearchResultsPageIsDisplayed()
    {
        Assert.NotNull(_localAuthoritySearchResultsPage);
        await _localAuthoritySearchResultsPage.IsDisplayed();
    }

    private static string LocalAuthoritySearchUrl() => $"{TestConfiguration.ServiceUrl}/local-authority";
}