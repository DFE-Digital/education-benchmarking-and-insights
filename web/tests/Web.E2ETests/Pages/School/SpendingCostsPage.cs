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
        "Teaching and teaching supply staff", "Administrative supplies", "Catering staff and services",
        "Educational ICT", "Educational supplies", "Non-educational support staff", "Other",
        "Premises staff and services", "Utilities",
    };

    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator Breadcrumbs => page.Locator(Selectors.GovBreadcrumbs);

    private ILocator ComparatorSetDetails =>
        page.Locator(Selectors.GovDetailsSummaryText,
            new PageLocatorOptions { HasText = "How we choose similar schools" });

    private ILocator ComparatorSetLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View or change which schools we compare you with" });

    private ILocator ComparatorSetDetailsText => page.Locator(Selectors.GovDetailsText);
    private ILocator PageH3Headings => page.Locator(Selectors.H3);
    private ILocator AllCharts => page.Locator(Selectors.ReactChartContainer);
    private ILocator AllChartsStats => page.Locator(Selectors.ReactChartStats);

    private ILocator TeachingAndTeachingSupplyStaffLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all teaching and teaching supply staff" });

    private ILocator AdministrativeSuppliesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all administrative supplies" });

    private ILocator CateringStaffAndServicesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all catering staff and services" });

    private ILocator EducationalIctLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all educational ICT" });

    private ILocator EducationalSuppliesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all educational supplies" });

    private ILocator NonEducationalSupportStaffLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all non-educational support staff" });

    private ILocator OtherLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all other" });

    private ILocator PremisesStaffAndServicesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all premises staff and services" });

    private ILocator UtilitiesLink => page.Locator(Selectors.GovLink,
        new PageLocatorOptions { HasText = "View all utilities" });


    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await Breadcrumbs.ShouldBeVisible();
        Assert.Equal(await PageH3Headings.Count(), _h3Names.Length);
        await AssertHeading3(_h3Names);
        Assert.Equal(9, await AllCharts.Count());
        Assert.Equal(9, await AllChartsStats.Count());
        await CheckVisibility(AllChartsStats);
        await CheckVisibility(AllCharts);

        //add assertions for the presence of links 
    }

    public async Task ClickComparatorSetDetails()
    {
        await ComparatorSetDetails.Click();
    }

    public async Task IsDetailsSectionVisible()
    {
        await ComparatorSetDetailsText.ShouldBeVisible();
        await ComparatorSetLink.ShouldBeVisible();
    }

    public async Task CheckOrderOfCharts(List<string> expectedOrder)
    {
        var actualOrder = new List<string>();
        foreach (var h3 in await PageH3Headings.AllAsync())
        {
            actualOrder.Add(await h3.TextContentAsync() ?? throw new InvalidOperationException());
        }

        Assert.Equal(actualOrder, expectedOrder);
    }

    private async Task AssertHeading3(string[] expected)
    {
        var allContent = await PageH3Headings.AllTextContentsAsync();

        foreach (var expectedHeading in expected)
        {
            Assert.Contains(expectedHeading, allContent);
        }
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

    private async Task CheckVisibility(ILocator locator)
    {
        foreach (var element in await locator.AllAsync())
        {
            await element.ShouldBeVisible();
        }
    }
}