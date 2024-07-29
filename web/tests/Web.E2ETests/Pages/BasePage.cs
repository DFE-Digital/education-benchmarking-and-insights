using Microsoft.Playwright;
namespace Web.E2ETests.Pages;

public abstract class BasePage(IPage page)
{
    protected IPage Page => page;

    protected ILocator PageH1Heading => Page.Locator($"main {Selectors.H1}");

    private ILocator CookieBanner => Page.Locator(Selectors.CookieBanner);
    private ILocator CookieFormButton => Page.Locator(Selectors.CookieFormButton);
    private ILocator CookieBannerButton(string accept) => Page.Locator(string.Format(Selectors.CookieBannerButtonFormat, accept));
    private ILocator CookieBannerDismissed(string accept) => Page.Locator(string.Format(Selectors.CookieBannerDismissedFormat, accept));
    private ILocator CookieFormRadio(string accept) => Page.Locator(string.Format(Selectors.CookieFormRadioFormat, accept));

    public abstract Task IsDisplayed();

    public async Task CookieBannerIsDisplayed()
    {
        await CookieBanner.ShouldBeVisible();
    }

    public async Task CookieBannerIsNotDisplayed()
    {
        await CookieBanner.ShouldNotBeVisible();
    }

    public async Task CookieBannerIsDismissed(string accept)
    {
        await CookieBannerDismissed(accept).ShouldBeVisible();
    }

    public async Task ClickCookieBannerButton(string accept)
    {
        await CookieBannerButton(accept).ClickAsync();
    }

    public async Task ClickCookieFormRadio(string accept)
    {
        await CookieFormRadio(accept).ClickAsync();
    }

    public async Task ClickCookieFormSubmit()
    {
        await CookieFormButton.ClickAsync();
    }

    public async Task<bool> HasCookies()
    {
        return (await page.Context.CookiesAsync()).Any();
    }
}