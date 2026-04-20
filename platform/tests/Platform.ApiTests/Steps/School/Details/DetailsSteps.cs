using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;
using Xunit;

namespace Platform.ApiTests.Steps.School.Details;

[Binding]
[Scope(Feature = "School Details")]
public class DetailsSteps(SchoolApiDriver api)
{
    private const string SingleSchoolKey = "single-school";
    private const string SchoolsCollectionKey = "schools-collection";
    private const string SingleSchoolCharacteristicsKey = "single-school-characteristics";

    private const string RouteFolder = "School";
    private const string SubFolder = "Details";

    [Given("I have a valid single school request")]
    public void GivenIHaveAValidSingleSchoolRequest()
    {
        api.CreateRequest(SingleSchoolKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/777042", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("I have a non-existent single school request")]
    public void GivenIHaveANonExistentSingleSchoolRequest()
    {
        api.CreateRequest(SingleSchoolKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/999999", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("I have a valid collection of schools request")]
    public void GivenIHaveAValidCollectionOfSchoolsRequest()
    {
        api.CreateRequest(SchoolsCollectionKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools?urns=777042&urns=777046", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("I have a valid single school characteristics request")]
    public void GivenIHaveAValidSingleSchoolCharacteristicsRequest()
    {
        api.CreateRequest(SingleSchoolCharacteristicsKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/777042/characteristics", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("I have a non-existent single school characteristics request")]
    public void GivenIHaveANonExistentSingleSchoolCharacteristicsRequest()
    {
        api.CreateRequest(SingleSchoolCharacteristicsKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/999999/characteristics", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("the request has an invalid API version")]
    public void GivenTheRequestHasAnInvalidApiVersion()
    {
        if (api.ContainsKey(SingleSchoolKey)) api[SingleSchoolKey].Request.Headers.Add("x-api-version", "9.9");
        if (api.ContainsKey(SchoolsCollectionKey)) api[SchoolsCollectionKey].Request.Headers.Add("x-api-version", "9.9");
        if (api.ContainsKey(SingleSchoolCharacteristicsKey)) api[SingleSchoolCharacteristicsKey].Request.Headers.Add("x-api-version", "9.9");
    }

    [When("I submit the single school request")]
    public async Task WhenISubmitTheSingleSchoolRequest()
    {
        await api.Send();
    }

    [When("I submit the collection of schools request")]
    public async Task WhenISubmitTheCollectionOfSchoolsRequest()
    {
        await api.Send();
    }

    [When("I submit the single school characteristics request")]
    public async Task WhenISubmitTheSingleSchoolCharacteristicsRequest()
    {
        await api.Send();
    }

    [Then("the single school result should be 'ok' and match the expected output in '(.*)'")]
    public async Task ThenTheSingleSchoolResultShouldBeOkAndMatch(string expectedFileName)
    {
        var response = api[SingleSchoolKey].Response;
        AssertHttpResponse.IsOk(response);
        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(expectedFileName, RouteFolder, SubFolder);
        actual.AssertDeepEquals(expected);
    }

    [Then("the single school result should be 'bad request'")]
    public void ThenTheSingleSchoolResultShouldBeBadRequest()
    {
        var response = api[SingleSchoolKey].Response;
        AssertHttpResponse.IsBadRequest(response);
    }

    [Then("the single school result should be 'not found'")]
    public void ThenTheSingleSchoolResultShouldBeNotFound()
    {
        var response = api[SingleSchoolKey].Response;
        AssertHttpResponse.IsNotFound(response);
    }

    [Then("the collection of schools result should be 'ok' and match the expected output in '(.*)'")]
    public async Task ThenTheCollectionOfSchoolsResultShouldBeOkAndMatch(string expectedFileName)
    {
        var response = api[SchoolsCollectionKey].Response;
        AssertHttpResponse.IsOk(response);
        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(expectedFileName, RouteFolder, SubFolder);
        actual.AssertDeepEquals(expected);
    }

    [Then("the collection of schools result should be 'bad request'")]
    public void ThenTheCollectionOfSchoolsResultShouldBeBadRequest()
    {
        var response = api[SchoolsCollectionKey].Response;
        AssertHttpResponse.IsBadRequest(response);
    }

    [Then("the single school characteristics result should be 'ok' and match the expected output in '(.*)'")]
    public async Task ThenTheSingleSchoolCharacteristicsResultShouldBeOkAndMatch(string expectedFileName)
    {
        var response = api[SingleSchoolCharacteristicsKey].Response;
        AssertHttpResponse.IsOk(response);
        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(expectedFileName, RouteFolder, SubFolder);
        actual.AssertDeepEquals(expected);
    }

    [Then("the single school characteristics result should be 'bad request'")]
    public void ThenTheSingleSchoolCharacteristicsResultShouldBeBadRequest()
    {
        var response = api[SingleSchoolCharacteristicsKey].Response;
        AssertHttpResponse.IsBadRequest(response);
    }

    [Then("the single school characteristics result should be 'not found'")]
    public void ThenTheSingleSchoolCharacteristicsResultShouldBeNotFound()
    {
        var response = api[SingleSchoolCharacteristicsKey].Response;
        AssertHttpResponse.IsNotFound(response);
    }
}