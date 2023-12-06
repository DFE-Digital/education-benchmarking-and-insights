using EducationBenchmarking.Web.A11yTests.Drivers;
using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
//using EducationBenchmarking.Web.A11yTests.Pages;
using Microsoft.Playwright;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EducationBenchmarking.Web.A11yTests.Steps;

[Binding]
public sealed class AccessibilitySteps
{
    private readonly Driver _driver;
    private readonly ScenarioContext _scenarioContext;
    private IPage page;
    private AxeResult? _axeResults;
    

    public AccessibilitySteps(Driver driver, ScenarioContext scenarioContext)
    {
        _driver = driver;
        _scenarioContext = scenarioContext;
        //  _schoolSearchPage = new SchoolSearchPage(_driver.Page);
    }
    

    [Given(@"I open the page with the url (.*)")]
    public async Task GivenIOpenThePageWithTheUrl(string url)
    {
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = false // Set to true for headless mode
        };
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync(launchOptions);
        var context = await browser.NewContextAsync();
        page = await context.NewPageAsync();
        
        await page.GotoAsync("https://hippodigital.co.uk/");
        
        _axeResults = await page.RunAxe();
        var seriousOrCriticalViolations = _axeResults.Violations
            .Where(violation => violation.Impact == "serious" || violation.Impact == "critical")
            .ToList();

        // Print the number of violations
        Console.WriteLine($"There are {seriousOrCriticalViolations.Count()} serious and critical issues on this page");

        // Categorize and print details for each violation
        PrintViolations(_axeResults.Violations, "Critical", "critical");
        PrintViolations(_axeResults.Violations, "Serious", "serious");
        Assert.That(seriousOrCriticalViolations, Is.Null.Or.Empty, "There are violations on the page");
         
        //  await browser.CloseAsync();
    }
    

    [When(@"I check the accessibility of the page")]
    public async Task WhenICheckTheAccessibilityOfThePage()
    {
       
    }

    [Then(@"there are no accessibility issues")]
    public async Task ThenThereAreNoAccessibilityIssues()
    {
       
    }
    private void PrintViolations(AxeResultItem[] violations, string category, string impact)
    {
        var categoryViolations = violations
            .Where(violation => violation.Impact == impact)
            .ToList();

        Console.WriteLine($"{category} issues: {categoryViolations.Count}");

        for (int i = 0; i < categoryViolations.Count; i++)
        {
            var violation = categoryViolations[i];
            Console.WriteLine($"Issue {i + 1}: {violation.Description}");
            // Print details for each node of the violation
            for (int j = 0; j < violation.Nodes.Count(); j++)
            {
                var node = violation.Nodes[j];
                Console.WriteLine($"  Occurrence {j + 1}: {node.Html}");
            }
        }
    }
}
