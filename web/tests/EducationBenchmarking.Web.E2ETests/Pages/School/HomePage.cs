using EducationBenchmarking.Web.E2ETests.Pages.School.CurriculumFinancialPlanningSteps;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages.School;

public class HomePage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator BreadCrumbs => page.Locator(Selectors.GovBreadcrumbs);
    private ILocator ChangeSchoolLink => page.Locator(Selectors.ChangeSchoolLink);
    private ILocator CompareYourCostsLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "Compare your costs" });
    private ILocator ViewAreasOfInvestigationLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "View your areas for investigation" });
    private ILocator CurriculumAndFinancialPlanningLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "Curriculum and financial planning" });
    private ILocator BenchmarkWorkforceDataLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "Benchmark workforce data" });
    private ILocator SchoolDetailsLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "School details" });

    public async Task IsDisplayed()
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

    public async Task<DetailsPage> ClickSchoolDetails()
    {
        await SchoolDetailsLink.Click();
        return new DetailsPage(page);
    }

    public async Task<CompareYourCostsPage> ClickCompareYourCosts()
    {
        await CompareYourCostsLink.Click();
        return new CompareYourCostsPage(page);
    }

    public async Task<CurriculumFinancialPlanningPage> ClickFinancialPlanning()
    {
        await CurriculumAndFinancialPlanningLink.Click();
        return new CurriculumFinancialPlanningPage(page);
    }

    public async Task<BenchmarkWorkforcePage> ClickBenchmarkWorkforce()
    {
        await BenchmarkWorkforceDataLink.Click();
        return new BenchmarkWorkforcePage(page);
    }
}