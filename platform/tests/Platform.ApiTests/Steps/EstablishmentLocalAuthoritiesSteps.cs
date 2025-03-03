using System.Text;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Functions;
using Platform.Json;
using Platform.Search;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
public class EstablishmentLocalAuthoritiesSteps(EstablishmentApiDriver api)
{
    private const string RequestKey = "get-local-authority";
    private const string NationalRankRequestKey = "get-local-authority-national-rank";
    private const string SuggestRequestKey = "suggest-local-authority";
    private const string StatisticalNeighboursRequestKey = "get-local-authority-statistical-neighbours";

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
            Size = 5
        };

        api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("an invalid local authorities suggest request with '(.*)' and '(.*)'")]
    private void GivenAnInvalidLocalAuthoritiesSuggestRequestWithAnd(string searchText, int size)
    {
        var content = new
        {
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

    [Given("a valid local authorities statistical neighbours request with id '(.*)'")]
    private void GivenAValidLocalAuthoritiesStatisticalNeighboursRequestWithId(string identifier)
    {
        api.CreateRequest(StatisticalNeighboursRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authority/{identifier}/statistical-neighbours", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid local authorities national rank request with sort order '(.*)'")]
    private void GivenAValidLocalAuthoritiesNationalRankRequestWithSortOrder(string sort)
    {
        api.CreateRequest(NationalRankRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/national-rank?sort={sort}", UriKind.Relative),
            Method = HttpMethod.Get
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
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<LocalAuthority>();

        table.CompareToInstance(result);
    }

    [Then("the local authority result should contain the following schools:")]
    private async Task ThenTheLocalAuthorityResultShouldContainTheFollowingSchools(DataTable table)
    {
        var response = api[RequestKey].Response;
        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<LocalAuthority>();

        var set = result.Schools!.Select(s => new
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
        AssertHttpResponse.IsNotFound(api[RequestKey].Response);
    }

    [Then("the local authorities suggest result should be ok and have the following values:")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<LocalAuthority>>().Results;
        var result = results.FirstOrDefault();
        Assert.NotNull(result);

        var actual = new
        {
            result.Text,
            result.Document?.Name,
            result.Document?.Code
        };

        table.CompareToInstance(actual);
    }

    [Then("the local authorities suggest result should be ok and have the following multiple values:")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldBeOkAndHaveTheFollowingMultipleValues(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;
        AssertHttpResponse.IsOk(response);

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
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<LocalAuthority>>().Results;
        Assert.Empty(results);
    }

    [Then("the local authorities suggest result should be bad request and have the following validation errors:")]
    private async Task ThenTheLocalAuthoritiesSuggestResultShouldBeBadRequestAndHaveTheFollowingValidationErrors(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ValidationError[]>();

        var filteredTable = new DataTable(table.Header.ToArray());
        foreach (var row in table.Rows.Where(r => !string.IsNullOrWhiteSpace(r["ErrorMessage"])))
        {
            filteredTable.AddRow(row);
        }

        filteredTable.CompareToSet(results);
    }

    [Then("the local authorities national rank result should contain the following:")]
    private async Task ThenTheLocalAuthoritiesNationalRankResultShouldContainTheFollowing(DataTable table)
    {
        var response = api[NationalRankRequestKey].Response;

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<LocalAuthorityRanking>();

        var set = result.Ranking.Select(s => new
        {
            s.Code,
            s.Name,
            s.Value,
            s.Rank
        }).ToList();

        table.CompareToSet(set);
    }

    [Then("the local authorities statistical neighbours result should be ok and have the following values:")]
    private async Task ThenTheLocalAuthoritiesStatisticalNeighboursResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[StatisticalNeighboursRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<LocalAuthority>();

        table.CompareToInstance(result);
    }

    [Then("the local authorities statistical neighbours result should contain the following neighbours:")]
    private async Task ThenTheLocalAuthoritiesStatisticalNeighboursResultShouldContainTheFollowingNeighbours(DataTable table)
    {
        var response = api[StatisticalNeighboursRequestKey].Response;

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<LocalAuthorityStatisticalNeighbours>();

        var set = result.StatisticalNeighbours?.Select(s => new
        {
            s.Code,
            s.Name,
            s.Order
        }).ToList();

        table.CompareToSet(set);
    }
}