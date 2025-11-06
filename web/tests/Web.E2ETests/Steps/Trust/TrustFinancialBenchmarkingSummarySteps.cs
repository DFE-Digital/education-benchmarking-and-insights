using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Xunit;

namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust Financial Benchmarking Insights Summary")]
public class TrustFinancialBenchmarkingInsightsSummarySteps(PageDriver driver)
{
    private TrustFinancialBenchmarkingInsightsSummaryPage? _trustFinancialBenchmarkingInsightsSummaryPage;
    private HomePage? _trustHomePage;
    private SpendingCostsPage? _trustSpendingCostsPage;
    private Web.E2ETests.Pages.School.SpendingCostsPage? _schoolSpendingCostsPage;



    [Given("I am on the trust insights page for company number '(.*)'")]
    public async Task GivenIAmOnTheTrustInsightsPageForCompanyNumber(string companyNumber)
    {
        var url = TrustFinancialBenchmarkingInsightsSummaryUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _trustFinancialBenchmarkingInsightsSummaryPage = new TrustFinancialBenchmarkingInsightsSummaryPage(page);
    }

    [Then("the page should display correctly")]
    public async Task ThenThePageShouldDisplayCorrectly()
    {
        Assert.NotNull(_trustFinancialBenchmarkingInsightsSummaryPage);
        await _trustFinancialBenchmarkingInsightsSummaryPage.IsDisplayed();
    }


    [When("I click the View more insights in FBIT link")]
    public async Task WhenIClickTheViewMoreInsightsInFbitLink()
    {
        Assert.NotNull(_trustFinancialBenchmarkingInsightsSummaryPage);
        _trustHomePage = await _trustFinancialBenchmarkingInsightsSummaryPage.ClickTheViewMoreInsightsInFbitLink();
    }

    [Then("I am taken to the correct Trust home page")]
    public async Task ThenIAmTakenToTheCorrectTrustHomePage()
    {
        Assert.NotNull(_trustHomePage);
        await _trustHomePage.IsDisplayed();
    }

    [When("I click the View all spending priorities at this trust")]
    public async Task WhenIClickTheViewAllSpendingPrioritiesAtThisTrust()
    {
        Assert.NotNull(_trustFinancialBenchmarkingInsightsSummaryPage);
        _trustSpendingCostsPage = await _trustFinancialBenchmarkingInsightsSummaryPage.ClickViewAllSpendingPrioritiesLink();
    }

    [Then("I am taken to the correct Trust spending and costs page")]
    public async Task ThenIAmTakenToTheCorrectTrustSpendingAndCostsPage()
    {
        Assert.NotNull(_trustSpendingCostsPage);
        await _trustSpendingCostsPage.IsDisplayed();
    }

    [When("I click a link for a spending priorities count")]
    public async Task WhenIClickEachLinkForSpendingPrioritiesCounts()
    {
        Assert.NotNull(_trustFinancialBenchmarkingInsightsSummaryPage);
        _schoolSpendingCostsPage = await _trustFinancialBenchmarkingInsightsSummaryPage.ClickFirstPriorityCountLink();
    }

    [Then("I am taken to the correct school spending and costs page")]
    public async Task ThenIAmTakenToTheCorrectSchoolSpendingAndCostsPage()
    {
        Assert.NotNull(_schoolSpendingCostsPage);
        await _schoolSpendingCostsPage.IsDisplayed();
    }

    [When("I click the View more information about schools at this trust link")]
    public async Task WhenIClickTheViewMoreInformationAboutSchoolsAtThisTrustLink()
    {
        Assert.NotNull(_trustFinancialBenchmarkingInsightsSummaryPage);
        _trustHomePage = await _trustFinancialBenchmarkingInsightsSummaryPage.ClickViewMoreInformationAboutSchoolsLink();
    }

    private static string TrustFinancialBenchmarkingInsightsSummaryUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}/summary";
}