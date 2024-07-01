using Microsoft.Playwright;
using TechTalk.SpecFlow.Infrastructure;

namespace Web.E2ETests.Drivers;

public class PageDriver : IDisposable
{
    private readonly Lazy<Task<IPage>> _current;
    private readonly ISpecFlowOutputHelper _output;
    private readonly HashSet<string> _pendingRequests;

    private bool _isDisposed;
    private IBrowser? _browser;
    private IPlaywright? _playwright;

    public PageDriver(ISpecFlowOutputHelper output)
    {
        _current = new Lazy<Task<IPage>>(CreatePageDriver);
        _output = output;

        _pendingRequests = new HashSet<string>();
    }

    public Task<IPage> Current => _current.Value;

    public async void Dispose()
    {
        await Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task WaitForPendingRequests(int millisecondsDelay = 100)
    {
        while (_pendingRequests.Count > 0)
        {
            await Task.Delay(millisecondsDelay);
        }
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
                    await _browser.DisposeAsync();
                }

                _playwright?.Dispose();
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
            page.RequestFinished += (_, e) => _output.WriteLine($"{e.Method} {e.Url} [{e.Timing.ResponseEnd} ms]");
            page.Response += (_, r) => _output.WriteLine($"{r.Request.Method} {r.Url} [{r.Status}]");
        }

        page.Request += (_, e) => _pendingRequests.Add(e.Url);
        page.RequestFinished += (_, e) => _pendingRequests.Remove(e.Url);
        page.RequestFailed += (_, e) => _pendingRequests.Remove(e.Url);

        return page;
    }

    private async Task<IBrowser> InitialiseBrowser()
    {
        _playwright ??= await InitialisePlaywright();
        var launchOptions = new BrowserTypeLaunchOptions { Headless = TestConfiguration.Headless };
        return await _playwright.Chromium.LaunchAsync(launchOptions);
    }

    private async Task<IPlaywright> InitialisePlaywright()
    {
        return await Playwright.CreateAsync();
    }
}