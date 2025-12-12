using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Non Financial education health care plans local authorities history endpoint")]
public class NonFinancialEducationHealthCarePlansLocalAuthoritiesHistory(NonFinancialApiDriver api)
{
    private const string Key = "education-health-care-plans";
    private const string HistoryKey = "education-health-care-plans-history";

    [Given("an education health care plans history request with dimension '(.*)' and LA codes:")]
    public void GivenAnEducationHealthCarePlansHistoryRequestWithDimensionAndLaCodes(string dimension, DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/education-health-care-plans/local-authorities/history?code={string.Join("&code=", codes)}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an education health care plans request with dimension '(.*)' and LA codes:")]
    public void GivenAnEducationHealthCarePlansRequestWithDimensionAndLaCodes(string dimension, DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/education-health-care-plans/local-authorities?code={string.Join("&code=", codes)}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an education health care plans history request with no codes")]
    public void GivenAnEducationHealthCarePlansHistoryRequestWithNoCodes()
    {
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/education-health-care-plans/local-authorities/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an education health care plans request with no codes")]
    public void GivenAnEducationHealthCarePlansRequestWithNoCodes()
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/education-health-care-plans/local-authorities", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the education health care plans request")]
    public async Task WhenISubmitTheEducationHealthCarePlansRequest()
    {
        await api.Send();
    }

    [Then("the education health care plans history result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheEducationHealthCarePlansHistoryResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[HistoryKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the education health care plans result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheEducationHealthCarePlansResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the education health care plans history result should be bad request")]
    public void ThenTheEducationHealthCarePlansHistoryResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[HistoryKey].Response);
    }

    [Then("the education health care plans result should be bad request")]
    public void ThenTheEducationHealthCarePlansResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[Key].Response);
    }
}