using EducationBenchmarking.Web.E2ETests.Helpers;
using EducationBenchmarking.Web.E2ETests.Hooks;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages.CurriculumFinancialPlanning;

public class StartPage(PageHook page)
{
    private readonly IPage _page = page.Current;
    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BackLink => _page.Locator(".govuk-back-link");
    private ILocator ContinueButton => _page.Locator(".govuk-button", new PageLocatorOptions { HasText = "Continue" });
    private ILocator HelpLink => _page.Locator(".govuk-link", new PageLocatorOptions { HasText = "can be found here" });
    
    public async Task AssertPage()
    {
       await PageH1Heading.ShouldBeVisible();
       await BackLink.ShouldBeVisible();
       await ContinueButton.ShouldBeVisible().ShouldBeEnabled();
       await HelpLink.ShouldBeVisible();
    }
    
    public async Task GoToPage(string urn)
    {
        await _page.GotoAsync(PageUrl(urn));
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task ClickHelp()
    {
        await HelpLink.Click();
    }
    
    public async Task ClickContinue()
    {
        await ContinueButton.Click();
    }

    
    private static string PageUrl(string urn)
    {
        return $"{TestConfiguration.ServiceUrl}/school/{urn}/financial-planning/steps/start";
    }
}