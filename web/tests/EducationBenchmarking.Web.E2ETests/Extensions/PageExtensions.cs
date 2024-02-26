using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests;

public static class PageExtensions
{
    public static async Task GotoAndWaitForLoadAsync(this IPage page, string url)
    {
        await page.GotoAsync(url);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }
}