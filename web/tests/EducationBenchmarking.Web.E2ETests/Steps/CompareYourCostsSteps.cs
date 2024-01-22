using EducationBenchmarking.Web.E2ETests.Pages;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class CompareYourCostsSteps
{
    private readonly IPage _page;
    private readonly CompareYourCostsPage _compareYourCostsPage;
    private readonly ITestOutputHelper _output;

    public CompareYourCostsSteps(IPage page, CompareYourCostsPage compareYourCostsPage, ITestOutputHelper output)
    {
        _page = page;
#if DEBUG
        _page.Response += (sender, r) => output.WriteLine($"{r.Request.Method} {r.Url} [{r.Status}]");
#endif 
        _compareYourCostsPage = compareYourCostsPage;
        _output = output;
    }

    [Then(@"I am taken to compare your costs page")]
    public void ThenIAmTakenToCompareYourCostsPage()
    {
        _page.WaitForURLAsync(Config.BaseUrl + "/school/*/comparison");
    }

    [Given(@"I am on compare your costs page")]
    public async Task GivenIAmOnCompareYourCostsPage()
    {
        await _compareYourCostsPage.AssertPage();
    }

    [When(@"i click on save as image for total expenditure")]
    public async Task WhenIClickOnSaveAsImageForTotalExpenditure()
    {
        await _compareYourCostsPage.ClickOnSaveImg();
    }

    [Then(@"chart image is downloaded")]
    public void ThenChartImageIsDownloaded()
    {
        _compareYourCostsPage.AssertImageDownload();
    }

    [Given(@"the dimension in dimension dropdown is '(.*)'")]
    [Then(@"the dimension in dimension dropdown is '(.*)'")]
    public async Task GivenTheDimensionInDimensionDropdownIs(string dimension)
    {
        await _compareYourCostsPage.AssertDimension(dimension);
    }

    [When(@"I change total expenditure dimension to '(.*)'")]
    public async Task WhenIChangeTotalExpenditureDimensionTo(string dimension)
    {
        await _compareYourCostsPage.ChangeDimension(dimension);
    }

    [Given(@"I am on compare your costs page for school with URN '(.*)'")]
    public async Task GivenIAmOnCompareYourCostsPageForSchoolWithUrn(string urn)
    {
        await _page.GotoAsync($"{Config.BaseUrl}/school/{urn}/comparison");
        await _compareYourCostsPage.AssertPage();
    }

    [Then(@"all accordions are showing table view")]
    public async Task ThenAllAccordionsAreShowingTableView()
    {
        await _compareYourCostsPage.AssertTablesAreShowing();
        await _compareYourCostsPage.AssertNoChartsAreShowing();
    }

    [Given(@"I click on view as table")]
    [When(@"I click on view as table")]
    public async Task GivenIClickOnViewAsTable()
    {
        await _compareYourCostsPage.ClickViewAsTable();
    }

    [Then(@"the following is showing in the Total expenditure")]
    public async Task ThenTheFollowingIsShowingInTheTotalExpenditure(Table expectedData)
    {
        List<List<string>> expectedTableContent = new List<List<string>>();
        {
            List<string> headers = new List<string>();
            foreach (var header in expectedData.Header)
            {
                headers.Add(header);
            }

            expectedTableContent.Add(headers);
            foreach (var row in expectedData.Rows)
            {
                List<string> rowData = new List<string>();
                foreach (var cell in row)
                {
                    rowData.Add(cell.Value);
                }

                expectedTableContent.Add(rowData);
            }
        }
        await _compareYourCostsPage.CompareTableData(expectedTableContent);
    }

    [Then(@"Save as image CTA is not showing")]
    public async Task ThenSaveAsImageCtaIsNotShowing()
    {
        await _compareYourCostsPage.CheckSaveCtaVisibility();
    }

    [Given(@"I click on Show all sections")]
    [When(@"I click on Show all sections")]
    public async Task WhenIClickOnShowAllSections()
    {
        await _compareYourCostsPage.ClickShowOrHideAllSectionsCta();
    }

    [Then(@"all accordions on the page are expanded")]
    public async Task ThenAllAccordionsOnThePageAreExpanded()
    {
        await _compareYourCostsPage.AssertAccordionsExpandState();
    }

    [Then(@"the text of cta changes to hide all sections")]
    public async Task ThenTheTextOfCtaChangesToHideAllSections()
    {
        await _compareYourCostsPage.AssertShowAllSectionsText("Hide all sections");
    }

    [Then(@"Save as image CTAs are not visible")]
    public async Task ThenSaveAsImageCtAsAreNotVisible()
    {
        await _compareYourCostsPage.AssertAllImageCtas(false);
    }

    [When(@"I click hide for non educational support staff")]
    public async Task WhenIClickHideForNonEducationalSupportStaff()
    {
        await _compareYourCostsPage.AssertAccordionSectionText("non-educational support staff", "Hide");
        await _compareYourCostsPage.AssertAccordionState("non-educational support staff", "true");
        await _compareYourCostsPage.AssertAccordionContentVisibility("non-educational support staff", true, "table");
        await _compareYourCostsPage.ClickHideBtn("non-educational support staff");
    }

    [Then(@"the accordion non educational support staff is collapsed")]
    public async Task ThenTheAccordionNonEducationalSupportStaffIsCollapsed()
    {
        await _compareYourCostsPage.AssertAccordionState("non-educational support staff", "false");
        await _compareYourCostsPage.AssertAccordionSectionText("non-educational support staff", "Show");
        await _compareYourCostsPage.AssertAccordionContentVisibility("non-educational support staff", false, "table");
    }

    [When(@"I click hide for teaching and teaching support staff")]
    public async Task WhenIClickHideForTeachingAndTeachingSupportStaff()
    {
        await _compareYourCostsPage.ClickHideBtn("Teaching and teaching support staff");
    }


    [Then(@"the accordion teaching and teaching support staff is collapsed")]
    public async Task ThenTheAccordionTeachingAndTeachingSupportStaffIsCollapsed()
    {
        await _compareYourCostsPage.AssertAccordionState("Teaching and teaching support staff", "false");
        await _compareYourCostsPage.AssertAccordionSectionText("Teaching and teaching support staff", "Show");
        await _compareYourCostsPage.AssertAccordionContentVisibility("Teaching and teaching support staff", false,
            "canvas");
    }

    [When(@"I click on Hide all sections")]
    public async Task WhenIClickOnHideAllSections()
    {
        await _compareYourCostsPage.ClickShowOrHideAllSectionsCta();
    }

    [Then(@"all accordions on the page are collapsed in charts view")]
    public async Task ThenAllAccordionsOnThePageAreCollapsedInChartsView()
    {
        await _compareYourCostsPage.AssertAllAccordionsExpandedState("false");
        await _compareYourCostsPage.AssertAccordionContentVisibility("all accordions", false, "canvas");
    }

    [Then(@"all accordions on the page are collapsed in table view")]
    public async Task ThenAllAccordionsOnThePageAreCollapsedInTableView()
    {
        await _compareYourCostsPage.AssertAllAccordionsExpandedState("false");
        await _compareYourCostsPage.AssertAccordionContentVisibility("all accordions", false, "canvas");
    }
}