using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Chart rendering line chart endpoint")]
public class ChartRenderingLineChartSteps(ChartRenderingApiDriver api)
{
    private const string Key = "line-charts";

    private const string RouteFolder = "ChartRendering";
    private const string SubFolder = "LineChart";

    [Given("a '(.*)' line chart request with accept header '(.*)' and request input from '(.*)'")]
    public void GivenALineChartRequestWithAcceptHeaderAndRequestInputFrom(string chartType, string acceptHeader, string inputFile)
    {
        var jsonContent = chartType.ToLowerInvariant() switch
        {
            "single" => TestDataProvider.GetJsonObjectData(inputFile, RouteFolder, SubFolder).ToString(),
            "multiple" => TestDataProvider.GetJsonArrayData(inputFile, RouteFolder, SubFolder).ToString(),
            _ => throw new ArgumentException($"Unsupported chart type: {chartType}")
        };

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/lineChart", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
        };
        request.Headers.Add("x-accept", acceptHeader);

        api.CreateRequest(Key, request);
    }

    [When("I submit the request")]
    public async Task WhenISubmitTheRequest()
    {
        await api.Send();
    }

    [Then("the response should be '(.*)', contain '(.*)' and match the expected output of '(.*)'")]
    public async Task ThenTheResponseShouldBeContainAndMatchTheExpectedOutputOf(string expectedStatus, string contentType, string testFile)
    {
        var response = api[Key].Response;

        switch (expectedStatus.ToLowerInvariant())
        {
            case "ok":
                AssertHttpResponse.IsOk(response);
                break;
            case "bad request":
                AssertHttpResponse.IsBadRequest(response);
                break;
            default:
                throw new ArgumentException($"Unsupported HTTP status assertion: {expectedStatus}");
        }

        var content = await response.Content.ReadAsStringAsync();

        switch (contentType.ToLowerInvariant())
        {
            case "an svg document":
                var actualXml = XDocument.Parse(content);
                var expectedXml = TestDataProvider.GetXmlData(testFile, RouteFolder, SubFolder);
                actualXml.AssertDeepEquals(expectedXml);
                break;

            case "a json object":
                var actualJsonObj = JObject.Parse(content);
                var expectedJsonObj = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);
                actualJsonObj.AssertDeepEquals(expectedJsonObj);
                break;

            case "a json array":
                var actualJsonArray = JArray.Parse(content);
                var expectedJsonArray = TestDataProvider.GetJsonArrayData(testFile, RouteFolder, SubFolder);
                actualJsonArray.AssertDeepEquals(expectedJsonArray);
                break;

            default:
                throw new ArgumentException($"Unsupported content type assertion: {contentType}");
        }
    }
}
