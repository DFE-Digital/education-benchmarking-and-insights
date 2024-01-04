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

    public async Task AssertPage()
    {
        await PageH1Heading.IsVisibleAsync();
        await BreadCrumbs.IsVisibleAsync();
        await ChangeSchoolLink.IsVisibleAsync();
        await SaveImageTotalExpenditure.IsVisibleAsync();
        await TotalExpenditureDimension.IsVisibleAsync();
        await TotalExpenditureChart.IsVisibleAsync();
        await ShowAllBtn.IsVisibleAsync();
        
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
        var download =await _downloadEvent;
        string downloadedFilePath = download.SuggestedFilename;
        Assert.Equal("total-expenditure.png", downloadedFilePath);
    }

    public async Task AssertDimension(string value)
    {
        var selectedValue = await TotalExpenditureDimension.EvaluateAsync<string>("select => select.options[select.selectedIndex].text");
        Assert.Equal(value, selectedValue.ToString());
    }

    public async Task ChangeDimension(string value)
    {
        await TotalExpenditureDimension.SelectOptionAsync(new SelectOptionValue { Value = value });
        await AssertDimension(value);
    }

    public async Task AssertChartUpdate()
    {
       // await Task.Delay(5000);
       var canvasElement =  TotalExpenditureChart;

        if (canvasElement != null)
        {
            var canvasBeforeChange = await _page.EvaluateAsync<byte[]>(
                "canvas => { const ctx = canvas.getContext('2d'); return ctx.getImageData(0, 0, canvas.width, canvas.height).data; }");
            var canvasAfterChange = await _page.EvaluateAsync<byte[]>(
                "canvas => { const ctx = canvas.getContext('2d'); return ctx.getImageData(0, 0, canvas.width, canvas.height).data; }");


            // Assert that the pixel data has changed
            Assert.NotEqual(canvasBeforeChange, canvasAfterChange);
        }
    }
}