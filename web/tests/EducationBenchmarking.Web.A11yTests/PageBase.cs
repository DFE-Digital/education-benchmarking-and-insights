using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
using Microsoft.Playwright;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests;

public abstract class PageBase(ITestOutputHelper outputHelper)
{
    protected abstract string PageUrl { get; }
    protected IPage? Page { get; set; }

    protected async Task EvaluatePage()
    {
        Assert.NotNull(Page);
        Assert.Equal( PageUrl, Page.Url);
        
        var results = await Page.RunAxe();
        var seriousOrCriticalViolations = results.Violations
            .Where(violation => violation.Impact is "serious" or "critical")
            .ToList();

        outputHelper.WriteLine($"There are {seriousOrCriticalViolations.Count} serious and critical issues on this page");

        PrintViolations(results.Violations, ["critical",  "serious"]);

        Assert.True(seriousOrCriticalViolations.Count == 0, "There are violations on the page");
    }
    
    private void PrintViolations(IEnumerable<AxeResultItem> violations, string[] categories)
    {
        var categoryViolations = violations
            .Where(violation => categories.Contains(violation.Impact))
            .ToList();

        foreach (var category in categories)
        {
            outputHelper.WriteLine($"{category} issues: {categoryViolations.Count(x => x.Impact == category)}");    
        }
        
        for (var i = 0; i < categoryViolations.Count; i++)
        {
            var violation = categoryViolations[i];
            outputHelper.WriteLine($"Issue {i + 1}: {violation.Description}");
            for (var j = 0; j < violation.Nodes.Length; j++)
            {
                var node = violation.Nodes[j];
                outputHelper.WriteLine($"  Occurrence {j + 1}: {node.Html}");
            }
        }
    }
}