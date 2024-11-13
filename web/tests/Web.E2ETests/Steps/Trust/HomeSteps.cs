using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Xunit;
namespace Web.E2ETests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust homepage")]
public class HomeSteps(PageDriver driver)
{
    private CompareYourCostsPage? _compareYourCostsPage;
    private HomePage? _trustHomePage;

    [Given("I am on trust homepage for trust with company number '(.*)'")]
    public async Task GivenIAmOnTrustHomepageForTrustWithCompanyNumber(string companyNumber)
    {
        var url = TrustHomeUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _trustHomePage = new HomePage(page);
        await _trustHomePage.IsDisplayed();
    }

    [When("I click on compare your costs")]
    public async Task WhenIClickOnCompareYourCosts()
    {
        Assert.NotNull(_trustHomePage);
        _compareYourCostsPage = await _trustHomePage.ClickCompareYourCosts();
    }

    [Then("the compare your costs page is displayed")]
    public async Task ThenTheCompareYourCostsPageIsDisplayed()
    {
        Assert.NotNull(_compareYourCostsPage);
        await _compareYourCostsPage.IsDisplayed();
    }

    [Then("I can see the following RAG ratings for cost categories in the trust:")]
    public async Task ThenICanSeeTheFollowingRagRatingsForSchsoolsInTheTrust(DataTable table)
    {
        Assert.NotNull(_trustHomePage);

        var rags = table.Rows.ToDictionary(row => row["Name"], row => row["Status"]);
        foreach (var row in rags)
        {
            await _trustHomePage.AssertCategoryRags(row.Key, row.Value);
        }
    }

    [Then("I can see the following RAG ratings for schools in the trust:")]
    public async Task ThenICanSeeTheFollowingRagRatingsForSchoolsInTheTrust(DataTable table)
    {
        Assert.NotNull(_trustHomePage);

        var rags = table.Rows.ToDictionary(row => row["Name"], row => row["Status"]);
        foreach (var row in rags)
        {
            await _trustHomePage.AssertSchoolRags(row.Key, row.Value);
        }
    }

    private static string TrustHomeUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}";
}