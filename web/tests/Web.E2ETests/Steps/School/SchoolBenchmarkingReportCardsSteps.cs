using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;
namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School Benchmarking Report Cards")]
public class SchoolBenchmarkingReportCardsSteps(PageDriver driver)
{
    private const string PrintButtonWaiterFunctionName = "waitForPrintDialog";
    private SchoolBenchmarkingReportCardsPage? _brcPage;
    private BenchmarkCensusPage? _censusPage;
    private CompareYourCostsPage? _comparisonPage;
    private HomePage? _homePage;

    [Given("I am on the Benchmarking Report Card page for school with urn '(.*)'")]
    public async Task GivenIAmOnTheBenchmarkingReportCardPageForSchoolWithUrn(string urn)
    {
        _brcPage = await LoadBenchmarkingReportCardsPageForSchoolWithUrn(urn);
        await _brcPage.IsDisplayed();
    }

    [Given("I am on the Benchmarking Report Card page for unavailable school with urn '(.*)'")]
    public async Task GivenIAmOnTheBenchmarkingReportCardPageForUnavailableSchoolWithUrn(string urn)
    {
        _brcPage = await LoadBenchmarkingReportCardsPageForSchoolWithUrn(urn);
        await _brcPage.IsDisplayed(true);
    }

    [Then("I should see the following boxes displayed under Key Information about school")]
    public async Task ThenIShouldSeeTheFollowingBoxesDisplayedUnderKeyInformationAboutSchool(DataTable table)
    {
        Assert.NotNull(_brcPage);
        await _brcPage.KeyInformationShouldBeVisible();

        foreach (var row in table.Rows)
        {
            await _brcPage.KeyInformationShouldContain(row["Name"], row["Value"]);
        }
    }

    [Then("I should see the following boxes displayed under Spend in priority areas")]
    public async Task ThenIShouldSeeTheFollowingBoxesDisplayedUnderSpendInPriorityAreas(DataTable table)
    {
        Assert.NotNull(_brcPage);
        await _brcPage.SpendPrioritySectionShouldBeVisible();

        foreach (var row in table.Rows)
        {
            await _brcPage.SpendPrioritySectionShouldContain(row["Name"], row["Tag"], row["Value"]);
        }
    }

    [Then("I should see the following top 3 spending priorities for my school under Other top spending priorities")]
    public async Task ThenIShouldSeeTheFollowingTopSpendingPrioritiesForMySchoolUnderOtherTopSpendingPriorities(DataTable table)
    {
        Assert.NotNull(_brcPage);
        await _brcPage.OtherSpendingPrioritiesSectionShouldBeVisible();

        foreach (var row in table.Rows)
        {
            await _brcPage.OtherTopSpendingPrioritiesSectionShouldContain(row["Name"], row["Tag"], row["Value"]);
        }
    }

    [Then("I should see the following boxes displayed under Pupil and workforce metrics")]
    public async Task ThenIShouldSeeTheFollowingBoxesDisplayedUnderPupilAndWorkforceMetrics(DataTable table)
    {
        Assert.NotNull(_brcPage);
        await _brcPage.PupilWorkforceMetricsSectionShouldBeVisible();

        foreach (var row in table.Rows)
        {
            await _brcPage.PupilWorkforceMetricsSectionShouldContain(row["Name"], row["Value"], row["Comparison"]);
        }
    }

    [Then("the print page cta is visible")]
    public async Task ThenThePrintPageCtaIsVisible()
    {
        Assert.NotNull(_brcPage);
        await _brcPage.PrintPageCtaShouldBeVisible();
    }

    [When("I click on the 'Print Page' button")]
    public async Task WhenIClickOnTheButton()
    {
        Assert.NotNull(_brcPage);
        await _brcPage.ClickPrintPageCta(PrintButtonWaiterFunctionName);
    }

    [Then("the print page dialog should be displayed")]
    public async Task ThenThePrintPageDialogShouldBeDisplayed()
    {
        Assert.NotNull(_brcPage);
        await _brcPage.EvaluatePrintPageCta(PrintButtonWaiterFunctionName);
    }

    [When("I click on the financial benchmarking and insight tool link under introduction")]
    public async Task WhenIClickOnTheFinancialBenchmarkingAndInsightToolLinkUnderIntroduction()
    {
        Assert.NotNull(_brcPage);
        _homePage = await _brcPage.ClickIntroductionLink();
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
        Assert.NotNull(_brcPage);
        _comparisonPage = await _brcPage.ClickKeyInformationLink();
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
        Assert.NotNull(_brcPage);
        _censusPage = await _brcPage.ClickCensusLink();
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
        Assert.NotNull(_brcPage);
        await _brcPage.AssertWarningMessage(commentary);
    }

    [Then("the response should be OK")]
    public void ThenTheResponseShouldBeOk()
    {
        Assert.NotNull(_brcPage);
        _brcPage.IsOk();
    }

    [Then("the response should be NotFound")]
    public void ThenTheResponseShouldBeNotFound()
    {
        Assert.NotNull(_brcPage);
        _brcPage.IsNotFound();
    }

    private async Task<SchoolBenchmarkingReportCardsPage> LoadBenchmarkingReportCardsPageForSchoolWithUrn(string urn)
    {
        var url = SchoolBenchmarkingReportCardsUrl(urn);
        var page = await driver.Current;
        var response = await page.GotoAndWaitForLoadAsync(url);

        await driver.WaitForPendingRequests(500);

        return new SchoolBenchmarkingReportCardsPage(page, response);
    }

    private static string SchoolBenchmarkingReportCardsUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/benchmarking-report-cards";
}