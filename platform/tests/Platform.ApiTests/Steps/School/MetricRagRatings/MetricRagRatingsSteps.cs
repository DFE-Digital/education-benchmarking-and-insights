using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps.School.MetricRagRatings;

[Binding]
[Scope(Feature = "School MetricRagRatings")]
public class MetricRagRatingsSteps(SchoolApiDriver api)
{
    private const string Key = "school-metric-rag-ratings";

    [Given("a valid summary request with LaCode '(.*)'")]
    public void GivenAValidSummaryRequestWithLaCode(string laCode)
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/metric-rag-ratings?laCode={laCode}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid summary request with Urns:")]
    public void GivenAValidSummaryRequestWithUrns(DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/metric-rag-ratings?urns={string.Join("&urns=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid summary request with CompanyNumber '(.*)'")]
    public void GivenAValidSummaryRequestWithCompanyNumber(string companyNumber)
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/metric-rag-ratings?companyNumber={companyNumber}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid summary request with Urns and OverallPhase '(.*)':")]
    public void GivenAValidSummaryRequestWithUrnsAndOverallPhase(string phase, DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/metric-rag-ratings?overallPhase={phase}&urns={string.Join("&urns=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a summary request with (.*), (.*), (.*) and (.*)")]
    public void GivenASummaryRequestWith(string overallPhase, string laCode, string urnsCsv, string companyNumber)
    {
        var urnsParam = string.IsNullOrWhiteSpace(urnsCsv) ? "" : $"urns={urnsCsv.Replace(",", "&urns=")}";
        var queryParts = new List<string> { urnsParam };

        if (!string.IsNullOrWhiteSpace(overallPhase)) queryParts.Add($"overallPhase={overallPhase}");
        if (!string.IsNullOrWhiteSpace(laCode)) queryParts.Add($"laCode={laCode}");
        if (!string.IsNullOrWhiteSpace(companyNumber)) queryParts.Add($"companyNumber={companyNumber}");

        var query = string.Join("&", queryParts.Where(p => !string.IsNullOrEmpty(p)));
        if (!string.IsNullOrEmpty(query)) query = "?" + query;

        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/metric-rag-ratings{query}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid details request with Urns:")]
    public void GivenAValidDetailsRequestWithUrns(DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/metric-rag-ratings/details?urns={string.Join("&urns=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid details request with CompanyNumber '(.*)'")]
    public void GivenAValidDetailsRequestWithCompanyNumber(string companyNumber)
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/metric-rag-ratings/details?companyNumber={companyNumber}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid details request with Categories '(.*)' and Statuses '(.*)' for Urns:")]
    public void GivenAValidDetailsRequestWithCategoriesAndStatusesForUrns(string categories, string statuses, DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/metric-rag-ratings/details?urns={string.Join("&urns=", urns)}&categories={categories.Replace(",", "&categories=")}&statuses={statuses.Replace(",", "&statuses=")}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a details request with (.*), (.*), (.*) and (.*)")]
    public void GivenADetailsRequestWith(string categoriesCsv, string statusesCsv, string urnsCsv, string companyNumber)
    {
        var queryParts = new List<string>();

        if (!string.IsNullOrWhiteSpace(urnsCsv)) queryParts.Add($"urns={urnsCsv.Replace(",", "&urns=")}");
        if (!string.IsNullOrWhiteSpace(categoriesCsv)) queryParts.Add($"categories={categoriesCsv.Replace(",", "&categories=")}");
        if (!string.IsNullOrWhiteSpace(statusesCsv)) queryParts.Add($"statuses={statusesCsv.Replace(",", "&statuses=")}");
        if (!string.IsNullOrWhiteSpace(companyNumber)) queryParts.Add($"companyNumber={companyNumber}");

        var query = string.Join("&", queryParts.Where(p => !string.IsNullOrEmpty(p)));
        if (!string.IsNullOrEmpty(query)) query = "?" + query;

        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/metric-rag-ratings/details{query}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an unsupported API version '(.*)'")]
    public void GivenAnUnsupportedApiVersion(string version)
    {
        api[Key].Request.Headers.Add("x-api-version", version);
    }

    [When("I submit the School MetricRagRatings request")]
    public async Task WhenISubmitTheSchoolMetricRagRatingsRequest()
    {
        await api.Send();
    }

    private const string RouteFolder = "School";
    private const string SubFolder = "MetricRagRatings";

    [Then("the School MetricRagRatings result should be OK and match the expected output in '(.*)'")]
    public async Task ThenTheSchoolMetricRagRatingsResultShouldBeOkAndMatchTheExpectedOutputIn(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);
        var expected = TestDataProvider.GetJsonArrayData(testFile, RouteFolder, SubFolder);
        actual.AssertDeepEquals(expected);
    }

    [Then("the School MetricRagRatings result should be Bad Request and match the expected output in '(.*)'")]
    public async Task ThenTheSchoolMetricRagRatingsResultShouldBeBadRequestAndMatchTheExpectedOutputIn(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);
        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);
        actual.AssertDeepEquals(expected);
    }

    [Then("the School MetricRagRatings result should be Bad Request and match the expected problem details in '(.*)'")]
    public async Task ThenTheSchoolMetricRagRatingsResultShouldBeBadRequestAndMatchTheExpectedProblemDetailsIn(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);
        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);
        actual.AssertDeepEquals(expected);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(DataTable table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }
}