using Microsoft.Playwright;

namespace Web.E2ETests;

public static class PageExtensions
{
    public static ILocator SignInLink(this IPage page) => page.Locator(Selectors.SignInLink);
    public static ILocator SignOutLink(this IPage page) => page.Locator(Selectors.SignOutLink);
    public static async Task<bool> IsSignedIn(this IPage page) => !await page.SignInLink().IsVisibleAsync() && await page.SignOutLink().IsVisibleAsync();


    public static async Task<IResponse?> GotoAndWaitForLoadAsync(this IPage page, string url)
    {
        var response = await page.GotoAsync(url);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return response;
    }

    public static Task<IDownload> WaitForDownloadAsync(this IPage page, TimeSpan timeout) => page.WaitForDownloadAsync(new PageWaitForDownloadOptions
    {
        Timeout = Convert.ToSingle(timeout.TotalMilliseconds)
    });
}