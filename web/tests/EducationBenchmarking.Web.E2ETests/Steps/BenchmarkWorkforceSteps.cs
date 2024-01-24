using EducationBenchmarking.Web.E2ETests.Pages;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class BenchmarkWorkforceSteps
{
    private readonly BenchmarkWorkforcePage _benchmarkWorkforcePage;

    public BenchmarkWorkforceSteps(BenchmarkWorkforcePage benchmarkWorkforcePage)
    {
        _benchmarkWorkforcePage = benchmarkWorkforcePage;
    }

    [Given("I am on workforce page for school with URN '(.*)'")]
    public async Task GivenIAmOnWorkforcePageForSchoolWithUrn(string urn)
    {
        await _benchmarkWorkforcePage.GotToPage(urn);
        await _benchmarkWorkforcePage.AssertPage();
    }

    [When("i click on save as image for school workforce")]
    public async Task WhenIClickOnSaveAsImageForSchoolWorkforce()
    {
        await _benchmarkWorkforcePage.ClickSaveImgBtn("SchoolWorkforce");
    }

    [Then("school workforce chart image is downloaded")]
    public void ThenSchoolWorkforceChartImageIsDownloaded()
    {
        _benchmarkWorkforcePage.AssertImageDownload("SchoolWorkforce");
    }

    [When("I change school workforce dimension to '(.*)'")]
    public async Task WhenIChangeSchoolWorkforceDimensionTo(string dimensionValue)
    {
        await _benchmarkWorkforcePage.ChangeDimension("SchoolWorkforce", dimensionValue);
    }

    [Then(@"the dimension in '(.*)' dimension dropdown is '(.*)'")]
    public async Task ThenTheDimensionInDimensionDropdownIs(string chartName, string dimensionValue)
    {
        await _benchmarkWorkforcePage.AssertDimensionValue(chartName, dimensionValue);
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
        await _benchmarkWorkforcePage.ChangeDimension("TotalNumberOfTeacher", dimensionValue);
    }

    [Then("the following header in the Total number of teachers table")]
    public async Task ThenTheFollowingHeaderInTheTotalNumberOfTeachersTable(Table expectedData)
    {
        var expectedTableHeaders = new List<List<string>>();
        {
            var headers = expectedData.Header.ToList();

            expectedTableHeaders.Add(headers);
        }
        await _benchmarkWorkforcePage.CheckTableHeaders("TotalNumberOfTeacher", expectedTableHeaders);
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
    
    [When("I click the dimension dropdown for '(.*)'")]
    public async Task WhenIClickTheDimensionDropdownFor(string chartName)
    {
        await _benchmarkWorkforcePage.ClickOnDimensionDropdown(chartName);
    }
    
    [Then("the '(.*)' are showing in the dimension dropdown for '(.*)'")]
    public async Task ThenTheAreShowingInTheDimensionDropdownFor(string dropdownOptions, string chartName)
    {
        var expectedOptions = dropdownOptions.Split(", ").Select(option => option.Trim()).ToArray();
        var chartDimensionLocator = _benchmarkWorkforcePage.GetChartDimensionDropdown(chartName);
       await _benchmarkWorkforcePage.AssertDropDownDimensions(chartDimensionLocator, expectedOptions);
    }
    
}