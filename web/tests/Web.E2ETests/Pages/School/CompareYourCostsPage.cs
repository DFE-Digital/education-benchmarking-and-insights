using Microsoft.Playwright;
using Web.E2ETests.Pages.School.Comparators;
using Xunit;
namespace Web.E2ETests.Pages.School;

public enum ComparisonChartNames
{
    Premises,
    TotalExpenditure,
    NonEducationalSupportStaff,
    TeachingAndTeachingSupplyStaff,
    AdministrativeSupplies,
    CateringStaffAndServices,
    EducationalIct,
    EducationalSupplies,
    Other,
    Utilities
}

public class CompareYourCostsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    //private ILocator Breadcrumbs => page.Locator(Selectors.GovBreadcrumbs);
    private ILocator SaveImageTotalExpenditure => page.Locator(Selectors.TotalExpenditureSaveAsImage);
    private ILocator TotalExpenditureDimension => page.Locator(Selectors.TotalExpenditureDimension);
    private ILocator TotalExpenditureChart => page.Locator(Selectors.TotalExpenditureChart);
    private ILocator ViewAsTableRadio => page.Locator(Selectors.ModeTable);
    private ILocator ViewAsChartRadio => page.Locator(Selectors.ModeChart);
    private ILocator TotalExpenditureTable => page.Locator(Selectors.ComparisonTables).First;
    private ILocator ShowHideAllSectionsLink => page.Locator(Selectors.GovShowAllLinkText);
    private ILocator Sections => page.Locator(Selectors.GovAccordionSection);
    private ILocator Tables => page.Locator(Selectors.SectionTable);
    private ILocator TeachingAndSupportAccordionContent => page.Locator(Selectors.SectionContent1);
    private ILocator NonEducationSupportStaffAccordionContent => page.Locator(Selectors.SectionContent2);
    private ILocator EducationalSuppliesAccordionContent => page.Locator(Selectors.SectionContent3);
    private ILocator EducationalIctAccordionContent => page.Locator(Selectors.SectionContent4);
    private ILocator PremisesAccordionContent => page.Locator(Selectors.SectionContent5);
    private ILocator UtilitiesAccordionContent => page.Locator(Selectors.SectionContent6);
    private ILocator AdministrativeSuppliesAccordionContent => page.Locator(Selectors.SectionContent7);
    private ILocator CateringServicesAccordionContent => page.Locator(Selectors.SectionContent8);
    private ILocator OtherAccordionContent => page.Locator(Selectors.SectionContent9);
    private ILocator PremisesDimension => page.Locator(Selectors.PremisesDimension);
    private ILocator CateringStaffAndServicesDimension => page.Locator(Selectors.CateringStaffAndServicesDimension);
    private ILocator CateringStaffAndServicesTables => page.Locator(Selectors.CateringStaffAndServicesTables);
    private ILocator ViewAsGrossRadio => page.Locator(Selectors.TypeGross);
    private ILocator ViewAsNetRadio => page.Locator(Selectors.TypeNet);
    private ILocator ChartTooltip => page.Locator(Selectors.ChartTooltips).First;
    private ILocator IncompleteFinancialBanner => page.Locator(Selectors.GovWarning);

    private ILocator SaveAsImageButtons =>
        page.Locator(Selectors.Button, new PageLocatorOptions
        {
            HasText = "Save"
        });
    private ILocator ComparatorSetDetails =>
        page.Locator(Selectors.GovLink,
            new PageLocatorOptions
            {
                HasText = "We've chosen 2 sets of similar schools"
            });
    private ILocator ComparatorSetLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "Choose your own similar schools"
        });
    private ILocator CustomComparatorLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "Create or save your own set of schools to benchmark against"
        });

    private ILocator CustomDataLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "Change the data for this school"
        });
    private ILocator SimilarSchoolLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "30 similar schools"
        });
    private ILocator ComparatorSetDetailsText => page.Locator(Selectors.GovDetailsText);
    private ILocator ChartBars => page.Locator(Selectors.ChartBars);
    private ILocator ChartTicks => page.Locator(Selectors.ChartYTicks);
    private ILocator AdditionalDetailsPopUps => page.Locator(Selectors.AdditionalDetailsPopUps);
    private ILocator SchoolLinksInCharts => page.Locator(Selectors.SchoolNamesLinksInCharts);

    public async Task IsDisplayed(bool isPartYear = false, bool isMissingComparatorSet = false)
    {
        await PageH1Heading.ShouldBeVisible();
        //await Breadcrumbs.ShouldBeVisible();

        if (isPartYear)
        {
            await IncompleteFinancialBanner.First.ShouldBeVisible();
            await IncompleteFinancialBanner.First.ShouldContainText(
                "This school doesn't have a complete set of financial data for this period.");
        }

        if (!isMissingComparatorSet)
        {
            await SaveImageTotalExpenditure.ShouldBeVisible();
            await TotalExpenditureDimension.ShouldBeVisible();
            await TotalExpenditureChart.ShouldBeVisible();
            await ShowHideAllSectionsLink.ShouldBeVisible();
            await ViewAsTableRadio.ShouldBeVisible().ShouldBeChecked(false);
            await ViewAsChartRadio.ShouldBeVisible().ShouldBeChecked();
            await ComparatorSetDetails.ShouldBeVisible();
            await CustomComparatorLink.ShouldBeVisible();
            await CustomDataLink.ShouldBeVisible();

            //TODO: test data is missing building comparator set. Building relate categories are not shown.
            /*await HasDimensionValuesForChart(ComparisonChartNames.Premises,
                ["£ per m²", "actuals", "percentage of expenditure", "percentage of income"]);*/

            return;
        }

        await IncompleteFinancialBanner.Last.ShouldContainText(
            "There isn't enough information available to create a set of similar schools.");
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
        await ChartTable(chartName).ShouldBeVisible();
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

    public async Task AreComparisonChartsAndTablesDisplayed(bool displayed = true)
    {
        var locator = page.Locator(Selectors.ComparisonChartsAndTables);
        if (displayed)
        {
            await locator.ShouldBeVisible();
        }
        else
        {
            await locator.ShouldNotBeVisible();
        }
    }

    public async Task IsSectionVisible(ComparisonChartNames chartName, bool visibility, string text, string chartMode)
    {
        var link = SectionLink(chartName);
        await link.ShouldHaveAttribute("aria-expanded", visibility.ToString().ToLower());
        await link.Locator(Selectors.ToggleSectionText).ShouldHaveText(text);
        await IsSectionContentVisible(chartName, visibility, chartMode);
    }

    public async Task IsSchoolDetailsPopUpVisible()
    {
        await AdditionalDetailsPopUps.First.ShouldBeVisible();
    }

    public async Task HoverOnGraphBar(int nth = 0)
    {
        await ChartBars.Nth(nth).HoverAsync();
    }

    public async Task<HomePage> ClickSchoolName()
    {
        await SchoolLinksInCharts.First.Click();
        return new HomePage(page);
    }

    public async Task TabToSchoolName()
    {
        await TotalExpenditureDimension.FocusAsync();
        await page.Keyboard.PressAsync("Tab"); // save as image button
        await page.Keyboard.PressAsync("Tab"); // first school
    }

    public async Task AssertSchoolNameFocused()
    {
        await Assertions.Expect(SchoolLinksInCharts.First).ToBeFocusedAsync();
    }

    public async Task<HomePage> PressEnterKey()
    {
        await page.Keyboard.PressAsync(Keyboard.EnterKey);
        return new HomePage(page);
    }

    public async Task ClickViewAsNet()
    {
        await ViewAsNetRadio.Click();
    }

    public async Task TooltipIsDisplayed()
    {
        await ChartTooltip.ShouldBeVisible();
    }

    public async Task<ComparatorsPage> ClickComparatorSetDetails()
    {
        await ComparatorSetDetails.Click();
        return new ComparatorsPage(page);
    }

    public async Task IsTableDataForTooltipDisplayed(List<List<string>> expectedData)
    {
        await TooltipIsDisplayed();
        await ChartTooltip.Locator("table").ShouldHaveTableContent(expectedData, true);
    }

    public async Task IsPartYearWarningInTooltipDisplayed(int months)
    {
        await TooltipIsDisplayed();
        await ChartTooltip.Locator(".tooltip-part-year-warning").ShouldHaveText($"!\nWarning\nThis school only has {months} months of data available.");
    }

    public async Task IsGraphTickTextEqual(int nth, string text)
    {
        var actual = await ChartTicks.Nth(nth).Locator("text").TextContentAsync();
        Assert.Equal(text, actual);
    }

    public async Task IsWarningIconDisplayedOnGraphTick(int nth)
    {
        await ChartTicks.Nth(nth).Locator("circle").ShouldBeVisible();
    }

    public async Task<CreateComparatorsPage> ClickCreateUserDefinedComparatorSet()
    {
        await CustomComparatorLink.ClickAsync();
        return new CreateComparatorsPage(page);
    }

    private ILocator SectionLink(ComparisonChartNames chartName)
    {
        var link = chartName switch
        {
            ComparisonChartNames.TeachingAndTeachingSupplyStaff => SectionLink(Selectors.SectionHeading1),
            ComparisonChartNames.NonEducationalSupportStaff => SectionLink(Selectors.SectionHeading2),
            ComparisonChartNames.EducationalSupplies => SectionLink(Selectors.SectionHeading3),
            ComparisonChartNames.EducationalIct => SectionLink(Selectors.SectionHeading4),
            ComparisonChartNames.Premises => SectionLink(Selectors.SectionHeading5),
            ComparisonChartNames.Utilities => SectionLink(Selectors.SectionHeading6),
            ComparisonChartNames.AdministrativeSupplies => SectionLink(Selectors.SectionHeading7),
            ComparisonChartNames.CateringStaffAndServices => SectionLink(Selectors.SectionHeading8),
            ComparisonChartNames.Other => SectionLink(Selectors.SectionHeading9),
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };
        return link;
    }

    private async Task IsSectionContentVisible(ComparisonChartNames chartName, bool visibility, string chartMode)
    {
        var contentLocator = chartName switch
        {
            ComparisonChartNames.TeachingAndTeachingSupplyStaff => TeachingAndSupportAccordionContent,
            ComparisonChartNames.NonEducationalSupportStaff => NonEducationSupportStaffAccordionContent,
            ComparisonChartNames.EducationalSupplies => EducationalSuppliesAccordionContent,
            ComparisonChartNames.EducationalIct => EducationalIctAccordionContent,
            ComparisonChartNames.Premises => PremisesAccordionContent,
            ComparisonChartNames.Utilities => UtilitiesAccordionContent,
            ComparisonChartNames.AdministrativeSupplies => AdministrativeSuppliesAccordionContent,
            ComparisonChartNames.CateringStaffAndServices => CateringServicesAccordionContent,
            ComparisonChartNames.Other => OtherAccordionContent,
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
        const string exp = "(select) => Array.from(select.options).map(option => option.label)";
        var dropdown = ChartDimensionDropdown(chartName);
        var actual = await dropdown.EvaluateAsync<string[]>(exp);

        Assert.Equal(expected, actual);
    }

    private ILocator ChartTable(ComparisonChartNames chartName)
    {
        var chart = chartName switch
        {
            ComparisonChartNames.TotalExpenditure => TotalExpenditureTable,
            ComparisonChartNames.CateringStaffAndServices => CateringStaffAndServicesTables.First,
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
            ComparisonChartNames.CateringStaffAndServices => CateringStaffAndServicesDimension,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        return chart;
    }

    private ILocator SectionLink(string sectionId) => page.Locator("button",
        new PageLocatorOptions
        {
            Has = page.Locator($"span{sectionId}")
        });
}