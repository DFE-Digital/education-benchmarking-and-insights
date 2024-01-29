using Microsoft.Playwright;

namespace EducationBenchmarking.Web.A11yTests;

public class WebDriver: IDisposable
{
    private IBrowser? _browser;

    public async Task<IPage> GetPage(string url)
    {
        var playwrightInstance = await Playwright.CreateAsync();

        var launchOptions = new BrowserTypeLaunchOptions { Headless = TestConfiguration.Headless };
        _browser = await playwrightInstance.Chromium.LaunchAsync(launchOptions);

        var contextOptions = new BrowserNewContextOptions { IgnoreHTTPSErrors = true };
        var browserContext = await _browser.NewContextAsync(contextOptions);

        var page = await browserContext.NewPageAsync();
        await page.GotoAsync(url);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return page;
    }
    
    public async void Dispose()
    {
        if (_browser != null) await _browser.DisposeAsync();
    }
}