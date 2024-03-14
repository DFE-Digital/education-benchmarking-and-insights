using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;

public enum ComparisonChartNames
{
    Premises,
    TotalExpenditure,
    NonEducationalSupportStaff
}

public class CompareYourCostsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator Breadcrumbs => page.Locator(Selectors.GovBreadcrumbs);
    private ILocator ChangeSchoolLink => page.Locator(Selectors.ChangeSchoolLink);
    private ILocator SaveImageTotalExpenditure => page.Locator(Selectors.TotalExpenditureSaveAsImage);
    private ILocator TotalExpenditureDimension => page.Locator(Selectors.TotalExpenditureDimension);
    private ILocator TotalExpenditureChart => page.Locator(Selectors.TotalExpenditureChart);
    private ILocator ViewAsTableRadio => page.Locator(Selectors.ModeTable);
    private ILocator ViewAsChartRadio => page.Locator(Selectors.ModeChart);
    private ILocator TotalExpenditureTable => page.Locator(Selectors.ComparisonTables).First;
    private ILocator ShowHideAllSectionsLink => page.Locator(Selectors.GovShowAllLinkText);
    private ILocator Sections => page.Locator(Selectors.GovAccordionSection);
    private ILocator Tables => page.Locator(Selectors.SectionTable);
    private ILocator NonEducationSupportStaffAccordionContent => page.Locator(Selectors.SectionContentTwo);
    private ILocator PremisesDimension => page.Locator(Selectors.PremisesDimension);

    private ILocator SaveAsImageButtons =>
        page.Locator(Selectors.Button, new PageLocatorOptions { HasText = "Save as image" });
    private ILocator HowWeChooseSimilarSchoolDetailsBtn =>
        page.Locator(Selectors.GovDetailsSummaryText, new PageLocatorOptions { HasText = "How we choose similar schools" });
    private ILocator ViewOrChangeSchoolComparatorLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View or change which schools we compare you with" });
    private ILocator HowWeChooseSimilarSchoolsDetailsSection => page.Locator(Selectors.GovDetailsText);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await Breadcrumbs.ShouldBeVisible();
        await ChangeSchoolLink.ShouldBeVisible();
        await SaveImageTotalExpenditure.ShouldBeVisible();
        await TotalExpenditureDimension.ShouldBeVisible();
        await TotalExpenditureChart.ShouldBeVisible();
        await ShowHideAllSectionsLink.ShouldBeVisible();
        await ViewAsTableRadio.ShouldBeVisible().ShouldBeChecked(false);
        await ViewAsChartRadio.ShouldBeVisible().ShouldBeChecked();
        await HowWeChooseSimilarSchoolDetailsBtn.ShouldBeVisible();
        await ViewOrChangeSchoolComparatorLink.ShouldNotBeVisible();
        await HowWeChooseSimilarSchoolsDetailsSection.ShouldNotBeVisible();

        await HasDimensionValuesForChart(ComparisonChartNames.Premises,
            ["£ per m²", "actuals", "percentage of expenditure", "percentage of income"]);
    }

    public async Task ClickSaveAsImage(ComparisonChartNames chartName)
    {
        var chartToDownload = chartName switch
        {
            ComparisonChartNames.TotalExpenditure => SaveImageTotalExpenditure,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        await chartToDownload.Click();
    }

    public async Task SelectDimensionForChart(ComparisonChartNames chartName, string value)
    {
        await ChartDimensionDropdown(chartName).SelectOption(value);
    }

    public async Task IsDimensionSelectedForChart(ComparisonChartNames chartName, string value)
    {
        await ChartDimensionDropdown(chartName).ShouldHaveSelectedOption(value);
    }

    public async Task ClickViewAsTable()
    {
        await ViewAsTableRadio.Click();
    }

    public async Task IsTableDataForChartDisplayed(ComparisonChartNames chartName, List<List<string>> expectedData)
    {
        await ChartTable(chartName).ShouldHaveTableContent(expectedData, true);
    }

    public async Task AreSaveAsImageButtonsDisplayed(bool isVisible = true)
    {
        var buttons = await SaveAsImageButtons.AllAsync();
        if (isVisible)
        {
            Assert.Equal(44, buttons.Count);
            await buttons.ShouldBeVisible();
        }
        else
        {
            Assert.Empty(buttons);
            await buttons.ShouldNotBeVisible();
        }
    }

    public async Task ClickShowAllSections()
    {
        var text = await ShowHideAllSectionsLink.TextContentAsync();
        if (text == "Show all sections")
        {
            await ShowHideAllSectionsLink.Click();
        }
    }

    public async Task AreSectionsExpanded()
    {
        var sections = await Sections.AllAsync();
        foreach (var section in sections)
        {
            await section.AssertLocatorClass("govuk-accordion__section govuk-accordion__section--expanded");
        }
    }

    public async Task IsShowHideAllSectionsText(string expectedText)
    {
        await ShowHideAllSectionsLink.TextEqual(expectedText);
    }

    public async Task AreTablesShown()
    {
        var tables = await Tables.AllAsync();
        foreach (var table in tables)
        {
            await table.ShouldBeVisible();
        }
    }

    public async Task ClickSectionLink(ComparisonChartNames chartName)
    {
        var link = SectionLink(chartName);

        await link.Locator(Selectors.ToggleSectionText).First.ClickAsync();
    }

    private ILocator SectionLink(ComparisonChartNames chartName)
    {
        var link = chartName switch
        {
            ComparisonChartNames.NonEducationalSupportStaff => SectionLink(Selectors.SectionHeadingTwo),
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };
        return link;
    }

    public async Task IsSectionVisible(ComparisonChartNames chartName, bool visibility, string text, string chartMode)
    {
        var link = SectionLink(chartName);
        await link.ShouldHaveAttribute("aria-expanded", visibility.ToString().ToLower());
        await link.Locator(Selectors.ToggleSectionText).ShouldHaveText(text);
        await IsSectionContentVisible(chartName, visibility, chartMode);
    }

    public async Task ClickHowWeChooseSimilarSchoolsBtn()
    {
        await HowWeChooseSimilarSchoolDetailsBtn.Click();
    }

    public async Task IsDetailsSectionVisible()
    {
        await HowWeChooseSimilarSchoolsDetailsSection.ShouldBeVisible();
        await ViewOrChangeSchoolComparatorLink.ShouldBeVisible();
    }

    private async Task IsSectionContentVisible(ComparisonChartNames chartName, bool visibility, string chartMode)
    {
        var contentLocator = chartName switch
        {
            ComparisonChartNames.NonEducationalSupportStaff => NonEducationSupportStaffAccordionContent,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        foreach (var locator in await contentLocator.Locator(chartMode).AllAsync())
        {
            if (visibility)
            {
                await locator.ShouldBeVisible();
            }
            else
            {
                await locator.ShouldNotBeVisible();
            }
        }
    }

    private async Task HasDimensionValuesForChart(ComparisonChartNames chartName, string[] expected)
    {
        const string exp = "(select) => Array.from(select.options).map(option => option.value)";
        var dropdown = ChartDimensionDropdown(chartName);
        var actual = await dropdown.EvaluateAsync<string[]>(exp);

        Assert.Equal(expected, actual);
    }

    private ILocator ChartTable(ComparisonChartNames chartName)
    {
        var chart = chartName switch
        {
            ComparisonChartNames.TotalExpenditure => TotalExpenditureTable,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        return chart;
    }

    private ILocator ChartDimensionDropdown(ComparisonChartNames chartName)
    {
        var chart = chartName switch
        {
            ComparisonChartNames.Premises => PremisesDimension,
            ComparisonChartNames.TotalExpenditure => TotalExpenditureDimension,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        return chart;
    }

    private ILocator SectionLink(string sectionId)
    {
        return page.Locator("button",
            new PageLocatorOptions { Has = page.Locator($"span{sectionId}") });
    }
}