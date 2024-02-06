using EducationBenchmarking.Web.E2ETests.Pages;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class FinancialPlanningSteps(CurriculumFinancialPlanningPages pages)
{

    [When("I click the can be found here in the content of page one")]
    public async Task WhenIClickTheCanBeFoundHereInTheContentOfPageOne()
    {
        await pages.Start.ClickHelp();
    }

    [Given("I am on the help page for school with URN '(.*)'")]
    public async Task GivenIAmOnTheHelpPageForSchoolWithUrn(string urn)
    {
        await pages.Help.GoToPage(urn);
        await pages.Help.AssertPage();
    }

    [When("I click back link on help page")]
    public async Task WhenIClickBacklinkOnHelpPage()
    {
        await pages.Help.ClickBack();
    }

    [Then("I am on the help page")]
    public async Task ThenIAmOnTheHelpPage()
    {
        await pages.Help.AssertPage();
    }
    
    [Given("I am on start page for school with URN '(.*)'")]
    public async Task GivenIAmOnStartPageForSchoolWithUrn(string urn)
    {
        await pages.Start.GoToPage(urn);
        await pages.Start.AssertPage();
    }

    [Then("I am on start page")]
    public async Task ThenIAmOnStartPage()
    {
        await pages.Start.AssertPage();
    }

    [When("I click continue on start page")]
    public async Task WhenIClickContinueOnStartPage()
    {
        await pages.Start.ClickContinue();
    }

    [Then("I am on the year selection page")]
    public async Task ThenIAmOnTheYearSelectionPage()
    {
        await pages.SelectYear.AssertPage();
    }

    [Given("I am on select a year page for school with URN '(.*)'")]
    public async Task GivenIAmOnSelectAYearPageForSchoolWithUrn(string urn)
    {
        await pages.SelectYear.GoToPage(urn);
        await pages.SelectYear.AssertPage();
    }

    [Given("I have select year '(.*)'")]
    public async Task GivenIHaveSelectYear(string year)
    {
        await pages.SelectYear.ChooseYear(year);
    }

    [When("I click continue on select a year page")]
    public async Task WhenIClickContinueOnSelectAYearPage()
    {
        await pages.SelectYear.ClickContinue();
    }

    [When("I click back link on select a year page")]
    public async Task WhenIClickBackLinkOnSelectAYearPage()
    {
        await pages.SelectYear.ClickBack();
    }

    [Then("I am on the pre-populated data page")]
    public async Task ThenIAmOnThePrePopulatedDataPage()
    {
        await pages.PrePopulatedData.AssertPage();
    }
}