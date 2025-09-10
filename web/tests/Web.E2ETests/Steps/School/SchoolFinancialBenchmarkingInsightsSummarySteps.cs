using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School Financial Benchmarking Insights Summary")]
public class SchoolFinancialBenchmarkingInsightsSummarySteps(PageDriver driver)
{
    private const string PrintButtonWaiterFunctionName = "waitForPrintDialog";
    private BenchmarkCensusPage? _censusPage;
    private CompareYourCostsPage? _comparisonPage;
    private SchoolFinancialBenchmarkingInsightsSummaryPage? _fbisPage;
    private HomePage? _homePage;

    [Given("I am on the Financial Benchmarking Insights Summary page for school with urn '(.*)'")]
    public async Task GivenIAmOnTheFinancialBenchmarkingInsightsSummaryPageForSchoolWithUrn(string urn)
    {
        _fbisPage = await LoadFinancialBenchmarkingInsightsSummaryPageForSchoolWithUrn(urn);
        await _fbisPage.IsDisplayed();
    }

    [Given("I am on the Financial Benchmarking Insights Summary page for unavailable school with urn '(.*)'")]
    public async Task GivenIAmOnTheFinancialBenchmarkingInsightsSummaryPageForUnavailableSchoolWithUrn(string urn)
    {
        _fbisPage = await LoadFinancialBenchmarkingInsightsSummaryPageForSchoolWithUrn(urn);
        await _fbisPage.IsDisplayed(true);
    }

    [Given("I am on the Financial Benchmarking Insights Summary page for school with urn '(.*)' with missing RAG and census data")]
    public async Task GivenIAmOnTheFinancialBenchmarkingInsightsSummaryPageForSchoolWithUrnWithMissingRagAndCensusData(string urn)
    {
        _fbisPage = await LoadFinancialBenchmarkingInsightsSummaryPageForSchoolWithUrn(urn);
        await _fbisPage.IsDisplayed(false, false, false);
    }

    [Then("I should see the following boxes displayed under Key Information about school")]
    public async Task ThenIShouldSeeTheFollowingBoxesDisplayedUnderKeyInformationAboutSchool(DataTable table)
    {
        Assert.NotNull(_fbisPage);
        await _fbisPage.KeyInformationShouldBeVisible();

        foreach (var row in table.Rows)
        {
            await _fbisPage.KeyInformationShouldContain(row["Name"], row["Value"]);
        }
    }

    [Then("I should see the following boxes displayed under Spend in priority areas")]
    public async Task ThenIShouldSeeTheFollowingBoxesDisplayedUnderSpendInPriorityAreas(DataTable table)
    {
        Assert.NotNull(_fbisPage);
        await _fbisPage.SpendPrioritySectionShouldBeVisible();

        foreach (var row in table.Rows)
        {
            await _fbisPage.SpendPrioritySectionShouldContain(row["Name"], row["Tag"], row["Value"]);
        }
    }

    [Then("I should see the following top 3 spending priorities for my school under Other top spending priorities")]
    public async Task ThenIShouldSeeTheFollowingTopSpendingPrioritiesForMySchoolUnderOtherTopSpendingPriorities(DataTable table)
    {
        Assert.NotNull(_fbisPage);
        await _fbisPage.OtherSpendingPrioritiesSectionShouldBeVisible();

        foreach (var row in table.Rows)
        {
            await _fbisPage.OtherTopSpendingPrioritiesSectionShouldContain(row["Name"], row["Tag"], row["Value"]);
        }
    }

    [Then("I should see the following boxes displayed under Pupil and workforce metrics")]
    public async Task ThenIShouldSeeTheFollowingBoxesDisplayedUnderPupilAndWorkforceMetrics(DataTable table)
    {
        Assert.NotNull(_fbisPage);
        await _fbisPage.PupilWorkforceMetricsSectionShouldBeVisible();

        foreach (var row in table.Rows)
        {
            await _fbisPage.PupilWorkforceMetricsSectionShouldContain(row["Name"], row["Value"], row["Comparison"]);
        }
    }

    [Then("I should see the warning message under Spend in priority areas")]
    public async Task ThenIShouldSeeTheWarningMessageUnderSpendInPriorityAreas()
    {
        Assert.NotNull(_fbisPage);
        await _fbisPage.SpendPrioritySectionShouldBeVisible();
        await _fbisPage.SpendPrioritySectionShouldContainWarning();
    }

