using Microsoft.Playwright;
using Xunit;
namespace Web.E2ETests.Pages.School;

public class SchoolBenchmarkingReportCardsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator IntroductionSection => page.Locator(Selectors.BrcIntroduction);
    private ILocator KeyInformationSection => page.Locator(Selectors.BrcKeyInformation);
    private ILocator KeyInformationContent => page.Locator(Selectors.KeyInformationContent);
    private ILocator SpendPrioritySection => page.Locator(Selectors.BrcPriorityAreas);
    private ILocator OtherSpendingPrioritiesSection => page.Locator(Selectors.BrcOtherPriorityAreas);
    private ILocator PupilWorkforceMetricsSection => page.Locator(Selectors.BrcPupilWorkforce);
    private ILocator NextStepsSection => page.Locator(Selectors.BrcNextSteps);
    private ILocator PrintPageCta => page.Locator(Selectors.GovButton).GetByText("Print this page");

    private ILocator VisitFbitButton => page.Locator(Selectors.GovButton)
        .GetByText("Visit the Financial Benchmarking and Insights Tool");
    private ILocator TeachingSupportStaff => page.Locator(Selectors.PriorityAreaTeachingSupportStaff);
    private ILocator NonEducationalSupportStaff => page.Locator(Selectors.PriorityAreaNonEducationSupportStaff);
    private ILocator AdministrativeSupplies => page.Locator(Selectors.PriorityAreaAdministrativeSupplies);
    private ILocator Top3SpendingPriorities => page.Locator($"{Selectors.BrcOtherPriorityAreas} .priority-position");
    private ILocator PupilToTeacherMetric => page.Locator("h3:has-text('Pupil-to-teacher metric')");
    private ILocator PupilToSeniorLeadershipRoles => page.Locator("h3:has-text('Pupil-to-senior leadership role metric')");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await KeyInformationSection.ShouldBeVisible();
        Assert.Equal(4, await KeyInformationContent.Count());
        foreach (var item in await KeyInformationContent.AllAsync())
        {
            await item.ShouldBeVisible();
        }
        await SpendPrioritySection.ShouldBeVisible();
        await AssertSpendPriorityItems();
        await OtherSpendingPrioritiesSection.ShouldBeVisible();
        await AssertOtherTopSpendingPriorities();
        Assert.Equal(3, await Top3SpendingPriorities.Count());
        foreach (var priority in await Top3SpendingPriorities.AllAsync())
        {
            await priority.ShouldBeVisible();
        }

        await PupilWorkforceMetricsSection.ShouldBeVisible();
        await AssertPupilWorkforceMetrics();
        await PrintPageCta.ShouldBeVisible();
        await VisitFbitButton.ShouldBeVisible();
    }

    public async Task IsNotDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await PageH1Heading.ShouldHaveText("Page not found");
    }

    private async Task AssertSpendPriorityItems()
    {
        await TeachingSupportStaff.ShouldBeVisible();
        await NonEducationalSupportStaff.ShouldBeVisible();
        await AdministrativeSupplies.ShouldBeVisible();
    }

    private async Task AssertOtherTopSpendingPriorities()
    {
        var topSpendingItems = await Top3SpendingPriorities.AllAsync();
        Assert.True(topSpendingItems.Count >= 3, "Expected at least 3 top spending priorities to be visible.");
    }

    private async Task AssertPupilWorkforceMetrics()
    {
        await PupilToTeacherMetric.ShouldBeVisible();
        await PupilToSeniorLeadershipRoles.ShouldBeVisible();
    }

    public async Task ClickPrintPageCta(string functionName)
    {
        // see https://playwright.dev/dotnet/docs/dialogs#print-dialogs
        await page.EvaluateAsync($"(() => {{window.{functionName} = new Promise(f => window.print = f);}})()");
        await PrintPageCta.ClickAsync();
    }

    public async Task EvaluatePrintPageCta(string functionName)
    {
        await page.WaitForFunctionAsync($"window.{functionName}");
    }

    public async Task<HomePage> ClickIntroductionLink()
    {
        await IntroductionSection.Locator("a").ClickAsync();
        return new HomePage(page);
    }

    public async Task<CompareYourCostsPage> ClickKeyInformationLink()
    {
        await KeyInformationSection.Locator("a").ClickAsync();
        return new CompareYourCostsPage(page);
    }
}