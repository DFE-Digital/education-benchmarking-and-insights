﻿using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;
namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School homepage")]
public class HomeSteps(PageDriver driver)
{
    private BenchmarkCensusPage? _benchmarkCensusPage;
    private CommercialResourcesPage? _commercialResourcesPage;
    private CompareYourCostsPage? _compareYourCostsPage;
    private CurriculumFinancialPlanningPage? _curriculumAndFinancialPlanningPage;
    private SchoolFinancialBenchmarkingInsightsSummaryPage? _financialBenchmarkingInsightsSummaryPage;
    private HistoricDataPage? _historicDataPage;
    private DetailsPage? _schoolDetailsPage;
    private HomePage? _schoolHomePage;
    private SpendingCostsPage? _spendingCostsPage;

    [Given("I am on school homepage for school with urn '(.*)'")]
    public async Task GivenIAmOnSchoolHomepageForSchoolWithUrn(string urn)
    {
        var url = SchoolHomeUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _schoolHomePage = new HomePage(page);
        await _schoolHomePage.IsDisplayed();
    }

    [Given("I am on part year school homepage for school with urn '(.*)'")]
    public async Task GivenIAmOnPartYearSchoolHomepageForSchoolWithUrn(string urn)
    {
        var url = SchoolHomeUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _schoolHomePage = new HomePage(page);
        await _schoolHomePage.IsDisplayed(true);
    }

    [Given("I am on academy homepage for school with urn '(.*)' and trust name '(.*)'")]
    public async Task GivenIAmOnAcademyHomepageForSchoolWithUrnAndTrustName(string urn, string trustName)
    {
        var url = SchoolHomeUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _schoolHomePage = new HomePage(page);
        await _schoolHomePage.IsDisplayed(trustName: trustName);
    }

    [When("I click on school details")]
    public async Task WhenIClickOnSchoolDetails()
    {
        Assert.NotNull(_schoolHomePage);
        _schoolDetailsPage = await _schoolHomePage.ClickSchoolDetails();
    }

    [Then("the school details page is displayed")]
    public async Task ThenTheSchoolDetailsPageIsDisplayed()
    {
        Assert.NotNull(_schoolDetailsPage);
        await _schoolDetailsPage.IsDisplayed();
    }

    [When("I click on compare your costs")]
    public async Task WhenIClickOnCompareYourCosts()
    {
        Assert.NotNull(_schoolHomePage);
        _compareYourCostsPage = await _schoolHomePage.ClickCompareYourCosts();
    }

    [Then("the compare your costs page is displayed")]
    public async Task ThenTheCompareYourCostsPageIsDisplayed()
    {
        Assert.NotNull(_compareYourCostsPage);
        await _compareYourCostsPage.IsDisplayed();
    }

    [Then("the compare your costs page is displayed for part year")]
    public async Task ThenTheCompareYourCostsPageIsDisplayedForPartYear()
    {
        Assert.NotNull(_compareYourCostsPage);
        await _compareYourCostsPage.IsDisplayed(true);
    }

    [When("I click on curriculum and financial planning")]
    public async Task WhenIClickOnCurriculumAndFinancialPlanning()
    {
        Assert.NotNull(_schoolHomePage);
        _curriculumAndFinancialPlanningPage = await _schoolHomePage.ClickFinancialPlanning();
    }

    [Then("the curriculum and financial planning page is displayed")]
    public async Task ThenTheCurriculumAndFinancialPlanningPageIsDisplayedAfterLoggingInWithOrganisation()
    {
        Assert.NotNull(_curriculumAndFinancialPlanningPage);
        await _curriculumAndFinancialPlanningPage.IsDisplayed();
    }

    [When("I click on benchmark census data")]
    public async Task WhenIClickOnBenchmarkCensusData()
    {
        Assert.NotNull(_schoolHomePage);
        _benchmarkCensusPage = await _schoolHomePage.ClickBenchmarkCensus();
    }

    [Then("the benchmark census page is displayed")]
    public async Task ThenTheBenchmarkCensusPageIsDisplayed()
    {
        Assert.NotNull(_benchmarkCensusPage);
        await _benchmarkCensusPage.IsDisplayed();
    }

    [When("I click on Financial Benchmarking Insights Summary")]
    public async Task WhenIClickOnFinancialBenchmarkingInsightsSummary()
    {
        Assert.NotNull(_schoolHomePage);
        _financialBenchmarkingInsightsSummaryPage = await _schoolHomePage.ClickFinancialBenchmarkingInsightsSummary();
    }

    [Then("the Financial Benchmarking Insights Summary page is displayed")]
    public async Task ThenTheFinancialBenchmarkingInsightsSummaryPageIsDisplayed()
    {
        Assert.NotNull(_financialBenchmarkingInsightsSummaryPage);
        await _financialBenchmarkingInsightsSummaryPage.IsDisplayed();
    }

    [When("I click on view all spending and costs")]
    public async Task WhenIClickOnViewAllSpendingAndCosts()
    {
        Assert.NotNull(_schoolHomePage);
        _spendingCostsPage = await _schoolHomePage.ClickSpendingAndCosts();
    }


    [Then("the spending and costs page is displayed")]
    public async Task ThenTheSpendingAndCostsPageIsDisplayed()
    {
        Assert.NotNull(_spendingCostsPage);
        await _spendingCostsPage.IsDisplayed();
    }
    private static string SchoolHomeUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}";

    [When("I click on find ways to spend less")]
    public async Task WhenIClickOnFindWaysToSpendLess()
    {
        Assert.NotNull(_schoolHomePage);
        _commercialResourcesPage = await _schoolHomePage.ClickFindWaysToSpendLess();

    }

    [Then("the commercial resources page is displayed")]
    public async Task ThenTheCommercialResourcesPageIsDisplayed()
    {
        Assert.NotNull(_commercialResourcesPage);
        await _commercialResourcesPage.IsDisplayed();
    }

    [When("I click on view historic data")]
    public async Task WhenIClickOnViewHistoricData()
    {
        Assert.NotNull(_schoolHomePage);
        _historicDataPage = await _schoolHomePage.ClickHistoricData();
    }

    [Then("the historic data page is displayed")]
    public async Task ThenTheHistoricDataPageIsDisplayed()
    {
        Assert.NotNull(_historicDataPage);
        await _historicDataPage.IsDisplayed();
    }

    [Then("the RAG commentary for each priority category is")]
    public async Task ThenTheRagCommentaryForEachPriorityCategoryIs(DataTable table)
    {
        Assert.NotNull(_schoolHomePage);
        foreach (var row in table.Rows)
        {
            await _schoolHomePage.AssertRagCommentary(row["Name"], row["Commentary"]);
        }
    }

    [Then("the RAG guidance is visible")]
    public async Task ThenTheRagGuidanceIsVisible()
    {
        Assert.NotNull(_schoolHomePage);
        await _schoolHomePage.AssertRagGuidance();
    }

    [Then("the service banner displays the title '(.*)', heading '(.*)' and body '(.*)'")]
    public async Task ThenTheServiceBannerDisplaysTheTitleHeadingAndBody(string title, string heading, string body)
    {
        Assert.NotNull(_schoolHomePage);
        await _schoolHomePage.HasBanner(title, heading, body);
    }
}