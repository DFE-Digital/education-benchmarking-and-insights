using Microsoft.Playwright;
using TechTalk.SpecFlow.Infrastructure;

namespace Web.E2ETests.Drivers;

public class PageDriver : IDisposable
{
    private readonly Lazy<Task<IPage>> _current;
    private readonly ISpecFlowOutputHelper _output;

    private bool _isDisposed;
    private IBrowser? _browser;

    public PageDriver(ISpecFlowOutputHelper output)
    {
        _current = new Lazy<Task<IPage>>(CreatePageDriver);
        _output = output;
    }

    public Task<IPage> Current => _current.Value;

    public async void Dispose()
    {
        await Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual async Task Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (_current.IsValueCreated)
            {
                if (_browser != null)
                {
                    await _browser.CloseAsync();
                }
            }

            _isDisposed = true;
        }
    }

    private async Task<IPage> CreatePageDriver()
    {
        _browser ??= await InitialiseBrowser();

        var contextOptions = new BrowserNewContextOptions { IgnoreHTTPSErrors = true };
        var browserContext = await _browser.NewContextAsync(contextOptions);

        var page = await browserContext.NewPageAsync();
        if (TestConfiguration.OutputPageResponse)
        {
            page.Response += (_, r) => _output.WriteLine($"{r.Request.Method} {r.Url} [{r.Status}]");
        }

        return page;
    }

    private static async Task<IBrowser> InitialiseBrowser()
    {
        var playwrightInstance = await Playwright.CreateAsync();
        var launchOptions = new BrowserTypeLaunchOptions { Headless = TestConfiguration.Headless };
        return await playwrightInstance.Chromium.LaunchAsync(launchOptions);
    }
}