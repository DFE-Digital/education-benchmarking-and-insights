using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School;

public class HomePage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator PageH2Headings => page.Locator($"{Selectors.H2}{Selectors.GovHeadingM}");
    private ILocator Breadcrumbs => page.Locator(Selectors.GovBreadcrumbs);
    private ILocator ChangeSchoolLink => page.Locator(Selectors.ChangeSchoolLink);
    private ILocator CompareYourCostsLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "Compare your costs" });
    private ILocator CurriculumAndFinancialPlanningLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "Curriculum and financial planning" });
    private ILocator BenchmarkCensusDataLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "Benchmark pupil and workforce data" });
    private ILocator SchoolDetailsLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "School contact details" });

    private ILocator DataSourcesAndInterpretation => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "Data sources and interpretation" });
    private ILocator SpendingAndCostsLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all spending priorities for this school" });

    private ILocator FindWaysToSpendLessLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "Find ways to spend less" });

    private ILocator ViewHistoricDataLink =>
        page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "View historic data" });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await Breadcrumbs.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible().ShouldHaveAttribute("href", "/find-organisation?method=school");
        var expectedH2Texts = new[] { "Spending  priorities for this school", "Finance tools", "Resources" };
        for (var i = 0; i < await PageH2Headings.CountAsync(); i++)
        {
            await PageH2Headings.Nth(i).ShouldBeVisible().ShouldHaveText(expectedH2Texts[i]);
        }
        await SpendingAndCostsLink.ShouldBeVisible();
        await CompareYourCostsLink.ShouldBeVisible();
        await CurriculumAndFinancialPlanningLink.ShouldBeVisible();
        await BenchmarkCensusDataLink.ShouldBeVisible();
        await FindWaysToSpendLessLink.ShouldBeVisible();
        await ViewHistoricDataLink.ShouldBeVisible();
        await SchoolDetailsLink.ShouldBeVisible();
        await DataSourcesAndInterpretation.ShouldBeVisible();

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

    public async Task<BenchmarkCensusPage> ClickBenchmarkCensus()
    {
        await BenchmarkCensusDataLink.Click();
        return new BenchmarkCensusPage(page);
    }

    public async Task<SpendingCostsPage> ClickSpendingAndCosts()
    {
        await SpendingAndCostsLink.Click();
        return new SpendingCostsPage(page);
    }

    public async Task<CommercialResourcesPage?> ClickFindWaysToSpendLess()
    {
        await FindWaysToSpendLessLink.Click();
        return new CommercialResourcesPage(page);
    }

    public async Task<HistoricDataPage?> ClickHistoricData()
    {
        await ViewHistoricDataLink.Click();
        return new HistoricDataPage(page);
    }
}