using System.Net;
using Microsoft.Playwright;
using Xunit;
using Xunit.Abstractions;
namespace Web.A11yTests.Drivers;

public class WebDriver(IMessageSink messageSink) : IDisposable
{
    private IBrowser? _browser;

    public async void Dispose()
    {
        await Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<IPage> GetPage(string url, HttpStatusCode statusCode, IPage? basePage = null)
    {
        _browser ??= await InitialiseBrowser();

        var contextOptions = new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true
        };
        var browserContext = await _browser.NewContextAsync(contextOptions);

        IPage page;
        if (basePage != null)
        {
            page = basePage;
        }
        else
        {
            page = await browserContext.NewPageAsync();
            page.Response += (_, r) => messageSink.OnMessage(r.ToDiagnosticMessage());
        }

        var response = await page.GotoAsync(url);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        Assert.Equal((int)statusCode, response?.Status);

        return page;
    }

    private static async Task<IBrowser> InitialiseBrowser()
    {
        var playwrightInstance = await Playwright.CreateAsync();
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = TestConfiguration.Headless
        };
        return await playwrightInstance.Chromium.LaunchAsync(launchOptions);
    }

    protected virtual async Task Dispose(bool disposing)
    {
        if (disposing)
        {
            messageSink.OnMessage("Disposing of browser instance".ToDiagnosticMessage());
            if (_browser != null)
            {
                await _browser.DisposeAsync();
            }
        }
    }
}