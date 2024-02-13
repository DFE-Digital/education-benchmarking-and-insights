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

        var launchOptions = new BrowserTypeLaunchOptions { Headless = TestConfiguration.Headless };
        _browser = await playwrightInstance.Chromium.LaunchAsync(launchOptions);

        var contextOptions = new BrowserNewContextOptions { IgnoreHTTPSErrors = true };
        var browserContext = await _browser.NewContextAsync(contextOptions);

        _page = await browserContext.NewPageAsync();
        if (TestConfiguration.OutputPageResponse)
        {
            _page.Response += (_, r) => output.WriteLine($"{r.Request.Method} {r.Url} [{r.Status}]");    
        }
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
}