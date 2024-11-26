using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Xunit;
namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School access to parent's Trust Forecast")]
public class AccessTrustForecastAsAcademy(PageDriver driver)
{
    private TrustForecastPage? _trustForecastPage;
    private HomePage? _trustHomePage;

    private static string TrustHomeUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}";

    [Given("I am on trust homepage for trust with company number '(.*)'")]
    public async Task GivenIAmOnTrustHomepageForTrustWithCompanyNumber(string companyNumber)
    {
        var url = TrustHomeUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _trustHomePage = new HomePage(page);
        await _trustHomePage.IsDisplayed();
    }

    [When("I click on trust forecast")]
    public async Task WhenIClickOnTrustForecast()
    {
        Assert.NotNull(_trustHomePage);
        _trustForecastPage = await _trustHomePage.ClickTrustForecast();
    }

    [Then("the trust forecast page is displayed")]
    public async Task ThenTheTrustForecastPageIsDisplayed()
    {
        Assert.NotNull(_trustForecastPage);
        await _trustForecastPage.IsDisplayed();
    }

    [Then("the forbidden page is displayed")]
    public async Task ThenTheTrustForecastPageIsForbidden()
    {
        Assert.NotNull(_trustForecastPage);
        await _trustForecastPage.IsForbidden();
    }
}