using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority Risks")]
public class RisksSteps(LocalAuthorityApiDriver api)
{
    private const string PagedKey = "paged-school-risks";
    private const string RouteFolder = "LocalAuthority";
    private const string SubFolder = "Risks";

    private List<string> _codes = [];

    [Given("a paged school risks request with codes:")]
    public void GivenAPagedSchoolRisksRequestWithCodes(DataTable table)
    {
        var codes = table.Rows
            .Select(r => r["Code"])
            .Where(c => !string.IsNullOrWhiteSpace(c));

        _codes = codes.ToList();
    }

    [Given("parameters:")]
    public void GivenParameters(DataTable table)
    {
        var row = table.Rows[0];

        var page = row["Page"];
        var pageSize = row["PageSize"];
        var sortField = row["SortField"];
        var sortOrder = row["SortOrder"];
        var phase = row["Phase"];

        var query = _codes.Select(code => $"code={code}").ToList();

        if (!string.IsNullOrWhiteSpace(page))
            query.Add($"page={page}");

        if (!string.IsNullOrWhiteSpace(pageSize))
            query.Add($"pageSize={pageSize}");

        if (!string.IsNullOrWhiteSpace(sortField))
            query.Add($"sortField={sortField}");

        if (!string.IsNullOrWhiteSpace(sortOrder))
            query.Add($"sortOrder={sortOrder}");

        if (!string.IsNullOrWhiteSpace(phase))
            query.Add($"phase={phase}");

        var uri = "/api/local-authorities/risks";

        if (query.Any())
            uri += "?" + string.Join("&", query);

        api.CreateRequest(PagedKey, new HttpRequestMessage
        {
            RequestUri = new Uri(uri, UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a paged school risks request with too many codes")]
    public void GivenAPagedSchoolRisksRequestWithTooManyCodes()
    {
        var codes = Enumerable.Range(1, 31)
            .Select(i => (100 + i).ToString());

        var query = string.Join("&", codes.Select(c => $"code={c}"));

        var uri = $"/api/local-authorities/risks?{query}";

        api.CreateRequest(PagedKey, new HttpRequestMessage
        {
            RequestUri = new Uri(uri, UriKind.Relative),
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
        var response = api[PagedKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }

    [Then("the result should be bad request and match the expected output of '(.*)'")]
    public async Task ThenTheResultShouldBeBadRequestAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[PagedKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }
}
