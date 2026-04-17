using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority Accounts - High Needs")]
public class AccountsHighNeedsSteps(LocalAuthorityApiDriver api)
{
    private const string Key = "high-needs";
    private const string HistoryKey = "high-needs-history";
    private const string RouteFolder = "LocalAuthority";
    private const string AccountsFolder = "Accounts";
    private const string HighNeedsFolder = "HighNeeds";


    [Given("a valid history request with dimension '(.*)' and LA codes:")]
    public void GivenAValidHistoryRequestWithDimensionAndLACodes(string dimension, DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/accounts/high-needs/history?code={string.Join("&code=", codes)}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid request with dimension '(.*)' and LA codes:")]
    public void GivenAValidRequestWithDimensionAndLACodes(string dimension, DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/accounts/high-needs?code={string.Join("&code=", codes)}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid history request with '(.*)'")]
    public void GivenAnInvalidHistoryRequestWith(string issue)
    {
        var uri = issue switch
        {
            "no codes" => "/api/local-authorities/accounts/high-needs/history",
            "more than 30 codes" => $"/api/local-authorities/accounts/high-needs/history?{string.Join("&", Enumerable.Range(1, 31).Select(i => $"code={100 + i}"))}",
            "invalid dimension" => "/api/local-authorities/accounts/high-needs/history?code=201&dimension=invalid",
            _ => throw new ArgumentOutOfRangeException(nameof(issue), issue, null)
        };

        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri(uri, UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid request with '(.*)'")]
    public void GivenAnInvalidRequestWith(string issue)
    {
        var uri = issue switch
        {
            "no codes" => "/api/local-authorities/accounts/high-needs",
            "more than 30 codes" => $"/api/local-authorities/accounts/high-needs?{string.Join("&", Enumerable.Range(1, 31).Select(i => $"code={100 + i}"))}",
            "invalid dimension" => "/api/local-authorities/accounts/high-needs?code=201&dimension=invalid",
            _ => throw new ArgumentOutOfRangeException(nameof(issue), issue, null)
        };

        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri(uri, UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the request")]
    public async Task WhenISubmitTheRequest()
    {
        await api.Send();
    }

    [Then("the history result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheHistoryResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[HistoryKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, AccountsFolder, HighNeedsFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile, RouteFolder, AccountsFolder, HighNeedsFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the history result should be bad request and match the expected output in '(.*)'")]
    public async Task ThenTheHistoryResultShouldBeBadRequestAndMatchTheExpectedOutputIn(string testFile)
    {
        var response = api[HistoryKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, AccountsFolder, HighNeedsFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the history result should be not found")]
    public void ThenTheHistoryResultShouldBeNotFound()
    {
        var response = api[HistoryKey].Response;
        AssertHttpResponse.IsNotFound(response);
    }

    [Then("the result should be bad request and match the expected output in '(.*)'")]
    public async Task ThenTheResultShouldBeBadRequestAndMatchTheExpectedOutputIn(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, AccountsFolder, HighNeedsFolder);

        actual.AssertDeepEquals(expected);
    }
}