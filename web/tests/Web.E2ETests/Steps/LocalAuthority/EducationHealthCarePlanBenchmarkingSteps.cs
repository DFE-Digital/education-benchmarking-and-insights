using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority Education Health Care Plan Benchmarking")]
public class EducationHealthCarePlanBenchmarkingSteps(PageDriver driver)
{
    private EducationHealthCarePlanBenchmarkingPage? _benchmarkingPage;
    private ChooseLocalAuthoritiesToComparePage? _comparatorsPage;

    [Given("I am on education health care plan comparators selection page for local authority with code '(.*)'")]
    public async Task GivenIAmOnEducationHealthCarePlanComparatorsSelectionPageForLocalAuthorityWithCode(string laCode)
    {
        var url = EducationHealthCarePlansComparatorsUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _comparatorsPage = new ChooseLocalAuthoritiesToComparePage(page);
        await _comparatorsPage.IsDisplayed();
    }

    [When("I click the Start Benchmarking button")]
    public async Task WhenIClickTheStartBenchmarkingButton()
    {
        Assert.NotNull(_comparatorsPage);
        _benchmarkingPage = await _comparatorsPage.ClickStartBenchmarkingButton();
    }

    [Given("I am on education health care plan benchmarking page for local authority with code '(.*)'")]
    public async Task GivenIAmOnEducationHealthCarePlanBenchmarkingPageForLocalAuthorityWithCode(string laCode)
    {
        var url = EducationHealthCarePlanBenchmarkingUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _benchmarkingPage = new EducationHealthCarePlanBenchmarkingPage(page);
        await _benchmarkingPage.IsDisplayed();
    }

    [Then("I should see all the education health care plan charts displayed:")]
    public async Task ThenIShouldSeeAllTheEducationHealthCarePlanChartsDisplayed(DataTable table)
    {
        Assert.NotNull(_benchmarkingPage);
        await _benchmarkingPage.AreAllChartsDisplayed();
    }

    [When("I change view to table")]
    public async Task WhenIChangeViewToTable()
    {
        Assert.NotNull(_benchmarkingPage);
        await _benchmarkingPage.ClickViewAsTableAndApply();
    }

    [Then("the following is shown for '(.*)'")]
    public async Task ThenTheFollowingIsShownFor(string chartName, Table table)
    {
        var expected = new List<List<string>>();
        {
            var headers = table.Header.ToList();
            expected.Add(headers);
            expected.AddRange(table.Rows.Select(row => row.Select(cell => cell.Value).ToList()));
        }

        Assert.NotNull(_benchmarkingPage);
        await _benchmarkingPage.IsTableDataForChartDisplayed(chartName, expected);
    }

    private static string EducationHealthCarePlansComparatorsUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/comparators?type=EducationHealthCarePlans";

    private static string EducationHealthCarePlanBenchmarkingUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/education-health-care-plans";
}