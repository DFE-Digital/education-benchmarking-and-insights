using System.Net;
using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;

public class SchoolFinancialBenchmarkingInsightsSummaryPage(IPage page, IResponse? response = null)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator IntroductionSection => page.Locator(Selectors.FbisIntroduction);
    private ILocator KeyInformationSection => page.Locator(Selectors.FbisKeyInformation);
    private ILocator KeyInformationContent => page.Locator(Selectors.KeyInformationContent);
    private ILocator SpendPrioritySection => page.Locator(Selectors.FbisPriorityAreas);
    private ILocator OtherSpendingPrioritiesSection => page.Locator(Selectors.FbisOtherPriorityAreas);
    private ILocator PupilWorkforceMetricsSection => page.Locator(Selectors.FbisPupilWorkforce);
    private ILocator NextStepsSection => page.Locator(Selectors.FbisNextSteps);
    private ILocator PrintPageCta => page.Locator(Selectors.GovButton).GetByText("Print this page");

    private ILocator VisitFbitButton => page.Locator(Selectors.GovButton)
        .GetByText("Visit the Financial Benchmarking and Insights Tool");

    private ILocator TeachingSupportStaff => page.Locator(Selectors.PriorityAreaTeachingSupportStaff);
    private ILocator NonEducationalSupportStaff => page.Locator(Selectors.PriorityAreaNonEducationSupportStaff);
    private ILocator AdministrativeSupplies => page.Locator(Selectors.PriorityAreaAdministrativeSupplies);
    private ILocator Top3SpendingPriorities => page.Locator($"{Selectors.FbisOtherPriorityAreas} .priority-position");
    private ILocator PupilToTeacherMetric => page.Locator("h3:has-text('Pupil-to-teacher metric')");
    private ILocator PupilToSeniorLeadershipRoles => page.Locator("h3:has-text('Pupil-to-senior leadership role metric')");
    private ILocator PupilWorkforceContent => page.Locator(Selectors.PupilWorkforceContent);
    private ILocator WarningMessage => page.Locator(Selectors.GovWarning);

    public async Task IsDisplayed(bool unavailable = false, bool hasRagData = true, bool hasCensusData = true)
    {
        await PageH1Heading.ShouldBeVisible();
        await VisitFbitButton.ShouldBeVisible();

        if (unavailable)
        {
            return;
        }

        await KeyInformationShouldBeVisible();

        if (hasRagData)
        {
            await SpendPrioritySectionShouldBeVisible();
            await AssertSpendPriorityItems();
            await OtherSpendingPrioritiesSectionShouldBeVisible();
            await AssertOtherTopSpendingPriorities();
            Assert.Equal(3, await Top3SpendingPriorities.Count());
            foreach (var priority in await Top3SpendingPriorities.AllAsync())
            {
                await priority.ShouldBeVisible();
            }
        }

        if (hasCensusData)
        {
            await PupilWorkforceMetricsSectionShouldBeVisible();
            await AssertPupilWorkforceMetrics();
        }

        await PrintPageCtaShouldBeVisible();
        await NextStepsSection.ShouldBeVisible();
    }

    public void IsOk()
    {
        Assert.Equal((int)HttpStatusCode.OK, response?.Status);
    }

    public void IsNotFound()
    {
        Assert.Equal((int)HttpStatusCode.NotFound, response?.Status);
    }

    public async Task KeyInformationShouldBeVisible()
    {
        await KeyInformationSection.ShouldBeVisible();
        Assert.Equal(3, await KeyInformationContent.Count());
        foreach (var item in await KeyInformationContent.AllAsync())
        {
            await item.ShouldBeVisible();
        }
    }

    public async Task KeyInformationShouldContain(string name, string value)
    {
        var nameElement = KeyInformationContent.Locator($"p:has-text('{name}')");
        await nameElement.ShouldBeVisible();
        await nameElement.Locator("~ p").ShouldHaveText(value);
    }

    public async Task SpendPrioritySectionShouldBeVisible()
    {
        await SpendPrioritySection.ShouldBeVisible();
    }

    public async Task SpendPrioritySectionShouldContainWarning()
    {
        await SpendPrioritySection.Locator("p").ShouldHaveText("This school does not have comparator data available for this period.");
    }

    public async Task SpendPrioritySectionShouldContain(string name, string tag, string value)
    {
        var nameElement = SpendPrioritySection.Locator($"h3:has-text('{name}')");
        await nameElement.ShouldBeVisible();
        await nameElement.Locator("~ div > div").First.ShouldHaveText($"{tag} {value}");
    }

    private async Task AssertSpendPriorityItems()
    {
        await TeachingSupportStaff.ShouldBeVisible();
        await NonEducationalSupportStaff.ShouldBeVisible();
        await AdministrativeSupplies.ShouldBeVisible();
    }

    public async Task OtherSpendingPrioritiesSectionShouldBeVisible()
    {
        await OtherSpendingPrioritiesSection.ShouldBeVisible();
    }

    public async Task OtherSpendingPrioritiesSectionShouldContainWarning()
    {
        await OtherSpendingPrioritiesSection.Locator("p").ShouldHaveText("This school does not have comparator data available for this period.");
    }

    private async Task AssertOtherTopSpendingPriorities()
    {
        var topSpendingItems = await Top3SpendingPriorities.AllAsync();
        Assert.True(topSpendingItems.Count >= 3, "Expected at least 3 top spending priorities to be visible.");
    }

    public async Task OtherTopSpendingPrioritiesSectionShouldContain(string name, string tag, string value)
    {
        var nameElement = OtherSpendingPrioritiesSection.Locator($"h3:has-text('{name}')");
        await nameElement.ShouldBeVisible();
        await nameElement.Locator("~ div > div").First.ShouldHaveText($"{tag} {value}");
    }

    public async Task PupilWorkforceMetricsSectionShouldBeVisible()
    {
        await PupilWorkforceMetricsSection.ShouldBeVisible();
    }

    public async Task PupilWorkforceMetricsSectionShouldContainWarning()
    {
        await PupilWorkforceMetricsSection.Locator("p").ShouldHaveText("This school does not have workforce data available for this period.");
    }

    private async Task AssertPupilWorkforceMetrics()
    {
        await PupilToTeacherMetric.ShouldBeVisible();
        await PupilToSeniorLeadershipRoles.ShouldBeVisible();
    }

    public async Task PupilWorkforceMetricsSectionShouldContain(string name, string value, string comparison)
    {
        var nameElement = PupilWorkforceContent.Locator($"h3:has-text('{name}')");
        await nameElement.ShouldBeVisible();
        await nameElement.Locator("~ div").ShouldHaveText(value);
        await nameElement.Locator("~ p").ShouldHaveText(comparison);
    }

    public async Task PrintPageCtaShouldBeVisible()
    {
        await PrintPageCta.ShouldBeVisible();
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

    public async Task<BenchmarkCensusPage> ClickCensusLink()
    {
        await PupilWorkforceMetricsSection.Locator("a").ClickAsync();
        return new BenchmarkCensusPage(page);
    }

    public async Task AssertWarningMessage(string commentary)
    {
        await WarningMessage.ShouldContainText(commentary);
    }
}