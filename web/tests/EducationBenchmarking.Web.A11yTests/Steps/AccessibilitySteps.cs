using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
using EducationBenchmarking.Web.A11yTests.Hooks;
using EducationBenchmarking.Web.A11yTests.TestSupport;
using Microsoft.Playwright;
using TechTalk.SpecFlow.Infrastructure;
using Xunit;

namespace EducationBenchmarking.Web.A11yTests.Steps;

[Binding]
public sealed class AccessibilitySteps
{
    private readonly IPage _page;
    private AxeResult? _axeResult;
    private readonly ISpecFlowOutputHelper _output;

    private AxeResult Result => _axeResult ?? throw new ArgumentNullException(nameof(_axeResult));

    public AccessibilitySteps(PageHook page, ISpecFlowOutputHelper output)
    {
        _page = page.Current;
        _output = output;
    }

    [Given("I am on the Service Landing Page")]
    public async Task GivenIAmOnTheServiceLandingPage()
    {
        await _page.GotoAsync($"{Config.BaseUrl}");
    }

    [Given("I am on the Choose your School Page")]
    public async Task GivenIAmOnTheChooseYourSchoolPage()
    {
        await _page.GotoAsync($"{Config.BaseUrl}/choose-school");
    }

    [Given("I am on the school '(.*)' Home Page")]
    public async Task GivenIAmOnTheSchoolHomePage(string urn)
    {
        await _page.GotoAsync($"{Config.BaseUrl}/school/{urn}");
    }

    [When("I check the accessibility of the page")]
    public async Task WhenICheckTheAccessibilityOfThePage()
    {
        _axeResult = await _page.RunAxe();
    }

    [Then("there are no accessibility issues")]
    public Task ThenThereAreNoAccessibilityIssues()
    {
        var seriousOrCriticalViolations = Result.Violations
            .Where(violation => violation.Impact is "serious" or "critical")
            .ToList();

        _output.WriteLine($"There are {seriousOrCriticalViolations.Count} serious and critical issues on this page");
        
        PrintViolations(Result.Violations, "Critical", "critical");
        PrintViolations(Result.Violations, "Serious", "serious");

        Assert.True(seriousOrCriticalViolations.Count == 0, "There are violations on the page");
        return Task.CompletedTask;
    }

    private void PrintViolations(IEnumerable<AxeResultItem> violations, string category, string impact)
    {
        var categoryViolations = violations
            .Where(violation => violation.Impact == impact)
            .ToList();

        _output.WriteLine($"{category} issues: {categoryViolations.Count}");

        for (var i = 0; i < categoryViolations.Count; i++)
        {
            var violation = categoryViolations[i];
            _output.WriteLine($"Issue {i + 1}: {violation.Description}");
            for (var j = 0; j < violation.Nodes.Length; j++)
            {
                var node = violation.Nodes[j];
                _output.WriteLine($"  Occurrence {j + 1}: {node.Html}");
            }
        }
    }

    [Given("I am on compare your costs page for school '(.*)'")]
    public async Task GivenIAmOnCompareYourCostsPageForSchool(string urn)
    {
        await _page.GotoAsync($"{Config.BaseUrl}/school/{urn}/comparison");
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }


    [Given("I am on school workforce page for school '(.*)'")]
    public async Task GivenIAmOnSchoolWorkforcePageForSchool(string urn)
    {
        await _page.GotoAsync($"{Config.BaseUrl}/school/{urn}/workforce");
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Given("I click on view as table")]
    public async Task GivenIClickOnViewAsTable()
    {
        await _page.Locator(".govuk-button:has-text('View as table')").ClickAsync();
    }
}