    [Then("I should see the warning message under Other top spending priorities")]
    public async Task ThenIShouldSeeTheWarningMessageUnderOtherTopSpendingPriorities()
    {
        Assert.NotNull(_fbisPage);
        await _fbisPage.OtherSpendingPrioritiesSectionShouldBeVisible();
        await _fbisPage.OtherSpendingPrioritiesSectionShouldContainWarning();
    }

    [Then("I should see the warning message under Pupil and workforce metrics")]
    public async Task ThenIShouldSeeTheWarningMessageUnderPupilAndWorkforceMetrics()
    {
        Assert.NotNull(_fbisPage);
        await _fbisPage.PupilWorkforceMetricsSectionShouldBeVisible();
        await _fbisPage.PupilWorkforceMetricsSectionShouldContainWarning();
    }

    [Then("the print page cta is visible")]
    public async Task ThenThePrintPageCtaIsVisible()
    {
        Assert.NotNull(_fbisPage);
        await _fbisPage.PrintPageCtaShouldBeVisible();
    }

    [When("I click on the 'Print Page' button")]
    public async Task WhenIClickOnTheButton()
    {
        Assert.NotNull(_fbisPage);
        await _fbisPage.ClickPrintPageCta(PrintButtonWaiterFunctionName);
    }

    [Then("the print page dialog should be displayed")]
    public async Task ThenThePrintPageDialogShouldBeDisplayed()
    {
        Assert.NotNull(_fbisPage);
        await _fbisPage.EvaluatePrintPageCta(PrintButtonWaiterFunctionName);
    }

    [When("I click on the financial benchmarking and insight tool link under introduction")]
    public async Task WhenIClickOnTheFinancialBenchmarkingAndInsightToolLinkUnderIntroduction()
    {
        Assert.NotNull(_fbisPage);
        _homePage = await _fbisPage.ClickIntroductionLink();
    }

    [Then("I am directed to school home page for the school with urn '(.*)'")]
    public async Task ThenIAmDirectedToSchoolHomePageForTheSchoolWithUrn(string urn)
    {
        Assert.NotNull(_homePage);
        await _homePage.IsDisplayed();
    }

    [When("I click on the financial benchmarking and insight tool link under key information")]
    public async Task WhenIClickOnTheFinancialBenchmarkingAndInsightToolLinkUnderKeyInformation()
    {
        Assert.NotNull(_fbisPage);
        _comparisonPage = await _fbisPage.ClickKeyInformationLink();
    }

    [Then("I am directed to school comparison page for the school with urn '(.*)'")]
    public async Task ThenIAmDirectedToSchoolComparisonPageForTheSchoolWithUrn(string urn)
    {
        Assert.NotNull(_comparisonPage);
        await _comparisonPage.IsDisplayed();
    }

    [When("I click on the financial benchmarking and insight tool link under pupil and workforce metrics")]
    public async Task WhenIClickOnTheFinancialBenchmarkingAndInsightToolLinkUnderPupilAndWorkforceMetrics()
    {
        Assert.NotNull(_fbisPage);
        _censusPage = await _fbisPage.ClickCensusLink();
        await driver.WaitForPendingRequests(500);
    }

    [Then("I am directed to school census page for the school with urn '(.*)'")]
    public async Task ThenIAmDirectedToSchoolCensusPageForTheSchoolWithUrn(string urn)
    {
        Assert.NotNull(_censusPage);
        await _censusPage.IsDisplayed();
    }

    [Then("the '(.*)' warning message should be displayed")]
    public async Task ThenTheWarningMessageShouldBeDisplayed(string commentary)
    {
        Assert.NotNull(_fbisPage);
        await _fbisPage.AssertWarningMessage(commentary);
    }

    [Then("the response should be OK")]
    public void ThenTheResponseShouldBeOk()
    {
        Assert.NotNull(_fbisPage);
        _fbisPage.IsOk();
    }

    [Then("the response should be NotFound")]
    public void ThenTheResponseShouldBeNotFound()
    {
        Assert.NotNull(_fbisPage);
        _fbisPage.IsNotFound();
    }

    private async Task<SchoolFinancialBenchmarkingInsightsSummaryPage> LoadFinancialBenchmarkingInsightsSummaryPageForSchoolWithUrn(string urn)
    {
        var url = SchoolFinancialBenchmarkingInsightsSummaryUrl(urn);
        var page = await driver.Current;
        var response = await page.GotoAndWaitForLoadAsync(url);

        await driver.WaitForPendingRequests(500);

        return new SchoolFinancialBenchmarkingInsightsSummaryPage(page, response);
    }

    private static string SchoolFinancialBenchmarkingInsightsSummaryUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/summary";
}