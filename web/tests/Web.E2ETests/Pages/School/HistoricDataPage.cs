using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;
public enum HistoryTabs
{
    Spending,
    Income,
    Balance,
    Workforce
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
        "Teaching and teaching support staff", "Non-educational support staff", "Educational supplies", "Educational ICT",
        "Premises and services", "Utilities", "Administrative supplies", "Catering staff and services", "Other",
    };

    private readonly string[] _spendingSubCategories = {
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

    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator SpendingCategoryHeadings => page.Locator($"{Selectors.H2} {Selectors.AccordionHeadingText}");
    private ILocator BackLink => page.Locator(Selectors.GovBackLink);
    private ILocator ShowHideAllSectionsLink => page.Locator(Selectors.GovShowAllLinkText);
    private ILocator ExpenditureDimension => page.Locator(Selectors.ExpenditureDimension);
    private ILocator ExpenditureModeTable => page.Locator(Selectors.ExpenditureModeTable);
    private ILocator ExpenditureModeChart => page.Locator(Selectors.ExpenditureModeChart);
    private ILocator IncomeDimension => page.Locator(Selectors.IncomeDimension);
    private ILocator Tables => page.Locator(Selectors.SectionTable);
    private ILocator SpendingSections =>
        page.Locator($"{Selectors.SpendingHistoryTab} {Selectors.GovAccordionSection}");

    private ILocator IncomeSections =>
        page.Locator($"{Selectors.IncomeHistoryTab} {Selectors.GovAccordionSection}");
    private ILocator SpendingSubCategories => page.Locator($"{Selectors.SpendingAccordions} {Selectors.H3}");

    private ILocator SpendingAccordion => page.Locator(Selectors.SpendingAccordions);
    private ILocator IncomeAccordion => page.Locator(Selectors.IncomeAccordions);
    private ILocator SpendingTableView => page.Locator(Selectors.SpendingTableMode);
    private ILocator IncomeTableView => page.Locator(Selectors.IncomeTableMode);
    private ILocator BalanceTableView => page.Locator(Selectors.BalanceTableMode);
    private ILocator WorkforceTableView => page.Locator(Selectors.WorkforceTableMode);
    private ILocator NonEducationSupportStaffAccordionContent => page.Locator(Selectors.SpendingAccordionContent2);



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
                await AssertCategoryNames(_spendingCategories);
                await ExpenditureDimension.ShouldHaveSelectedOption("actuals");
                break;
            case HistoryTabs.Income:
                break;
            case HistoryTabs.Balance:
                break;
            case HistoryTabs.Workforce:
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
        Assert.Equal(await GetSubCategoriesOfTab(tab), _spendingSubCategories);
    }

    public async Task AreTablesShown(HistoryTabs tab)
    {
        var sections = tab switch
        {
            HistoryTabs.Spending => SpendingAccordion.Locator(Tables),
            HistoryTabs.Income => IncomeAccordion.Locator(Tables),
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        var tables = await sections.AllAsync();
        foreach (var table in tables)
        {
            await table.ShouldBeVisible();
        }
    }

    public async Task AreTableStatsShown(HistoryTabs tab)
    {
        var sections = tab switch
        {
            HistoryTabs.Spending => SpendingAccordion.Locator(Selectors.LineChartStats),
            HistoryTabs.Income => IncomeAccordion.Locator(Selectors.LineChartStats),
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
            HistoryTabs.Workforce => WorkforceTableView,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        await viewAsTableRadio.Click();
    }

    public async Task ClickSectionLink(SpendingCategoriesNames categoryName)
    {
        var link = SectionLink(categoryName);

        await link.Locator(Selectors.ToggleSectionText).First.ClickAsync();
    }

    public async Task IsSectionVisible(SpendingCategoriesNames categoryName, bool visibility, string text, string chartMode)
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
        var subCategories = tab switch
        {
            HistoryTabs.Spending => await SpendingSubCategories.AllAsync(),
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
    private async Task AssertCategoryNames(string[] expected)
    {
        var allContent = await SpendingCategoryHeadings.AllTextContentsAsync();

        foreach (var expectedHeading in expected)
        {
            Assert.Contains(expectedHeading, allContent);
        }
    }

    private ILocator HistoryDimensionDropdown(HistoryTabs tabName)
    {
        var chart = tabName switch
        {
            HistoryTabs.Spending => ExpenditureDimension,
            HistoryTabs.Income => IncomeDimension,
            _ => throw new ArgumentOutOfRangeException(nameof(tabName))
        };

        return chart;
    }

}