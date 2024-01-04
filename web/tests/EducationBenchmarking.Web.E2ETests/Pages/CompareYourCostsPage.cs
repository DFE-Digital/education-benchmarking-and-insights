using Microsoft.Playwright;
using Xunit;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class CompareYourCostsPage
{
    private IPage _page;

    public CompareYourCostsPage(IPage page)
    {
        _page = page;
    }

    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator BreadCrumbs => _page.Locator(".govuk-breadcrumbs");
    private ILocator ChangeSchoolLink => _page.Locator("#change-school");
    private ILocator SaveImageTotalExpenditure => _page.Locator("xpath=//*[@id='compare-your-school']/div[2]/div[2]/button");
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
      await  SaveImageTotalExpenditure.IsVisibleAsync();
      await TotalExpenditureDimension.IsVisibleAsync();
      await TotalExpenditureChart.IsVisibleAsync();
        //check total dimesion defualt value
        await ShowAllBtn.IsVisibleAsync();
    }

    public async Task ClickOnSaveImg()
    {
        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
       
       // await Task.Delay(7000);
        await SaveImageTotalExpenditure.ClickAsync();
        
    }

    public async Task AssertImageDownload()
    {
            var downloadEvent = await _page.WaitForDownloadAsync();
            await SaveImageTotalExpenditure.ClickAsync();
            string downloadedFilePath = downloadEvent.SuggestedFilename;
            Assert.EndsWith(".png", downloadedFilePath, StringComparison.OrdinalIgnoreCase);
    }

}