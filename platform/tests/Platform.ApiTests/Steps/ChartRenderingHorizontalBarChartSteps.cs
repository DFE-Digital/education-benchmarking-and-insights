using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.Models;
using Platform.ApiTests.TestDataHelpers;
using Platform.Json;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Chart rendering horizontal bar chart endpoint")]
public class ChartRenderingHorizontalBarChartSteps(ChartRenderingApiDriver api)
{
    private const string SingleKey = "horizontal-bar-charts";

    [Given("a single horizontal bar chart request with accept header '(.*)', highlighted item '(.*)', sort '(.*)', width '(.*)', bar height '(.*)', id '(.*)', valueType '(.*)' and the following data:")]
    public void GivenASingleHorizontalBarChartRequestWithAcceptHeaderHighlightedItemSortWidthBarHeightIdValueTypeAndTheFollowingData(string accept, string highlight, string sort, int width, int barHeight, string id, string valueType, DataTable table)
    {
        var data = table.Rows.Select(row => new TestDatum
        {
            Key = row["Key"],
            Value = string.IsNullOrWhiteSpace(row["Value"]) ? null : decimal.Parse(row["Value"])
        });

        var content = BuildRequest(highlight, sort, width, barHeight, data, id, valueType);

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/horizontalBarChart", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("x-accept", accept);

        api.CreateRequest(SingleKey, request);
    }

    [Given("multiple horizontal bar chart requests with the following data:")]
    public void GivenMultipleHorizontalBarChartRequestsWithTheFollowingData(DataTable table)
    {
        var content = new List<PostHorizontalBarChartRequest<TestDatum>>();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var row in table.Rows)
        {
            var id = row["Id"];
            var highlight = row["Highlight"];
            var sort = row["Sort"];
            var width = int.Parse(row["Width"]);
            var barHeight = int.Parse(row["BarHeight"]);
            var valueType = row["ValueType"];
            var data = row["Data"].FromJson<TestDatum[]>();

            content.Add(BuildRequest(highlight, sort, width, barHeight, data, id, valueType));
        }

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/horizontalBarChart", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(new PostHorizontalBarChartsRequest<TestDatum>(content).ToJson(), Encoding.UTF8, "application/json")
        };

        api.CreateRequest(SingleKey, request);
    }

    [When("I submit the horizontal bar chart request")]
    public async Task WhenISubmitTheHorizontalBarChartRequest()
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

    [Then("the chart response should be bad request, contain a JSON object and match the expected output of '(.*)'")]
    public async Task ThenTheChartResponseShouldBeBadRequestContainAJsonObjectAndMatchTheExpectedOutputOf(string testFile)
    {
        var response = api[SingleKey].Response;
        AssertHttpResponse.IsBadRequest(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile);

        actual.AssertDeepEquals(expected);
    }

    private static PostHorizontalBarChartRequest<TestDatum> BuildRequest(string highlight, string sort, int width, int barHeight, IEnumerable<TestDatum> data, string id, string valueType) => new()
    {
        KeyField = nameof(TestDatum.Key).ToLower(),
        ValueField = nameof(TestDatum.Value).ToLower(),
        HighlightKey = highlight,
        Sort = sort,
        BarHeight = barHeight,
        Width = width,
        Data = data.ToArray(),
        Id = id,
        ValueType = valueType
    };
}