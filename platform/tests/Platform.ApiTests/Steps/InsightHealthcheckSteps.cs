using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding, Scope(Feature = "Insight healthcheck endpoint")]
public class InsightHealthcheckSteps(InsightApiDriver api)
{
    private const string RequestKey = "health-check";

    [Given("a valid insight health check request")]
    private void GivenAValidInsightHealthCheckRequest()
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insight health check request")]
    private async Task WhenISubmitTheInsightHealthCheckRequest()
    {
        await api.Send();
    }

    [Then("the insight health check result should be healthy")]
    private async Task ThenTheInsightHealthCheckResultShouldBeHealthy()
    {
        var result = api[RequestKey].Response;
        AssertHttpResponse.IsOk(result);

        var content = await result.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", content);
    }
}