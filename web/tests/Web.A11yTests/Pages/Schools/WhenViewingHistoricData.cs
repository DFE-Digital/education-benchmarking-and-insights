using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.Schools;

public class WhenViewingHistoricData(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{TestConfiguration.School}/history";

    [Theory]
    [InlineData("spending", true, "expenditure-mode-table", "accordion-expenditure")]
    [InlineData("spending", true, "expenditure-mode-chart", "accordion-expenditure")]
    [InlineData("spending", false, "expenditure-mode-table", null)]
    [InlineData("spending", false, "expenditure-mode-chart", null)]
    [InlineData("income", true, "income-mode-table", "accordion-income")]
    [InlineData("income", true, "income-mode-chart", "accordion-income")]
    [InlineData("income", false, "income-mode-table", null)]
    [InlineData("income", false, "income-mode-chart", null)]
    [InlineData("balance", false, "balance-mode-table", null)]
    [InlineData("balance", false, "balance-mode-chart", null)]
    [InlineData("census", false, "census-mode-table", null)]
    [InlineData("census", false, "census-mode-chart", null)]
    public async Task ThenThereAreNoAccessibilityIssues(string resource, bool shouldShowAll, string mode, string? accordion)
    {
        await GoToPage();
        await Page.Locator($"#tab_{resource}").ClickAsync();
        await Page.Locator($"#{mode}").ClickAsync();
        if (shouldShowAll && accordion != null)
        {
            await Page.Locator($"#{accordion}").Locator(".govuk-accordion__show-all").ClickAsync();
        }

        await EvaluatePage();
    }
}