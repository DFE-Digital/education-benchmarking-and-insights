using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps.School;
[Binding]
[Scope(Feature = "School find ways to spend less")]
public class CommercialResourcesSteps(PageDriver driver)
{
    private CommercialResourcesPage? _commercialResourcesPage;
    
    [Given("I am on resources page for school with URN '(.*)'")]
    public async Task GivenIAmOnResourcesPageForSchoolWithUrn(string urn)
    {
        var url = CommercialResourcesUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _commercialResourcesPage = new CommercialResourcesPage(page);
        await _commercialResourcesPage.IsDisplayed();
    }

    [Given("the following priority categories are shown on the page")]
    public async Task GivenTheFollowingPriorityCategoriesAreShownOnThePage(Table table)
    {
        Assert.NotNull(_commercialResourcesPage);
        var expectedOrder = new List<string[]>();
        foreach (var row in table.Rows)
        {
            string[] chartPriorityArray = {
                row["Name"],
                row["Priority"]
            };
            expectedOrder.Add(chartPriorityArray);
        }

        await _commercialResourcesPage.CheckPriorityOrderOfResources(expectedOrder);
    }

    [When("I click on all resources")]
    public async Task WhenIClickOnAllResources()
    {
        Assert.NotNull(_commercialResourcesPage);
        await _commercialResourcesPage.ClickAllResources();
    }
    
    [Then("all resources tab is displayed")]
    public async Task ThenAllResourcesTabIsDisplayed()
    {
        Assert.NotNull(_commercialResourcesPage);
        await _commercialResourcesPage.IsAllResourcesDisplayed();
    }
    
    [When("I click on show all sections")]
    public async Task WhenIClickOnShowAllSections()
    {
        Assert.NotNull(_commercialResourcesPage);
        await _commercialResourcesPage.ClickShowAllSections();
    }
    
    
    [Then("all sections on the page are expanded")]
    public async Task ThenAllSectionsOnThePageAreExpanded()
    {
        Assert.NotNull(_commercialResourcesPage);
        await _commercialResourcesPage.AreSectionsExpanded();
    }
    
    [Then("the show all text changes to hide all sections")]
    public async Task ThenTheShowAllTextChangesToHideAllSections()
    {
        Assert.NotNull(_commercialResourcesPage);
        await _commercialResourcesPage.IsShowHideAllSectionsText("Hide all sections");
    }
    private static string CommercialResourcesUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/find-ways-to-spend-less";
    
}