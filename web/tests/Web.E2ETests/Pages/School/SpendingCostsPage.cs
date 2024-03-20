using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;

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

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await Breadcrumbs.ShouldBeVisible();
        Assert.Equal( await PageH3Headings.Count(), _h3Names.Length);
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

    private async Task AssertHeading3(string[] expected)
    {
        var allContent = await PageH3Headings.AllTextContentsAsync();

        foreach (var expectedHeading in expected)
        {
            Assert.Contains(expectedHeading, allContent);
        }
    }

    private async Task CheckVisibility(ILocator locator)
    {
        foreach (var element in await locator.AllAsync())
        {
            await element.ShouldBeVisible();
        }
    }
}