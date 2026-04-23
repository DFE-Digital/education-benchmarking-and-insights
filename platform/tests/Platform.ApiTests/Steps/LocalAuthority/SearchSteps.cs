using System.Text;
using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;
using Platform.Infrastructure;
using Platform.Json;

namespace Platform.ApiTests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority Search")]
public class SearchSteps(LocalAuthorityApiDriver api)
{
    private const string SuggestRequestKey = "suggest-local-authority";
    private const string SearchRequestKey = "search-local-authority";
    private const string RouteFolder = "LocalAuthority";
    private const string SearchFolder = "Search";

    [Given("a valid local authorities suggest request with searchText '(.*)'")]
    public void GivenAValidLocalAuthoritiesSuggestRequestWithSearchText(string searchText)
    {
        var content = new
        {
            SuggesterName = ResourceNames.Search.Suggesters.LocalAuthority,
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

    [Given("an invalid local authorities suggest request with searchText '(.*)' and size (.*)")]
    public void GivenAnInvalidLocalAuthoritiesSuggestRequestWithSearchTextAndSize(string searchText, int size)
    {
        var content = new
        {
            SuggesterName = ResourceNames.Search.Suggesters.LocalAuthority,
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

    [Given("a valid local authorities search request with searchText '(.*)' page (.*) size (.*) orderByField '(.*)' orderByValue '(.*)'")]
    public void GivenAValidLocalAuthoritiesSearchRequestWithSearchTextPageSizeOrderByFieldOrderByValue(
        string searchText,
        int page,
        int size,
        string orderByField,
        string orderByValue)
    {
        var content = new
        {
            SearchText = searchText,
            Page = page,
            PageSize = size,
            OrderBy = string.IsNullOrEmpty(orderByField) && string.IsNullOrEmpty(orderByValue)
                ? null
                : new
                {
                    Field = orderByField,
                    Value = orderByValue
                }
        };

        api.CreateRequest(SearchRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/search", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("an invalid local authorities search request with searchText '(.*)' page (.*) size (.*) orderByField '(.*)' orderByValue '(.*)'")]
    public void GivenAnInvalidLocalAuthoritiesSearchRequestWithSearchTextPageSizeOrderByFieldOrderByValue(
        string searchText,
        int page,
        int size,
        string orderByField,
        string orderByValue)
    {
        var content = new
        {
            SearchText = string.IsNullOrWhiteSpace(searchText) ? null : searchText,
            Page = page,
            PageSize = size,
            OrderBy = string.IsNullOrEmpty(orderByField) && string.IsNullOrEmpty(orderByValue)
                ? null
                : new
                {
                    Field = orderByField,
                    Value = orderByValue
                }
        };

        api.CreateRequest(SearchRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/search", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [When("I submit the search request")]
    public async Task WhenISubmitTheSearchRequest()
    {
        await api.Send();
    }

    [Then("the local authorities suggest result should be ok and match the expected output in '(.*)'")]
    public async Task ThenTheLocalAuthoritiesSuggestResultShouldBeOkAndMatchTheExpectedOutputIn(string testFile)
    {
        var response = api[SuggestRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SearchFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the local authorities suggest result should be bad request and match the expected output in '(.*)'")]
    public async Task ThenTheLocalAuthoritiesSuggestResultShouldBeBadRequestAndMatchTheExpectedOutputIn(string testFile)
    {
        var response = api[SuggestRequestKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SearchFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the search local authorities response should be ok and match the expected output in '(.*)'")]
    public async Task ThenTheSearchLocalAuthoritiesResponseShouldBeOkAndMatchTheExpectedOutputIn(string testFile)
    {
        var response = api[SearchRequestKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SearchFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the search local authorities response should be bad request and match the expected output in '(.*)'")]
    public async Task ThenTheSearchLocalAuthoritiesResponseShouldBeBadRequestAndMatchTheExpectedOutputIn(string testFile)
    {
        var response = api[SearchRequestKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SearchFolder);

        actual.AssertDeepEquals(expected);
    }
}