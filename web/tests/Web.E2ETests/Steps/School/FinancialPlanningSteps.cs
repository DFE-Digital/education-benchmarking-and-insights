using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.School.CurriculumFinancialPlanningSteps;
using Xunit;

namespace Web.E2ETests.Steps.School;

[Binding]
[Scope(Feature = "School curriculum and financial planning")]
public class FinancialPlanningSteps(PageDriver driver)
{
    private StartPage? _startPage;
    private HelpPage? _helpPage;
    private SelectYearPage? _selectYearPage;
    private PrePopulatedDataPage? _prePopulatedDataPage;

    [Given("I am on start page for school with URN '(.*)'")]
    public async Task GivenIAmOnStartPageForSchoolWithUrn(string urn)
    {
        var url = StartUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _startPage = new StartPage(page);
        await _startPage.IsDisplayed();
    }

    [When("I click can be found here on start page")]
    public async Task WhenIClickCanBeFoundHereOnStartPage()
    {
        Assert.NotNull(_startPage);
        _helpPage = await _startPage.ClickHelp();
    }

    [Then("the help page is displayed")]
    public async Task ThenTheHelpPageIsDisplayed()
    {
        Assert.NotNull(_helpPage);
        await _helpPage.IsDisplayed();
    }

    [Given("I am on the help page for school with URN '(.*)'")]
    public async Task GivenIAmOnTheHelpPageForSchoolWithUrn(string urn)
    {
        var url = HelpUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _helpPage = new HelpPage(page);
        await _helpPage.IsDisplayed();
    }

    [When("I click back link on help page")]
    public async Task WhenIClickBacklinkOnHelpPage()
    {
        Assert.NotNull(_helpPage);
        _startPage = await _helpPage.ClickBack();
    }

    [Then("the start page is displayed")]
    public async Task ThenTheStartPageIsDisplayed()
    {
        Assert.NotNull(_startPage);
        await _startPage.IsDisplayed();
    }

    [When("I click continue on start page")]
    public async Task WhenIClickContinueOnStartPage()
    {
        Assert.NotNull(_startPage);
        _selectYearPage = await _startPage.ClickContinue();
    }

    [Then("the year selection page is displayed")]
    public async Task ThenTheYearSelectionPageIsDisplayed()
    {
        Assert.NotNull(_selectYearPage);
        await _selectYearPage.IsDisplayed();
    }

    [Given("I am on year selection page for school with URN '(.*)'")]
    public async Task GivenIAmOnYearSelectionPageForSchoolWithUrn(string urn)
    {
        var url = SelectYearUrl(urn);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _selectYearPage = new SelectYearPage(page);
        await _selectYearPage.IsDisplayed();
    }

    [Given("'(.*)' is selected")]
    public async Task GivenIsSelected(string year)
    {
        Assert.NotNull(_selectYearPage);

        await _selectYearPage.ChooseYear(PlanYearFromFriendlyName(year));
    }

    [When("I click continue on year selection page")]
    public async Task WhenIClickContinueOnYearSelectionPage()
    {
        Assert.NotNull(_selectYearPage);
        _prePopulatedDataPage = await _selectYearPage.ClickContinue();
    }

    [Then("the pre-populated data page is displayed")]
    public async Task ThenThePrePopulatedDataPageIsDisplayed()
    {
        Assert.NotNull(_prePopulatedDataPage);
        await _prePopulatedDataPage.IsDisplayed();
    }

    [When("I click back link on year selection page")]
    public async Task WhenIClickBackLinkOnYearSelectionPage()
    {
        Assert.NotNull(_selectYearPage);
        _startPage = await _selectYearPage.ClickBack();
    }

    private static FinancialPlanYear PlanYearFromFriendlyName(string year)
    {
        return year switch
        {
            "now" => FinancialPlanYear.ThisYear,
            "next" => FinancialPlanYear.NextYear,
            "two" => FinancialPlanYear.TwoYearsTime,
            "three" => FinancialPlanYear.ThreeYearsTime,
            _ => throw new ArgumentOutOfRangeException(nameof(year))
        };
    }

    private static string StartUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/financial-planning/create?step=start";
    private static string HelpUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/financial-planning/create?step=help";
    private static string SelectYearUrl(string urn) => $"{TestConfiguration.ServiceUrl}/school/{urn}/financial-planning/create?step=select-year";
}