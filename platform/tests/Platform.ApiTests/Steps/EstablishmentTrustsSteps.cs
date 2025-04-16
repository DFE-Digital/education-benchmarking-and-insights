using System.Text;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Functions;
using Platform.Json;
using Platform.Search;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
public class EstablishmentTrustsSteps(EstablishmentApiDriver api)
{
    private const string RequestKey = "get-trust";
    private const string SuggestRequestKey = "suggest-trust";
    private const string SearchRequestKey = "search-trust";

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
            Size = 5
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

    [Given("a valid trusts search request with searchText '(.*)' page '(.*)' size '(.*)' orderByField '(.*)' orderByValue '(.*)'")]
    private void GivenAValidTrustsSearchRequestWithSearchTextPageSizeOrderByFieldOrderByValue(
        string searchText,
        int page,
        int size,
        string? orderByField,
        string? orderByValue)
    {
        var content = new SearchRequest
        {
            SearchText = searchText,
            Page = page,
            PageSize = size,
            OrderBy = string.IsNullOrEmpty(orderByField) && string.IsNullOrEmpty(orderByValue)
                ? null
                : new OrderByCriteria
                {
                    Field = orderByField,
                    Value = orderByValue
                }
        };

        api.CreateRequest(SearchRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/search", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("a valid trusts search request with searchText '(.*)' page '(.*)' size '(.*)'")]
    private void GivenAValidTrustsSearchRequestWithSearchTextPageSize(string searchText, int page, int size)
    {
        GivenAValidTrustsSearchRequestWithSearchTextPageSizeOrderByFieldOrderByValue(searchText, page, size, null, null);
    }

    [Given("an invalid trusts search request")]
    private void GivenAnInvalidTrustsSearchRequest()
    {
        var invalidFilters = new[]
        {
            new FilterCriteria
            {
                Field = "test",
                Value = "test"
            }
        };
        var content = new SearchRequest
        {
            SearchText = "te",
            Page = 1,
            PageSize = 5,
            OrderBy = new OrderByCriteria
            {
                Field = "test",
                Value = "test"
            },
            Filters = invalidFilters
        };

        api.CreateRequest(SearchRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/search", UriKind.Relative),
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
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<Trust>();

        table.CompareToInstance(result);
    }

    [Then("the trust result should contain the following schools:")]
    private async Task ThenTheTrustResultShouldContainTheFollowingSchools(DataTable table)
    {
        var response = api[RequestKey].Response;

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
        AssertHttpResponse.IsNotFound(api[RequestKey].Response);
    }

    [Then("the trust suggest result should be ok and have the following values:")]
    private async Task ThenTheTrustSuggestResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<TrustSummary>>().Results;
        var result = results.FirstOrDefault();
        Assert.NotNull(result);

        var actual = new
        {
            result.Text,
            result.Document?.TrustName,
            result.Document?.CompanyNumber
        };

        table.CompareToInstance(actual);
    }

    [Then("the trust suggest result should be ok and have the following multiple values:")]
    private async Task ThenTheTrustSuggestResultShouldBeOkAndHaveTheFollowingMultipleValues(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<TrustSummary>>().Results;

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
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<TrustSummary>>().Results;
        Assert.Empty(results);
    }

    [Then("the trust suggest result should be bad request and have the following validation errors:")]
    private async Task ThenTheTrustSuggestResultShouldBeBadRequestAndHaveTheFollowingValidationErrors(DataTable table)
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

    [Then("the search trusts response should be ok and have the following values:")]
    private async Task ThenTheSearchTrustsResponseShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[SearchRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var actual = content.FromJson<SearchResponse<TrustSummary>>();

        table.CompareToInstance(actual);
    }

    [Then("the results should include the following trusts:")]
    private async Task ThenTheResultsShouldIncludeTheFollowingTrusts(DataTable table)
    {
        var response = api[SearchRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SearchResponse<TrustSummary>>().Results.ToList();

        table.CompareToSet(results);
    }

    [Then("the trusts search result should be empty")]
    private async Task ThenTheTrustsSearchResultShouldBeEmpty()
    {
        var response = api[SearchRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SearchResponse<TrustSummary>>().Results;
        Assert.Empty(results);
    }

    [Then("the search trusts response should be bad request containing validation errors")]
    private async Task ThenTheSearchTrustsResponseShouldBeBadRequestContainingValidationErrors()
    {
        var response = api[SearchRequestKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ValidationError[]>();

        Assert.NotEmpty(results);

        foreach (var error in results)
        {
            Assert.NotNull(error.PropertyName);
            Assert.NotNull(error.ErrorMessage);
        }
    }
}