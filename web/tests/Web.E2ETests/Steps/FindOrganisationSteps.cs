using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Web.E2ETests.Pages.School;
using Xunit;
using HomePage = Web.E2ETests.Pages.School.HomePage;

namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "Find organisation")]
public class FindOrganisationSteps(PageDriver driver)
{
    private FindOrganisationPage? _findOrganisationPage;
    private HomePage? _schoolHomePage;
    private SearchPage? _schoolSearchPage;

    [Given(@"I am on find organisation page")]
    public async Task GivenIAmOnFindOrganisationPage()
    {
        var url = FindOrganisationUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _findOrganisationPage = new FindOrganisationPage(page);
        await _findOrganisationPage.IsDisplayed();
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

    [When("I click Continue to school search")]
    public async Task WhenIClickContinueToSchoolSearch()
    {
        Assert.NotNull(_findOrganisationPage);
        _schoolSearchPage = await _findOrganisationPage.ClickContinueToSchoolSearch();
    }

    [Then("the school search page is displayed")]
    public async Task ThenTheSchoolSearchPageIsDisplayed()
    {
        Assert.NotNull(_schoolSearchPage);
        await _schoolSearchPage.IsDisplayed();
    }

    private static string FindOrganisationUrl() => $"{TestConfiguration.ServiceUrl}/find-organisation";
}