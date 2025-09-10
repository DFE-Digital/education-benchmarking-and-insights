using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;
using HomePage = Web.E2ETests.Pages.School.HomePage;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School search page")]
public class SearchPageSteps(PageDriver driver)
{
    private HomePage? _schoolHomePage;
    private SearchPage? _schoolSearchPage;
    private SearchResultsPage? _schoolSearchResultsPage;

    [Given(@"I am on school search page")]
    public async Task GivenIAmOnSchoolSearchPage()
    {
        var url = SchoolSearchUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _schoolSearchPage = new SearchPage(page);
        await _schoolSearchPage.IsDisplayed();
    }

    [When("I press the enter key after selecting a result")]
    public async Task WhenIPressTheEnterKeyAfterSelectingAResult()
    {
        Assert.NotNull(_schoolSearchPage);
        await _schoolSearchPage.SelectItemFromSuggesterWithKeyboard();
        _schoolHomePage = await _schoolSearchPage.PressEnterKey(p => new HomePage(p));
    }

    [When("I press the enter key without selecting a result")]
    public async Task WhenIPressTheEnterKeyWithoutSelectingAResult()
    {
        Assert.NotNull(_schoolSearchPage);
        _schoolSearchResultsPage = await _schoolSearchPage.PressEnterKey(p => new SearchResultsPage(p));
    }

    [When("I type '(.*)' into the search bar")]
    public async Task WhenITypeIntoTheSearchBar(string keyword)
    {
        Assert.NotNull(_schoolSearchPage);
        await _schoolSearchPage.TypeIntoSearchBox(keyword);
    }

    [When("I click Search after selecting a result")]
    public async Task WhenIClickSearchAfterSelectingAResult()
    {
        Assert.NotNull(_schoolSearchPage);
        await _schoolSearchPage.SelectItemFromSuggesterWithMouse();
        _schoolHomePage = await _schoolSearchPage.ClickSearch(p => new HomePage(p));
    }

    [When("I click Search without selecting a result")]
    public async Task WhenIClickSearchWithoutSelectingAResult()
    {
        Assert.NotNull(_schoolSearchPage);
        _schoolSearchResultsPage = await _schoolSearchPage.ClickSearch(p => new SearchResultsPage(p));
    }

    [Then("the school homepage is displayed")]
    public async Task ThenTheSchoolHomepageIsDisplayed()
    {
        Assert.NotNull(_schoolHomePage);
        await _schoolHomePage.IsDisplayed();
    }

    [Then("each suggester result contains '(.*)'")]
    public async Task ThenEachSuggesterResultContains(string keyword)
    {
        Assert.NotNull(_schoolSearchPage);
        await _schoolSearchPage.AssertSearchResults(keyword);
    }

    [Then("the school search results page is displayed")]
    public async Task ThenTheSchoolSearchResultsPageIsDisplayed()
    {
        Assert.NotNull(_schoolSearchResultsPage);
        await _schoolSearchResultsPage.IsDisplayed();
    }

    private static string SchoolSearchUrl() => $"{TestConfiguration.ServiceUrl}/school";
}