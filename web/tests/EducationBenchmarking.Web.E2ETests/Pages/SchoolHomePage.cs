using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class SchoolHomePage
{
    private IPage _page;

    public SchoolHomePage(IPage page)
    {
        _page = page;
    }

    private ILocator CompareYourCostsLink =>
        _page.Locator("h3 a.govuk-link:has-text(\"Compare your costs\")");

    public async Task ClickOnCompareYourCosts()
    {
        await CompareYourCostsLink.ClickAsync();
    }
}