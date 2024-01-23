using EducationBenchmarking.Web.E2ETests.Helpers;
using FluentAssertions;
using Microsoft.Playwright;
using Xunit;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class CompareYourCostsPage
{
    private readonly IPage _page;
    private IDownload? _download;

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

    private ILocator ViewAsTableBtn => _page.Locator(".govuk-button:has-text('View as table')");
    private ILocator TotalExpenditureTable => _page.Locator("#compare-your-school table.govuk-table").First;
    private ILocator ShowOrHideAllSectionsCta => _page.Locator(".govuk-accordion__show-all-text");
    private ILocator Accordions => _page.Locator(".govuk-accordion__section");
    private ILocator AllCharts => _page.Locator(".govuk-accordion__section canvas");
    private ILocator AllTables => _page.Locator(".govuk-accordion__section .govuk-table");
    private ILocator AllSaveImgCtas => _page.Locator(".govuk-accordion__section .govuk-button");

    private readonly string _nonEducationalSupportStaffAccordionHeadingId =
        "#accordion-heading-non-educational-support-staff";

    private readonly string _teachingAndTeachingSupportStaffHeadingId = "#accordion-heading-teaching-support-staff";

    private ILocator GetAccordionSectionBtn(string accordionHeadingId) => _page.Locator("button",
        new PageLocatorOptions()
            { Has = _page.Locator($"span{accordionHeadingId}") });

    private readonly string _showHideAccordionTextLocator = ".govuk-accordion__section-toggle-text";

    private ILocator NonEducationSupportStaffAccordionContent =>
        _page.Locator("#accordion-content-non-educational-support-staff");

    private ILocator TeachingSupportStaffAccordionContent => _page.Locator("#accordion-content-teaching-support-staff");
    private ILocator AllAccordions => _page.Locator(".govuk-accordion__section h2 button");

    private ILocator AllAccordionsContent =>
        _page.Locator(".govuk-accordion__section .govuk-accordion__section-content");

    private ILocator PremisesDimensionsDropdown => _page.Locator("#total-premises-staff-service-costs-dimension");

    public async Task AssertPage()
    {
        await PageH1Heading.ShouldBeVisible();
        await BreadCrumbs.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible();
        await SaveImageTotalExpenditure.ShouldBeVisible();
        await TotalExpenditureDimension.ShouldBeVisible();
        await TotalExpenditureChart.ShouldBeVisible();
        await ShowOrHideAllSectionsCta.ShouldBeVisible();
        var expectedOptions = new[] { "£ per m²", "actuals", "percentage of expenditure", "percentage of income" };
        //todo add assertions for utilities dropdown below
        await AssertDropDownDimensions(PremisesDimensionsDropdown, expectedOptions);
    }

    public async Task ClickOnSaveImg()
    {
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var downloadTask = _page.WaitForDownloadAsync();

        await SaveImageTotalExpenditure.ClickAsync();

        _download = await downloadTask;
    }

    public void AssertImageDownload()
    {
        Assert.NotNull(_download);
        
        var downloadedFilePath = _download.SuggestedFilename;
        Assert.True(
            string.Equals("total-expenditure.png", downloadedFilePath, StringComparison.OrdinalIgnoreCase),
            $"Expected file name: total-expenditure.png. Actual: {downloadedFilePath}"
        );
    }

    public async Task AssertDimensionValue(string chartName, string expectedValue)
    {
        await GetChart(chartName).ShouldHaveSelectedOption(expectedValue);
    }

    public async Task ChangeDimension(string chartName, string value)
    {
        await GetChart(chartName).SelectOption(value);
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

    public async Task ClickShowOrHideAllSectionsCta()
    {
        await ShowOrHideAllSectionsCta.Click();
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
        var actualText = await ShowOrHideAllSectionsCta.InnerTextAsync();
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
                await chartImage.ShouldBeVisible();
            }
            else
            {
                await chartImage.ShouldNotBeVisible();
            }
        }
    }

    public async Task ClickHideBtn(string accordionName)
    {
        switch (accordionName)
        {
            case "non-educational support staff":
                await GetAccordionSectionBtn(_nonEducationalSupportStaffAccordionHeadingId)
                    .Locator(_showHideAccordionTextLocator)
                    .First
                    .ClickAsync();
                break;
            case "Teaching and teaching support staff":
                await GetAccordionSectionBtn(_teachingAndTeachingSupportStaffHeadingId)
                    .Locator(_showHideAccordionTextLocator)
                    .First
                    .ClickAsync();
                break;
            default:
                throw new ArgumentException($"Unsupported accordion name: {accordionName}");
        }
    }

    public async Task AssertAccordionState(string accordionName, string expandedState)
    {
        var accordionToAssertHeadingId = GetAccordionHeadingId(accordionName);

        await GetAccordionSectionBtn(accordionToAssertHeadingId)
            .ShouldHaveAttribute("aria-expanded", expandedState);
    }
    
    public async Task AssertAccordionSectionText(string accordionName, string text)
    {
        var accordionToAssertHeadingId = GetAccordionHeadingId(accordionName);

        await GetAccordionSectionBtn(accordionToAssertHeadingId)
            .Locator(_showHideAccordionTextLocator)
            .ShouldHaveText(text);
    }

    public async Task AssertAccordionContentVisibility(string accordionName, bool visibility, string type)
    {
        var accordionToAssert = accordionName switch
        {
            "non-educational support staff" => NonEducationSupportStaffAccordionContent,
            "Teaching and teaching support staff" => TeachingSupportStaffAccordionContent,
            "all accordions" => AllAccordionsContent,
            _ => throw new ArgumentException($"Unsupported accordion name: {accordionName}")
        };

        foreach (var table in await accordionToAssert.Locator(type).AllAsync())
        {
            if (visibility)
            {
                await table.ShouldBeVisible();
            }
            else
            {
                await table.ShouldNotBeVisible();
            }
        }
    }

    public async Task AssertAllAccordionsExpandedState(string expandedState)
    {
        var accordionsExpandedStateBtns = await AllAccordions.AllAsync();
        foreach (var accordionState in accordionsExpandedStateBtns)
        {
            await accordionState
                .ShouldHaveAttribute("aria-expanded", expandedState);
        }
    }

    private ILocator GetChart(string chartName)
    { 
        var chart = chartName switch
        {
            "total expenditure" => TotalExpenditureDimension,
            _ => throw new ArgumentException($"Unsupported chart name: {chartName}")
        };

        return chart;
    }
    
    private string GetAccordionHeadingId(string accordionName)
    {
        var accordionToAssertHeadingId = accordionName switch
        {
            "non-educational support staff" => _nonEducationalSupportStaffAccordionHeadingId,
            "Teaching and teaching support staff" => _teachingAndTeachingSupportStaffHeadingId,
            _ => throw new ArgumentException($"Unsupported accordion name: {accordionName}")
        };
        return accordionToAssertHeadingId;
    }
    
    private async Task AssertDropDownDimensions(ILocator dimensionsDropdown, string[] expectedOptions)
    {
        var actualOptions =
            await dimensionsDropdown.EvaluateAsync<string[]>(
                "(select) => Array.from(select.options).map(option => option.value)");
        Assert.Equal(actualOptions, expectedOptions);
    }
}