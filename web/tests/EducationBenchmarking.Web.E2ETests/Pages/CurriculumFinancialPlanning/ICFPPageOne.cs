using EducationBenchmarking.Web.E2ETests.Helpers;
using EducationBenchmarking.Web.E2ETests.Hooks;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages.CurriculumFinancialPlanning;

public class ICFPPageOne(PageHook page)
{
    private readonly IPage _page = page.Current;
    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BreadCrumbs => _page.Locator(".govuk-breadcrumbs");
    private ILocator ContinueCta => _page.Locator(".govuk-button", new PageLocatorOptions { HasText = "Continue" });
    private ILocator HelpPageLink => _page.Locator(".govuk-link", new PageLocatorOptions { HasText = "can be found here" });
    
    public async Task AssertPage()
    {
       await PageH1Heading.ShouldBeVisible();
       await BreadCrumbs.ShouldBeVisible();
       await ContinueCta.ShouldBeVisible();
       await ContinueCta.ShouldBeEnabled();
       await HelpPageLink.ShouldBeVisible();
       
       
    }
    public async Task GoToPage(string urn)
    {
        await _page.GotoAsync(PageUrl(urn));
    }

    private static string PageUrl(string urn)
    {
        return $"{Config.BaseUrl}/school/{urn}/financial-planning";
    }

    public async Task clickOnHelpBtn()
    {
        await HelpPageLink.Click();
    }
}