using System.Net;
using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
using EducationBenchmarking.Web.A11yTests.Drivers;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests;

public abstract class PageBase(ITestOutputHelper testOutputHelper, WebDriver webDriver)
    : IClassFixture<WebDriver>
{
    private static string ServiceUrl => TestConfiguration.Instance.GetValue<string>("ServiceUrl") ??
                                        throw new InvalidOperationException("Service URL missing from configuration");

    private static IEnumerable<string> Impacts =>
        TestConfiguration.Instance.GetSection("Impacts").Get<string[]>() ?? ["critical", "serious"];

    private IPage? _page;

    protected abstract string PageUrl { get; }

    protected IPage Page =>
        _page ?? throw new InvalidOperationException("Ensure the page has been successfully navigated to");

    protected ITestOutputHelper TestOutputHelper => testOutputHelper;

    protected async Task GoToPage(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        Assert.False(string.IsNullOrEmpty(PageUrl));
        var fullUrl = $"{ServiceUrl}{PageUrl}";

        _page = await webDriver.GetPage(fullUrl, statusCode);

        Assert.NotNull(_page);
        Assert.Equal(fullUrl, _page.Url);
    }

    protected async Task EvaluatePage(AxeRunContext? context = null)
    {
        var results = context != null ? await Page.RunAxe(context) : await Page.RunAxe();
        var violations = results
            .Violations
            .Where(violation => Impacts.Contains(violation.Impact))
            .ToArray();

        PrintViolations(violations);

        Assert.True(violations.Length == 0, "There are violations on the page");
    }

    private void PrintViolations(IReadOnlyList<AxeResultItem> violations)
    {
        if (violations.Any())
        {
            foreach (var impact in Impacts)
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
                    TestOutputHelper.WriteLine($"Occurrence {j + 1}: {node.Html}");
                }
            }
        }
    }
}