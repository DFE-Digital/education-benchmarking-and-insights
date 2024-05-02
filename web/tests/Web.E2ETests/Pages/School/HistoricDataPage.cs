using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;

public enum HistoryTabs
{
    Spending,
    Income,
    Balance,
    Census
}

public enum SpendingCategoriesNames
{
    TeachingAndTeachingSupportStaff,
    NonEducationalSupportStaff
}

public class HistoricDataPage(IPage page)
{
    private readonly string[] _spendingCategories =
    {
        "Teaching and teaching support staff", "Non-educational support staff", "Educational supplies",
        "Educational ICT",
        "Premises and services", "Utilities", "Administrative supplies", "Catering staff and services", "Other",
    };

    private readonly string[] _incomeCategories = { "Grant funding", "Self-generated", "Direct revenue financing" };
    private readonly string[] _balanceCategories = { "In-year balance", "Revenue reserve" };

    private readonly string[] _censusCategories =
    {
        "Pupils on roll",
        "School workforce (full time equivalent)",
        "Total number of teachers (full time equivalent)",
        "Teachers with qualified teacher status (%)",
        "Senior leadership (full time equivalent)",
        "Teaching assistants (full time equivalent)",
        "Non-classroom support staff - excluding auxiliary staff (full time equivalent)",
        "Auxiliary staff (full time equivalent)",
        "School workforce (headcount)"
    };

    private readonly string[] _spendingSubCategories =
    {
        "Total teaching and teaching support staff costs",
        "Teaching staff costs",
        "Supply teaching staff",
        "Educational consultancy",
        "Education support staff",
        "Agency supply teaching staff",
        "Total non-educational support staff costs",
        "Administrative and clerical staff costs",
        "Auditor costs",
        "Other staff costs",
        "Professional services (non-curriculum) cost",
        "Total educational supplies costs",
        "Examination fees costs",
        "Learning resources (non ICT equipment) costs",
        "ICT learning resources costs",
        "Total premises staff and services costs",
        "Cleaning and caretaking costs",
        "Maintenance of premises costs",
        "Other occupation costs",
        "Premises staff costs",
        "Total utilities costs",
        "Energy costs",
        "Water and sewerage costs",
        "Administration supplies (non educational) costs",
        "Total gross catering costs",
        "Catering staff costs",
        "Catering supplies costs",
        "Total other costs",
        "Direct revenue financing costs",
        "Grounds maintenance costs",
        "Indirect employee expenses costs",
        "Interest changes for loan and bank costs",
        "Other insurance premiums costs",
        "PFI charges costs",
        "Rents and rates costs",
        "Special facilities costs",
        "Staff development and training costs",
        "Staff-related insurance costs",
        "Supply teacher insurance costs",
        "Community focused school staff (maintained schools only)"
    };

    private readonly string[] _incomeSubCategories =
    {
        "Grant funding total",
        "Direct grants",
        "Pre-16 and post-16 funding",
        "Other DfE/EFA revenue grants",
        "Other income (local authority and other government grants)",
        "Government source (non-grant)",
        "Community grants",
        "Academies",
        "Self-generated funding total",
        "Income from facilities and services",
        "Income from catering",
        "Donations and/or voluntary funds",
        "Receipts from supply teacher insurance claims",
        "Investment income",
        "Other self-generated income",
        "Direct revenue financing (capital reserves transfers)"
    };


    private ILocator PageH1Heading => page.Locator(Selectors.H1);

    private ILocator SpendingCategoryHeadings =>
        SpendingTabContent.Locator($"{Selectors.H2} {Selectors.AccordionHeadingText}");

    private ILocator IncomeCategoryHeadings =>
        IncomeTabContent.Locator($"{Selectors.H2} {Selectors.AccordionHeadingText}");

    private ILocator BalanceCategoryHeadings => BalanceTabContent.Locator(Selectors.H2);
    private ILocator CensusCategoryHeadings => CensusTabContent.Locator(Selectors.H2);

    private ILocator BackLink => page.Locator(Selectors.GovBackLink);
    private ILocator ShowHideAllSectionsLink => page.Locator(Selectors.GovShowAllLinkText);
    private ILocator ExpenditureDimension => page.Locator(Selectors.ExpenditureDimension);
    private ILocator ExpenditureModeTable => page.Locator(Selectors.ExpenditureModeTable);
    private ILocator ExpenditureModeChart => page.Locator(Selectors.ExpenditureModeChart);
    private ILocator IncomeModeTable => page.Locator(Selectors.IncomeModeTable);
    private ILocator IncomeModeChart => page.Locator(Selectors.IncomeModeChart);

