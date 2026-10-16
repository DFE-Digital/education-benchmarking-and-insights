using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;
using Xunit;

namespace Platform.ApiTests.Steps.Content.Files;

[Binding]
[Scope(Feature = "Local Authority Risk Score File")]
public class LocalAuthorityRiskScoresFileSteps(ContentApiDriver api)
{
    private const string RiskScoreFileKey = "RiskScoreFile";

    private const string RouteFolder = "Content";
    private const string SubFolder = "Files";

    [Given("a local authority risk score file request with year '(.*)'")]
    public void GivenALocalAuthorityRiskScoreFileRequestWithYear(string year)
    {
        api.CreateRequest(RiskScoreFileKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/files/local-authority-risk-scores/{year}", UriKind.Relative),
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
        "risk score file" => RiskScoreFileKey,
        _ => throw new ArgumentOutOfRangeException(nameof(key), $"Unknown key {key}")
    };
}
