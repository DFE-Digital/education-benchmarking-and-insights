using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Hooks;

[Binding]
public class PageHook
{
    private IPage? _page;
    private IBrowser? _browser;

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
    }
    
    [AfterScenario]
    public async Task ClosePage()
    {
        if (_browser != null)
        {
            _page = null;
            await _browser.CloseAsync(new BrowserCloseOptions {Reason = "End of e2e test scenario"});
        }
    }
}