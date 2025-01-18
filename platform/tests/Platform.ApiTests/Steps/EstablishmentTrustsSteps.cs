using System.Net;
using System.Text;
using FluentAssertions;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.ApiTests.Drivers;
using Platform.Functions;
using Platform.Json;
using Platform.Search;

namespace Platform.ApiTests.Steps;

[Binding]
public class EstablishmentTrustsSteps(EstablishmentApiDriver api)
{
    private const string RequestKey = "get-trust";
    private const string SuggestRequestKey = "suggest-trust";

    [Given("a valid trust request with id '(.*)'")]
    private void GivenAValidTrustRequestWithId(string id)
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/trust/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid trust request with id '(.*)'")]
    private void GivenAnInvalidTrustRequestWithId(string id)
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/trust/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust suggest request with searchText '(.*)")]
    private void GivenAValidTrustSuggestRequestWithSearchText(string searchText)
    {
        var content = new
        {
            SearchText = searchText,
            Size = 5,
        };

        api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("an invalid trust suggest request with '(.*)' and '(.*)'")]
    private void GivenAnInvalidTrustSuggestRequestWithAnd(string searchText, int size)
    {
        var content = new
        {
            SearchText = string.IsNullOrWhiteSpace(searchText) ? null : searchText,
            Size = size
        };

        api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [When("I submit the trust request")]
    private async Task WhenISubmitTheTrustRequest()
    {
        await api.Send();
    }

    [Then("the trust result should be ok and have the following values:")]
    private async Task ThenTheTrustResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[RequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<Trust>();

        table.CompareToInstance(result);
    }

    [Then("the trust result should contain the following schools:")]
    private async Task ThenTheTrustResultShouldContainTheFollowingSchools(DataTable table)
    {
        var response = api[RequestKey].Response;

        response.Should().NotBeNull();

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<Trust>();

        var set = result.Schools!.Select(s => new
        {
            s.URN,
            s.SchoolName,
            s.OverallPhase
        }).ToList();

        table.CompareToSet(set);
    }

    [Then("the trust result should be not found")]
    private void ThenTheTrustResultShouldBeNotFound()
    {
        var result = api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the trust suggest result should be ok and have the following values:")]
    private async Task ThenTheTrustSuggestResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<Trust>>().Results;
        var result = results.FirstOrDefault();
        result.Should().NotBeNull();

        var actual = new
        {
            result?.Text,
            result?.Document?.TrustName,
            result?.Document?.CompanyNumber
        };

        table.CompareToInstance(actual);
    }

    [Then("the trust suggest result should be ok and have the following multiple values:")]
    private async Task ThenTheTrustSuggestResultShouldBeOkAndHaveTheFollowingMultipleValues(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<Trust>>().Results;

        var set = results.Select(result => new
        {
            result.Text,
            result.Document?.TrustName,
            result.Document?.CompanyNumber
        }).ToList();

        table.CompareToSet(set);
    }

    [Then("the trust suggest result should be empty")]
    private async Task ThenTheTrustSuggestResultShouldBeEmpty()
    {
        var response = api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<Trust>>().Results;

        results.Should().BeEmpty();
    }

    [Then("the trust suggest result should be bad request and have the following validation errors:")]
    private async Task ThenTheTrustSuggestResultShouldBeBadRequestAndHaveTheFollowingValidationErrors(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ValidationError[]>();

        var filteredTable = new DataTable(table.Header.ToArray());
        foreach (var row in table.Rows.Where(r => !string.IsNullOrWhiteSpace(r["ErrorMessage"])))
        {
            filteredTable.AddRow(row);
        }

        filteredTable.CompareToSet(results);
    }
}