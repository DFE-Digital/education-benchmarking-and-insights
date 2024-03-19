using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Web.E2ETests.Pages.School;
using Xunit;

namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "Find organisation")]
public class FindOrganisationSteps(PageDriver driver)
{
    private FindOrganisationPage? _findOrganisationPage;
    private HomePage? _homePage;

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
        _homePage = await _findOrganisationPage.SelectSchoolFromSuggester(urn);
    }

    [Then("the school homepage is displayed")]
    public async Task ThenTheSchoolHomepageIsDisplayed()
    {
        Assert.NotNull(_homePage);
        await _homePage.IsDisplayed();
    }

    private static string FindOrganisationUrl() => $"{TestConfiguration.ServiceUrl}/find-organisation";


}