using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Local authorities schools endpoints")]
public class LocalAuthoritiesSchoolsSteps(LocalAuthorityFinancesApiDriver api)
{
    private const string SchoolsKey = "schools-key";

    [Given("a local authorities schools finance summary request with code '(.*)' and query parameters:")]
    public void GivenALocalAuthoritiesSchoolsFinanceSummaryRequestWithCodeAndQueryParameters(string code, DataTable table)
    {
        var queryString = BuildQueryString(table);

        api.CreateRequest(SchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/{code}/schools/finance{queryString}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a local authorities schools workforce summary request with code '(.*)' and query parameters:")]
    public void GivenALocalAuthoritiesSchoolsWorkforceSummaryRequestWithCodeAndQueryParameters(string code, DataTable table)
    {
        var queryString = BuildQueryString(table);

        api.CreateRequest(SchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/local-authorities/{code}/schools/workforce{queryString}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the local authorities schools request")]
    public async Task WhenISubmitTheLocalAuthoritiesSchoolsRequest()
    {
        await api.Send();
    }

    [Then("the local authorities schools result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheLocalAuthoritiesSchoolsResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[SchoolsKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the local authorities schools result should be bad request and match the expected output in '(.*)'")]
    public async Task ThenTheLocalAuthoritiesSchoolsResultShouldBeBadRequestAndMatchTheExpectedOutputIn(string testFile)
    {
        var response = api[SchoolsKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the local authorities schools result should be not found")]
    public void ThenTheLocalAuthoritiesSchoolsResultShouldBeNotFound()
    {
        var response = api[SchoolsKey].Response;
        AssertHttpResponse.IsNotFound(response);
    }

    private static string BuildQueryString(Table table)
    {
        var row = table.Rows[0];
        var queryParts = (from column in table.Header select $"{column}={row[column]}").ToList();

        return queryParts.Count == 0 ? string.Empty : $"?{string.Join("&", queryParts)}";
    }

}