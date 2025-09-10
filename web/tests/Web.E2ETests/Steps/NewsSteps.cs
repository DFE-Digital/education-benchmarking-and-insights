using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Xunit;

namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "News")]
public class NewsSteps(PageDriver driver)
{
    private HomePage? _homePage;
    private NewsArticlePage? _newsArticlePage;
    private NewsPage? _newsPage;

    [Given("I am on home page")]
    public async Task GivenIAmOnHomePage()
    {
        var url = HomePageUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _homePage = new HomePage(page);
        await _homePage.IsDisplayed();
    }

    [Given("I am on news page")]
    public async Task GivenIAmOnNewsPage()
    {
        var url = NewsPageUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _newsPage = new NewsPage(page);
        await _newsPage.IsDisplayed();
    }

    [When("I click on the news link")]
    public async Task WhenIClickOnTheNewsLink()
    {
        Assert.NotNull(_homePage);
        _newsPage = await _homePage.ClickNewsLink();
    }

    [When("I click on the '(.*)' article link")]
    public async Task WhenIClickOnTheArticleLink(string title)
    {
        Assert.NotNull(_newsPage);
        _newsArticlePage = await _newsPage.ClickNewsArticleLink(title);
    }

    [Then("the news page is displayed with '(.*)' articles")]
    public async Task ThenTheNewsPageIsDisplayedWithArticles(int count)
    {
        Assert.NotNull(_newsPage);
        await _newsPage.IsDisplayed();
        await _newsPage.HasArticleCount(count);
    }

    [Then("the news article is displayed with the heading '(.*)'")]
    public async Task ThenTheNewsArticleIsDisplayedWithTheHeading(string heading)
    {
        Assert.NotNull(_newsArticlePage);
        await _newsArticlePage.IsDisplayed();
        await _newsArticlePage.HasHeading(heading);
    }

    private static string HomePageUrl() => $"{TestConfiguration.ServiceUrl}/";

    private static string NewsPageUrl() => $"{TestConfiguration.ServiceUrl}/news";
}