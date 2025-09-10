using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Xunit;

namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "Home page")]
public class HomePageSteps(PageDriver driver)
{
    private FindOrganisationPage? _findOrganisationPage;
    private HomePage? _homePage;

    [Given("I am on home page")]
    public async Task GivenIAmOnHomePage()
    {
        var url = HomePageUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _homePage = new HomePage(page);
        await _homePage.IsDisplayed();
    }

    [When("I click Start now button")]
    public async Task WhenIClickStartNowButton()
    {
        Assert.NotNull(_homePage);
        _findOrganisationPage = await _homePage.ClickStartNowButton();
    }

    [Then("the find organisation page is disabled")]
    public async Task ThenTheFindOrganisationPageIsDisabled()
    {
        Assert.NotNull(_findOrganisationPage);
        await _findOrganisationPage.IsDisplayed();
    }

    [Then("the service banner displays the title '(.*)', heading '(.*)' and body '(.*)'")]
    public async Task ThenTheServiceBannerDisplaysTheTitleHeadingAndBody(string title, string heading, string body)
    {
        Assert.NotNull(_homePage);
        await _homePage.HasBanner(title, heading, body);
    }

    private static string HomePageUrl() => $"{TestConfiguration.ServiceUrl}/";
}