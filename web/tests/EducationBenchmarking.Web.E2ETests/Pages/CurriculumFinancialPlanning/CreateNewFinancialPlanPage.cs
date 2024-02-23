using EducationBenchmarking.Web.E2ETests.Helpers;
using EducationBenchmarking.Web.E2ETests.Hooks;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages.CurriculumFinancialPlanning;

public class CreateNewFinancialPlanPage(PageHook page)
{
    private readonly IPage _page = page.Current;
    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BreadCrumbs => _page.Locator(".govuk-breadcrumbs");
    private ILocator ChangeSchoolLink => _page.Locator(":text('Change school')");

    private async Task WaitForPage(string urn)
    {
        await _page.WaitForURLAsync($"{TestConfiguration.ServiceUrl}/school/{urn}/financial-planning");
    }

    private async Task AssertPage()
    {
        await PageH1Heading.ShouldBeVisible();
        await BreadCrumbs.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible();
    }

    public async Task IsPageDisplayed(string urn)
    {
        await WaitForPage(urn);
        await AssertPage();

    }
}