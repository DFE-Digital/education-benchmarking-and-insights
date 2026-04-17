using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority Statistical Neighbours")]
public class StatisticalNeighboursSteps(LocalAuthorityApiDriver api)
{
    private const string Key = "statistical-neighbours";
    private const string RouteFolder = "LocalAuthority";
    private const string DetailsFolder = "StatisticalNeighbours";



    [Given(@"^a get request for local authority statistical neighbours with code '([^']*)'$")]
    public void GivenAGetRequestForLocalAuthorityStatisticalNeighboursWithCode(string code)
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/{code}/statistical-neighbours", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }


    [Given(@"^a get request for local authority statistical neighbours with code '([^']*)' and api version '([^']*)'$")]
    public void GivenAGetRequestForLocalAuthorityStatisticalNeighboursWithCodeAndApiVersion(string code, string version)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/{code}/statistical-neighbours", UriKind.Relative),
            Method = HttpMethod.Get
        };
        request.Headers.Add("x-api-version", version);
        api.CreateRequest(Key, request);
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

    [Then("the result should be not found")]
    public void ThenTheResultShouldBeNotFound()
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsNotFound(response);
    }

    [Then("the result should be bad request")]
    public void ThenTheResultShouldBeBadRequest()
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsBadRequest(response);
    }
    
    [Then("the result should be bad request and match the expected output of '(.*)'")]
    public async Task ThenTheResultShouldBeBadRequestAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, DetailsFolder);

        actual.AssertDeepEquals(expected);
    }
}