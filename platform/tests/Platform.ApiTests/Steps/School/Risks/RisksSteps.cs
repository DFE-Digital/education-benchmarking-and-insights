using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;
using Xunit;

namespace Platform.ApiTests.Steps.School.Risks;

[Binding]
[Scope(Feature = "School Risks")]
public class RisksSteps(SchoolApiDriver api)
{
    private const string HistoryKey = "history";
    private const string RisksKey = "risks";
    private const string MetricsKey = "metrics";

    private const string RouteFolder = "School";
    private const string SubFolder = "Risks";

    [Given("a school risks history request with URN '(.*)'")]
    public void GivenASchoolRisksHistoryRequestWithUrn(string urn)
    {
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/risks/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a school risks request with URN '(.*)'")]
    public void GivenASchoolRisksRequestWithUrn(string urn)
    {
        api.CreateRequest(RisksKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/risks", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a school risks metrics request with URN '(.*)'")]
    public void GivenASchoolRisksMetricsRequestWithUrn(string urn)
    {
        api.CreateRequest(MetricsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/risks/metrics", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the request")]
    public async Task WhenISubmitTheRequest()
    {
        await api.Send();
    }

    [Then("the '(.*)' result status should be '(.*)'")]
    public void ThenTheResultStatusShouldBe(string key, string expectedStatus)
    {
        var apiKey = GetApiKeyFromFriendlyName(key);
        var response = api[apiKey].Response;

        switch (expectedStatus)
        {
            case "ok":
                AssertHttpResponse.IsOk(response);
                break;
            case "not found":
                AssertHttpResponse.IsNotFound(response);
                break;
            default:
                Assert.Fail($"unexpected status: {expectedStatus}");
                break;
        }
    }

    [Then("the '(.*)' result should match the expected output in '(.*)'")]
    public async Task ThenTheResultShouldMatchTheExpectedOutputIn(string key, string testFile)
    {
        var apiKey = GetApiKeyFromFriendlyName(key);
        var response = api[apiKey].Response;

        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
        {
            Assert.Empty(content);
            return;
        }

        var token = JToken.Parse(content);
        JToken expected;

        switch (token)
        {
            case JObject actualObj:
                expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);
                actualObj.AssertDeepEquals(expected);
                break;
            case JArray actualArray:
                expected = TestDataProvider.GetJsonArrayData(testFile, RouteFolder, SubFolder);
                actualArray.AssertDeepEquals(expected);
                break;
            default:
                Assert.Fail($"Unexpected JSON token type: {token.Type}");
                break;
        }
    }

    private static string GetApiKeyFromFriendlyName(string key) => key.ToLowerInvariant() switch
    {
        "history" => HistoryKey,
        "risks" => RisksKey,
        "metrics" => MetricsKey,
        _ => throw new ArgumentOutOfRangeException(nameof(key), $"Unknown key {key}")
    };
}
