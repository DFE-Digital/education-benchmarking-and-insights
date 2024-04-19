using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.School;

public class CommercialResourcesPage(IPage page)
{
    private readonly string[] _allResourcesNames =
    {
        "Teaching and teaching support staff", "Agency supply teaching staff", "Professional services",
        "Teaching and teaching support staff", "Non-educational support staff", "Non-educational support staff",
        "Educational supplies", "Learning Resources", "Educational ICT", "Educational ICT", "Premises staff and services",
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
    private ILocator AllCommercialLinks => page.Locator(Selectors.AllCommercialLinks);


    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await BackLink.ShouldBeVisible();
        Assert.Equal("Recommended for this school", await RecommendedResourcesHeadings.First.TextContentAsync());
    }

    public async Task CheckPriorityOrderOfResources(List<string[]> expectedOrder)
    {
        var actualOrder = new List<string[]>();
        var chartNames = await GetResourceNames("recommended");
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

    public async Task AreAllResourcesVisible()
    {
       
        Assert.Equal(await GetResourceNames("all"), _allResourcesNames);
    }
    
    public async Task AreCorrectLinksDisplayed()
    {
        List<(string expectedText, string expectedHref, string expectedTarget)> expectedElements = new List<(string, string, string)>
{
    ("find a framework agreement for goods or services", "https://www.gov.uk/guidance/find-a-dfe-approved-framework-for-your-school", "_blank"),
    ("Hiring supply teachers and agency workers", "https://find-dfe-approved-framework.service.gov.uk/list/supply-teachers", "_blank"),
    ("Specialist professional services", "https://find-dfe-approved-framework.service.gov.uk/list/specialist-professional-services", "_blank"),
    ("Guidance for CFP", "https://www.gov.uk/guidance/integrated-curriculum-and-financial-planning-icfp", "_blank"),
    ("Teaching vacancies", "https://teaching-vacancies.service.gov.uk/", "_blank"),
    ("Books and educational resources buying guidance", "https://www.gov.uk/guidance/buying-for-schools/books-and-educational-resources", "_blank"),
    ("Print Marketplace", "https://find-dfe-approved-framework.service.gov.uk/list/print-marketplace", "_blank"),
    ("Books and educational resources", "https://www.gov.uk/guidance/buying-for-schools--2", "_blank"),
    ("Audio visual solutions", "https://find-dfe-approved-framework.service.gov.uk/list/av-solutions", "_blank"),
    ("Network connectivity and telecommunication solutions", "https://find-dfe-approved-framework.service.gov.uk/list/network-telecomms", "_blank"),
    ("Print market place", "https://find-dfe-approved-framework.service.gov.uk/list/print-marketplace", "_blank"),
    ("Building in use - support services", "https://find-dfe-approved-framework.service.gov.uk/list/fm-support-service", "_blank"),
    ("Good estate management for schools", "https://www.gov.uk/guidance/good-estate-management-for-schools", "_blank"),
    ("Internal fit-out and maintenance", "https://find-dfe-approved-framework.service.gov.uk/list/internal-maintenance", "_blank"),
    ("Electricity", "https://www.gov.uk/guidance/buying-for-schools--2", "_blank"),
    ("Water, wastewater and ancillary services 2", "https://find-dfe-approved-framework.service.gov.uk/list/water", "_blank"),
    ("Digital Marketplace (G-Cloud 12)", "https://find-dfe-approved-framework.service.gov.uk/list/digital-marketplace", "_blank"),
    ("DFE Furniture", "https://www.gov.uk/guidance/buy-school-furniture", "_blank"),
    ("Software licenses and associated services for academies and schools", "https://find-dfe-approved-framework.service.gov.uk/list/software-licenses", "_blank"),
    ("Audio visual solutions", "https://find-dfe-approved-framework.service.gov.uk/list/av-solutions", "_blank"),
    ("Office supplies", "https://find-dfe-approved-framework.service.gov.uk/list/office-supplies", "_blank"),
    ("Building in use", "https://find-dfe-approved-framework.service.gov.uk/list/fm-support-service", "_blank"),
    ("National professional qualification (NPQ) framework", "https://find-dfe-approved-framework.service.gov.uk/list/npq", "_blank"),
    ("Staff absence protection and reimbursement", "https://find-dfe-approved-framework.service.gov.uk/list/staff-absence", "_blank"),
    ("Risk protection arrangement", "https://find-dfe-approved-framework.service.gov.uk/list/rpa", "_blank")
};

        var elements = await AllCommercialLinks.AllAsync();
        Assert.Equal(expectedElements.Count, elements.Count);

        for (int i = 0; i < elements.Count; i++)
        {
            var element = elements[i];
            string? actualText = await element.InnerTextAsync();
            Assert.Contains(expectedElements[i].expectedText, actualText?.Trim());
            string? actualHref = await element.GetAttributeAsync("href");
            Assert.Equal(expectedElements[i].expectedHref, actualHref);
            string? actualTarget = await element.GetAttributeAsync("target");
            Assert.Equal(expectedElements[i].expectedTarget, actualTarget);
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
            int commaIndex = headingName.IndexOf(',');
            if (commaIndex != -1)
            {
                headingName = headingName.Substring(0, commaIndex).Trim();
            }
            resourcesHeading.Add(headingName.Trim());
        }

        return resourcesHeading;
    }
    
}