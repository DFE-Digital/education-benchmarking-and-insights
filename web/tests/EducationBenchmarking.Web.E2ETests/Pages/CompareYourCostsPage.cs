using EducationBenchmarking.Web.E2ETests.Helpers;
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

    private ILocator TeachingAndTeachingSupportStaffAccordion =>
        _page.Locator("#accordion-heading-teaching-support-staff");

    private ILocator ViewAsTableBtn => _page.Locator(".govuk-button:has-text('View as table')");

    private ILocator TableContent(string table) =>
        _page.Locator($"h2:has-text(\"{table}\") + div table.govuk-table");

    private ILocator TotalExpenditureTable => _page.Locator("#compare-your-school table.govuk-table").First;
    private ILocator ShowAllSectionsCta => _page.Locator(".govuk-accordion__show-all-text");
    private ILocator Accordions => _page.Locator(".govuk-accordion__section");
    private ILocator AllCharts => _page.Locator(".govuk-accordion__section canvas");
    private ILocator AllTables => _page.Locator(".govuk-accordion__section .govuk-table");
    private ILocator AllSaveImgCtas => _page.Locator(".govuk-accordion__section .govuk-button");

    private ILocator NonEducationalSupportStaffAccordion =>
        _page.Locator("#accordion-heading-non-educational-support-staff");

    private ILocator HideAccordionCta(ILocator accordionLocator) =>
        _page.Locator($"{accordionLocator.ToString()} button:has-text('Hide')");
    /*private ILocator HideAccordionCta(string accordionLocator) =>
        _page.Locator($"{accordionLocator}", new PageLocatorOptions { HasTextString = "Hide" });*/


    public async Task AssertPage()
    {
        await PageH1Heading.ShouldBeVisible();
        await BreadCrumbs.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible();
        await SaveImageTotalExpenditure.ShouldBeVisible();
        await TotalExpenditureDimension.ShouldBeVisible();
        await TotalExpenditureChart.ShouldBeVisible();
        await ShowAllSectionsCta.ShouldBeVisible();
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
        await TotalExpenditureDimension.Select(value);
        await TotalExpenditureDimension.SelectOptionAsync(new SelectOptionValue { Value = value });
        await AssertDimension(value);
    }

    public async Task ClickViewAsTable()
    {
        await ViewAsTableBtn.Click();
    }

    public async Task CompareTableData(List<List<string>> expectedData)
    {
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await TotalExpenditureTable.ShouldHaveTableContent(expectedData, true);
    }

    public async Task CheckSaveCtaVisibility()
    {
        await SaveImageTotalExpenditure.ShouldNotBeVisible();
    }

    public async Task ClickShowAllSectionsCta()
    {
        await ShowAllSectionsCta.Click();
    }

    public async Task AssertAccordionsExpandState()
    {
        var singleAccordion = await Accordions.AllAsync();
        foreach (var accordion in singleAccordion)
        {
            await accordion.AssertLocatorClass("govuk-accordion__section govuk-accordion__section--expanded");
        }
    }

    public async Task AssertShowAllSectionsText(string expectedText)
    {
        var actualText = await ShowAllSectionsCta.InnerTextAsync();
        LocatorAssert.AreEqual(actualText, expectedText);
    }

    public async Task AssertNoChartsAreShowing()
    {
        await AllCharts.ShouldNotBeVisible();
    }

    public async Task AssertTablesAreShowing()
    {
        var singleTable = await AllTables.AllAsync();
        foreach (var table in singleTable)
        {
            await table.ShouldBeVisible();
        }
    }

    public async Task AssertAllImageCtas(bool visibility)
    {
        var chartImages = await AllSaveImgCtas.AllAsync();
        foreach (var chartImage in chartImages)
        {
            if (visibility)
            {
                await AllSaveImgCtas.ShouldBeVisible();
            }
            else
            {
                await AllSaveImgCtas.ShouldNotBeVisible();
            }
        }
    }

    public async Task ClickHideBtn(string accordionName)
    {
        switch (accordionName)
        {
            case "non-educational support staff" :
                await NonEducationalSupportStaffAccordion.Filter(new LocatorFilterOptions { HasText = "Hide" }).Click();
           //  await  HideAccordionCta(NonEducationalSupportStaffAccordion).Click();
                break;
            
        }
    }

    public async Task AssertAccordionState(string accordionName)
    {
        switch (accordionName)
        {
            case "non-educational support staff" :
                await NonEducationalSupportStaffAccordion.AssertLocatorClass("govuk-accordion__section govuk-accordion__section--expanded");
                break;
            
        }
    }
}