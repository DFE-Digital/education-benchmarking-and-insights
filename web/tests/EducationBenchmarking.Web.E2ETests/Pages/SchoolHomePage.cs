using EducationBenchmarking.Web.E2ETests.Helpers;
using EducationBenchmarking.Web.E2ETests.Hooks;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class SchoolHomePage(PageHook page)
{
    private readonly IPage _page = page.Current;
    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BreadCrumbs => _page.Locator(".govuk-breadcrumbs");
    private ILocator ChangeSchoolLink => _page.Locator(":text('Change school')");
    private ILocator CompareYourCostsLink => _page.Locator("h3 a.govuk-link", new PageLocatorOptions{HasText = "Compare your costs"});
    private ILocator ViewAreasOfInvestigationLink => _page.Locator("h3 a.govuk-link", new PageLocatorOptions{HasText = "View your areas for investigation"});
    private ILocator CurriculumAndFinancialPlanningLink => _page.Locator("h3 a.govuk-link", new PageLocatorOptions{HasText = "Curriculum and financial planning"});
    private ILocator BenchmarkWorkforceDataLink => _page.Locator("h3 a.govuk-link", new PageLocatorOptions{HasText = "Benchmark workforce data"});
    private ILocator SchoolDetailsLink => _page.Locator("h3 a.govuk-link", new PageLocatorOptions{HasText = "School details"});

    public async Task ClickOnCompareYourCosts()
    {
        await CompareYourCostsLink.ClickAsync();
    }

    public async Task WaitForPage(string urn)
    {
        await _page.WaitForURLAsync($"{TestConfiguration.ServiceUrl}/school/{urn}");
    }

    public async Task GotToPage(string urn)
    {
        await _page.GotoAsync($"{TestConfiguration.ServiceUrl}/school/{urn}");
    }

    public async Task AssertPage()
    {
        await PageH1Heading.ShouldBeVisible();
        await BreadCrumbs.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible();
        await CompareYourCostsLink.ShouldBeVisible();
        await ViewAreasOfInvestigationLink.ShouldBeVisible();
        await CurriculumAndFinancialPlanningLink.ShouldBeVisible();
        await BenchmarkWorkforceDataLink.ShouldBeVisible();
        await SchoolDetailsLink.ShouldBeVisible();
    }

    public async Task ClickLink(string linkToClick)
    {
        var link = linkToClick switch
        {
            "school details" => SchoolDetailsLink,
            "compare your costs" => CompareYourCostsLink,
            "curriculum and financial planning" => CurriculumAndFinancialPlanningLink,
            _ => throw new ArgumentException($"Unsupported link name: {linkToClick}")
        };
        await link.Click();
    }
}