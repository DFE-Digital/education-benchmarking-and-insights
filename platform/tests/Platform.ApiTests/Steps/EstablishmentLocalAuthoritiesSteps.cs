using System.Net;
using System.Text;
using FluentAssertions;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.ApiTests.Drivers;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Search.Responses;
namespace Platform.ApiTests.Steps;

[Binding]
public class EstablishmentLocalAuthoritiesSteps(EstablishmentApiDriver api)
{
    private const string RequestKey = "get-local-authority";
    private const string SuggestRequestKey = "suggest-local-authority";

    [Given("a valid local authority request with id '(.*)'")]
    private void GivenAValidLocalAuthorityRequestWithId(string id)
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authority/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid local authority request with id '(.*)'")]
    private void GivenAnInvalidLocalAuthorityRequestWithId(string id)
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authority/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid local authorities suggest request with searchText '(.*)")]
    private void GivenAValidLocalAuthoritiesSuggestRequestWithSearchText(string searchText)
    {
        var content = new
        {
            SearchText = searchText,
            Size = 5,
            SuggesterName = "local-authority-suggester"
        };

        api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("an invalid local authorities suggest request with '(.*)', '(.*)' and '(.*)'")]
    private void GivenAnInvalidLocalAuthoritiesSuggestRequestWithAnd(string suggesterName, string searchText, int size)
    {
        var content = new
        {
            SuggesterName = string.IsNullOrWhiteSpace(suggesterName) ? null : suggesterName,
            SearchText = string.IsNullOrWhiteSpace(searchText) ? null : searchText,
            Size = size
        };

        api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [When("I submit the local authorities request")]
    private async Task WhenISubmitTheLocalAuthoritiesRequest()
    {
        await api.Send();
    }

    [Then("the local authority result should be ok and have the following values:")]
    private async Task ThenTheLocalAuthorityResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[RequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<LocalAuthority>();

        table.CompareToInstance(result);
    }

    [Then("the local authority result should contain the following schools:")]
    private async Task ThenTheLocalAuthorityResultShouldContainTheFollowingSchools(DataTable table)
    {
        var response = api[RequestKey].Response;

        response.Should().NotBeNull();

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<LocalAuthority>();

        var set = result.Schools.Select(s => new
        {
            s.URN,
            s.SchoolName,
            s.OverallPhase
        }).ToList();

        table.CompareToSet(set);
    }

    [Then("the local authority result should be not found")]
    private void ThenTheLocalAuthorityResultShouldBeNotFound()
    {
        var result = api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the local authorities suggest result should be ok and have the following values:")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;

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
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldBeOkAndHaveTheFollowingMultipleValues(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;

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
        var response = api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<LocalAuthority>>().Results;

        results.Should().BeEmpty();
    }

    [Then("the local authorities suggest result should be bad request and have the following validation errors:")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldBeBadRequestAndHaveTheFollowingValidationErrors(DataTable table)
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