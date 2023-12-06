using Microsoft.Playwright;
using TechTalk.SpecFlow;
namespace EducationBenchmarking.Web.A11yTests.Hooks;

    [Binding]
    public class Hooks
    {
        private static IBrowser browser;
        private static IPage page;

        [BeforeScenario]
        public async void BeforeScenario()
        {
            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = false // Set to true for headless mode
            };

            using var playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(launchOptions);
            var context = await browser.NewContextAsync();
            page = await context.NewPageAsync();

            // Share the browser and page instances across steps using ScenarioContext
            var scenarioContext = ScenarioContext.Current;
            scenarioContext.Add("Browser", browser);
            scenarioContext.Add("Page", page);
        }

        [AfterScenario]
        public async void AfterScenario()
        {
            // Retrieve the browser and page instances from ScenarioContext
            var scenarioContext = ScenarioContext.Current;
            var browser = (IBrowser)scenarioContext["Browser"];
            var page = (IPage)scenarioContext["Page"];

            // Close the browser
            if (browser != null)
            {
                await browser.CloseAsync();
            }
        }
    }
    