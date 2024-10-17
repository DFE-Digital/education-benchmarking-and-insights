using Microsoft.Playwright;
namespace Web.E2ETests;

public static class PageExtensions
{
    private static ILocator SignInLink(this IPage page) => page.Locator(Selectors.SignInLink);
    private static ILocator SignOutLink(this IPage page) => page.Locator(Selectors.SignOutLink);

    public static async Task GotoAndWaitForLoadAsync(this IPage page, string url)
    {
        await page.GotoAsync(url);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public static async Task SignInWithOrganisation(this IPage page, string organisation, bool authGuard = false)
    {
        if (!authGuard)
        {
            if (await page.IsSignedIn())
            {
                return;
            }

            await page.SignInLink().ClickAsync();
        }

        await page.Locator("input[id='username']").Fill(TestConfiguration.LoginEmail);
        await page.Locator("button[type='submit']").Click();
        await page.Locator("h1:text-is('Enter your password')").CheckVisible();
        await page.Locator("input[id='password']").Fill(TestConfiguration.LoginPassword);
        await page.Locator("button[type='submit']").Click();
        await page.Locator("label", new PageLocatorOptions
        {
            HasTextString = organisation
        }).Check();
        await page.Locator("input[type='submit']").Click();

        await page.SignOutLink().IsVisibleAsync();
    }

    public static async Task SignOut(this IPage page)
    {
        if (await page.SignOutLink().IsVisibleAsync())
        {
            await page.SignOutLink().ClickAsync();
            await page.WaitForURLAsync($"{TestConfiguration.ServiceUrl}/");
        }
    }

    private static async Task<bool> IsSignedIn(this IPage page) => !await page.SignInLink().IsVisibleAsync() && await page.SignOutLink().IsVisibleAsync();
}