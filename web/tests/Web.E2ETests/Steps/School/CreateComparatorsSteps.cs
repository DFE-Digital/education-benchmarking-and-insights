using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School;
using Web.E2ETests.Pages.School.Comparators;
using Xunit;
namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School create comparator set")]
public class CreateComparatorsSteps(PageDriver driver)
{
    private CreateComparatorsByCharacteristicPage? _createComparatorsByCharacteristicPage;
    private CreateComparatorsByNamePage? _createComparatorsByNamePage;
    private CreateComparatorsByPage? _createComparatorsByPage;
    private CreateComparatorsPage? _createComparatorsPage;
    private HomePage? _schoolHomepage;

    [Given("I am on create comparators page for school with URN '(.*)'")]
    public async Task GivenIAmOnCreateComparatorsPageForSchoolWithURN(string urn)
    {
        var url = CreateComparatorsUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _createComparatorsPage = new CreateComparatorsPage(page);
        await _createComparatorsPage.IsDisplayed();
    }

    [Given("I have selected organisation '(.*)' after logging in")]
    public async Task GivenIHaveSelectedOrganisationAfterLoggingIn(string organisation)
    {
        Assert.NotNull(_createComparatorsPage);
        await _createComparatorsPage.SignIn(organisation);
    }

    [When("I click continue")]
    public async Task WhenIClickContinue()
    {
        Assert.NotNull(_createComparatorsPage);
        _createComparatorsByPage = await _createComparatorsPage.ClickContinue();
    }

    [When("I select the option By Characteristic and click continue")]
    public async Task WhenISelectTheOptionByCharacteristicAndClickContinue()
    {
        await SelectTheOptionByAndClickContinue(ComparatorsByTypes.Characteristic);
    }

    [When("I select the option By Name and click continue")]
    public async Task WhenISelectTheOptionByNameAndClickContinue()
    {
        await SelectTheOptionByAndClickContinue(ComparatorsByTypes.Name);
    }

    [When("I select the school with urn '(.*)' from suggester")]
    public async Task WhenISelectTheSchoolWithUrnFromSuggester(string urn)
    {
        Assert.NotNull(_createComparatorsByNamePage);
        await _createComparatorsByNamePage.TypeIntoSchoolSearchBox(urn);
        await _createComparatorsByNamePage.SelectItemFromSuggester();
    }

    [When("I click the choose school button")]
    public async Task WhenIClickTheChooseSchoolButton()
    {
        Assert.NotNull(_createComparatorsByNamePage);
        await _createComparatorsByNamePage.ClickChooseSchoolButton();
    }

    [When("I click the create set button")]
    public async Task WhenIClickTheCreateSetButton()
    {
        Assert.NotNull(_createComparatorsByNamePage);
        _schoolHomepage = await _createComparatorsByNamePage.ClickCreateSetButton();
    }

    [Then("the create comparators by page is displayed")]
    public async Task ThenTheCreateComparatorsByPageIsDisplayed()
    {
        Assert.NotNull(_createComparatorsByPage);
        await _createComparatorsByPage.IsDisplayed();
    }

    [Then("the create comparators by characteristic page is displayed")]
    public async Task ThenTheCreateComparatorsByCharacteristicPageIsDisplayed()
    {
        Assert.NotNull(_createComparatorsByCharacteristicPage);
        await _createComparatorsByCharacteristicPage.IsDisplayed();
    }

    [Then("the create comparators by name page is displayed")]
    public async Task ThenTheCreateComparatorsByNamePageIsDisplayed()
    {
        Assert.NotNull(_createComparatorsByNamePage);
        await _createComparatorsByNamePage.IsDisplayed();
    }

    [Then("the school home page is displayed")]
    public async Task ThenTheSchoolHomePageIsDisplayed()
    {
        Assert.NotNull(_schoolHomepage);
        await _schoolHomepage.IsDisplayed(isUserDefinedComparator: true);
    }

    private async Task SelectTheOptionByAndClickContinue(ComparatorsByTypes type)
    {
        Assert.NotNull(_createComparatorsByPage);
        await _createComparatorsByPage.SelectComparatorsBy(type);
        _createComparatorsByNamePage = null;
        _createComparatorsByCharacteristicPage = null;

        if (type == ComparatorsByTypes.Characteristic)
        {
            _createComparatorsByCharacteristicPage = await _createComparatorsByPage.ClickContinue(type) as CreateComparatorsByCharacteristicPage;
        }
        else
        {
            _createComparatorsByNamePage = await _createComparatorsByPage.ClickContinue(type) as CreateComparatorsByNamePage;
        }
    }

    private static string CreateComparatorsUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/comparators/create";
    internal static string CreateComparatorsByNameUrl(string urn) => $"{CreateComparatorsUrl(urn)}/by/name";
}