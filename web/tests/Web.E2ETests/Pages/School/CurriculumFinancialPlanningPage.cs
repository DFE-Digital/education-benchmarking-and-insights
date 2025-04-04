﻿using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School;

public class CurriculumFinancialPlanningPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    //private ILocator Breadcrumbs => page.Locator(Selectors.GovBreadcrumbs);

    private ILocator CreateNewPlanBtn => page.Locator(Selectors.GovButton, new PageLocatorOptions { HasText = "Continue" });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible().ShouldHaveText("Curriculum and financial planning (CFP)");
        //await Breadcrumbs.ShouldBeVisible();
        await CreateNewPlanBtn.ShouldBeVisible().ShouldBeEnabled();
    }

    public async Task IsForbidden()
    {
        await PageH1Heading.ShouldBeVisible().ShouldHaveText("Access denied");
    }
}