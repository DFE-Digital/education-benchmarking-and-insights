using System.Text;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Functions;
using Platform.Json;
using Platform.Search;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
public class EstablishmentSchoolsSteps(EstablishmentApiDriver api)
{
    private const string RequestKey = "get-school";
    private const string SuggestRequestKey = "suggest-school";

    [Given("a valid school request with id '(.*)'")]
    private void GivenAValidSchoolRequestWithId(string id)
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/school/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid school request with id '(.*)'")]
    private void GivenAnInvalidSchoolRequestWithId(string id)
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/school/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid schools suggest request with searchText '(.*)")]
    private void GivenAValidSchoolsSuggestRequestWithSearchText(string searchText)
    {
        var content = new
        {
            SearchText = searchText,
            Size = 5,
        };

        api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("an invalid schools suggest request with '(.*)' and '(.*)'")]
    private void GivenAnInvalidSchoolsSuggestRequestWithAnd(string searchText, int size)
    {
        var content = new
        {
            SearchText = string.IsNullOrWhiteSpace(searchText) ? null : searchText,
            Size = size
        };

        api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [When("I submit the schools request")]
    private async Task WhenISubmitTheSchoolsRequest()
    {
        await api.Send();
    }

    [Then("the school result should be ok and have the following values:")]
    private async Task ThenTheSchoolResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[RequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<School>();

        table.CompareToInstance(result);
    }

    [Then("the school result should be not found")]
    private void ThenTheSchoolResultShouldBeNotFound()
    {
        AssertHttpResponse.IsNotFound(api[RequestKey].Response);
    }

    [Then("the school suggest result should be ok and have the following values:")]
    private async Task ThenTheSchoolSuggestResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<School>>().Results;
        var result = results.FirstOrDefault();
        Assert.NotNull(result);

        var actual = new
        {
            result.Text,
            result.Document?.SchoolName,
            result.Document?.URN
        };

        table.CompareToInstance(actual);
    }

    [Then("the schools suggest result should be ok and have the following multiple values:")]
    private async Task ThenTheSchoolsSuggestResultShouldBeOkAndHaveTheFollowingMultipleValues(DataTable table)
    {
        var response = api[SuggestRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<School>>().Results.ToList();

        var set = results.Select(result => new
        {
            result.Text,
            result.Document?.SchoolName,
            result.Document?.URN
        }).ToList();

        table.CompareToSet(set);
    }

    [Then("the schools suggest result should be empty")]
    private async Task ThenTheSchoolsSuggestResultShouldBeEmpty()
    {
        var response = api[SuggestRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<School>>().Results;
        Assert.Empty(results);
    }

    [Then("the schools suggest result should be bad request and have the following validation errors:")]
    private async Task ThenTheSchoolsSuggestResultShouldBeBadRequestAndHaveTheFollowingValidationErrors(DataTable table)
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
}