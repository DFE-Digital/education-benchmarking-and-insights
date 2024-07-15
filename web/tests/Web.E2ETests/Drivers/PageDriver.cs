using System.Collections.Concurrent;
using Microsoft.Playwright;
using TechTalk.SpecFlow.Infrastructure;
namespace Web.E2ETests.Drivers;

public class PageDriver : IDisposable
{
    private readonly Lazy<Task<IPage>> _current;
    private readonly ISpecFlowOutputHelper _output;
    private readonly ConcurrentDictionary<string, byte> _pendingRequests;

    protected readonly BrowserNewContextOptions ContextOptions = new()
    {
        IgnoreHTTPSErrors = true
    };
    private IBrowser? _browser;

    private bool _isDisposed;
    private IPlaywright? _playwright;

    protected PageDriver(ISpecFlowOutputHelper output)
    {
        _current = new Lazy<Task<IPage>>(CreatePageDriver);
        _output = output;

        _pendingRequests = new ConcurrentDictionary<string, byte>();
    }

    public Task<IPage> Current => _current.Value;

    public async void Dispose()
    {
        await Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task WaitForPendingRequests(int millisecondsDelay = 100)
    {
        await Task.Delay(millisecondsDelay);

        while (!_pendingRequests.IsEmpty)
        {
            _output.WriteLine($"Awaiting for pending requests. Count : {_pendingRequests.Count}");
            await Task.Delay(100);
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

        var browserContext = await _browser.NewContextAsync(ContextOptions);

        var page = await browserContext.NewPageAsync();
        if (TestConfiguration.OutputPageResponse)
        {
            page.RequestFinished += (_, e) => _output.WriteLine($"{e.Method} {e.Url} [{e.Timing.ResponseEnd} ms]");
            page.Response += (_, r) => _output.WriteLine($"{r.Request.Method} {r.Url} [{r.Status}]");
        }

        page.Request += (_, e) => _pendingRequests.TryAdd(e.Url, byte.MinValue);
        page.RequestFinished += (_, e) => _pendingRequests.TryRemove(e.Url, out var _);
        page.RequestFailed += (_, e) => _pendingRequests.TryRemove(e.Url, out var _);

        return page;
    }

    private async Task<IBrowser> InitialiseBrowser()
    {
        _playwright ??= await InitialisePlaywright();
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = TestConfiguration.Headless
        };
        return await _playwright.Chromium.LaunchAsync(launchOptions);
    }

    private async Task<IPlaywright> InitialisePlaywright() => await Playwright.CreateAsync();
}

public class PageDriverWithJavaScriptDisabled : PageDriver
{
    public PageDriverWithJavaScriptDisabled(ISpecFlowOutputHelper output) : base(output)
    {
        ContextOptions.JavaScriptEnabled = false;
    }
}