using Microsoft.Playwright;
using Xunit;
namespace Web.E2ETests.Pages.School;

public class HomePage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator PageH2Headings => page.Locator($"main {Selectors.H2}{Selectors.GovHeadingM}");
    //private ILocator Breadcrumbs => page.Locator(Selectors.GovBreadcrumbs);
    private ILocator ChangeSchoolLink => page.Locator(Selectors.ChangeSchoolLink);
    private ILocator CompareYourCostsLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "Benchmark spending"
    });
    private ILocator CurriculumAndFinancialPlanningLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "Curriculum and financial planning"
    });
    private ILocator BenchmarkCensusDataLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "Benchmark pupil and workforce data"
    });
    private ILocator SchoolDetailsLink => page.Locator(Selectors.GovLink, new PageLocatorOptions
    {
        HasText = "School contact details"
    });
    private ILocator IncompleteFinancialBanner => page.Locator(Selectors.GovWarning);

    private ILocator DataSourcesAndInterpretation => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "Data sources and interpretation"
        });
    private ILocator SpendingPrioritiesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "View all spending priorities for this school"
        });

    private ILocator FindWaysToSpendLessLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "Find ways to spend less"
        });

    private ILocator ViewHistoricDataLink =>
        page.Locator(Selectors.GovLink, new PageLocatorOptions
        {
            HasText = "View historic data"
        });

    private ILocator CookieBanner => page.Locator(Selectors.CookieBanner);
    private ILocator RagGuidance => page.Locator("#rag-guidance");

    public async Task IsDisplayed(bool isPartYear = false, string? trustName = null)
    {
        await PageH1Heading.ShouldBeVisible();
        //await Breadcrumbs.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible().ShouldHaveAttribute("href", "/find-organisation?method=school");

        List<string> expectedH2Texts = ["Benchmarking and planning tools", "Resources"];
        if (!isPartYear)
        {
            expectedH2Texts.Insert(0, "Spending priorities for this school");
        }

        if (!string.IsNullOrWhiteSpace(trustName))
        {
            expectedH2Texts.Insert(0, $"Part of {trustName}");
        }

        for (var i = 0; i < await PageH2Headings.CountAsync(); i++)
        {
            await PageH2Headings.Nth(i).ShouldBeVisible().ShouldHaveText(expectedH2Texts[i]);
        }

        await CompareYourCostsLink.ShouldBeVisible();
        await CurriculumAndFinancialPlanningLink.ShouldBeVisible();
        await BenchmarkCensusDataLink.ShouldBeVisible();
        await ViewHistoricDataLink.ShouldBeVisible();
        await SchoolDetailsLink.ShouldBeVisible();
        await DataSourcesAndInterpretation.ShouldBeVisible();
        await FindWaysToSpendLessLink.ShouldBeVisible();
        if (!isPartYear)
        {

            await SpendingPrioritiesLink.ShouldBeVisible();
        }
        else
        {
            await SpendingPrioritiesLink.ShouldNotBeVisible();
            await IncompleteFinancialBanner.ShouldBeVisible();
        }

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
        await SpendingPrioritiesLink.Click();
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

    public async Task CookieBannerIsDisplayed()
    {
        await CookieBanner.ShouldBeVisible();
    }

    public async Task AssertRagCommentary(string categoryName, string commentary)
    {
        var categoryHeader = page.Locator("h4").And(page.GetByText(categoryName));
        Assert.NotNull(categoryHeader);

        var priority = categoryHeader.Locator("//following-sibling::p[1]");
        Assert.NotNull(priority);

        var text = await priority.InnerTextAsync();
        Assert.Equal(commentary, text);
    }

    public async Task AssertRagGuidance()
    {
        await RagGuidance.ShouldBeVisible();
    }
}