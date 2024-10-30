using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;
namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School find ways to spend less")]
public class CommercialResourcesSteps(PageDriver driver)
{
    private CommercialResourcesPage? _commercialResourcesPage;

    [Given("I am on '(.*)' resources page for school with URN '(.*)'")]
    public async Task GivenIAmOnResourcesPageForSchoolWithUrn(string tab, string urn)
    {
        var url = CommercialResourcesUrl(tab, urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _commercialResourcesPage = new CommercialResourcesPage(page);
        await _commercialResourcesPage.IsDisplayed();
    }

    [Given("the following priority categories are shown on the page")]
    public async Task GivenTheFollowingPriorityCategoriesAreShownOnThePage(DataTable table)
    {
        Assert.NotNull(_commercialResourcesPage);
        var expectedOrder = new List<string[]>();
        foreach (var row in table.Rows)
        {
            string[] chartPriorityArray =
            {
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
    [Given("I click on show all sections")]
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

    [Then("all resource sub categories are displayed on the page")]
    public async Task ThenAllResourceSubCategoriesAreDisplayedOnThePage(DataTable table)
    {
        Assert.NotNull(_commercialResourcesPage);
        var resourceNames = table.Rows
            .Select(row => row["Name"])
            .ToList();
        await _commercialResourcesPage.AreAllResourcesVisible(resourceNames);
    }

    [Then("all sub categories have correct links")]
    public async Task ThenAllSubCategoriesHaveCorrectLinks(DataTable table)
    {
        Assert.NotNull(_commercialResourcesPage);
        var expectedLinks = table.Rows
            .Select(row => new ValueTuple<string, string, string>(row["Text"], row["Href"], row["Target"]))
            .ToList();
        await _commercialResourcesPage.AreCorrectLinksDisplayed(expectedLinks);
    }

    private static string CommercialResourcesUrl(string tab, string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/find-ways-to-spend-less#{tab}";
}