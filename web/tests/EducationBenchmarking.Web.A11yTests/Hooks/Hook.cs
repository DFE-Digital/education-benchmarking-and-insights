using BoDi;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
namespace EducationBenchmarking.Web.A11yTests.Hooks;

    [Binding]
    public class Hooks
    {
        private IBrowserContext BrowserContext { get; set; } = null!;
        private IPlaywright PlaywrightInstance { get; set; } = null!;
        private readonly IObjectContainer _objectContainer;

        public Hooks(ScenarioContext scenarioContext, IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }


        [BeforeTestRun]
        public static async void InstallBrowsers()
        {
            var exitCode = Program.Main(new[] {"install", "chromium"});
            if (exitCode != 0)
            {
                throw new Exception($"Playwright exited with code {exitCode}");
            }
        }

        [BeforeScenario]
        public async Task RegisterInstance()
        {
            PlaywrightInstance = await Playwright.CreateAsync();
            var browser = await PlaywrightInstance.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
                });
            BrowserContext = await browser.NewContextAsync(
                new BrowserNewContextOptions
                {
                    IgnoreHTTPSErrors = true
                });
            var page = await BrowserContext.NewPageAsync();
            _objectContainer.RegisterInstanceAs(browser);
            _objectContainer.RegisterInstanceAs(page);
            
        }
        
        [AfterScenario]
        public async Task AfterScenario()
        {
            PlaywrightInstance.Dispose();
        }
    }
    