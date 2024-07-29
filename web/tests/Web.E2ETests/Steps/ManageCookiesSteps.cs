using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Xunit;
namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "Manage cookies")]
public class ManageCookiesSteps(PageDriver driver, PageDriverWithJavaScriptDisabled driverNoJs)
{
    private CookiesPage? _cookiesPage;
    private HomePage? _homePage;
    private bool _javascriptDisabled;

    [Given("JavaScript is '(.*)'")]
    public void GivenJavaScriptIs(string enabled)
    {
        _javascriptDisabled = enabled == "disabled";
    }

    [Given("I am on home page")]
    public async Task GivenIAmOnHomePage()
    {
        var url = HomePageUrl();
        var page = await (_javascriptDisabled ? driverNoJs : driver).Current;
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

    [Then("I have no cookies in context")]
    public async Task ThenIHaveNoCookiesInContext()
    {
        Assert.NotNull(_homePage);
        Assert.False(await _homePage.HasCookies());
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

    [Then("the cookie banner is not displayed")]
    public async Task ThenTheCookieBannerIsNotDisplayed()
    {
        Assert.NotNull(_homePage);
        await _homePage.CookieBannerIsNotDisplayed();
    }

    [Then("the cookie banner is dismissed with the '(.*)' message")]
    public async Task ThenTheCookieBannerIsDismissedWithTheMessage(string accept)
    {
        if (_homePage != null)
        {
            await _homePage.CookieBannerIsDismissed(accept);
        }

        if (_cookiesPage != null)
        {
            await _cookiesPage.CookieBannerIsDismissed(accept);
        }
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