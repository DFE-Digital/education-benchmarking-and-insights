using EducationBenchmarking.Web.E2ETests.Helpers;
using EducationBenchmarking.Web.E2ETests.Hooks;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages.CurriculumFinancialPlanning;

public class SelectYearPage(PageHook page)
{
    private readonly IPage _page = page.Current;
    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BackLink => _page.Locator(".govuk-back-link");
    private ILocator ContinueButton => _page.Locator(".govuk-button", new PageLocatorOptions { HasText = "Continue" });
    private ILocator YearRadio(int year) => _page.Locator($"#year-{year}");
    private static int CurrentYear => DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
    private static IEnumerable<int> AvailableYears => Enumerable.Range(CurrentYear, 4).ToArray();

    public async Task AssertPage()
    {
        await PageH1Heading.ShouldBeVisible();
        await BackLink.ShouldBeVisible();
        await ContinueButton.ShouldBeVisible().ShouldBeEnabled();
        foreach (var year in AvailableYears)
        {
            await YearRadio(year).ShouldBeVisible();
        }
    }

    public async Task GoToPage(string urn)
    {
        await _page.GotoAsync(PageUrl(urn));
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    private static string PageUrl(string urn)
    {
        return $"{TestConfiguration.ServiceUrl}/school/{urn}/financial-planning/steps/select-year";
    }

    public async Task ChooseYear(string year)
    {
        var selectedYear = year.ToLower() switch
        {
            "now" => CurrentYear,
            "next" => CurrentYear + 1,
            "two" => CurrentYear + 2,
            "three" => CurrentYear + 3,
            _ => throw new ArgumentOutOfRangeException(nameof(year))
        };

        await YearRadio(selectedYear).ClickAsync();
    }

    public async Task ClickContinue()
    {
        await ContinueButton.Click();
    }

    public async Task ClickBack()
    {
        await BackLink.Click();
    }
}