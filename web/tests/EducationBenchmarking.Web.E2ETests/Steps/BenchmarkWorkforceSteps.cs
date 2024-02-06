using EducationBenchmarking.Web.E2ETests.Pages;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class BenchmarkWorkforceSteps(BenchmarkWorkforcePage benchmarkWorkforcePage)
{
    [Given("I am on workforce page for school with URN '(.*)'")]
    public async Task GivenIAmOnWorkforcePageForSchoolWithUrn(string urn)
    {
        await benchmarkWorkforcePage.GotToPage(urn);
        await benchmarkWorkforcePage.AssertPage();
    }

    [When("i click on save as image for school workforce")]
    public async Task WhenIClickOnSaveAsImageForSchoolWorkforce()
    {
        await benchmarkWorkforcePage.ClickSaveImgBtn("SchoolWorkforce");
    }

    [Then("school workforce chart image is downloaded")]
    public void ThenSchoolWorkforceChartImageIsDownloaded()
    {
        benchmarkWorkforcePage.AssertImageDownload("SchoolWorkforce");
    }

    [When("I change school workforce dimension to '(.*)'")]
    public async Task WhenIChangeSchoolWorkforceDimensionTo(string dimensionValue)
    {
        await benchmarkWorkforcePage.ChangeDimension("SchoolWorkforce", dimensionValue);
    }

    [Then("the dimension in '(.*)' dimension dropdown is '(.*)'")]
    public async Task ThenTheDimensionInDimensionDropdownIs(string chartName, string dimensionValue)
    {
        await benchmarkWorkforcePage.AssertDimensionValue(chartName, dimensionValue);
    }

    [When("I click on view as table on workforce page")]
    [Given("I click on view as table on workforce page")]
    public async Task GivenIClickOnViewAsTableOnWorkforcePage()
    {
        await benchmarkWorkforcePage.ClickViewAsTable();
    }

    [When("I change Total number of teachers dimension to '(.*)'")]
    public async Task WhenIChangeTotalNumberOfTeachersDimensionTo(string dimensionValue)
    {
        await benchmarkWorkforcePage.ChangeDimension("TotalNumberOfTeacher", dimensionValue);
    }

    [Then("the following header in the Total number of teachers table")]
    public async Task ThenTheFollowingHeaderInTheTotalNumberOfTeachersTable(Table expectedData)
    {
        var expectedTableHeaders = new List<List<string>>();
        {
            var headers = expectedData.Header.ToList();

            expectedTableHeaders.Add(headers);
        }
        await benchmarkWorkforcePage.CheckTableHeaders("TotalNumberOfTeacher", expectedTableHeaders);
    }

    [Then("the table view is showing on workforce page")]
    public async Task ThenTheTableViewIsShowingOnWorkforcePage()
    {
        await benchmarkWorkforcePage.AssertTableView();
    }

    [When("I click on view as chart on workforce page")]
    public async Task WhenIClickOnViewAsChartOnWorkforcePage()
    {
        await benchmarkWorkforcePage.ClickViewAsChart();
    }

    [Then("chart view is showing on workforce page")]
    public async Task ThenChartViewIsShowingOnWorkforcePage()
    {
        await benchmarkWorkforcePage.AssertChartView();
    }

    [When("I click the dimension dropdown for '(.*)'")]
    public async Task WhenIClickTheDimensionDropdownFor(string chartName)
    {
        await benchmarkWorkforcePage.ClickOnDimensionDropdown(chartName);
    }

    [Then("the '(.*)' are showing in the dimension dropdown for '(.*)'")]
    public async Task ThenTheAreShowingInTheDimensionDropdownFor(string[] dropdownOptions, string chartName)
    {
        var chartDimensionLocator = benchmarkWorkforcePage.GetChartDimensionDropdown(chartName);
        await benchmarkWorkforcePage.AssertDropDownDimensions(chartDimensionLocator, dropdownOptions);
    }

    [StepArgumentTransformation]
    public string[] TransformTDropdownOptionsToListOfStrings(string commaSeperatedList)
    {
        return commaSeperatedList.Split(',').Select(option => option.Trim()).ToArray();
    }

    [Given("the chart view is shown on workforce page")]
    public async Task GivenTheChartViewIsShownOnWorkforcePage()
    {
        await benchmarkWorkforcePage.ClickViewAsTable();
    }

    [Then("save image ctas are not visible on workforce page")]
    public async Task ThenSaveImageCtasAreNotVisibleOnWorkforcePage()
    {
        await benchmarkWorkforcePage.AssertAllImageCtas(false);
    }

    [Then("save image ctas are visible on workforce page")]
    public async Task ThenSaveImageCtasAreVisibleOnWorkforcePage()
    {
        await benchmarkWorkforcePage.AssertAllImageCtas(true);
    }
}