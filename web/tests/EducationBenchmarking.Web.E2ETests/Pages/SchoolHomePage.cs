using EducationBenchmarking.Web.E2ETests.Hooks;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class SchoolHomePage(PageHook page)
{
    private readonly IPage _page = page.Current;

    private ILocator CompareYourCostsLink =>
        _page.Locator("h3 a.govuk-link:has-text(\"Compare your costs\")");

    public async Task ClickOnCompareYourCosts()
    {
        await CompareYourCostsLink.ClickAsync();
    }

    public async Task WaitForPage(string urn)
    {
        await _page.WaitForURLAsync($"{Config.BaseUrl}/school/{urn}");
    }
}