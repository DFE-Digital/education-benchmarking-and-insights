using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;
using LandingPage=Web.E2ETests.Pages.HomePage;
namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School Benchmarking Report Cards")]
public class SchoolBenchmarkingReportCardsSteps(PageDriver driver)
{
    private const string PrintButtonWaiterFunctionName = "waitForPrintDialog";
    private SchoolBenchmarkingReportCardsPage? _brcPage;
    private CompareYourCostsPage _comparisonPage;
    private HomePage _homePage;
    private LandingPage _landingPage;

    [Given("I am on the Benchmarking Report Card page for school with urn '(.*)'")]
    public async Task GivenIAmOnTheBenchmarkingReportCardPageForSchoolWithUrn(string urn)
    {
        _brcPage = await LoadBenchmarkingReportCardsPageForSchoolWithUrn(urn);
        await _brcPage.IsDisplayed();
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

    private async Task<SchoolBenchmarkingReportCardsPage> LoadBenchmarkingReportCardsPageForSchoolWithUrn(string urn)
    {
        var url = SchoolBenchmarkingReportCardsUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        await driver.WaitForPendingRequests(500);

        return new SchoolBenchmarkingReportCardsPage(page);
    }

    private static string SchoolBenchmarkingReportCardsUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/benchmarking-report-cards";
}