using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Xunit;
namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "Manage cookies")]
public class ManageCookiesSteps(PageDriver driver)
{
    private CookiesPage? _cookiesPage;
    private HomePage? _homePage;

    [Given("I am on home page")]
    public async Task GivenIAmOnHomePage()
    {
        var url = HomePageUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _homePage = new HomePage(page);
        await _homePage.IsDisplayed();
    }

    [Given("I am on cookies page")]
    public async Task GivenIAmOnCookiesPage()
    {
        var url = ManageCookiesUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _cookiesPage = new CookiesPage(page);
        await _cookiesPage.IsDisplayed();
    }

    [When("I have no cookies in context")]
    public async Task WhenIHaveNoCookiesInContext()
    {
        var page = await driver.Current;
        await page.Context.ClearCookiesAsync(new BrowserContextClearCookiesOptions
        {
            Path = "/"
        });
        await page.ReloadAsync();
    }

    [When("I click to '(.*)' cookies")]
    public async Task WhenIClickToCookies(string accept)
    {
        if (_homePage != null)
        {
            await _homePage.ClickCookieBannerButton(accept);
        }

        if (_cookiesPage != null)
        {
            await _cookiesPage.ClickCookieBannerButton(accept);
        }
    }

    [When("I click to '(.*)' cookies using the form")]
    public async Task WhenIClickToCookiesUsingTheForm(string accept)
    {
        Assert.NotNull(_cookiesPage);
        await _cookiesPage.ClickCookieFormRadio(accept);
        await _cookiesPage.ClickCookieFormSubmit();
    }

    [Then("the cookie banner is displayed")]
    public async Task ThenTheCookieBannerIsDisplayed()
    {
        if (_homePage != null)
        {
            await _homePage.CookieBannerIsDisplayed();
        }

        if (_cookiesPage != null)
        {
            await _cookiesPage.CookieBannerIsDisplayed();
        }
    }

    [Then("the cookie banner is dismissed with the '(.*)' message")]
    public async Task ThenTheCookieBannerIsDismissedWithTheMessage(string accept)
    {
        Assert.NotNull(_homePage);
        await _homePage.CookieBannerIsDismissed(accept);
    }

    [Then("the cookies saved banner is displayed")]
    public async Task ThenTheCookiesSavedBannerIsDisplayed()
    {
        Assert.NotNull(_cookiesPage);
        await _cookiesPage.CookiesSavedBannerIsDisplayed();
    }

    private static string HomePageUrl() => $"{TestConfiguration.ServiceUrl}/";
    private static string ManageCookiesUrl() => $"{TestConfiguration.ServiceUrl}/cookies";
}