using BoDi;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Hooks;

[Binding]
public class Hooks
{
    private readonly IObjectContainer _objectContainer;

    public Hooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeTestRun]
    public static void InstallBrowsers()
    {
        var exitCode = Program.Main(["install", "chromium"]);
        if (exitCode != 0)
        {
            throw new Exception($"Playwright exited with code {exitCode}");
        }
    }

    [BeforeScenario]
    public async Task RegisterInstance()
    {
        var playwrightInstance = await Playwright.CreateAsync();

        var launchOptions = new BrowserTypeLaunchOptions { Headless = false };
        var browser = await playwrightInstance.Chromium.LaunchAsync(launchOptions);

        var contextOptions = new BrowserNewContextOptions { IgnoreHTTPSErrors = true };
        var browserContext = await browser.NewContextAsync(contextOptions);

        var page = await browserContext.NewPageAsync();

        _objectContainer.RegisterInstanceAs(browser);
        _objectContainer.RegisterInstanceAs(page);
    }
}