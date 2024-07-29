using Microsoft.Playwright;
namespace Web.E2ETests.Pages;

public class CookiesPage(IPage page) : BasePage(page)
{
    private ILocator TrackingCookieRadios => Page.Locator(Selectors.GovRadios);
    private ILocator SaveCookieSettingsButton =>
        Page.Locator(Selectors.GovButton, new PageLocatorOptions
        {
            HasText = "Save cookie settings"
        });
    private ILocator CookiesSavedBanner => Page.Locator(Selectors.CookiesSavedBanner);

    public override async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await TrackingCookieRadios.ShouldBeVisible();
        await SaveCookieSettingsButton.ShouldBeVisible().ShouldBeEnabled();
    }

    public async Task CookiesSavedBannerIsDisplayed()
    {
        await CookiesSavedBanner.ShouldBeVisible();
    }
}