using EducationBenchmarking.Web.E2ETests.Helpers;
using EducationBenchmarking.Web.E2ETests.Hooks;
using Microsoft.Playwright;
using Xunit;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class SchoolDetailsPage(PageHook page)
{
    private readonly IPage _page = page.Current;
    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BackLink => _page.Locator(".govuk-back-link");
    private ILocator ChangeSchoolLink => _page.Locator(":text('Change school')");
    private ILocator GiasPageLink => _page.Locator("a[data-id='gias-school-details']");
    private ILocator EmailAddress => _page.Locator(".govuk-summary-list__key:has-text('Contact email') + .govuk-summary-list__value");

    public async Task WaitForPage(string urn)
    {
        await _page.WaitForURLAsync($"{TestConfiguration.ServiceUrl}/school/{urn}/details");
    }

    public async Task AssertPage()
    {
        await PageH1Heading.ShouldBeVisible();
        await PageH1Heading.ShouldHaveText("Contact details");
        await BackLink.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible();
        await GiasPageLink.ShouldBeVisible();
        Assert.True(await GiasPageLink.GetAttributeAsync("target") == "_blank");
        Assert.True(await EmailAddress.GetInnerText() == "Log in to view contact information");
    }
}