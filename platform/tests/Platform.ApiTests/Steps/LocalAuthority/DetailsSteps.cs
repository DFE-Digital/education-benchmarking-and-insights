using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority Details")]
public class DetailsSteps(LocalAuthorityApiDriver api)
{
    private const string Key = "details";
    private const string RouteFolder = "LocalAuthority";
    private const string DetailsFolder = "Details";


    [Given("a get request for a single Local Authority with code '(.*)'")]
    public void GivenAGetRequestForASingleLocalAuthorityWithCode(string code)
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/{code}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a get request for a collection of Local Authorities")]
    public void GivenAGetRequestForACollectionOfLocalAuthorities()
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/local-authorities", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the request")]
    public async Task WhenISubmitTheRequest()
    {
        await api.Send();
    }

    [Then("the result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, DetailsFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the collection result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheCollectionResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile, RouteFolder, DetailsFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the result should be not found")]
    public async Task ThenTheResultShouldBeNotFound()
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsNotFound(response);
    }
}