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
                outputHelper.WriteLine($"{impact} issues: {violations.Count(x => x.Impact == impact)}");    
            }
        
            for (var i = 0; i < violations.Count; i++)
            {
                var violation = violations[i];
                outputHelper.WriteLine($"Issue {i + 1}: {violation.Description}");
                for (var j = 0; j < violation.Nodes.Length; j++)
                {
                    var node = violation.Nodes[j];
                    outputHelper.WriteLine($"  Occurrence {j + 1}: {node.Html}");
                }
            }
        }
    }
}