using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;
using TechTalk.SpecFlow.Infrastructure;

namespace EducationBenchmarking.Web.E2ETests.Hooks;

[Binding]
public class PageHook(ISpecFlowOutputHelper output)
{
    private IBrowser? _browser;
    private IPage? _page;

    public IPage Current => _page ?? throw new ArgumentNullException(nameof(_page));

    [BeforeScenario]
    public async Task CreatePageInstance()
    {
        var playwrightInstance = await Playwright.CreateAsync();

        var launchOptions = new BrowserTypeLaunchOptions { Headless = Config.Headless };
        _browser = await playwrightInstance.Chromium.LaunchAsync(launchOptions);

        var contextOptions = new BrowserNewContextOptions { IgnoreHTTPSErrors = true };
        var browserContext = await _browser.NewContextAsync(contextOptions);

        _page = await browserContext.NewPageAsync();
#if DEBUG
        _page.Response += (sender, r) => output.WriteLine($"{r.Request.Method} {r.Url} [{r.Status}]");
#endif
    }

    [AfterScenario]
    public async Task ClosePage()
    {
        if (_browser != null)
        {
            _page = null;
            await _browser.CloseAsync(new BrowserCloseOptions { Reason = "End of e2e test scenario" });
        }
    }

    public void WriteOutputLine(string format, params object[] args)
    {
        output.WriteLine(format, args);
    }

    public void WriteOutputLine(string message)
    {
        output.WriteLine(message);
    }
}