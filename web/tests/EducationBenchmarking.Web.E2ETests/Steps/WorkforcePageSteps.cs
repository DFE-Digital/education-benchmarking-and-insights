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
}