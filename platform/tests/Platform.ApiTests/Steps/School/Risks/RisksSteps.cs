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

    [When("I submit the request")]
    public async Task WhenISubmitTheRequest()
    {
        await api.Send();
    }

    [Then("the '(.*)' result should be '(.*)' and match the expected output in '(.*)'")]
    public async Task ThenTheResultShouldBeAndMatchTheExpectedOutputIn(string key, string result, string testFile)
    {
        var apiKey = GetApiKeyFromFriendlyName(key);
        var response = api[apiKey].Response;

        switch (result)
        {
            case "ok":
                AssertHttpResponse.IsOk(response);
                break;
            case "not found":
                AssertHttpResponse.IsNotFound(response);
                break;
            default:
                Assert.Fail($"unexpected result: {result}");
                break;
        }

        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
        {
            Assert.Empty(content);
            return;
        }

        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }

    private static string GetApiKeyFromFriendlyName(string key) => key switch
    {
        "history" => HistoryKey,
        _ => throw new ArgumentOutOfRangeException(nameof(key), $"Unknown key {key}")
    };
}
