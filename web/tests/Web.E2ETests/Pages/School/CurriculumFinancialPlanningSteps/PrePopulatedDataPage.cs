using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School.CurriculumFinancialPlanningSteps;

public class PrePopulatedDataPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator BackLink => page.Locator(Selectors.GovBackLink);

    private ILocator ContinueButton =>
        page.Locator(Selectors.GovButton, new PageLocatorOptions { HasText = "Continue" });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await BackLink.ShouldBeVisible();
        await ContinueButton.ShouldBeVisible().ShouldBeEnabled();
    }
}