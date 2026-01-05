using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority Accounts High Needs")]
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

    [Given("an invalid history request")]
    public void GivenAnInvalidHistoryRequest()
    {
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/accounts/high-needs/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid request")]
    public void GivenAnInvalidRequest()
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/accounts/high-needs", UriKind.Relative),
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
    private async Task ThenTheResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile, RouteFolder, AccountsFolder, HighNeedsFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the history result should be bad request")]
    public void ThenTheHistoryResultShouldBeBadRequest()
    {
        var response = api[HistoryKey].Response;
        AssertHttpResponse.IsBadRequest(response);
    }

    [Then("the result should be bad request")]
    public void ThenTheResultShouldBeBadRequest()
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsBadRequest(response);
    }
}