using EducationBenchmarking.Web.E2ETests.Helpers;
using EducationBenchmarking.Web.E2ETests.Hooks;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using FluentAssertions;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages.CurriculumFinancialPlanning;

public class ICFPHelpPage(PageHook page)
{
    private readonly IPage _page = page.Current;
    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BackLink => _page.Locator(".govuk-back-link");
    private ILocator SubmitAnEnquiryLink => _page.Locator("a", new PageLocatorOptions { HasText = "submit an enquiry" });


    public async Task AssertPage()
    {
        _page.Url.Should().EndWith("/financial-planning/help");
        await PageH1Heading.ShouldBeVisible();
        await SubmitAnEnquiryLink.ShouldHaveAttribute("href", "/submit-an-enquiry");
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
        return $"{Config.BaseUrl}/school/{urn}/financial-planning/help";
    }
}