using System.Text;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Svg.Dom;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.Models;
using Platform.Json;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Chart rendering horizontal bar chart endpoint")]
public class ChartRenderingHorizontalBarChartSteps(ChartRenderingApiDriver api)
{
    private const string SingleKey = "horizontal-bar-charts";
    private const string MultipleKey = "horizontal-bar-charts";

    [Given("a single horizontal bar chart request with accept header '(.*)', highlighted item '(.*)', sort '(.*)', width '(.*)', bar height '(.*)' and the following data:")]
    public void GivenASingleHorizontalBarChartRequestWithAcceptHeaderHighlightedItemSortWidthHeightAndTheFollowingData(string accept, string highlight, string sort, int width, int barHeight, DataTable table)
    {
        var content = BuildRequest(highlight, sort, width, barHeight, table.Rows.Select(row => new TestDatum
        {
            Key = row["Key"],
            Value = decimal.Parse(row["Value"] ?? string.Empty)
        }));

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
            var data = row["Data"].FromJson<TestDatum[]>();

            content.Add(BuildRequest(highlight, sort, width, barHeight, data, id));
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

    [Then("the chart response should contain a single chart with the expected properties:")]
    public async Task ThenTheChartResponseShouldContainASingleChartWithTheExpectedProperties(DataTable table)
    {
        var response = api[SingleKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ChartResponse>();
        Assert.NotNull(result);

        var row = table.Rows.First();
        AssertChartResponse(row, result);
    }

    [Then("the response should be SVG with the expected properties:")]
    public async Task ThenTheResponseShouldBeSvgWithTheExpectedProperties(DataTable table)
    {
        var response = api[SingleKey].Response;
        AssertHttpResponse.IsOk(response);

        var svg = await response.Content.ReadAsStringAsync();
        Assert.NotNull(svg);

        var row = table.Rows.First();
        AssertChartHtml(row, svg);
    }

    [Then("the chart response should contain multiple charts with the expected properties:")]
    public async Task ThenTheChartResponseShouldContainMultipleChartsWithTheExpectedProperties(DataTable table)
    {
        var response = api[MultipleKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ChartResponse[]>();
        Assert.NotNull(results);

        for (var i = 0; i < table.RowCount; i++)
        {
            var row = table.Rows.ElementAt(i);
            var id = row["Id"];
            var result = string.IsNullOrWhiteSpace(id) ? results.ElementAt(i) : results.SingleOrDefault(r => r.Id == id);
            AssertChartResponse(row, result);
        }
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

    private static PostHorizontalBarChartRequest<TestDatum> BuildRequest(string highlight, string sort, int width, int barHeight, IEnumerable<TestDatum> data, string? id = null) => new()
    {
        KeyField = nameof(TestDatum.Key).ToLower(),
        ValueField = nameof(TestDatum.Value).ToLower(),
        HighlightKey = highlight,
        Sort = sort,
        BarHeight = barHeight,
        Width = width,
        Data = data.ToArray(),
        Id = id
    };

    private static void AssertChartResponse(DataTableRow expected, ChartResponse? actual)
    {
        Assert.NotNull(actual);

        var expectedId = expected["Id"];
        if (string.IsNullOrWhiteSpace(expectedId))
        {
            Assert.False(string.IsNullOrWhiteSpace(actual.Id));
        }
        else
        {
            Assert.Equal(expectedId, actual.Id);
        }

        AssertChartHtml(expected, actual.Html);
    }

    private static void AssertChartHtml(DataTableRow expected, string? html)
    {
        Assert.NotNull(html);

        var parser = new HtmlParser(new HtmlParserOptions());
        var svg = parser.ParseDocument(html).Body?.FirstChild as SvgElement;
        Assert.NotNull(svg);
        Assert.Equal("svg", svg.NodeName);

        var expectedWidth = expected["Width"];
        Assert.Equal(expectedWidth, svg.GetAttribute("width"));
        var expectedHeight = expected["Height"];
        Assert.Equal(expectedHeight, svg.GetAttribute("height"));
        Assert.Equal($"0,0,{expectedWidth},{expectedHeight}", svg.GetAttribute("viewBox"));

        var g = svg.FirstElementChild;
        Assert.NotNull(g);

        var expectedRectCount = int.Parse(expected["RectCount"]);
        var rects = g.Children;
        Assert.NotNull(rects);
        Assert.Equal(expectedRectCount, rects.Length);

        var expectedHighlightIndex = int.Parse(expected["HighlightIndex"]);
        var highlightRect = rects.SingleOrDefault(r => r.ClassList.Contains("chart-cell__highlight"));
        Assert.NotNull(highlightRect);
        Assert.Equal(expectedHighlightIndex, rects.Index(highlightRect));
    }
}