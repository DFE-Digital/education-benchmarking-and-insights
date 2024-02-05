using EducationBenchmarking.Web.E2ETests.Helpers;
using EducationBenchmarking.Web.E2ETests.Hooks;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages.CurriculumFinancialPlanning;

public class PrePopulatedDataPage(PageHook page)
{
    private readonly IPage _page = page.Current;
    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BackLink => _page.Locator(".govuk-back-link");
    private ILocator ContinueButton => _page.Locator(".govuk-button", new PageLocatorOptions { HasText = "Continue" });
    
    public async Task AssertPage()
    {
       await PageH1Heading.ShouldBeVisible();
       await BackLink.ShouldBeVisible();
       await ContinueButton.ShouldBeVisible().ShouldBeEnabled();
    }
    
    private static string PageUrl(string urn, int year)
    {
        return $"{TestConfiguration.ServiceUrl}/school/{urn}/financial-planning/{year}";
    }
}