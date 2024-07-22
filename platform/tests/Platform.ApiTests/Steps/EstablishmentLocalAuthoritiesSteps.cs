using System.Net;
using System.Text;
using FluentAssertions;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.ApiTests.Drivers;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Search;
using TechTalk.SpecFlow.Assist;

namespace Platform.ApiTests.Steps;

[Binding]
public class EstablishmentLocalAuthoritiesSteps
{
    private const string RequestKey = "get-local-authority";
    private const string SuggestRequestKey = "suggest-local-authority";
    private readonly EstablishmentApiDriver _api;

    public EstablishmentLocalAuthoritiesSteps(EstablishmentApiDriver api)
    {
        _api = api;
    }

    [Given("a valid local authority request with id '(.*)'")]
    private void GivenAValidLocalAuthorityRequestWithId(string id)
    {
        _api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authority/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid local authority request with id '(.*)'")]
    private void GivenAnInvalidValidLocalAuthorityRequestWithId(string id)
    {
        _api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authority/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid local authorities suggest request with searchText '(.*)")]
    private void GivenAValidLocalAuthoritiesSuggestRequest(string searchText)
    {
        var content = new { SearchText = searchText, Size = 5, SuggesterName = "local-authority-suggester" };

        _api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("an invalid local authorities suggest request")]
    private void GivenAnInvalidLocalAuthoritiesSuggestRequest()
    {
        var content = new { Size = 0 };

        _api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [When("I submit the local authorities request")]
    private async Task WhenISubmitTheLocalAuthoritiesRequest()
    {
        await _api.Send();
    }

    [Then("the local authority result should be ok and have the following values:")]
    private async Task ThenTheLocalAuthorityResultShouldHaveValues(Table table)
    {
        var response = _api[RequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<LocalAuthority>();

        table.CompareToInstance(result);
    }

    [Then("the local authority result should be not found")]
    private void ThenTheLocalAuthorityResultShouldBeNotFound()
    {
        var result = _api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the local authorities suggest result should be ok and have the following values:")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldHaveValues(Table table)
    {
        var response = _api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<LocalAuthority>>().Results;
        var result = results.FirstOrDefault();
        result.Should().NotBeNull();

        var actual = new
        {
            result?.Text,
            result?.Document?.Name,
            result?.Document?.Code
        };

        table.CompareToInstance(actual);
    }

    [Then("the local authorities suggest result should be ok and have the following multiple values:")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldHaveMultipleValues(Table table)
    {
        var response = _api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<LocalAuthority>>().Results.ToList();

        var set = results.Select(result => new
        {
            result.Text,
            result.Document?.Name,
            result.Document?.Code
        }).ToList();

        table.CompareToSet(set);
    }

    [Then("the local authorities suggest result should be empty")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldBeEmpty()
    {
        var response = _api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<LocalAuthority>>().Results;

        results.Should().BeEmpty();
    }

    [Then("the local authorities suggest result should be bad request and have the following validation errors:")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldHaveTheFollowValidationErrors(Table table)
    {
        var response = _api[SuggestRequestKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ValidationError[]>();

        table.CompareToSet(results);
    }
}