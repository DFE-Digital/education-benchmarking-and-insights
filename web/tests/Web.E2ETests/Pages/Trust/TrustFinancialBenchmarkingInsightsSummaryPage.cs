using Microsoft.Playwright;

namespace Web.E2ETests.Pages.Trust;

public class TrustFinancialBenchmarkingInsightsSummaryPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator KeyInformationSection => page.Locator("section#key-information-section");
    private ILocator SpendingPrioritiesSection => page.Locator("section#spending-focus-section");
    private ILocator SpendingAtSchoolsSection => page.Locator("section#priority-schools-section");
    private ILocator NextStepsSection => page.Locator("section#next-steps-section");
    private ILocator ViewMoreInsightsInFbitLink => KeyInformationSection.Locator("a.govuk-link:has-text('Financial Benchmarking and Insights Tool')");
    private ILocator ViewAllSpendingPrioritiesLink => SpendingPrioritiesSection.Locator("a.govuk-link:has-text('View spending focus for this trust')");
    private ILocator ViewMoreInformationAboutSchoolsLink => SpendingAtSchoolsSection.Locator("a.govuk-link:has-text('View more information about schools at this trust')");
    private ILocator FirstPriorityCountLink => SpendingAtSchoolsSection.Locator(".priority-count a.govuk-link").First;

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await KeyInformationSection.ShouldBeVisible();
        await SpendingPrioritiesSection.ShouldBeVisible();
        await SpendingAtSchoolsSection.ShouldBeVisible();
        await NextStepsSection.ShouldBeVisible();
    }

    public async Task<HomePage> ClickTheViewMoreInsightsInFbitLink()
    {
        await ViewMoreInsightsInFbitLink.Click();
        return new HomePage(page);
    }

    public async Task<SpendingCostsPage> ClickViewAllSpendingPrioritiesLink()
    {
        await ViewAllSpendingPrioritiesLink.Click();
        return new SpendingCostsPage(page);
    }

    public async Task<HomePage> ClickViewMoreInformationAboutSchoolsLink()
    {
        await ViewMoreInformationAboutSchoolsLink.Click();
        return new HomePage(page);
    }

    public async Task<Web.E2ETests.Pages.School.SpendingCostsPage> ClickFirstPriorityCountLink()
    {
        await FirstPriorityCountLink.Click();
        return new School.SpendingCostsPage(page);
    }
}