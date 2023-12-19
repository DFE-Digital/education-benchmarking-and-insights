using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
using EducationBenchmarking.Web.A11yTests.TestSupport;
using Microsoft.Playwright;
using Xunit;

namespace EducationBenchmarking.Web.A11yTests.Steps;

[Binding]
public sealed class AccessibilitySteps
{
    private readonly IPage _page;
    private AxeResult? _axeResults;

    public AccessibilitySteps(ScenarioContext scenarioContext, IPage page)
    {
        _page = page;
    }

    [Given(@"I am on the Service Landing Page")]
    public async Task GivenIAmOnTheServiceLandingPage()
    {
        await _page.GotoAsync($"{Config.BaseUrl}");
    }

    [Given(@"I am on the Choose your School Page")]
    public async Task GivenIAmOnTheChooseYourSchoolPage()
    {
        await _page.GotoAsync($"{Config.BaseUrl}/choose-school");
    }

    [Given(@"I am on the school ""(.*)"" Home Page")]
    public async Task GivenIAmOnTheSchoolHomePage(string urn)
    {
        await _page.GotoAsync($"{Config.BaseUrl}/school/{urn}");
    }

    [When(@"I check the accessibility of the page")]
    public async Task WhenICheckTheAccessibilityOfThePage()
    {
        _axeResults = await _page.RunAxe();
    }

    [Then(@"there are no accessibility issues")]
    public Task ThenThereAreNoAccessibilityIssues()
    {
        var seriousOrCriticalViolations = _axeResults!.Violations
            .Where(violation => violation.Impact == "serious" || violation.Impact == "critical")
            .ToList();
        Console.WriteLine($"There are {seriousOrCriticalViolations.Count()} serious and critical issues on this page");
        PrintViolations(_axeResults.Violations, "Critical", "critical");
        PrintViolations(_axeResults.Violations, "Serious", "serious");
        Assert.True(seriousOrCriticalViolations.Count == 0, "There are violations on the page");
        return Task.CompletedTask;
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
            for (int j = 0; j < violation.Nodes.Count(); j++)
            {
                var node = violation.Nodes[j];
                Console.WriteLine($"  Occurrence {j + 1}: {node.Html}");
            }
        }
    }
    
}