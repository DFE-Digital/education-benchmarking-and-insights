using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using FluentAssertions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class EstablishmentLocalAuthoritiesSteps
{
    private const string GetRequestKey = "get-local-authority";
    private const string SuggestRequestKey = "suggest-local-authority";
    private const string SearchRequestKey = "search-local-authority";
    private const string QueryRequestKey = "query-local-authority";
    private readonly ApiDriver _api = new(Config.Apis.Establishment ?? throw new NullException(Config.Apis.Establishment));

   
    [When("I submit the local authorities request")]
    private async Task WhenISubmitTheLocalAuthoritiesRequest()
    {
        await _api.Send();
    }

    [Then("the local authority result should be ok")]
    private void ThenTheLocalAuthorityResultShouldBeOk()
    {
        var result = _api[GetRequestKey].Response ?? throw new NullException(_api[GetRequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Given("a valid local authority request with id '(.*)'")]
    private void GivenAValidLocalAuthorityRequestWithId(string id)
    {
        _api.CreateRequest(GetRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authority/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid local authorities suggest request")]
    private void GivenAValidLocalAuthoritiesSuggestRequest()
    {
        _api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/suggest", UriKind.Relative),
            Method = HttpMethod.Post
        });
    }

    [Given("a valid local authorities search request")]
    private void GivenAValidLocalAuthoritiesSearchRequest()
    {
        _api.CreateRequest(SearchRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/search", UriKind.Relative),
            Method = HttpMethod.Post
        });
    }

    [Given("a valid local authorities query request")]
    private void GivenAValidLocalAuthoritiesQueryRequest()
    {
        _api.CreateRequest(QueryRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }
    
    [Then("the local authorities suggest result should be ok")]
    private void ThenTheLocalAuthoritiesSuggestResultShouldBeOk()
    {
        var result = _api[SuggestRequestKey].Response ?? throw new NullException(_api[SuggestRequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then("the local authorities search result should be ok")]
    private void ThenTheLocalAuthoritiesSearchResultShouldBeOk()
    {
        var result = _api[SearchRequestKey].Response ?? throw new NullException(_api[SearchRequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then("the local authorities query result should be ok")]
    private void ThenTheLocalAuthoritiesQueryResultShouldBeOk()
    {
        var result = _api[QueryRequestKey].Response ?? throw new NullException(_api[QueryRequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}