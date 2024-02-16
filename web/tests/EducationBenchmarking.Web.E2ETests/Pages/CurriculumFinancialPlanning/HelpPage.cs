using EducationBenchmarking.Web.E2ETests.Helpers;
using EducationBenchmarking.Web.E2ETests.Hooks;
using FluentAssertions;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages.CurriculumFinancialPlanning;

public class HelpPage(PageHook page)
{
    private readonly IPage _page = page.Current;
    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BackLink => _page.Locator(".govuk-back-link");


    public async Task AssertPage()
    {
        _page.Url.Should().EndWith("/financial-planning/steps/help");
        await PageH1Heading.ShouldBeVisible();
        await BackLink.ShouldBeVisible();
    }

    public async Task ClickBack()
    {
        await BackLink.Click();
    }

    public async Task GoToPage(string urn)
    {
        await _page.GotoAsync(PageUrl(urn));
    }

    private static string PageUrl(string urn)
    {
        return $"{TestConfiguration.ServiceUrl}/school/{urn}/financial-planning/steps/help";
    }
}