using System.Net;
using System.Text;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Functions.Extensions;
using EducationBenchmarking.Platform.Infrastructure.Search;
using FluentAssertions;
using TechTalk.SpecFlow.Assist;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class EstablishmentTrustsSteps
{
    private const string RequestKey = "get-trust";
    private const string QueryRequestKey = "query-trust";
    private const string SearchRequestKey = "search-trust";
    private const string SuggestInvalidRequestKey = "suggest-trust-invalid";
    private const string SuggestValidRequestKey = "suggest-trust-invalid";
    private readonly EstablishmentApiDriver _api;

    public EstablishmentTrustsSteps(EstablishmentApiDriver api)
    {
        _api = api;
    }

    [Given("a valid trust request with id '(.*)'")]
    private void GivenAValidTrustRequestWithId(string id)
    {
        _api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/trust/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the trusts request")]
    private async Task WhenISubmitTheTrustsRequest()
    {
        await _api.Send();
    }

    [Then("the trust result should be ok")]
    private void ThenTheTrustResultShouldBeOk()
    {
        var result = _api[RequestKey].Response;
        
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Given("a valid trusts query request")]
    private void GivenAValidTrustsQueryRequest()
    {
        _api.CreateRequest(QueryRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the trusts query result should be ok")]
    private void ThenTheTrustsQueryResultShouldBeOk()
    {
        var result = _api[QueryRequestKey].Response;
        
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Given("a valid trusts search request")]
    private void GivenAValidTrustsSearchRequest()
    {
        _api.CreateRequest(SearchRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/search", UriKind.Relative),
            Method = HttpMethod.Post
        });
    }

    [Then("the trusts search result should be ok")]
    private void ThenTheTrustsSearchResultShouldBeOk()
    {
        var result = _api[SearchRequestKey].Response;
        
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Given("an invalid trusts suggest request")]
    private void GivenAnInvalidTrustsSuggestRequest()
    {
        var content = new { Size = 0 };

        _api.CreateRequest(SuggestInvalidRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Then("the trusts suggest result should have the follow validation errors:")]
    private async Task ThenTheTrustsSuggestResultShouldHaveTheFollowValidationErrors(Table table)
    {
        var response = _api[SuggestInvalidRequestKey].Response;
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ValidationError[]>();
        var set = new List<dynamic>();
        
        foreach (var result in results)
        {
            set.Add(new { result.PropertyName, result.ErrorMessage });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Given("a valid trusts suggest request")]
    private void GivenAValidTrustsSuggestRequest()
    {
        var content = new { SearchText = "trust", Size = 10, SuggesterName = "trust-suggester" };

        _api.CreateRequest(SuggestValidRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Then("the trusts suggest result should be:")]
    private async Task ThenTheTrustsSuggestResultShouldBe(Table table)
    {
        var response = _api[SuggestValidRequestKey].Response;
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestOutput<Trust>>().Results ;
        var set = new List<dynamic>();
        
        foreach (var result in results)
        {
            set.Add(new { result.Text, result.Document?.Name, result.Document?.CompanyNumber });
        }

        table.CompareToDynamicSet(set, false);
    }
}