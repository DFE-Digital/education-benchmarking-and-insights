using System.Net;
using Microsoft.Playwright;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests;

public class WebDriver(ITestOutputHelper testOutputHelper) : IDisposable
{
    private IBrowser? _browser;

    public async void Dispose()
    {
        if (_browser != null)
        {
            await _browser.DisposeAsync();
        }
    }

    public async Task<IPage> GetPage(string url, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var playwrightInstance = await Playwright.CreateAsync();

        var launchOptions = new BrowserTypeLaunchOptions { Headless = TestConfiguration.Headless };
        _browser = await playwrightInstance.Chromium.LaunchAsync(launchOptions);

        var contextOptions = new BrowserNewContextOptions { IgnoreHTTPSErrors = true };
        var browserContext = await _browser.NewContextAsync(contextOptions);

        var page = await browserContext.NewPageAsync();
#if DEBUG
        page.Response += (sender, r) => testOutputHelper.WriteLine($"{r.Request.Method} {r.Url} [{r.Status}]");
#endif

        var response = await page.GotoAsync(url);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        Assert.Equal((int)statusCode, response?.Status);

        return page;
    }
}