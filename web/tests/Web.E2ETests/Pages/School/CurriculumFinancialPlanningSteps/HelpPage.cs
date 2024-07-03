using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School.CurriculumFinancialPlanningSteps;

public class HelpPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    //private ILocator BackLink => page.Locator(Selectors.GovBackLink);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        //await BackLink.ShouldBeVisible();
    }

    /*public async Task<StartPage> ClickBack()
    {
        //await BackLink.Click();
        return new StartPage(page);
    }*/
}