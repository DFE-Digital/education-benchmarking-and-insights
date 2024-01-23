using EducationBenchmarking.Web.E2ETests.Pages;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class BenchmarkWorkforceSteps
{
    private readonly IPage _page;
    private readonly BenchmarkWorkforcePage _benchmarkWorkforcePage;

    public BenchmarkWorkforceSteps(IPage page, BenchmarkWorkforcePage benchmarkWorkforcePage)
    {
        _page = page;
        _benchmarkWorkforcePage = benchmarkWorkforcePage;
    }

    [Given("I am on workforce page for school with URN '(.*)'")]
    public async Task GivenIAmOnWorkforcePageForSchoolWithUrn(string urn)
    {
        await _page.GotoAsync($"{Config.BaseUrl}/school/{urn}/workforce");
        await _benchmarkWorkforcePage.AssertPage();
    }

    [When("i click on save as image for school workforce")]
    public async Task WhenIClickOnSaveAsImageForSchoolWorkforce()
    {
        await _benchmarkWorkforcePage.ClickSaveImgBtn("school workforce");
    }

    [Then("school workforce chart image is downloaded")]
    public void ThenSchoolWorkforceChartImageIsDownloaded()
    {
        _benchmarkWorkforcePage.AssertImageDownload("school workforce");
    }

    [When("I change school workforce dimension to '(.*)'")]
    public async Task WhenIChangeSchoolWorkforceDimensionTo(string dimensionValue)
    {
        await _benchmarkWorkforcePage.ChangeDimension("school workforce", dimensionValue);
    }

    [Then("the dimension in school workforce dimension dropdown is '(.*)'")]
    public async Task ThenTheDimensionInSchoolWorkforceDimensionDropdownIs(string dimensionValue)
    {
        await _benchmarkWorkforcePage.AssertDimensionValue("school workforce", dimensionValue);
    }

    [When("I click on view as table on workforce page")]
    [Given("I click on view as table on workforce page")]
    public async Task GivenIClickOnViewAsTableOnWorkforcePage()
    {
        await _benchmarkWorkforcePage.ClickViewAsTable();
    }

    [When("I change Total number of teachers dimension to '(.*)'")]
    public async Task WhenIChangeTotalNumberOfTeachersDimensionTo(string dimensionValue)
    {
        await _benchmarkWorkforcePage.ChangeDimension("total teachers", dimensionValue);
    }

    [Then("the following header in the Total number of teachers table")]
    public async Task ThenTheFollowingHeaderInTheTotalNumberOfTeachersTable(Table expectedData)
    {
        var expectedTableHeaders = new List<List<string>>();
        {
            var headers = expectedData.Header.ToList();

            expectedTableHeaders.Add(headers);
        }
        await _benchmarkWorkforcePage.CheckTableHeaders("total teachers", expectedTableHeaders);
    }

    [Then("the table view is showing on workforce page")]
    public async Task ThenTheTableViewIsShowingOnWorkforcePage()
    {
        await _benchmarkWorkforcePage.AssertTableView();
    }

    [When("I click on view as chart on workforce page")]
    public async Task WhenIClickOnViewAsChartOnWorkforcePage()
    {
        await _benchmarkWorkforcePage.ClickViewAsChart();
    }

    [Then("chart view is showing on workforce page")]
    public async Task ThenChartViewIsShowingOnWorkforcePage()
    {
        await _benchmarkWorkforcePage.AssertChartView();
    }
}