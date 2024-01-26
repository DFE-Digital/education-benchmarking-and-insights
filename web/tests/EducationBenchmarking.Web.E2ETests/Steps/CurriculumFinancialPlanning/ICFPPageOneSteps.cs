using EducationBenchmarking.Web.E2ETests.Pages.CurriculumFinancialPlanning;

namespace EducationBenchmarking.Web.E2ETests.Steps.CurriculumFinancialPlanning;

[Binding]
public class ICFPPageOneSteps(ICFPPageOne _icfpPageOne, ICFPHelpPage _icfpHelpPage)
{
    [Given(@"I am on page 1 of the curriculum and financial planning journey for school with URN '(.*)'")]
    public async Task GivenIAmOnPageOfTheCurriculumAndFinancialPlanningJourneyForSchoolWithUrn(string urn)
    {
        await _icfpPageOne.GoToPage(urn);
        await _icfpPageOne.AssertPage();
    }


    [When(@"I click the can be found here in the content of page one")]
    public async Task WhenIClickCanBeFoundHereTheInTheContentOfPageOne()
    {
        await _icfpPageOne.clickOnHelpBtn();
    }

    [Given(@"I am on the help page  for school with URN '(.*)'")]
    [Then(@"I am on the help page  for school with URN '(.*)'")]
    public async Task ThenIAmOnTheHelpPage(string urn)
    {
        await _icfpHelpPage.GoToPage(urn);
        await _icfpHelpPage.AssertPage();
    }

    [When(@"I click back CTA on help page")]
    public async Task WhenIClickBackCtaOnHelpPage()
    {
        await _icfpHelpPage.ClickBack();
    }

    [Then(@"I am on the help page")]
    public async Task ThenIAmOnTheHelpPage()
    {
        await _icfpHelpPage.AssertPage();
    }

    [Then(@"I am on page (.*) of the curriculum and financial planning journey")]
    public async Task ThenIAmOnPageOfTheCurriculumAndFinancialPlanningJourney(int p0)
    {
        await _icfpPageOne.AssertPage();
    }
}