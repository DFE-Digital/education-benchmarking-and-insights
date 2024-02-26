using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages.School.CurriculumFinancialPlanningSteps;

public class CreateNewFinancialPlanPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator BreadCrumbs => page.Locator(Selectors.GovBreadcrumbs);
    private ILocator ChangeSchoolLink => page.Locator(Selectors.ChangeSchoolLink);

    private ILocator CreateNewPlanBtn => page.Locator(Selectors.GovButton, new PageLocatorOptions { HasText = "Create new plan" });

    public async Task IsDisplayed()
    {
       await PageH1Heading.ShouldBeVisible();
       await PageH1Heading.ShouldHaveText("Curriculum and financial planning (CFP)");
       await BreadCrumbs.ShouldBeVisible();
       await ChangeSchoolLink.ShouldBeVisible();
       await CreateNewPlanBtn.ShouldBeVisible().ShouldBeEnabled();
    }
    
}