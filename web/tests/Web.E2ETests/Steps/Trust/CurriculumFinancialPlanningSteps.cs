using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.Trust;
using Xunit;

namespace Web.E2ETests.Steps.Trust;
[Binding]
[Scope(Feature = "Trust curriculum and financial planning")]
public class CurriculumFinancialPlanningSteps(PageDriver driver)
{
    private CurriculumFinancialPlanningPage? _curriculumFinancialPlanningPage;
    
    [Given("I am on Curriculum and financial planning page for trust with company number '(.*)'")]
    public async Task GivenIAmOnCurriculumAndFinancialPlanningPageForTrustWithCompanyNumber(string companyNumber)
    {
        var url = TrustCurriculumFinancialPlanningPageUrl(companyNumber);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);
        _curriculumFinancialPlanningPage = new CurriculumFinancialPlanningPage(page);
        await _curriculumFinancialPlanningPage.IsDisplayed();
    }

    [When("I click on show all sections")]
    public async Task WhenIClickOnShowAllSections()
    {
        Assert.NotNull(_curriculumFinancialPlanningPage);
        await _curriculumFinancialPlanningPage.ClickShowAllSections();
    }

    [Then("all accordions are expanded")]
    public async Task ThenAllAccordionsAreExpanded()
    {
        Assert.NotNull(_curriculumFinancialPlanningPage);
        await _curriculumFinancialPlanningPage.AreSectionsExpanded();
    }

    [Then("following is shown for '(.*)'")]
    public async Task ThenFollowingIsShownFor(string accordionName, Table table)
    {
        var expected = new List<List<string>>();
        {
            var headers = table.Header.ToList();

            expected.Add(headers);
            expected.AddRange(table.Rows.Select(row => row.Select(cell => cell.Value).ToList()));
        }
        Assert.NotNull(_curriculumFinancialPlanningPage);
        await _curriculumFinancialPlanningPage.IsTableDataDisplayed(AccordionsNamesFromFriendlyNames(accordionName),
            expected);
    }

    private static AccordionsNames AccordionsNamesFromFriendlyNames(string accordionName)
    {
        return accordionName switch
        {
            "Contact Ratio" => AccordionsNames.ContactRatio,
            "Average class size" => AccordionsNames.AverageClassSize,
            "In-year balance" => AccordionsNames.InYearBalance,
            _ => throw new ArgumentOutOfRangeException(nameof(accordionName))
        };
    }
    private static string TrustCurriculumFinancialPlanningPageUrl(string companyNumber) => $"{TestConfiguration.ServiceUrl}/trust/{companyNumber}/financial-planning";
    
}