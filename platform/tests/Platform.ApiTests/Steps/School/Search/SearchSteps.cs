using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;
using Xunit;

namespace Platform.ApiTests.Steps.School.Search;

[Binding]
[Scope(Feature = "School Search")]
public class SearchSteps(SchoolApiDriver api)
{
    private object _searchRequest = null!;
    private object _suggestRequest = null!;
    private string _scenario = null!;
    private string _apiVersion = "1.0";
    private const string SearchKey = "search-request";
    private const string SuggestKey = "suggest-request";
    private const string RouteFolder = "School";
    private const string SubFolder = "Search";

    [Given("a valid school search request with '(.*)'")]
    public void GivenAValidSchoolSearchRequestWith(string scenario)
    {
        _scenario = scenario;
        _searchRequest = scenario switch
        {
            "pagination" => new { searchText = "school", page = 2, pageSize = 5 },
            "order-by" => new { searchText = "school", orderBy = new { field = "SchoolNameSortable", value = "asc" } },
            "filter-phase" => new { searchText = "school", filters = new[] { new { field = "OverallPhase", value = "Primary" } } },
            _ => new { searchText = "school" }
        };
    }

    [Given("an invalid school search request with '(.*)'")]
    public void GivenAnInvalidSchoolSearchRequestWith(string scenario)
    {
        _scenario = scenario;
        _searchRequest = scenario switch
        {
            "missing-search-text" => new { searchText = (string?)null },
            "short-search-text" => new { searchText = "ab" },
            "long-search-text" => new { searchText = new string('a', 101) },
            "invalid-order-by-field" => new { searchText = "school", orderBy = new { field = "InvalidField", value = "asc" } },
            "invalid-order-by-value" => new { searchText = "school", orderBy = new { field = "SchoolNameSortable", value = "invalid" } },
            "invalid-filter-field" => new { searchText = "school", filters = new[] { new { field = "InvalidField", value = "Primary" } } },
            "invalid-filter-value" => new { searchText = "school", filters = new[] { new { field = "OverallPhase", value = "InvalidPhase" } } },
            _ => new { searchText = "school" }
        };
    }

    [When("I submit the school search request")]
    public async Task WhenISubmitTheSchoolSearchRequest()
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/search", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(_searchRequest), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("x-api-version", _apiVersion);
        api.CreateRequest(SearchKey, request);

        await api.Send();
    }

    [When("I submit the school search request with an unsupported API version")]
    public async Task WhenISubmitTheSchoolSearchRequestWithAnUnsupportedApiVersion()
    {
        _apiVersion = "99.99";
        await WhenISubmitTheSchoolSearchRequest();
    }

    [Then(@"the response status code is (\d+) (.*)")]
    public void ThenTheResponseStatusCodeIs(int code, string message)
    {
        var response = api.ContainsKey(SearchKey) ? api[SearchKey].Response : api[SuggestKey].Response;
        Assert.Equal(code, (int)response.StatusCode);
    }

    [Then("the school search response body matches the expected JSON")]
    public async Task ThenTheSchoolSearchResponseBodyMatchesTheExpectedJson()
    {
        var response = api[SearchKey].Response;
        var content = await response.Content.ReadAsStringAsync();

        var path = $"Search_{_scenario}.json";
        var actual = JObject.Parse(content);
        var expected = TestDataProvider.GetJsonObjectData(path, RouteFolder, SubFolder);
        actual.AssertDeepEquals(expected);
    }

    [Then("the school search response body matches the expected validation errors JSON")]
    public async Task ThenTheSchoolSearchResponseBodyMatchesTheExpectedValidationErrorsJson()
    {
        var response = api[SearchKey].Response;
        var content = await response.Content.ReadAsStringAsync();

        var path = $"Search_validation_{_scenario}.json";
        var actual = JObject.Parse(content);
        var expected = TestDataProvider.GetJsonObjectData(path, RouteFolder, SubFolder);
        actual.AssertDeepEquals(expected);
    }

    [Then("the response body is a standard problem details JSON")]
    public async Task ThenTheResponseBodyIsAStandardProblemDetailsJson()
    {
        var response = api.ContainsKey(SearchKey) ? api[SearchKey].Response : api[SuggestKey].Response;
        var content = await response.Content.ReadAsStringAsync();

        var actual = JObject.Parse(content);

        Assert.NotNull(actual["title"]);
        Assert.NotNull(actual["status"]);
    }

    [Given("a valid school suggest request with '(.*)'")]
    public void GivenAValidSchoolSuggestRequestWith(string scenario)
    {
        _scenario = scenario;
        _suggestRequest = scenario switch
        {
            "valid-size" => new { searchText = "school", size = 10 },
            "valid-exclude" => new { searchText = "school", exclude = new[] { "123456" } },
            "exclude-missing-financial" => new { searchText = "school", excludeMissingFinancialData = true },
            _ => new { searchText = "school" }
        };
    }

    [Given("an invalid school suggest request with '(.*)'")]
    public void GivenAnInvalidSchoolSuggestRequestWith(string scenario)
    {
        _scenario = scenario;
        _suggestRequest = scenario switch
        {
            "missing-search-text" => new { searchText = (string?)null },
            "short-search-text" => new { searchText = "ab" },
            "long-search-text" => new { searchText = new string('a', 101) },
            "invalid-size" => new { searchText = "school", size = 4 },
            _ => new { searchText = "school" }
        };
    }

    [When("I submit the school suggest request")]
    public async Task WhenISubmitTheSchoolSuggestRequest()
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(_suggestRequest), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("x-api-version", _apiVersion);
        api.CreateRequest(SuggestKey, request);

        await api.Send();
    }

    [When("I submit the school suggest request with an unsupported API version")]
    public async Task WhenISubmitTheSchoolSuggestRequestWithAnUnsupportedApiVersion()
    {
        _apiVersion = "99.99";
        await WhenISubmitTheSchoolSuggestRequest();
    }

    [Then("the school suggest response body matches the expected JSON")]
    public async Task ThenTheSchoolSuggestResponseBodyMatchesTheExpectedJson()
    {
        var response = api[SuggestKey].Response;
        var content = await response.Content.ReadAsStringAsync();

        var path = $"Suggest_{_scenario}.json";
        var actual = JObject.Parse(content);
        var expected = TestDataProvider.GetJsonObjectData(path, RouteFolder, SubFolder);
        actual.AssertDeepEquals(expected);
    }

    [Then("the school suggest response body matches the expected validation errors JSON")]
    public async Task ThenTheSchoolSuggestResponseBodyMatchesTheExpectedValidationErrorsJson()
    {
        var response = api[SuggestKey].Response;
        var content = await response.Content.ReadAsStringAsync();

        var path = $"Suggest_validation_{_scenario}.json";
        var actual = JObject.Parse(content);
        var expected = TestDataProvider.GetJsonObjectData(path, RouteFolder, SubFolder);
        actual.AssertDeepEquals(expected);
    }
}