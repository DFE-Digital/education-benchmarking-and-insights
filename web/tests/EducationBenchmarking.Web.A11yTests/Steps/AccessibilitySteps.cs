using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
using EducationBenchmarking.Web.A11yTests.TestSupport;
using Microsoft.Playwright;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests.Steps;

[Binding]
public sealed class AccessibilitySteps
{
    private readonly IPage _page;
    private AxeResult? _axeResults;
    private readonly ITestOutputHelper _output;

    public AccessibilitySteps(ScenarioContext scenarioContext, IPage page, ITestOutputHelper output)
    {
        _page = page;
        _output = output;
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
        
        _output.WriteLine($"There are {seriousOrCriticalViolations.Count()} serious and critical issues on this page");
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

        _output.WriteLine($"{category} issues: {categoryViolations.Count}");

        for (int i = 0; i < categoryViolations.Count; i++)
        {
            var violation = categoryViolations[i];
            _output.WriteLine($"Issue {i + 1}: {violation.Description}");
            for (int j = 0; j < violation.Nodes.Count(); j++)
            {
                var node = violation.Nodes[j];
                _output.WriteLine($"  Occurrence {j + 1}: {node.Html}");
            }
        }
    }

    [Given(@"I am on compare your costs page for school '(.*)'")]
    public async Task GivenIAmOnCompareYourCostsPageForSchool(string urn)
    {
        await _page.GotoAsync($"{Config.BaseUrl}/school/{urn}/comparison");
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }
    

    [Given(@"I am on school workforce page for school '(.*)'")]
    public async Task GivenIAmOnSchoolWorkforcePageForSchool(string urn)
    {
        await _page.GotoAsync($"{Config.BaseUrl}/school/{urn}/workforce");
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
    }

    [Given(@"I click on view as table")]
    public async Task GivenIClickOnViewAsTable()
    {
        await _page.Locator(".govuk-button:has-text('View as table')").ClickAsync();

    }
}