using System.Net;
using Microsoft.Playwright;
using Xunit;

namespace EducationBenchmarking.Web.A11yTests;

public class WebDriver: IDisposable
{
    private IBrowser? _browser;

    public async Task<IPage> GetPage(string url, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var playwrightInstance = await Playwright.CreateAsync();

        var launchOptions = new BrowserTypeLaunchOptions { Headless = TestConfiguration.Headless };
        _browser = await playwrightInstance.Chromium.LaunchAsync(launchOptions);

        var contextOptions = new BrowserNewContextOptions { IgnoreHTTPSErrors = true };
        var browserContext = await _browser.NewContextAsync(contextOptions);

        var page = await browserContext.NewPageAsync();
        var response = await page.GotoAsync(url);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        Assert.Equal((int)statusCode, response?.Status);
        
        return page;
    }
    
    public async void Dispose()
    {
        if (_browser != null) await _browser.DisposeAsync();
    }
}