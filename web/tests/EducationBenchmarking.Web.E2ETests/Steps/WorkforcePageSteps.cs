using EducationBenchmarking.Web.E2ETests.Pages;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class WorkforcePageSteps
{
    private readonly IPage _page;
    private readonly WorkforcePage _workforcePage;

    public WorkforcePageSteps(IPage page, WorkforcePage workforcePage)
    {
        _page = page;
        _workforcePage = workforcePage;
    }

    [Given(@"I am on workforce page for school with URN '(.*)'")]
    public async Task GivenIAmOnWorkforcePageForSchoolWithUrn(string urn)
    {
        await _page.GotoAsync($"{Config.BaseUrl}/school/{urn}/workforce");
        await _workforcePage.AssertPage();
    }

    [When(@"i click on save as image for school workforce")]
    public async Task WhenIClickOnSaveAsImageForSchoolWorkforce()
    {
        await _workforcePage.ClickSaveImgBtn("school workforce");
    }

    [Then(@"school workforce chart image is downloaded")]
    public void ThenSchoolWorkforceChartImageIsDownloaded()
    {
        _workforcePage.AssertImageDownload("school workforce");
    }

    [When(@"I change school workforce dimension to '(.*)'")]
    public async Task WhenIChangeSchoolWorkforceDimensionTo(string dimensionValue)
    {
        await _workforcePage.ChangeDimension("school workforce", dimensionValue);
    }

    [Then(@"the dimension in school workforce dimension dropdown is '(.*)'")]
    public async Task ThenTheDimensionInSchoolWorkforceDimensionDropdownIs(string dimensionValue)
    {
        await _workforcePage.AssertDimensionValue("school workforce", dimensionValue);
    }

    [When(@"I click on view as table on workforce page")]
    [Given(@"I click on view as table on workforce page")]
    public async Task GivenIClickOnViewAsTableOnWorkforcePage()
    {
        await _workforcePage.ClickViewAsTable();
    }

    [When(@"I change Total number of teachers dimension to '(.*)'")]
    public async Task WhenIChangeTotalNumberOfTeachersDimensionTo(string dimensionValue)
    {
        await _workforcePage.ChangeDimension("total teachers", dimensionValue);
    }

    [Then(@"the following header in the Total number of teachers table")]
    public async Task ThenTheFollowingHeaderInTheTotalNumberOfTeachersTable(Table expectedData)
    {
        List<List<string>> expectedTableHeaders = new List<List<string>>();
        {
            List<string> headers = new List<string>();
            foreach (var header in expectedData.Header)
            {
                headers.Add(header);
            }

            expectedTableHeaders.Add(headers);
        }
        await _workforcePage.CheckTableHeaders("total teachers", expectedTableHeaders);
    }

    [Then(@"the table view is showing on workforce page")]
    public async Task ThenTheTableViewIsShowingOnWorkforcePage()
    {
        await _workforcePage.AssertTableView();
    }

    [When(@"I click on view as chart on workforce page")]
    public async Task WhenIClickOnViewAsChartOnWorkforcePage()
    {
        await _workforcePage.ClickViewAsChart();
    }

    [Then(@"chart view is showing on workforce page")]
    public async Task ThenChartViewIsShowingOnWorkforcePage()
    {
        await _workforcePage.AssertChartView();
    }
}