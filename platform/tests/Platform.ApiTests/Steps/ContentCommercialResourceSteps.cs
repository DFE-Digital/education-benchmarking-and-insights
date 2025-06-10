using System.Reflection;
using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Content commercial resource endpoints")]
public class ContentCommercialResourceSteps(ContentApiDriver api)
{
    private const string Key = "commercial-resources";

    [Given("a valid request")]
    public void GivenAValidRequest()
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/commercial-resources", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the request")]
    public async Task WhenISubmitTheRequest()
    {
        await api.Send();
    }

    [Then("the result should be ok and match the expected output")]
    public async Task ThenTheResultShouldBeOkAndMatchTheExpectedOutput()
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = GetArrayData("CommercialResources.json");

        Assert.True(JToken.DeepEquals(expected, actual));
    }

    private static JArray GetArrayData(string file)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream($"Platform.ApiTests.Data.{file}");
        using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
        var jsonString = reader.ReadToEnd();

        return JArray.Parse(jsonString);
    }
}