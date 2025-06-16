using Microsoft.Playwright;
using Web.E2ETests.Assist;
using Xunit;

namespace Web.E2ETests.Pages.Trust.Benchmarking;

public class TrustBenchmarkSpendingPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator ShowHideAllSectionsLink => page.Locator(Selectors.GovShowAllLinkText);
    private ILocator ViewOrChangeSetLink => page.Locator("a:has-text('View and change your set of trusts')");
    private ILocator SpendingTab => page.Locator(Selectors.TrustBenchmarkingSpendingTab);
    private ILocator Balance => page.Locator(Selectors.TrustBenchmarkingBalanceTab);
    private ILocator ViewAsChartRadio => page.Locator(Selectors.GovRadios).Locator("#spending-mode-chart");
    private ILocator ViewCentralSpendRadio => page.Locator(Selectors.GovRadios).Locator("#spending-include-breakdown");
    private ILocator SaveAsImageButtons => page.Locator(Selectors.GovButton, new PageLocatorOptions
    {
        HasText = "Save "
    });
    private ILocator TotalExpenditureDimension => page.Locator(Selectors.TotalExpenditureDimension);
    private ILocator TeachingAndSupportDimension => page.Locator(Selectors.TeachingAndSupportDimension);
    private ILocator NonEducationSupportStaffDimension => page.Locator(Selectors.NonEducationSupportStaffDimension);
    private ILocator EducationalSuppliesDimension => page.Locator(Selectors.EducationalSuppliesDimension);
    private ILocator EducationalIctDimension => page.Locator(Selectors.EducationalIctDimension);
    private ILocator PremisesDimension => page.Locator(Selectors.PremisesDimension);
    private ILocator UtilitiesDimension => page.Locator(Selectors.UtilitiesDimension);
    private ILocator AdministrativeSuppliesDimension => page.Locator(Selectors.AdministrativeSuppliesDimension);
    private ILocator CateringServicesDimension => page.Locator(Selectors.CateringServicesDimension);
    private ILocator OtherDimension => page.Locator(Selectors.OtherDimension);
    private ILocator Sections => page.Locator(Selectors.GovAccordionSection);
    private ILocator AllCharts => page.Locator(Selectors.ChartContainer);
    private ILocator ViewAsTableRadio => page.Locator(Selectors.SpendingModeTable);
    private ILocator ExcludeCentralSpendingRadio => page.Locator(Selectors.CentralSpendingModeExclude);
    private ILocator TrustComparisonTitles => page.Locator(Selectors.TrustComparisonTitles);
    private ILocator ComparisonTables => page.Locator(Selectors.TrustComparisonTables);
    private ILocator SubChartTable(string dataTitle) => page.Locator($"[data-title='{dataTitle}'] table.govuk-table");
    private ILocator SubChartWarning(string dataTitle) => page.Locator($"[data-title='{dataTitle}'] {Selectors.GovWarning}");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await ViewOrChangeSetLink.ShouldBeVisible();
        await SpendingTab.ShouldBeVisible().ShouldHaveAttribute("tabindex", "0");
        await Balance.ShouldBeVisible().ShouldHaveAttribute("tabindex", "-1");
        await ViewAsChartRadio.ShouldBeVisible().ShouldBeChecked();
        await ViewCentralSpendRadio.ShouldBeVisible().ShouldBeChecked();
        await SaveAsImageButtons.Nth(0).ShouldBeVisible();
        await TotalExpenditureDimension.ShouldBeVisible();
        var allOptions = await TotalExpenditureDimension.InnerTextAsync();
        var expectedOptions = new[] { "£ per pupil", "actuals", "percentage of income" };
        foreach (var expected in expectedOptions)
        {
            Assert.Contains(expected, allOptions);
        }
        await ShowHideAllSectionsLink.ShouldBeVisible();
        foreach (var sec in await Sections.AllAsync())
        {
            await sec.ShouldBeVisible();
        }
        Assert.Equal(9, await Sections.Count());
        await AllCharts.Nth(0).ShouldBeVisible();
    }

    public async Task ClickViewAsTable()
    {
        await ViewAsTableRadio.Click();
    }

    public async Task ClickExcludeCentralSpending()
    {
        await ExcludeCentralSpendingRadio.Click();
    }

    public async Task ClickShowAllSections()
    {
        await ShowHideAllSectionsLink.Click();
    }

    public async Task TableContainsValues(string category, DataTable expected)
    {
        var titles = await TrustComparisonTitles.AllInnerTextsAsync();
        var index = titles.Skip(2).ToList().IndexOf(category);
        var table = ComparisonTables.Nth(index);
        await table.ShouldBeVisible();
        await AssertExpenditure(table, expected);
    }

    public async Task HasDimensionValuesForChart(ComparisonChartNames chartName, string[] expected)
    {
        const string exp = "(select) => Array.from(select.options).map(option => option.label)";
        var dropdown = ChartDimensionDropdown(chartName);
        var actual = await dropdown.EvaluateAsync<string[]>(exp);

        Assert.True(expected.SequenceEqual(actual), $"Test fails on {chartName}. Expected: {string.Join(", ", expected)}, Actual: {string.Join(", ", actual)}");
    }

    public async Task HighExecutivePaySubChartTableHasExpectedValues(string subChartName, DataTable expected)
    {
        var highExecutivePayTable = SubChartTable(subChartName);
        var set = new List<dynamic>();
        var rows = await highExecutivePayTable.Locator("tbody tr").AllAsync();
        foreach (var row in rows)
        {
            var cells = await row.Locator("td").AllAsync();

            set.Add(new
            {
                TrustName = await cells.ElementAt(0).InnerTextAsync(),
                HighestEmolumentBand = await cells.ElementAt(1).InnerTextAsync()
            });
        }

        expected.CompareToDynamicSet(set, false);
    }

    public async Task IsSubChartNameWarningTextVisible(string subChartName)
    {
        var highExecutivePayTableWarning = SubChartWarning(subChartName).First;

        await highExecutivePayTableWarning.ShouldBeVisible();
    }

    private static async Task AssertExpenditure(ILocator expenditureTable, DataTable expected)
    {
        var set = new List<dynamic>();
        var rows = await expenditureTable.Locator("tbody tr").AllAsync();
        foreach (var row in rows)
        {
            var cells = await row.Locator("td").AllAsync();
            if (cells.Count > 2)
            {
                set.Add(new
                {
                    TrustName = await cells.ElementAt(0).InnerTextAsync(),
                    TotalAmount = await cells.ElementAt(1).InnerTextAsync(),
                    SchoolAmount = await cells.ElementAt(2).InnerTextAsync(),
                    CentralAmount = await cells.ElementAt(3).InnerTextAsync()
                });
            }
            else
            {
                set.Add(new
                {
                    TrustName = await cells.ElementAt(0).InnerTextAsync(),
                    TotalAmount = await cells.ElementAt(1).InnerTextAsync()
                });
            }
        }

        expected.CompareToDynamicSet(set, false);
    }

    private ILocator ChartDimensionDropdown(ComparisonChartNames chartName)
    {
        var chart = chartName switch
        {
            ComparisonChartNames.TotalExpenditure => TotalExpenditureDimension,
            ComparisonChartNames.TeachingAndTeachingSupplyStaff => TeachingAndSupportDimension,
            ComparisonChartNames.NonEducationalSupportStaff => NonEducationSupportStaffDimension,
            ComparisonChartNames.EducationalSupplies => EducationalSuppliesDimension,
            ComparisonChartNames.EducationalIct => EducationalIctDimension,
            ComparisonChartNames.Premises => PremisesDimension,
            ComparisonChartNames.Utilities => UtilitiesDimension,
            ComparisonChartNames.AdministrativeSupplies => AdministrativeSuppliesDimension,
            ComparisonChartNames.CateringStaffAndServices => CateringServicesDimension,
            ComparisonChartNames.Other => OtherDimension,
            _ => throw new ArgumentOutOfRangeException(nameof(chartName))
        };

        return chart;
    }
}