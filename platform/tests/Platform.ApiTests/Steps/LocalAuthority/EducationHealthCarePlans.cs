using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority Education Health Care Plans")]
public class EducationHealthCarePlans(LocalAuthorityApiDriver api)
{
    private const string Key = "education-health-care-plans";
    private const string HistoryKey = "education-health-care-plans-history";
    private const string RouteFolder = "LocalAuthority";
    private const string SubFolder = "EHCP";

    [Given("a history request with dimension '(.*)' and LA codes:")]
    public void GivenAHistoryRequestWithDimensionAndLACodes(string dimension, DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/education-health-care-plans/history?code={string.Join("&code=", codes)}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a request with dimension '(.*)' and LA codes:")]
    public void GivenARequestWithDimensionAndLACodes(string dimension, DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/education-health-care-plans?code={string.Join("&code=", codes)}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a history request with (.*)")]
    public void GivenAHistoryRequestWithIssue(string issue)
    {
        switch (issue)
        {
            case "no codes":
                api.CreateRequest(HistoryKey, new HttpRequestMessage
                {
                    RequestUri = new Uri("/api/local-authorities/education-health-care-plans/history", UriKind.Relative),
                    Method = HttpMethod.Get
                });
                break;
            case "more than 30 codes":
                var codes30 = Enumerable.Range(1, 31).Select(i => (100 + i).ToString());
                api.CreateRequest(HistoryKey, new HttpRequestMessage
                {
                    RequestUri = new Uri($"/api/local-authorities/education-health-care-plans/history?code={string.Join("&code=", codes30)}", UriKind.Relative),
                    Method = HttpMethod.Get
                });
                break;
            case "invalid dimension":
                api.CreateRequest(HistoryKey, new HttpRequestMessage
                {
                    RequestUri = new Uri("/api/local-authorities/education-health-care-plans/history?code=201&dimension=invalid", UriKind.Relative),
                    Method = HttpMethod.Get
                });
                break;
            default:
                throw new ArgumentException($"Unknown issue: {issue}");
        }
    }

    [Given("a request with (.*)")]
    public void GivenARequestWithIssue(string issue)
    {
        switch (issue)
        {
            case "no codes":
                api.CreateRequest(Key, new HttpRequestMessage
                {
                    RequestUri = new Uri("/api/local-authorities/education-health-care-plans", UriKind.Relative),
                    Method = HttpMethod.Get
                });
                break;
            case "more than 30 codes":
                var codes30 = Enumerable.Range(1, 31).Select(i => (100 + i).ToString());
                api.CreateRequest(Key, new HttpRequestMessage
                {
                    RequestUri = new Uri($"/api/local-authorities/education-health-care-plans?code={string.Join("&code=", codes30)}", UriKind.Relative),
                    Method = HttpMethod.Get
                });
                break;
            case "invalid dimension":
                api.CreateRequest(Key, new HttpRequestMessage
                {
                    RequestUri = new Uri("/api/local-authorities/education-health-care-plans?code=201&dimension=invalid", UriKind.Relative),
                    Method = HttpMethod.Get
                });
                break;
            default:
                throw new ArgumentException($"Unknown issue: {issue}");
        }
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

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the history result should be bad request and match the expected output of '(.*)'")]
    public async Task ThenTheHistoryResultShouldBeBadRequestAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[HistoryKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the history result should be not found")]
    public void ThenTheHistoryResultShouldBeNotFound()
    {
        AssertHttpResponse.IsNotFound(api[HistoryKey].Response);
    }

    [Then("the result should be bad request and match the expected output of '(.*)'")]
    public async Task ThenTheResultShouldBeBadRequestAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }
}