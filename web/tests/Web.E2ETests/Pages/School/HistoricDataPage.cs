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
public class HistoricDataPage(IPage page)
{
    private readonly string[] _expenditureCategories =
    {
        "Teaching and teaching support staff", "Educational supplies", "Educational ICT",
        "Premises and services", "Utilities", "Administrative supplies", "Catering staff and services", "Other",
    };

    private readonly string[] _spendingSubCategories = {
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
    private ILocator SpendingCategoriesHeadings => page.Locator($"{Selectors.SpendingHistoryTab} {Selectors.H2}");
    private ILocator ShowHideAllSectionsLink => page.Locator(Selectors.GovShowAllLinkText);
    private ILocator ExpenditureDimension => page.Locator(Selectors.ExpenditureDimension);
    private ILocator ExpenditureModeTable => page.Locator(Selectors.ExpenditureModeTable);
    private ILocator ExpenditureModeChart => page.Locator(Selectors.ExpenditureModeChart);
    private ILocator IncomeDimension => page.Locator(Selectors.IncomeDimension);
    
    private ILocator Sections => page.Locator(Selectors.GovAccordionSection);

    private ILocator SpendingSections =>
        page.Locator($"{Selectors.SpendingHistoryTab} {Selectors.GovAccordionSection}");
    
    private ILocator IncomeSections =>
        page.Locator($"{Selectors.SpendingHistoryTab} {Selectors.GovAccordionSection}");
    private ILocator SpendingSubCategories => page.Locator($"{Selectors.SpendingHistoryTab} {Selectors.H3}");

    

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await BackLink.ShouldBeVisible();
        await ExpenditureDimension.ShouldBeVisible();
        await ShowHideAllSectionsLink.First.ShouldBeVisible();
        await ExpenditureModeTable.ShouldBeVisible().ShouldBeChecked(false);
        await ExpenditureModeChart.ShouldBeVisible().ShouldBeChecked();
        await HasDimensionValues(ExpenditureDimension, ["£ per pupil", "actuals", "percentage of expenditure",
            "percentage of income"]);
        await AssertCategoryNames(_expenditureCategories);
        await ExpenditureDimension.ShouldHaveSelectedOption("actuals");
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