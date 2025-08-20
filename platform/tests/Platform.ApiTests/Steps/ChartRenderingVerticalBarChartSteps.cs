using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.Models;
using Platform.ApiTests.TestDataHelpers;
using Platform.Json;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Chart rendering vertical bar chart endpoint")]
public class ChartRenderingVerticalBarChartSteps(ChartRenderingApiDriver api)
{
    private const string SingleKey = "vertical-bar-charts";
    private const string MultipleKey = "vertical-bar-charts";

    [Given("a single vertical bar chart request with accept header '(.*)', highlighted item '(.*)', sort '(.*)', width '(.*)', height '(.*)', id '(.*)' and the following data:")]
    public void GivenASingleVerticalBarChartRequestWithAcceptHeaderHighlightedItemSortWidthHeightIdAndTheFollowingData(
        string accept, string highlight, string sort, int width, int height, string id, DataTable table)
    {
        var content = BuildRequest(highlight, sort, width, height, id, table.Rows.Select(row => new TestDatum
        {
            Key = row["Key"],
            Value = decimal.Parse(row["Value"] ?? string.Empty)
        }));

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/verticalBarChart", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("x-accept", accept);

        api.CreateRequest(SingleKey, request);
    }

    [Given("multiple vertical bar chart requests with the following data:")]
    public void GivenMultipleVerticalBarChartRequestsWithTheFollowingData(DataTable table)
    {
        var content = new List<PostVerticalBarChartRequest<TestDatum>>();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var row in table.Rows)
        {
            var id = row["Id"];
            var highlight = row["Highlight"];
            var sort = row["Sort"];
            var width = int.Parse(row["Width"]);
            var height = int.Parse(row["Height"]);
            var data = row["Data"].FromJson<TestDatum[]>();

            content.Add(BuildRequest(highlight, sort, width, height, id, data));
        }

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/verticalBarChart", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(new PostVerticalBarChartsRequest<TestDatum>(content).ToJson(), Encoding.UTF8, "application/json")
        };

        api.CreateRequest(SingleKey, request);
    }

    [When("I submit the vertical bar chart request")]
    public async Task WhenISubmitTheVerticalBarChartRequest()
    {
        await api.Send();
    }

    [Then("the response should be ok, contain a JSON array and match the expected output of '(.*)'")]
    public async Task ThenTheResponseShouldBeOkContainAJsonArrayAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[SingleKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the response should be ok, contain a JSON object and match the expected output of '(.*)'")]
    public async Task ThenTheResponseShouldBeOkContainAJsonObjectAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[SingleKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the response should be ok, contain an SVG document and match the expected output of '(.*)'")]
    public async Task ThenTheResponseShouldBeOkContainAnSvgDocumentAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[SingleKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = XDocument.Parse(content);

        var expected = TestDataProvider.GetXmlData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the chart response should be bad request with the following errors:")]
    public async Task ThenTheChartResponseShouldBeBadRequestWithTheFollowingErrors(DataTable table)
    {
        var response = api[MultipleKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ChartErrorResponse>();
        Assert.NotNull(results);

        var expected = table.Rows.Select(r => r["Error"]);
        Assert.Equal(expected, results.Errors);
    }

    private static PostVerticalBarChartRequest<TestDatum> BuildRequest(string highlight, string sort, int width,
        int height, string id, IEnumerable<TestDatum> data)
    {
        return new PostVerticalBarChartRequest<TestDatum>
        {
            KeyField = nameof(TestDatum.Key).ToLower(),
            ValueField = nameof(TestDatum.Value).ToLower(),
            HighlightKey = highlight,
            Sort = sort,
            Height = height,
            Width = width,
            Data = data.ToArray(),
            Id = id
        };
    }
}