using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School.CurriculumFinancialPlanningSteps;

public class StartPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    //private ILocator BackLink => page.Locator(Selectors.GovBackLink);

    private ILocator ContinueButton =>
        page.Locator(Selectors.GovButton, new PageLocatorOptions { HasText = "Continue" });

    private ILocator HelpLink =>
        page.Locator(Selectors.GovLink, new PageLocatorOptions { HasText = "can be found here" });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        //await BackLink.ShouldBeVisible();
        await ContinueButton.ShouldBeVisible().ShouldBeEnabled();
        await HelpLink.ShouldBeVisible();
    }

    public async Task<HelpPage> ClickHelp()
    {
        await HelpLink.Click();
        return new HelpPage(page);
    }

    public async Task<SelectYearPage> ClickContinue()
    {
        await ContinueButton.Click();
        return new SelectYearPage(page);
    }
}