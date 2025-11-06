using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Insights IT Spend endpoints")]
public class InsightItSpendSteps(InsightApiDriver api)
{
    private const string SchoolItSpendKey = "school-it-spend";
    private const string TrustItSpendKey = "trust-it-spend";
    private const string TrustForecastItSpendKey = "trust-forecast-it-spend";

    [Given("a schools IT spend request with dimension '(.*)' and URNs:")]
    public void GivenASchoolsItSpendRequestWithDimensionAndUrns(string dimension, DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);

        api.CreateRequest(SchoolItSpendKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/it-spend/schools?urns={string.Join(",", urns)}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a trusts IT spend request with company numbers:")]
    public void GivenATrustsItSpendRequestWithCompanyNumbers(DataTable table)
    {
        var companyNumbers = GetFirstColumnsFromTableRowsAsString(table);

        api.CreateRequest(TrustItSpendKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/it-spend/trusts?companyNumbers={string.Join(",", companyNumbers)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a trusts IT spend request with no company numbers")]
    public void GivenATrustsItSpendRequestWithNoCompanyNumbers()
    {
        api.CreateRequest(TrustItSpendKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/it-spend/trusts", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a trust forecast IT spend request with company number '(.*)'")]
    public void GivenATrustForecastItSpendRequestWithCompanyNumber(string companyNumber)
    {
        api.CreateRequest(TrustForecastItSpendKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/it-spend/trust/{companyNumber}/forecast", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the IT spend request")]
    public async Task WhenISubmitTheItSpendRequest()
    {
        await api.Send();
    }

    [Then("the schools result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheSchoolsResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[SchoolItSpendKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the trusts result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheTrustsResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[TrustItSpendKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the trust forecast result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheTrustForecastResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[TrustForecastItSpendKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the schools IT spend result should be bad request")]
    public void ThenTheSchoolsItSpendResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[SchoolItSpendKey].Response);
    }

    [Then("the trusts IT spend result should be bad request")]
    public void ThenTheTrustsItSpendResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[TrustItSpendKey].Response);
    }

    [Then("the trust forecast IT spend result should be not found")]
    public void ThenTheTrustForecastItSpendResultShouldBeNotFound()
    {
        AssertHttpResponse.IsNotFound(api[TrustForecastItSpendKey].Response);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(DataTable table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }
}