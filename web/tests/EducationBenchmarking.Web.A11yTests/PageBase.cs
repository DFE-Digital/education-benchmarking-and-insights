using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
using Microsoft.Playwright;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests;

public abstract class PageBase : IDisposable
{
    protected PageBase(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
        Driver = new WebDriver(TestOutputHelper);
    }

    protected abstract string PageUrl { get; }
    protected IPage? Page { get; set; }
    protected WebDriver Driver { get; }
    protected ITestOutputHelper TestOutputHelper { get; }

    public void Dispose()
    {
        Driver.Dispose();
    }

    protected async Task EvaluatePage(AxeRunContext? context = null)
    {
        Assert.NotNull(Page);
        Assert.Equal(PageUrl, Page.Url);

        var results = context != null ? await Page.RunAxe(context) : await Page.RunAxe();
        var violations = results.Violations
            .Where(violation => TestConfiguration.Impacts.Contains(violation.Impact))
            .ToArray();

        PrintViolations(violations);

        Assert.True(violations.Length == 0, "There are violations on the page");
    }

    private void PrintViolations(IReadOnlyList<AxeResultItem> violations)
    {
        if (violations.Any())
        {
            foreach (var impact in TestConfiguration.Impacts)
            {
                TestOutputHelper.WriteLine($"{impact} issues: {violations.Count(x => x.Impact == impact)}");
            }

            for (var i = 0; i < violations.Count; i++)
            {
                var violation = violations[i];
                TestOutputHelper.WriteLine($"Issue {i + 1}: {violation.Description}");
                for (var j = 0; j < violation.Nodes.Length; j++)
                {
                    var node = violation.Nodes[j];
                    TestOutputHelper.WriteLine($"  Occurrence {j + 1}: {node.Html}");
                }
            }
        }
    }
}