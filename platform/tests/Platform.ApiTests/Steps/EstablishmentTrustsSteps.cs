using System.Net;
using System.Text;
using FluentAssertions;
using Platform.Api.Establishment.Trusts;
using Platform.ApiTests.Drivers;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Search;
using TechTalk.SpecFlow.Assist;

namespace Platform.ApiTests.Steps;

[Binding]
public class EstablishmentTrustsSteps
{
    private const string RequestKey = "get-trust";
    private const string SuggestRequestKey = "suggest-trust";
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

    [Given("an invalid trust request with id '(.*)'")]
    private void GivenAnInvalidValidTrustRequestWithId(string id)
    {
        _api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/trust/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust suggest request with searchText '(.*)")]
    private void GivenAValidTrustsSuggestRequest(string searchText)
    {
        var content = new { SearchText = searchText, Size = 5, SuggesterName = "trust-suggester" };

        _api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("an invalid trust suggest request")]
    private void GivenAnInvalidTrustsSuggestRequest()
    {
        var content = new { Size = 0 };

        _api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [When("I submit the trust request")]
    private async Task WhenISubmitTheTrustsRequest()
    {
        await _api.Send();
    }

    [Then("the trust result should be correct")]
    private async Task ThenTheTrustResultShouldBeCorrect()
    {
        var response = _api[RequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<Trust>();

        result.CompanyNumber.Should().Be("7539918");
        result.TrustName.Should().Be("Test Company/Trust  1");
    }

    [Then("the trust result should be not found")]
    private void ThenTheTrustResultShouldBeNotFound()
    {
        var result = _api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the trust suggest result should be correct")]
    private async Task ThenTheTrustsSuggestResultShouldBeCorrect()
    {
        var response = _api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<Trust>>().Results;
        var result = results.FirstOrDefault();
        result.Should().NotBeNull();

        result?.Text.Should().Be("*7539918*");
        result?.Document?.TrustName.Should().Be("Test Company/Trust  1");
        result?.Document?.CompanyNumber.Should().Be("7539918");
    }

    [Then("the trust suggest result should be:")]
    private async Task ThenTheTrustsSuggestResultShouldBe(Table table)
    {
        var response = _api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<Trust>>().Results;

        var set = new List<dynamic>();

        foreach (var result in results)
        {
            set.Add(new { result.Text, result.Document?.TrustName, result.Document?.CompanyNumber });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the trust suggest result should be empty")]
    private async Task ThenTheTrustsSuggestResultShouldBeEmpty()
    {
        var response = _api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<Trust>>().Results;

        results.Should().BeEmpty();
    }

    [Then("the trust suggest result should have the follow validation errors:")]
    private async Task ThenTheTrustsSuggestResultShouldHaveTheFollowValidationErrors(Table table)
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