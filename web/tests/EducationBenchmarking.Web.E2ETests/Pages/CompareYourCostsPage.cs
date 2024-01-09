using FluentAssertions;
using Google.Protobuf;
using Microsoft.Playwright;
using Xunit;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class CompareYourCostsPage
{
    private IPage _page;
    private Task<IDownload> _downloadEvent;

    public CompareYourCostsPage(IPage page)
    {
        _page = page;
    }

    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BreadCrumbs => _page.Locator(".govuk-breadcrumbs");
    private ILocator ChangeSchoolLink => _page.Locator("#change-school");

    private ILocator SaveImageTotalExpenditure =>
        _page.Locator("xpath=//*[@id='compare-your-school']/div[2]/div[2]/button");

    private ILocator TotalExpenditureDimension => _page.Locator("#total-expenditure-dimension");

    private ILocator TotalExpenditureChart =>
        _page.Locator("xpath=//*[@id='compare-your-school']/div[3]/div/div/canvas");

    private ILocator ShowAllBtn => _page.Locator("span.govuk-accordion__show-all-text");

    private ILocator TeachingAndTeachingSupportStaffAccordion =>
        _page.Locator("#accordion-heading-teaching-support-staff");

    private ILocator ViewAsTableBtn => _page.Locator(".govuk-button:has-text('View as table')");

    private ILocator TableContent(string table) =>
        _page.Locator($"h2:has-text(\"{table}\") + div table.govuk-table");

    private ILocator TotalExpenditureTable => _page.Locator("#compare-your-school table.govuk-table").First;


    public async Task AssertPage()
    {
        await PageH1Heading.IsVisibleAsync();
        await BreadCrumbs.IsVisibleAsync();
        await ChangeSchoolLink.IsVisibleAsync();
        await SaveImageTotalExpenditure.IsVisibleAsync();
        await TotalExpenditureDimension.IsVisibleAsync();
        await TotalExpenditureChart.IsVisibleAsync();
        await ShowAllBtn.IsVisibleAsync();
        /*Given I am viewing charts of the Utilities or Premises expenditure category
        When I click on dimensions dropdown
        Then I am presented with a select component which permits the toggling between £ per m², actuals, percentage of expenditure and percentage of income, in that order
        And the initial value displayed in this drop down is £ per m²*/
        //add further asserts for rest of accordions
    }

    public async Task ClickOnSaveImg()
    {
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        _downloadEvent = _page.WaitForDownloadAsync();
        await SaveImageTotalExpenditure.ClickAsync();
    }

    public async Task AssertImageDownload()
    {
        var download = await _downloadEvent;
        string downloadedFilePath = download.SuggestedFilename;
        Assert.Equal("total-expenditure.png", downloadedFilePath);
    }

    public async Task AssertDimension(string value)
    {
        var selectedValue =
            await TotalExpenditureDimension.EvaluateAsync<string>(
                "select => select.options[select.selectedIndex].text");
        Assert.Equal(value, selectedValue.ToString());
    }

    public async Task ChangeDimension(string value)
    {
        await TotalExpenditureDimension.SelectOptionAsync(new SelectOptionValue { Value = value });
        await AssertDimension(value);
    }

    public async Task ClickViewAsTable()
    {
        await ViewAsTableBtn.ClickAsync();
    }

    public async Task CompareTableData(List<List<string>> expectedData)
    {
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      await ShouldHaveTableContent(TotalExpenditureTable, expectedData, true);
    }
    public static async Task ShouldHaveTableContent(ILocator locator, List<List<string>> expectedData,
        bool includeHeaderRow)
    {
        var actualData = new List<List<string>>();

        if (includeHeaderRow)
        {
            var headerCells = await locator.Locator("th").AllAsync();
            var headerData = new List<string>();
            foreach (var cell in headerCells)
            {
                headerData.Add(await cell.InnerTextAsync());
            }
        
            actualData.Add(headerData);
        }
    
        var rows = await locator.Locator("tbody").Locator("tr").AllAsync();
    
        foreach (var row in rows)
        {
            var cells = await row.Locator("td").AllAsync();

            var rowData = new List<string>();
            foreach (var cell in cells)
            {
                rowData.Add(await cell.InnerTextAsync());
            }
        
            actualData.Add(rowData);
        }

        for (int i = 0; i < expectedData.Count; i++)
        {
            var expectedTableCell = expectedData[i];
            var actualTableCell = actualData[i];
        
            for (int j = 0; j < expectedTableCell.Count; j++)
            {
                actualTableCell[j].Should().Be(expectedTableCell[j], "actual table cells should have the expected data");
            }
        }
        
    }

    public async Task CheckSaveCtaVisibility()
    {
       await Assertions.Expect(SaveImageTotalExpenditure).ToBeHiddenAsync();
    }
}