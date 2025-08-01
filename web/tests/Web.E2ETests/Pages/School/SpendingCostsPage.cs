﻿using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;

public enum CostCategoryNames
{
    TeachingAndTeachingSupplyStaff,
    AdministrativeSupplies,
    CateringStaffAndServices,
    EducationalIct,
    EducationalSupplies,
    NonEducationalSupportStaff,
    Other,
    PremisesStaffAndServices,
    Utilities
}

public partial class SpendingCostsPage(IPage page)
{
    private readonly string[] _h3Names =
    [
        "Teaching and Teaching support staff",
        "Administrative supplies",
        "Catering staff and supplies",
        "Educational ICT",
        "Educational supplies",
        "Non-educational support staff",
        "Premises staff and services",
        "Utilities"
    ];

    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator ComparatorSetDetails =>
        page.Locator(Selectors.GovDetailsSummaryText,
            new PageLocatorOptions
            {
                HasText = "How we choose and compare similar schools"
            });

    private ILocator PageH3Headings => page.Locator(Selectors.H3);
    private ILocator AllCharts => page.Locator(Selectors.ChartContainer);
    private ILocator AllChartsStats => page.Locator(Selectors.ChartStats);

    private ILocator TeachingAndTeachingSupplyStaffLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "View all teaching and teaching support staff"
        });

    private ILocator AdministrativeSuppliesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "View all administrative supplies costs"
        });

    private ILocator CateringStaffAndServicesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "View all catering staff and supplies costs"
        });

    private ILocator EducationalIctLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "View all Educational ICT costs"
        });

    private ILocator EducationalSuppliesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "View all educational supplies costs"
        });

    private ILocator NonEducationalSupportStaffLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "View all non-educational support staff"
        });

    private ILocator OtherLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "View all other costs"
        });

    private ILocator PremisesStaffAndServicesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "View all premises staff and services costs"
        });

    private ILocator UtilitiesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions
        {
            HasText = "View all utilities"
        });

    private ILocator EducationIctCostCategory => page.Locator(Selectors.EducationIctSpendingCosts);
    private ILocator EducationIctWarningText => page.Locator($"{Selectors.EducationIctSpendingCosts} {Selectors.GovWarning}");
    private ILocator SaveImageTeachingAndTeachingSupportStaff => page.Locator(Selectors.TeachingAndTeachingSupportStaffSaveAsImage);
    private ILocator CopyImageTeachingAndTeachingSupportStaff => page.Locator(Selectors.TeachingAndTeachingSupportStaffCopyImage);
    private ILocator SaveChartImagesButton => page.Locator(Selectors.SaveChartImages);

    private ILocator SaveImagesButton =>
        page.Locator(Selectors.Button, new PageLocatorOptions
        {
            HasText = "Save chart images"
        });
    private ILocator ChartStatsSummary(ILocator chart) => chart.Locator(".chart-stat-summary");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        Assert.Equal(await PageH3Headings.Count(), _h3Names.Length);
        await AssertChartNames(_h3Names);
        Assert.Equal(8, await AllCharts.Count());
        Assert.Equal(8, await AllChartsStats.Count());
        await CheckVisibility(AllChartsStats);
        await CheckVisibility(AllCharts);
        await SaveImageTeachingAndTeachingSupportStaff.ShouldBeVisible();
        await CopyImageTeachingAndTeachingSupportStaff.ShouldBeVisible();
        await SaveChartImagesButton.ShouldNotBeVisible();
    }

    public async Task AssertOrderOfCharts(List<string[]> expectedOrder)
    {
        var actualOrder = new List<string[]>();
        var chartNames = await GetCategoryNames();
        var priorityTags = chartNames.Select(
            chartName => page.GetByTestId(
                $"{chartName}-rag-commentary").Locator(Selectors.GovukTag)).ToList();
        for (var i = 0; i < chartNames.Count; i++)
        {
            if (i < priorityTags.Count)
            {
                var chartName = chartNames[i];
                var priorityTag = await priorityTags[i].TextContentAsync() ?? string.Empty;
                var chartDetails = new[] { chartName, priorityTag.Trim() };
                actualOrder.Add(chartDetails);
            }
            else
            {
                Assert.Fail("chart name and priority tag count doesn't match");
                break;
            }
        }

        Assert.Equal(expectedOrder, actualOrder);
    }

    public async Task AssertRagCommentary(string categoryName, string commentary)
    {
        var categoryHeader = page.Locator("h3").And(page.GetByText(categoryName));
        Assert.NotNull(categoryHeader);

        var priority = page.GetByTestId($"{categoryName}-rag-commentary");
        Assert.NotNull(priority);

        var text = await priority.InnerTextAsync();
        Assert.EndsWith(commentary, text);
    }

    public async Task AssertCommercialResources(string categoryName, string[] commercialResources)
    {
        var categorySection = page.Locator($"[id^=spending-priorities-{categoryName.ToSlug()}]");
        await categorySection.IsVisibleAsync();

        var resourcesSection = categorySection.Locator(".app-resources");

        var resources = await resourcesSection.Locator("ul li").AllTextContentsAsync();
        var expected = resources.Select(r => r.Replace("Opens in a new window", string.Empty).Trim()).ToArray();
        Assert.Equal(commercialResources, expected);
    }

    public async Task AssertCategoryCommentary(string categoryName, string commentary)
    {
        var categorySection = page.Locator($"[id^=spending-priorities-{categoryName.ToSlug()}]");
        await categorySection.IsVisibleAsync();

        var categoryCommentary = await categorySection.Locator(".category-commentary").InnerTextAsync();
        Assert.Equal(commentary, categoryCommentary);
    }

    public async Task<CompareYourCostsPage> ClickOnLink(CostCategoryNames costCategory)
    {
        var linkToClick = costCategory switch
        {
            CostCategoryNames.TeachingAndTeachingSupplyStaff => TeachingAndTeachingSupplyStaffLink,
            CostCategoryNames.AdministrativeSupplies => AdministrativeSuppliesLink,
            CostCategoryNames.CateringStaffAndServices => CateringStaffAndServicesLink,
            CostCategoryNames.EducationalIct => EducationalIctLink,
            CostCategoryNames.EducationalSupplies => EducationalSuppliesLink,
            CostCategoryNames.NonEducationalSupportStaff => NonEducationalSupportStaffLink,
            CostCategoryNames.Other => OtherLink,
            CostCategoryNames.PremisesStaffAndServices => PremisesStaffAndServicesLink,
            CostCategoryNames.Utilities => UtilitiesLink,
            _ => throw new ArgumentOutOfRangeException(nameof(costCategory))
        };
        await linkToClick.Click();
        return new CompareYourCostsPage(page);
    }

    public async Task<CostCategoriesGuidancePage> ClickOnCostCategoriesGuidanceLink()
    {
        await page.Locator("#cost-categories-guidance").ClickAsync();
        await page.BringToFrontAsync();
        return new CostCategoriesGuidancePage(page);
    }

    public async Task AssertCostCategoryData(CostCategoryNames costCategory, Table expectedTable)
    {
        var actualData = await GetCostCategoryData(costCategory);
        for (var i = 0; i < expectedTable.Rows.Count; i++)
        {
            var expectedDescription = expectedTable.Rows[i]["Description"];
            var expectedValue = expectedTable.Rows[i]["Value"];
            Assert.True(actualData.Count > i, $"Expected more entries in actual data for '{expectedDescription}'.");
            var actualDescription = actualData[i].Description;
            var actualValue = actualData[i].Value;
            Assert.Equal(expectedDescription, actualDescription);
            Assert.Equal(expectedValue, actualValue);
        }
    }

    public async Task IsWarningMessageVisibleForCategory(CostCategoryNames categoryName)
    {
        var warningMessage = categoryName switch
        {
            CostCategoryNames.EducationalIct => EducationIctWarningText,
            _ => throw new ArgumentOutOfRangeException(nameof(categoryName))
        };
        await warningMessage.ShouldBeVisible();
    }

    public async Task IsSaveImagesButtonDisplayed()
    {
        await SaveImagesButton.ShouldBeVisible();
    }

    public async Task ClickSaveImagesButton()
    {
        await SaveImagesButton.ClickAsync();
    }

    private async Task<List<(string Description, string Value)>> GetCostCategoryData(CostCategoryNames costCategory)
    {
        var chartStats = ChartStatsSummary(GetSelectorForCostCategory(costCategory));

        if (chartStats == null)
        {
            throw new Exception($"Cost category '{costCategory}' not found on the page.");
        }
        var rows = new List<(string Description, string Value)>();
        var chartStatWrappers = chartStats.Locator(".chart-stat-wrapper");
        var count = await chartStatWrappers.CountAsync();
        for (var i = 0; i < count; i++)
        {
            var wrapper = chartStatWrappers.Nth(i);
            var labelElement = wrapper.Locator(".chart-stat-label");
            var valueElement = wrapper.Locator(".chart-stat-value");
            var suffixElement = wrapper.Locator(".chart-stat-suffix");
            var label = (await labelElement.TextContentAsync())?.Trim();
            var value = (await valueElement.TextContentAsync())?.Trim();

            if (!string.IsNullOrWhiteSpace(value))
            {
                value = MultipleWhitespaceRegex().Replace(value, string.Empty);
            }

            var suffix = (await suffixElement.TextContentAsync())?.Trim();
            if (!string.IsNullOrEmpty(label) && value != null && suffix != null)
            {
                rows.Add((label, $"{value} {suffix}"));
            }
        }

        return rows;
    }

    private ILocator GetSelectorForCostCategory(CostCategoryNames costCategoryName)
    {
        var chartSelector = costCategoryName switch
        {
            CostCategoryNames.EducationalIct => EducationIctCostCategory,
            _ => throw new ArgumentOutOfRangeException(nameof(costCategoryName))
        };
        return chartSelector;
    }


    private async Task<List<string>> GetCategoryNames()
    {
        var h3Elements = await PageH3Headings.AllAsync();
        var categoryNames = new List<string>();

        foreach (var h3 in h3Elements)
        {
            var chartName = await h3.TextContentAsync() ?? string.Empty;
            categoryNames.Add(chartName.Trim());
        }

        return categoryNames;
    }

    private async Task CheckVisibility(ILocator locator)
    {
        foreach (var element in await locator.AllAsync())
        {
            await element.ShouldBeVisible();
        }
    }

    private async Task AssertChartNames(string[] expected)
    {
        var allContent = await PageH3Headings.AllTextContentsAsync();

        foreach (var expectedHeading in expected)
        {
            Assert.Contains(expectedHeading, allContent);
        }
    }

    [GeneratedRegex("\\s{2,}")]
    private static partial Regex MultipleWhitespaceRegex();
}