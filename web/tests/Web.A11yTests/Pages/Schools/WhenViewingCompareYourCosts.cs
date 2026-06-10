using Microsoft.Playwright;
using Web.A11yTests.Drivers;
using Xunit;
using Xunit.Abstractions;

namespace Web.A11yTests.Pages.Schools;

public class WhenViewingCompareYourCosts(
    ITestOutputHelper testOutputHelper,
    WebDriver webDriver)
    : PageBase(testOutputHelper, webDriver)
{
    protected override string PageUrl => $"/school/{TestConfiguration.School}/comparison";

    [Trait("Category", "SchoolComparisonFilterDisabled")]
    [Theory]
    [InlineData("table")]
    [InlineData("chart")]
    public async Task ShowAllSectionsThenThereAreNoAccessibilityIssues(string mode)
    {
        await GoToPage();
        await Page.Locator($"#mode-{mode}").ClickAsync();
        await Page.Locator(".govuk-accordion__show-all").ClickAsync();
        await EvaluatePage();
    }

    [Trait("Category", "SchoolComparisonFilterDisabled")]
    [Theory]
    [InlineData("table")]
    [InlineData("chart")]
    public async Task ThenThereAreNoAccessibilityIssues(string mode)
    {
        await GoToPage();
        await Page.Locator($"#mode-{mode}").ClickAsync();
        await EvaluatePage();
    }

    [Trait("Category", "SchoolComparisonFilterEnabled")]
    [Fact]
    public async Task ThenThereAreNoAccessibilityIssuesWhenApplyingOptionsAndFilters()
    {
        await GoToPage();
        await EvaluatePage();

        // apply options
        await Page.Locator($"#view-Table").ClickAsync();

        await Page
            .Locator(".options-form__apply")
            .GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Apply" })
            .ClickAsync();

        await EvaluatePage();

        // apply filter
        // expand accordion
        await Page
            .Locator(".app-filter")
            .GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Total expenditure" })
            .ClickAsync();

        // tick checkbox
        await Page
            .Locator(".app-filter")
            .GetByRole(AriaRole.Checkbox, new LocatorGetByRoleOptions { Name = "Total expenditure" })
            .CheckAsync();

        // submit filters
        await Page
            .Locator(".app-filter")
            .GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Apply filters" })
            .ClickAsync();
        await EvaluatePage();
    }
}