    private ILocator BalanceModeTable => page.Locator(Selectors.BalanceModeTable);
    private ILocator BalanceModeChart => page.Locator(Selectors.BalanceModeChart);

    private ILocator CensusModeTable => page.Locator(Selectors.CensusModeTable);
    private ILocator CensusModeChart => page.Locator(Selectors.CensusModeChart);

    private ILocator IncomeDimension => page.Locator(Selectors.IncomeDimension);
    private ILocator BalanceDimension => page.Locator(Selectors.BalanceDimension);
    private ILocator CensusDimension => page.Locator(Selectors.CensusDimension);

    private ILocator Tables => page.Locator(Selectors.GovTable);

    private ILocator SpendingSections =>
        page.Locator($"{Selectors.SpendingHistoryTab} {Selectors.GovAccordionSection}");

    private ILocator IncomeSections =>
        page.Locator($"{Selectors.IncomeHistoryTab} {Selectors.GovAccordionSection}");

    private ILocator SpendingAccordion => page.Locator(Selectors.SpendingAccordions);
    private ILocator SpendingTabContent => page.Locator(Selectors.SpendingPanel);
    private ILocator IncomeTabContent => page.Locator(Selectors.IncomePanel);
    private ILocator BalanceTabContent => page.Locator(Selectors.BalancePanel);
    private ILocator CensusTabContent => page.Locator(Selectors.CensusPanel);

    private ILocator SpendingSubCategories => SpendingAccordion.Locator($"{Selectors.H3}");

    private ILocator IncomeAccordion => page.Locator(Selectors.IncomeAccordions);
    private ILocator IncomeSubCategories => IncomeAccordion.Locator($"{Selectors.H3}");

    private ILocator SpendingTableView => page.Locator(Selectors.SpendingTableMode);
    private ILocator IncomeTableView => page.Locator(Selectors.IncomeTableMode);
    private ILocator BalanceTableView => page.Locator(Selectors.BalanceTableMode);
    private ILocator CensusTableView => page.Locator(Selectors.CensusTableMode);
    private ILocator NonEducationSupportStaffAccordionContent => page.Locator(Selectors.SpendingAccordionContent2);

    private ILocator AllSpendingCharts => SpendingTabContent.Locator(Selectors.Charts);
    private ILocator SpendingChartsStats => SpendingTabContent.Locator(Selectors.LineChartStats);
    private ILocator AllIncomeCharts => IncomeTabContent.Locator(Selectors.Charts);
    private ILocator IncomeChartsStats => IncomeTabContent.Locator(Selectors.LineChartStats);
    private ILocator AllBalanceCharts => BalanceTabContent.Locator(Selectors.Charts);
    private ILocator BalanceChartsStats => BalanceTabContent.Locator(Selectors.LineChartStats);
    private ILocator AllCensusCharts => CensusTabContent.Locator(Selectors.Charts);
    private ILocator CensusChartsStats => CensusTabContent.Locator(Selectors.LineChartStats);


