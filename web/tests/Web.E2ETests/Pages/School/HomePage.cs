﻿using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;

public class HomePage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator PageH2Headings => page.Locator(Selectors.H2);
    private ILocator Breadcrumbs => page.Locator(Selectors.GovBreadcrumbs);
    private ILocator ChangeSchoolLink => page.Locator(Selectors.ChangeSchoolLink);
    private ILocator CompareYourCostsLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "Compare your costs" });
    private ILocator CurriculumAndFinancialPlanningLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "Curriculum and financial planning" });
    private ILocator BenchmarkWorkforceDataLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "Benchmark workforce data" });
    private ILocator SchoolDetailsLink => page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "School contact details" });

    private ILocator SpendingAndCostsLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all spending and costs" });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await Breadcrumbs.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible().ShouldHaveAttribute("href", "/find-organisation");
        string[] expectedH2Texts = { "Spending and costs", "Finance tools", "Resources", "Get help" };
        for (int i = 0; i < await PageH2Headings.CountAsync(); i++)
        {
            await PageH2Headings.Nth(i).ShouldBeVisible().ShouldHaveText(expectedH2Texts[i]);
        }
        await SpendingAndCostsLink.ShouldBeVisible();
        await CompareYourCostsLink.ShouldBeVisible();
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

    public async Task<SpendingCostsPage> ClickSpendingAndCosts()
    {
        await SpendingAndCostsLink.Click();
        return new SpendingCostsPage(page);
    }
}