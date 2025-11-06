using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightSchoolsSteps(InsightApiDriver api)
{
    private const string InsightSchoolsKey = "insight-schools";

    [Given("a valid school characteristics request with urn '(.*)'")]
    public void GivenAValidSchoolCharacteristicsRequestWithUrn(string urn)
    {
        api.CreateRequest(InsightSchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/school/{urn}/characteristics", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid school characteristics request with urn '(.*)'")]
    public void GivenAnInvalidSchoolCharacteristicsRequestWithUrn(string urn)
    {
        api.CreateRequest(InsightSchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/school/{urn}/characteristics", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school characteristics request with urns:")]
    public void GivenAValidSchoolCharacteristicsRequestWithUrns(DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(InsightSchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/characteristics?urns={string.Join("&urns=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insight schools request")]
    public async Task WhenISubmitTheInsightSchoolsRequest()
    {
        await api.Send();
    }

    [Then("the school characteristics result should be not found")]
    public void ThenTheSchoolCharacteristicsResultShouldBeNotFound()
    {
        AssertHttpResponse.IsNotFound(api[InsightSchoolsKey].Response);
    }

    [Then("the school characteristics result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheSchoolCharacteristicsResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[InsightSchoolsKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the schools characteristics result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheSchoolsCharacteristicsResultShouldBeOkAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[InsightSchoolsKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

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