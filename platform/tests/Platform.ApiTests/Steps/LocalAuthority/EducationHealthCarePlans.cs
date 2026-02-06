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

    [Given("a history request with no codes")]
    public void GivenAHistoryRequestWithNoCodes()
    {
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/education-health-care-plans/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a request with no codes")]
    public void GivenARequestWithNoCodes()
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities/education-health-care-plans", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid history request with more than 30 codes")]
    public void GivenAnInvalidHistoryRequestWithMoreThan30Codes()
    {
        var codes = Enumerable.Range(1, 31).Select(i => (100 + i).ToString());
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/education-health-care-plans/history?code={string.Join("&code=", codes)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid request with more than 30 codes")]
    public void GivenAnInvalidRequestWithMoreThan30Codes()
    {
        var codes = Enumerable.Range(1, 31).Select(i => (100 + i).ToString());
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/education-health-care-plans?code={string.Join("&code=", codes)}", UriKind.Relative),
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

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the result should be ok and match the expected output of '(.*)'")]
    private async Task ThenTheResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the history result should be bad request")]
    public void ThenTheHistoryResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[HistoryKey].Response);
    }

    [Then("the result should be bad request")]
    public void ThenTheResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[Key].Response);
    }
}