    public async Task IsDisplayed(HistoryTabs? tab = null)
    {
        HistoryTabs selectedTab = tab ?? HistoryTabs.Spending;
        await PageH1Heading.ShouldBeVisible();
        await BackLink.ShouldBeVisible();
        switch (selectedTab)
        {
            case HistoryTabs.Spending:
                await ExpenditureDimension.ShouldBeVisible();
                await ShowHideAllSectionsLink.First.ShouldBeVisible();
                await ExpenditureModeTable.ShouldBeVisible().ShouldBeChecked(false);
                await ExpenditureModeChart.ShouldBeVisible().ShouldBeChecked();
                await HasDimensionValues(ExpenditureDimension, ["£ per pupil",
                    "actuals",
                    "percentage of expenditure",
                    "percentage of income"]);
                await AllSpendingCharts.First.ShouldBeVisible();
                await SpendingChartsStats.First.ShouldBeVisible();
                await AssertCategoryNames(_spendingCategories, selectedTab);
                await ExpenditureDimension.ShouldHaveSelectedOption("actuals");
                Assert.Equal(41, await AllSpendingCharts.Count());

                break;
            case HistoryTabs.Income:
                await IncomeDimension.ShouldBeVisible();
                await ShowHideAllSectionsLink.Nth(1).ShouldBeVisible();
                await IncomeModeTable.ShouldBeVisible().ShouldBeChecked(false);
                await IncomeModeChart.ShouldBeVisible().ShouldBeChecked();
                await HasDimensionValues(IncomeDimension, ["£ per pupil",
                    "actuals",
                    "percentage of expenditure",
                    "percentage of income"]);
                await ExpenditureDimension.ShouldHaveSelectedOption("actuals");
                await AllIncomeCharts.First.ShouldBeVisible();
                await IncomeChartsStats.First.ShouldBeVisible();
                await AssertCategoryNames(_incomeCategories, selectedTab);
                Assert.Equal(17, await AllIncomeCharts.Count());
                break;
            case HistoryTabs.Balance:
                await BalanceDimension.ShouldBeVisible();
                await BalanceModeTable.ShouldBeVisible().ShouldBeChecked(false);
                await BalanceModeChart.ShouldBeVisible().ShouldBeChecked();
                await HasDimensionValues(BalanceDimension, ["£ per pupil",
                    "actuals",
                    "percentage of expenditure",
                    "percentage of income"]);
                await BalanceDimension.ShouldHaveSelectedOption("actuals");
                await AreChartStatsVisible(selectedTab);
                await AreChartsVisible(selectedTab);
                await AssertCategoryNames(_balanceCategories, selectedTab);
                Assert.Equal(2, await AllBalanceCharts.Count());
                break;
            case HistoryTabs.Census:
                await CensusDimension.ShouldBeVisible();
                await CensusModeTable.ShouldBeVisible().ShouldBeChecked(false);
                await CensusModeChart.ShouldBeVisible().ShouldBeChecked();
                await HasDimensionValues(CensusDimension, ["total",
                    "headcount per FTE",
                    "percentage of workforce",
                    "pupils per staff role"]);
                await CensusDimension.ShouldHaveSelectedOption("pupils per staff role");
                await AreChartStatsVisible(selectedTab);
                await AreChartsVisible(selectedTab);
                await AssertCategoryNames(_censusCategories, selectedTab);
                Assert.Equal(9, await AllCensusCharts.Count());
                break;
        }
    }

    public async Task SelectDimension(HistoryTabs tab, string dimensionValue)
    {
        await HistoryDimensionDropdown(tab).SelectOption(dimensionValue);
    }

    public async Task IsDimensionSelected(HistoryTabs tab, string value)
    {
        await HistoryDimensionDropdown(tab).ShouldHaveSelectedOption(value);
    }

