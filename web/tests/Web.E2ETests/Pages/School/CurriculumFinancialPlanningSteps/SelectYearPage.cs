using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School.CurriculumFinancialPlanningSteps;

public enum FinancialPlanYear
{
    ThisYear,
    NextYear,
    TwoYearsTime,
    ThreeYearsTime
}

public class SelectYearPage(IPage page)
{
    private static int CurrentYear => DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
    private static int[] AvailableYears => Enumerable.Range(CurrentYear, 4).ToArray();

    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    //private ILocator BackLink => page.Locator(Selectors.GovBackLink);
    private ILocator YearRadio(int year) => page.Locator($"#year-{year}");

    private ILocator ContinueButton =>
        page.Locator(Selectors.GovButton, new PageLocatorOptions { HasText = "Continue" });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        //await BackLink.ShouldBeVisible();
        await ContinueButton.ShouldBeVisible().ShouldBeEnabled();
        foreach (var year in AvailableYears)
        {
            await YearRadio(year).ShouldBeVisible();
        }
    }

    public async Task ChooseYear(FinancialPlanYear year)
    {
        var selectedYear = year switch
        {
            FinancialPlanYear.ThisYear => AvailableYears[0],
            FinancialPlanYear.NextYear => AvailableYears[1],
            FinancialPlanYear.TwoYearsTime => AvailableYears[2],
            FinancialPlanYear.ThreeYearsTime => AvailableYears[3],
            _ => throw new ArgumentOutOfRangeException(nameof(year))
        };

        await YearRadio(selectedYear).ClickAsync();
    }

    public async Task<PrePopulatedDataPage> ClickContinue()
    {
        await ContinueButton.Click();
        return new PrePopulatedDataPage(page);
    }

    /*public async Task<StartPage> ClickBack()
    {
        await BackLink.Click();
        return new StartPage(page);
    }*/
}