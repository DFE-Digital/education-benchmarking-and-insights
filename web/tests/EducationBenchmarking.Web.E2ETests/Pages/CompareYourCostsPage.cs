using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class CompareYourCostsPage
{
    private IPage _page;

    public CompareYourCostsPage(IPage page)
    {
        _page = page;
    }

    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BreadCrumbs => _page.Locator(".govuk-breadcrumbs");
    private ILocator ChangeSchoolLink => _page.Locator("#change-school");
}