using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Content commercial resource endpoints")]
public class ContentCommercialResourceSteps(ContentApiDriver api)
{
    private const string Key = "commercial-resources";

    [Given("a valid request")]
    public void GivenAValidRequest()
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/commercial-resources", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid request with API version '(.*)'")]
    public void GivenAValidRequestWithApiVersion(string version)
    {
        GivenAValidRequest();
        api[Key].Request.Headers.Add("x-api-version", version);
    }

    [When("I submit the request")]
    public async Task WhenISubmitTheRequest()
    {
        await api.Send();
    }

    [Then("the result should be ok and match the expected output")]
    public async Task ThenTheResultShouldBeOkAndMatchTheExpectedOutput()
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData("CommercialResources.json");

        Assert.True(JToken.DeepEquals(expected, actual));
    }

    [Then("the result should be bad request")]
    public void ThenTheResultShouldBeBadRequest()
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsBadRequest(response);
    }
}