using System.Net;
using FluentAssertions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class EstablishmentLocalAuthoritiesSteps : EstablishmentSteps
{
    private const string GetRequestKey = "get-local-authority";
    private const string SuggestRequestKey = "suggest-local-authority";
    private const string SearchRequestKey = "search-local-authority";
    private const string QueryRequestKey = "query-local-authority";

    public EstablishmentLocalAuthoritiesSteps(ITestOutputHelper output) : base(output)
    {
    }

    [When("I submit the local authorities request")]
    private async Task WhenISubmitTheLocalAuthoritiesRequest()
    {
        await Api.Send();
    }

    [Then("the local authority result should be ok")]
    private void ThenTheLocalAuthorityResultShouldBeOk()
    {
        var result = Api[GetRequestKey].Response ?? throw new NullException(Api[GetRequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Given("a valid local authority request with id '(.*)'")]
    private void GivenAValidLocalAuthorityRequestWithId(string id)
    {
        Api.CreateRequest(GetRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authority/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid local authorities suggest request")]
    private void GivenAValidLocalAuthoritiesSuggestRequest()
    {
        Api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/suggest", UriKind.Relative),
            Method = HttpMethod.Post
        });
    }

    [Given("a valid local authorities search request")]
    private void GivenAValidLocalAuthoritiesSearchRequest()
    {
        Api.CreateRequest(SearchRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/search", UriKind.Relative),
            Method = HttpMethod.Post
        });
    }

    [Given("a valid local authorities query request")]
    private void GivenAValidLocalAuthoritiesQueryRequest()
    {
        Api.CreateRequest(QueryRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the local authorities suggest result should be ok")]
    private void ThenTheLocalAuthoritiesSuggestResultShouldBeOk()
    {
        var result = Api[SuggestRequestKey].Response ?? throw new NullException(Api[SuggestRequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then("the local authorities search result should be ok")]
    private void ThenTheLocalAuthoritiesSearchResultShouldBeOk()
    {
        var result = Api[SearchRequestKey].Response ?? throw new NullException(Api[SearchRequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then("the local authorities query result should be ok")]
    private void ThenTheLocalAuthoritiesQueryResultShouldBeOk()
    {
        var result = Api[QueryRequestKey].Response ?? throw new NullException(Api[QueryRequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}