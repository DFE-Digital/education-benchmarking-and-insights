using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority Maintained Schools")]
public class MaintainedSchoolsSteps(LocalAuthorityApiDriver api)
{
    private const string SchoolsKey = "schools-key";
    private const string RouteFolder = "LocalAuthority";
    private const string SubFolder = "MaintainedSchools";

    [Given("a finance summary request with code '(.*)' and query parameters:")]
    public void GivenAFinanceSummaryRequestWithCodeAndQueryParameters(string code, DataTable table)
    {
        var queryString = BuildQueryString(table);

        api.CreateRequest(SchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/{code}/maintained-schools/finance{queryString}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a finance summary request with code '(.*)'")]
    public void GivenAFinanceSummaryRequestWithCode(string code)
    {

        api.CreateRequest(SchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/{code}/maintained-schools/finance", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a workforce summary request with code '(.*)' and query parameters:")]
    public void GivenAWorkforceSummaryRequestWithCodeAndQueryParameters(string code, DataTable table)
    {
        var queryString = BuildQueryString(table);

        api.CreateRequest(SchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/{code}/maintained-schools/workforce{queryString}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a workforce summary request with code '(.*)'")]
    public void GivenAWorkforceSummaryRequestWithCodeAndQueryParameters(string code)
    {
        api.CreateRequest(SchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/{code}/maintained-schools/workforce", UriKind.Relative),
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

    [Then("the result should be bad request and match the expected output in '(.*)'")]
    public async Task ThenTheResultShouldBeBadRequestAndMatchTheExpectedOutputIn(string testFile)
    {
        var response = api[SchoolsKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }


    [Then("the result should be not found")]
    public void ThenTheResultShouldBeNotFound()
    {
        var response = api[SchoolsKey].Response;
        AssertHttpResponse.IsNotFound(response);
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