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
    private const string RequestKey = "education-health-care-plans-history";
    private History<LocalAuthorityNumberOfPlansYear>? _result;

    [Given("an education health care plans history request with codes '(.*)'")]
    public void GivenAnEducationHealthCarePlansLocalAuthoritiesRequest(string codes)
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/education-health-care-plans/local-authorities/history?code={codes}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the education health care plans history request")]
    public async Task WhenISubmitTheEducationHealthCarePlansLocalAuthoritiesRequest()
    {
        await api.Send();
    }

    [Then("the education health care plans history result should be ok and have the following values:")]
    public async Task ThenTheEducationHealthCarePlansLocalAuthoritiesResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[RequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        _result = content.FromJson<History<LocalAuthorityNumberOfPlansYear>>();
        var startYear = table.Rows.First()["StartYear"];
        var endYear = table.Rows.First()["EndYear"];

        Assert.Equal(startYear, _result.StartYear.ToString());
        Assert.Equal(endYear, _result.EndYear.ToString());
    }

    [Then("the education health care plans history result should be ok and have the following plan for '(.*)':")]
    public void ThenTheEducationHealthCarePlansLocalAuthoritiesResultShouldContainTheFollowingPlan(int year, DataTable table)
    {
        var response = api[RequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var actual = _result?.Plans?.FirstOrDefault(p => p.Year == year);

        Assert.NotNull(actual);
        table.CompareToInstance(actual);
    }


    [Given("an education health care plans history request with no codes")]
    public void GivenAnInvalidTheEducationHealthCarePlansLocalAuthoritiesRequest()
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/education-health-care-plans/local-authorities/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the education health care plans history result should be bad request")]
    public void ThenTheEducationHealthCarePlansLocalAuthoritiesResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[RequestKey].Response);
    }
}