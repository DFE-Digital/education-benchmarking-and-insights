using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Non Financial education health care plans local authorities history endpoint")]
public class NonFinancialEducationHealthCarePlansLocalAuthoritiesHistory(NonFinancialApiDriver api)
{
    private const string Key = "education-health-care-plans";
    private const string HistoryKey = "education-health-care-plans-history";
    private History<LocalAuthorityNumberOfPlansYear>? _historyResult;
    private LocalAuthorityNumberOfPlans[] _results = [];

    [Given("an education health care plans history request with LA codes:")]
    public void GivenAnEducationHealthCarePlansHistoryRequestWithLaCodes(DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/education-health-care-plans/local-authorities/history?code={string.Join("&code=", codes)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an education health care plans request with dimension '(.*)' and LA codes:")]
    public void GivenAnEducationHealthCarePlansRequestWithDimensionAndLaCodes(string dimension, DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/education-health-care-plans/local-authorities?code={string.Join("&code=", codes)}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the education health care plans request")]
    public async Task WhenISubmitTheEducationHealthCarePlansRequest()
    {
        await api.Send();
    }

    [Then("the education health care plans history result should be ok and have the following values:")]
    public async Task ThenTheEducationHealthCarePlansHistoryResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[HistoryKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        _historyResult = content.FromJson<History<LocalAuthorityNumberOfPlansYear>>();
        var startYear = table.Rows.First()["StartYear"];
        var endYear = table.Rows.First()["EndYear"];

        Assert.Equal(startYear, _historyResult.StartYear.ToString());
        Assert.Equal(endYear, _historyResult.EndYear.ToString());
    }

    [Then("the education health care plans history result should have the following plan for '(.*)':")]
    public void ThenTheEducationHealthCarePlansHistoryResultShouldHaveTheFollowingPlanFor(int year, DataTable table)
    {
        var actual = _historyResult?.Plans?.FirstOrDefault(p => p.RunId == year.ToString());

        Assert.NotNull(actual);
        table.CompareToInstance(actual);
    }

    [Then("the education health care plans result should be ok and contain the following values:")]
    public async Task ThenTheEducationHealthCarePlansResultShouldBeOkAndContainTheFollowingValues(DataTable table)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        _results = content.FromJson<LocalAuthorityNumberOfPlans[]>();

        table.CompareToSet(_results);
    }

    [Given("an education health care plans history request with no codes")]
    public void GivenAnEducationHealthCarePlansHistoryRequestWithNoCodes()
    {
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/education-health-care-plans/local-authorities/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an education health care plans request with no codes")]
    public void GivenAnEducationHealthCarePlansRequestWithNoCodes()
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/education-health-care-plans/local-authorities", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the education health care plans history result should be bad request")]
    public void ThenTheEducationHealthCarePlansHistoryResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[HistoryKey].Response);
    }

    [Then("the education health care plans result should be bad request")]
    public void ThenTheEducationHealthCarePlansResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[Key].Response);
    }
}