    public async Task ClickShowAllSections(HistoryTabs tab)
    {
        if (tab == HistoryTabs.Balance || tab == HistoryTabs.Census)
            return;
        var showAllSectionsLink = tab switch
        {
            HistoryTabs.Spending => ShowHideAllSectionsLink.First,
            HistoryTabs.Income => ShowHideAllSectionsLink.Nth(1),
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        var textContent = await showAllSectionsLink.TextContentAsync();

        if (textContent == "Show all sections")
        {
            await showAllSectionsLink.Click();
        }
    }


    public async Task AreSectionsExpanded(HistoryTabs tab)
    {
        var tabSections = tab switch
        {
            HistoryTabs.Spending => SpendingSections,
            HistoryTabs.Income => IncomeSections,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        var sections = await tabSections.AllAsync();
        foreach (var section in sections)
        {
            await section.AssertLocatorClass("govuk-accordion__section govuk-accordion__section--expanded");
        }
    }

    public async Task IsShowHideAllSectionsText(HistoryTabs tab, string expectedText)
    {
        var showAllSectionsLink = tab switch
        {
            HistoryTabs.Spending => ShowHideAllSectionsLink.First,
            HistoryTabs.Income => ShowHideAllSectionsLink.Nth(1),
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
        await showAllSectionsLink.TextEqual(expectedText);
    }

    public async Task AreSubCategoriesVisible(HistoryTabs tab)
    {
        var expectedSubCategories = tab switch
        {
            HistoryTabs.Spending => _spendingSubCategories,
            HistoryTabs.Income => _incomeSubCategories,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
        await AreChartStatsVisible(tab);
        await AreChartsVisible(tab);
        Assert.Equal(expectedSubCategories, await GetSubCategoriesOfTab(tab));
    }

    public async Task AreTablesShown(HistoryTabs tab)
    {
        var sections = tab switch
        {
            HistoryTabs.Spending => SpendingTabContent.Locator(Tables),
            HistoryTabs.Income => IncomeTabContent.Locator(Tables),
            HistoryTabs.Balance => BalanceTabContent.Locator(Tables),
            HistoryTabs.Census => CensusTabContent.Locator(Tables),
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        var tables = await sections.AllAsync();
        foreach (var table in tables)
        {
            await table.ShouldBeVisible();
        }
    }

    public async Task ClickViewAsTable(HistoryTabs tab)
    {
        var viewAsTableRadio = tab switch
        {
            HistoryTabs.Spending => SpendingTableView,
            HistoryTabs.Income => IncomeTableView,
            HistoryTabs.Balance => BalanceTableView,
            HistoryTabs.Census => CensusTableView,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        await viewAsTableRadio.Click();
    }

    public async Task ClickSectionLink(SpendingCategoriesNames categoryName)
    {
        var link = SectionLink(categoryName);

        await link.Locator(Selectors.ToggleSectionText).First.ClickAsync();
    }

    public async Task IsSectionVisible(SpendingCategoriesNames categoryName, bool visibility, string text,
        string chartMode)
    {
        var link = SectionLink(categoryName);
        await link.ShouldHaveAttribute("aria-expanded", visibility.ToString().ToLower());
        await link.Locator(Selectors.ToggleSectionText).ShouldHaveText(text);
        await IsSectionContentVisible(categoryName, visibility, chartMode);
    }

    private async Task IsSectionContentVisible(SpendingCategoriesNames categoryName, bool visibility, string chartMode)
    {
        var contentLocator = categoryName switch
        {
            SpendingCategoriesNames.NonEducationalSupportStaff => NonEducationSupportStaffAccordionContent,
            _ => throw new ArgumentOutOfRangeException(nameof(categoryName))
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

    private ILocator SectionLink(SpendingCategoriesNames categoryName)
    {
        var link = categoryName switch
        {
            SpendingCategoriesNames.NonEducationalSupportStaff => SectionLink(Selectors.SpendingAccordionHeading2),
            _ => throw new ArgumentOutOfRangeException(nameof(categoryName))
        };
        return link;
    }

    private ILocator SectionLink(string sectionId)
    {
        return page.Locator("button",
            new PageLocatorOptions { Has = page.Locator($"span{sectionId}") });
    }

    private async Task<List<string>> GetSubCategoriesOfTab(HistoryTabs tab)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var subCategories = tab switch
        {
            HistoryTabs.Spending => await SpendingSubCategories.AllAsync(),
            HistoryTabs.Income => await IncomeSubCategories.AllAsync(),
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
        var subCategoriesHeadings = new List<string>();
        foreach (var category in subCategories)
        {
            var headingName = await category.TextContentAsync() ?? string.Empty;
            subCategoriesHeadings.Add(headingName.Trim());
        }

        return subCategoriesHeadings;
    }

    private async Task HasDimensionValues(ILocator dimensionTab, string[] expected)
    {
        const string exp = "(select) => Array.from(select.options).map(option => option.label)";
        var actual = await dimensionTab.EvaluateAsync<string[]>(exp);
        Assert.Equal(expected, actual);
    }

    private async Task AssertCategoryNames(string[] expected, HistoryTabs? tab)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var categories = tab switch
        {
            HistoryTabs.Spending => await SpendingCategoryHeadings.AllTextContentsAsync(),
            HistoryTabs.Income => await IncomeCategoryHeadings.AllTextContentsAsync(),
            HistoryTabs.Balance => await BalanceCategoryHeadings.AllTextContentsAsync(),
            HistoryTabs.Census => await CensusCategoryHeadings.AllTextContentsAsync(),
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
        foreach (var expectedHeading in expected)
        {
            Assert.Contains(expectedHeading, categories);
        }
    }

    private ILocator HistoryDimensionDropdown(HistoryTabs tabName)
    {
        var chart = tabName switch
        {
            HistoryTabs.Spending => ExpenditureDimension,
            HistoryTabs.Income => IncomeDimension,
            HistoryTabs.Balance => BalanceDimension,
            HistoryTabs.Census => CensusDimension,
            _ => throw new ArgumentOutOfRangeException(nameof(tabName))
        };

        return chart;
    }

    private async Task AreChartStatsVisible(HistoryTabs? tab)
    {
        var sections = tab switch
        {
            HistoryTabs.Spending => SpendingChartsStats,
            HistoryTabs.Income => IncomeChartsStats,
            HistoryTabs.Balance => BalanceChartsStats,
            HistoryTabs.Census => CensusChartsStats,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
        await CheckVisibility(sections);
    }

    private async Task AreChartsVisible(HistoryTabs? tab)
    {
        var sections = tab switch
        {
            HistoryTabs.Spending => AllSpendingCharts,
            HistoryTabs.Income => AllIncomeCharts,
            HistoryTabs.Balance => AllBalanceCharts,
            HistoryTabs.Census => AllCensusCharts,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
        await CheckVisibility(sections);
    }

    private async Task CheckVisibility(ILocator locator)
    {

        var elements = await locator.AllAsync();
        foreach (var element in elements)
        {
            await element.ShouldBeVisible();
        }
    }
}