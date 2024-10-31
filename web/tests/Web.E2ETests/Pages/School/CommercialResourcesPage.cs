using Microsoft.Playwright;
using Xunit;
namespace Web.E2ETests.Pages.School;

public class CommercialResourcesPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    //private ILocator BackLink => page.Locator(Selectors.GovBackLink);
    private ILocator RecommendedResourcesHeadings => page.Locator($"{Selectors.RecommendedResources} {Selectors.H2}");
    private ILocator PriorityTags => page.Locator($"{Selectors.MainContent} {Selectors.GovukTag}");
    private ILocator AllResourcesTab => page.Locator(Selectors.AllResourcesTab);
    private ILocator AllResourcesHeadings => page.Locator($"{Selectors.AllResources} {Selectors.H2}");
    private ILocator ShowHideAllSectionsLink => page.Locator(Selectors.GovShowAllLinkText);
    private ILocator Sections => page.Locator(Selectors.GovAccordionSection);
    private ILocator AllCommercialLinks => page.Locator(Selectors.AllCommercialLinks);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        Assert.Equal("Recommended for this school", await RecommendedResourcesHeadings.First.TextContentAsync());
    }

    public async Task CheckPriorityOrderOfResources(List<string[]> expectedOrder)
    {
        var actualOrder = new List<string[]>();
        var chartNames = await GetResourceNames("recommended");
        var priorityTags = await PriorityTags.AllAsync();
        for (var i = 0; i < chartNames.Count; i++)
        {
            if (i < priorityTags.Count)
            {
                var chartName = chartNames[i];
                var priorityTag = await priorityTags[i].TextContentAsync() ?? string.Empty;
                var chartDetails = new[]
                {
                    chartName,
                    priorityTag.Trim()
                };
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
        //await BackLink.ShouldBeVisible();
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

    public async Task AreAllResourcesVisible(List<string> resourceNames) => Assert.Equal(resourceNames, await GetResourceNames("all"));

    public async Task AreCorrectLinksDisplayed(List<(string text, string href, string target)> expectedElements)
    {
        var elements = await AllCommercialLinks.AllAsync();
        Assert.Equal(expectedElements.Count, elements.Count);

        for (var i = 0; i < elements.Count; i++)
        {
            var element = elements[i];
            var actualText = await element.InnerTextAsync();
            Assert.Contains(expectedElements[i].text, actualText.Trim());
            var actualHref = await element.GetAttributeAsync("href");
            Assert.Equal(expectedElements[i].href, actualHref);
            var actualTarget = await element.GetAttributeAsync("target");
            Assert.Equal(expectedElements[i].target, actualTarget);
        }
    }

    private async Task<List<string>> GetResourceNames(string resource)
    {
        var h2Elements = resource switch
        {
            "all" => await AllResourcesHeadings.AllAsync(),
            "recommended" => await RecommendedResourcesHeadings.AllAsync(),
            _ => throw new ArgumentOutOfRangeException(nameof(resource))
        };
        var resourcesHeading = new List<string>();

        foreach (var h2 in h2Elements.Skip(1))
        {
            var headingName = await h2.TextContentAsync() ?? string.Empty;
            var commaIndex = headingName.IndexOf(',');
            if (commaIndex != -1)
            {
                headingName = headingName[..commaIndex].Trim();
            }
            resourcesHeading.Add(headingName.Trim());
        }

        return resourcesHeading;
    }
}