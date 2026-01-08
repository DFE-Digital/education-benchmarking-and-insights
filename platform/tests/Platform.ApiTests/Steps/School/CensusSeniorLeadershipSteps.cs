using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps.School;

[Binding]
[Scope(Feature = "Census Senior Leadership")]
public class MaintainedSchoolsSteps(SchoolApiDriver api)
{
    private const string SchoolsKey = "schools-key";
    private const string RouteFolder = "School";
    private const string SubFolder = "Census";

    [Given("a senior leadership request with query parameters:")]
    public void GivenASeniorLeadershipRequestWithQueryParameters(DataTable table)
    {
        var queryString = BuildQueryString(table);

        api.CreateRequest(SchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/census/senior-leadership{queryString}", UriKind.Relative),
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
        var response = api[SchoolsKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the result should be bad request and match the expected output of '(.*)'")]
    public async Task ThenTheResultShouldBeBadRequestAndMatchTheExpectedOutputIn(string testFile)
    {
        var response = api[SchoolsKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }

    private static string BuildQueryString(Table table)
    {
        const int nameColumn = 0;
        const int valueColumn = 1;

        var parts = table.Rows.Select(x => $"{x[nameColumn]}={x[valueColumn]}").ToArray();
        return parts.Length > 0
            ? $"?{string.Join("&", parts)}"
            : string.Empty;
    }
}