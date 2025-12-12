using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Local authorities high needs endpoints")]
public class LocalAuthoritiesHighNeedsSteps(LocalAuthorityFinancesApiDriver api)
{
    private const string Key = "high-needs";
    private const string HistoryKey = "high-needs-history";

    [Given("a valid high needs history request with dimension '(.*)' and LA codes:")]
    public void GivenAValidHighNeedsHistoryRequestWithLaCodes(string dimension, DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/high-needs/history?code={string.Join("&code=", codes)}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid high needs request with dimension '(.*)' and LA codes:")]
    public void GivenAValidHighNeedsRequestWithDimensionAndLaCodes(string dimension, DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/high-needs?code={string.Join("&code=", codes)}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid high needs history request")]
    public void GivenAnInvalidHighNeedsHistoryRequest()
    {
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/high-needs/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid high needs request")]
    public void GivenAnInvalidHighNeedsRequest()
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/high-needs", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the high needs request")]
    public async Task WhenISubmitTheHighNeedsRequest()
    {
        await api.Send();
    }

    [Then("the high needs history result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheHighNeedsHistoryResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[HistoryKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the high needs result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheHighNeedsResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the high needs history result should be bad request")]
    public void ThenTheHighNeedsHistoryResultShouldBeBadRequest()
    {
        var response = api[HistoryKey].Response;
        AssertHttpResponse.IsBadRequest(response);
    }

    [Then("the high needs result should be bad request")]
    public void ThenTheHighNeedsResultShouldBeBadRequest()
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsBadRequest(response);
    }
}