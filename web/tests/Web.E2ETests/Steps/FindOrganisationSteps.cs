using Reqnroll;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Xunit;
using HomePage = Web.E2ETests.Pages.School.HomePage;

namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "Find organisation")]
public class FindOrganisationSteps(PageDriver driver)
{
    private FindOrganisationPage? _findOrganisationPage;
    private HomePage? _schoolHomePage;

    [Given(@"I am on find organisation page")]
    public async Task GivenIAmOnFindOrganisationPage()
    {
        var url = FindOrganisationUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _findOrganisationPage = new FindOrganisationPage(page);
        await _findOrganisationPage.IsDisplayed();
    }

    [When("I select the school with urn '(.*)' from suggester")]
    public async Task WhenISelectTheSchoolWithUrnFromSuggester(string urn)
    {
        Assert.NotNull(_findOrganisationPage);
        await _findOrganisationPage.TypeIntoSchoolSearchBox(urn);
        await _findOrganisationPage.SelectItemFromSuggester();
    }

    [When("I type '(.*)' into the search bar")]
    public async Task WhenITypeIntoTheSearchBar(string keyword)
    {
        Assert.NotNull(_findOrganisationPage);
        await _findOrganisationPage.TypeIntoSchoolSearchBox(keyword);
    }


    [When("I click Continue")]
    public async Task WhenIClickContinue()
    {
        Assert.NotNull(_findOrganisationPage);
        _schoolHomePage = await _findOrganisationPage.ClickContinue();
    }

    [Then("the school homepage is displayed")]
    public async Task ThenTheSchoolHomepageIsDisplayed()
    {
        Assert.NotNull(_schoolHomePage);
        await _schoolHomePage.IsDisplayed();
    }

    [Given("'(.*)' organisation type is selected")]
    public async Task GivenOrganisationTypeIsSelected(string organisationType)
    {
        Assert.NotNull(_findOrganisationPage);
        var parsed = Enum.TryParse(organisationType, out OrganisationTypes type);
        await _findOrganisationPage.SelectOrganisationType(type);
    }

    [Then("each suggester result contains '(.*)'")]
    public async Task ThenEachSuggesterResultContains(string keyword)
    {
        Assert.NotNull(_findOrganisationPage);
        await _findOrganisationPage.AssertSearchResults(keyword);
    }

    private static string FindOrganisationUrl() => $"{TestConfiguration.ServiceUrl}/find-organisation";
}