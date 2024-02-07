using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions.Extensions;
using FluentAssertions;
using Newtonsoft.Json.Linq;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

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

    [Then("the school expenditure result should be ok")]
    public async Task ThenTheSchoolExpenditureResultShouldBeOk()
    {
        var response = _api[SchoolFinancesKey].Response;
        response.Should().NotBeNull().And.Subject.StatusCode.Should().Be(HttpStatusCode.OK);

        var jsonString = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(jsonString);

        json.Should().ContainKey("results");
        
        var resultsArray = json["results"]?.ToObject<JArray>() ?? throw new ArgumentNullException();
        
        resultsArray.Should().NotBeEmpty();

        var firstResult = resultsArray[0].ToObject<SchoolExpenditure>() ?? throw new ArgumentNullException();
        var secondResult = resultsArray[1].ToObject<SchoolExpenditure>()  ?? throw new ArgumentNullException();
        
        firstResult.Name.Should().Be("Wells Free School");
        firstResult.Urn.Should().Be("139696");
        
        secondResult.Name.Should().Be("Hadlow Rural Community School");
        secondResult.Urn.Should().Be("139697");

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

    [Then("the schools expenditure result should be page '(.*)' with '(.*)' page size")]
    public async Task ThenTheSchoolsExpenditureResultShouldBePageWithPageSize(int page, int pageSize)
    {
        var response = _api[SchoolFinancesKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<PagedResults<Finances>>() ?? throw new ArgumentNullException();
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);
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

    [Then("the school workforce result should be ok")]
    public async Task ThenTheSchoolWorkforceResultShouldBeOk()
    {
        var response = _api[SchoolWorkforceKey].Response;
        response.Should().NotBeNull().And.Subject.StatusCode.Should().Be(HttpStatusCode.OK);

        var jsonString = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(jsonString);

        json.Should().ContainKey("results");
        
        var resultsArray = json["results"]?.ToObject<JArray>() ?? throw new ArgumentNullException();
        
        resultsArray.Should().NotBeEmpty();

        var result = resultsArray[0].ToObject<SchoolWorkforce>() ?? throw new ArgumentNullException();
        
        result.Name.Should().Be("Wells Free School");
        result.Urn.Should().Be("139696");
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

    [Then("the schools workforce result should be page '(.*)' with '(.*)' page size")]
    public async Task ThenTheSchoolsWorkforceResultShouldBePageWithPageSize(int page, int pageSize)
    {
        var response = _api[SchoolWorkforceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsByteArrayAsync();
        //todo troubleshoot the reason of failure 
        var result = content.FromJson<PagedResults<SchoolWorkforce>>() ?? throw new ArgumentNullException();
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);
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