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

    [Then("the local authority result should be correct")]
    private async Task ThenTheLocalAuthorityResultShouldBeCorrect()
    {
        var response = _api[RequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<LocalAuthority>();

        result.Code.Should().Be("201");
        result.Name.Should().Be("City of London");
    }

    [Then("the local authority result should be not found")]
    private void ThenTheLocalAuthorityResultShouldBeNotFound()
    {
        var result = _api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the local authorities suggest result should be correct")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldBeCorrect()
    {
        var response = _api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<LocalAuthority>>().Results;
        var result = results.FirstOrDefault();
        result.Should().NotBeNull();

        result?.Text.Should().Be("*201*");
        result?.Document?.Name.Should().Be("City of London");
        result?.Document?.Code.Should().Be("201");
    }

    [Then("the local authorities suggest result should be:")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldBe(Table table)
    {
        var response = _api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<LocalAuthority>>().Results;

        var set = new List<dynamic>();

        foreach (var result in results)
        {
            set.Add(new { result.Text, result.Document?.Name, result.Document?.Code });
        }

        table.CompareToDynamicSet(set, false);
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

    [Then("the local authorities suggest result should have the follow validation errors:")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldHaveTheFollowValidationErrors(Table table)
    {
        var response = _api[SuggestRequestKey].Response;

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
}