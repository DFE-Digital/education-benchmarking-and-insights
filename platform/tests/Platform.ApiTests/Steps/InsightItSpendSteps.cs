using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Insights IT Spend endpoints")]
public class InsightItSpendSteps(InsightApiDriver api)
{
    private const string SchoolItSpendKey = "school-it-spend";

    [Given("a schools query request with dimension '(.*)' and with URNs:")]
    public void GivenAValidSchoolExpenditureDimensionRequest(string dimension, DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);

        api.CreateRequest(SchoolItSpendKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/it-spend/schools?urns={string.Join(",", urns)}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the IT spend schools request")]
    public async Task WhenISubmitTheInsightsExpenditureRequest()
    {
        await api.Send();
    }

    [Then("the schools IT spend query result should be ok and match the expected output of '(.*)'")]
    public async Task ThenTheResultShouldBeOkAndMatchTheExpectedOutput(string testFile)
    {
        var response = api[SchoolItSpendKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

        Assert.True(JToken.DeepEquals(expected, actual));
    }

    [Then("the schools IT spend query result should be bad request")]
    public void ThenTheSchoolExpenditureResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[SchoolItSpendKey].Response);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(DataTable table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }
}