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

public class SpendingCostsPage(IPage page)
{
    private readonly string[] _h3Names =
    {
        "Teaching and Teaching support staff", "Administrative supplies", "Catering staff and supplies",
        "Educational ICT", "Educational supplies", "Non-educational support staff and services", "Other costs",
        "Premises staff and services", "Utilities"
    };

    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    //private ILocator Breadcrumbs => page.Locator(Selectors.GovBreadcrumbs);

    private ILocator ComparatorSetDetails =>
        page.Locator(Selectors.GovDetailsSummaryText,
            new PageLocatorOptions { HasText = "How we choose and compare similar schools" });

    private ILocator PageH3Headings => page.Locator(Selectors.H3);
    private ILocator AllCharts => page.Locator(Selectors.ReactChartContainer);
    private ILocator AllChartsStats => page.Locator(Selectors.ReactChartStats);

    private ILocator TeachingAndTeachingSupplyStaffLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all teaching and teaching support staff" });

    private ILocator AdministrativeSuppliesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all administrative supplies costs" });

    private ILocator CateringStaffAndServicesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all catering staff and supplies costs" });

    private ILocator EducationalIctLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all educational ICT costs" });

    private ILocator EducationalSuppliesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all educational supplies costs" });

    private ILocator NonEducationalSupportStaffLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all non-educational support staff and services" });

    private ILocator OtherLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all other costs costs" });

    private ILocator PremisesStaffAndServicesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all premises staff and services costs" });

    private ILocator UtilitiesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all utilities" });

    private ILocator PriorityTags => page.Locator($"{Selectors.MainContent} {Selectors.GovukTag}");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        //await Breadcrumbs.ShouldBeVisible();
        Assert.Equal(await PageH3Headings.Count(), _h3Names.Length);
        await AssertChartNames(_h3Names);
        Assert.Equal(9, await AllCharts.Count());
        Assert.Equal(9, await AllChartsStats.Count());
        await CheckVisibility(AllChartsStats);
        await CheckVisibility(AllCharts);
    }


    public async Task CheckOrderOfCharts(List<string[]> expectedOrder)
    {
        var actualOrder = new List<string[]>();
        var chartNames = await GetCategoryNames();
        var priorityTags = await PriorityTags.AllAsync();
        for (int i = 0; i < chartNames.Count; i++)
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
}