using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;

public class CommercialResourcesPage(IPage page)
{
    private readonly string[] _allResourcesNames =
    {
        "Teaching and teaching support staff", "Agency supply teaching staff", "Professional services",
        "Teaching and teaching support staff", "Non-educational support staff", "Non-educational support staff",
        "Educational supplies", "Educational ICT", "Educational ICT", "Premises staff and services",
        "Cleaning and caretaking", "Maintenance of premises", "Utilities", "Energy", "Water and Sewerage",
        "Administrative supplies", "Administrative supplies (non-educational)", "Catering staff and services",
        "Catering Staff", "Other costs", "Staff development and training", "Staff related insurance",
        "Other insurance premiums"
    };

    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator BackLink => page.Locator(Selectors.GovBackLink);
    private ILocator RecommendedResourcesHeadings => page.Locator($"{Selectors.RecommendedResources} {Selectors.H2}");

    private ILocator PriorityTags => page.Locator($"{Selectors.MainContent} {Selectors.GovukTag}");
    private ILocator AllResourcesTab => page.Locator(Selectors.AllResourcesTab);
    private ILocator AllResourcesHeadings => page.Locator($"{Selectors.AllResources} {Selectors.H2}");
    private ILocator ShowHideAllSectionsLink => page.Locator(Selectors.GovShowAllLinkText);
    private ILocator Sections => page.Locator(Selectors.GovAccordionSection);



    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await BackLink.ShouldBeVisible();
        Assert.Equal("Recommended for this school", await RecommendedResourcesHeadings.First.TextContentAsync());
    }

    public async Task CheckPriorityOrderOfResources(List<string[]> expectedOrder)
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

    public async Task ClickAllResources()
    {
        await AllResourcesTab.Click();
    }

    public async Task IsAllResourcesDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await BackLink.ShouldBeVisible();
        Assert.Equal("All resources", await AllResourcesHeadings.First.TextContentAsync());
        await ShowHideAllSectionsLink.ShouldBeVisible();
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

    private async Task<List<string>> GetCategoryNames()
    {
        var h2Elements = await RecommendedResourcesHeadings.AllAsync();
        var categoryNames = new List<string>();

        foreach (var h2 in h2Elements.Skip(1))
        {
            var categoryName = await h2.TextContentAsync() ?? string.Empty;
            categoryNames.Add(categoryName.Trim());
        }

        return categoryNames;
    }
    
}