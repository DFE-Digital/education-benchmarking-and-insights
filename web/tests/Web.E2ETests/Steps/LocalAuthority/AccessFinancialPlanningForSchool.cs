using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Xunit;
using HomePage = Web.E2ETests.Pages.School.HomePage;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority access to School curriculum and financial planning for its schools.")]
public class AccessFinancialPlanningForSchools(PageDriver driver)
{
    private CurriculumFinancialPlanningPage? _curriculumAndFinancialPlanningPage;
    private HomePage? _schoolHomePage;

    private static string SchoolHomeUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}";

    [Given("I am on school homepage for school with urn '(.*)'")]
    public async Task GivenIAmOnSchoolHomepageForSchoolWithUrn(string urn)
    {
        var url = SchoolHomeUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _schoolHomePage = new HomePage(page);
        await _schoolHomePage.IsDisplayed();
    }

    [When("I click on curriculum and financial planning")]
    public async Task WhenIClickOnCurriculumAndFinancialPlanning()
    {
        Assert.NotNull(_schoolHomePage);
        _curriculumAndFinancialPlanningPage = await _schoolHomePage.ClickFinancialPlanning();
    }

    [Then("the curriculum and financial planning page is displayed")]
    public async Task ThenTheCurriculumAndFinancialPlanningPageIsDisplayed()
    {
        Assert.NotNull(_curriculumAndFinancialPlanningPage);
        await _curriculumAndFinancialPlanningPage.IsDisplayed();
    }

    [Then("the forbidden page is displayed")]
    public async Task ThenTheCurriculumAndFinancialPlanningPageIsForbidden()
    {
        Assert.NotNull(_curriculumAndFinancialPlanningPage);
        await _curriculumAndFinancialPlanningPage.IsForbidden();
    }
}