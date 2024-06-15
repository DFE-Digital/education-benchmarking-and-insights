using System.Net;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Platform.ApiTests.Drivers;
using Platform.Domain;
using Platform.Functions.Extensions;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightSchoolsSteps
{
    private const string SchoolFinancesKey = "get-school-finances";
    private const string SchoolWorkforceKey = "get-school-workforce";
    private readonly InsightApiDriver _api;

    public InsightSchoolsSteps(InsightApiDriver api)
    {
        _api = api;
    }

    [When("I submit the schools insights request")]
    public async Task WhenISubmitTheSchoolsInsightsRequest()
    {
        await _api.Send();
    }

    [Given("a valid schools expenditure request with urn '(.*)' and '(.*)'")]
    public void GivenAValidSchoolsExpenditureRequestWithUrnAnd(string urn1, string urn2)
    {
        _api.CreateRequest(SchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn1}%2c{urn2}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure request with page '(.*)' and urn '(.*)'")]
    public void GivenAValidSchoolExpenditureRequestWithPageAndUrn(string size, string urn)
    {
        _api.CreateRequest(SchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn}&page={size}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure request with size '(.*)' and urn '(.*)'")]
    public void GivenAValidSchoolExpenditureRequestWithSizeAndUrn(string pageSize, string urn)
    {
        _api.CreateRequest(SchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn}&pageSize={pageSize}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid schools workforce request with urn '(.*)'")]
    public void GivenAValidSchoolsWorkforceRequestWithUrn(string urn)
    {
        _api.CreateRequest(SchoolWorkforceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/workforce?urns={urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school workforce request with page '(.*)' and urn '(.*)'")]
    public void GivenAValidSchoolWorkforceRequestWithPageAndUrn(string page, string urn)
    {
        _api.CreateRequest(SchoolWorkforceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/workforce?urns={urn}&page={page}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school workforce request with size '(.*)' and urn '(.*)'")]
    public void GivenAValidSchoolWorkforceRequestWithSizeAndUrn(string pageSize, string urn)
    {
        _api.CreateRequest(SchoolWorkforceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/workforce?urns={urn}&pageSize={pageSize}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }
}