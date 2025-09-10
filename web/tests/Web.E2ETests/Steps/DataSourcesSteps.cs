using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Xunit;

namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "Data sources")]
public class DataSourcesSteps(PageDriver driver)
{
    private DataSourcesPage? _dataSourcesPage;
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

    [When("I click on the data sources link")]
    public async Task WhenIClickOnTheDataSourcesLink()
    {
        Assert.NotNull(_homePage);
        _dataSourcesPage = await _homePage.ClickDataSourcesLink();
    }

    [Then("the following CFR data sources are listed:")]
    public async Task ThenTheFollowingCfrDataSourcesAreListed(DataTable table)
    {
        Assert.NotNull(_dataSourcesPage);
        await _dataSourcesPage.AssertCfrDataSources(table);
    }

    [Then("the following AAR data sources are listed:")]
    public async Task ThenTheFollowingAarDataSourcesAreListed(DataTable table)
    {
        Assert.NotNull(_dataSourcesPage);
        await _dataSourcesPage.AssertAarDataSources(table);
    }

    private static string HomePageUrl() => $"{TestConfiguration.ServiceUrl}/";
}