using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding, Scope(Feature = "Content healthcheck endpoint")]
public class ContentHealthcheckSteps(ContentApiDriver api)
{
    private const string RequestKey = "health-check";

    [Given("a valid content health check request")]
    private void GivenAValidContentHealthCheckRequest()
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the content health check request")]
    private async Task WhenISubmitTheContentHealthCheckRequest()
    {
        await api.Send();
    }

    [Then("the content health check result should be healthy")]
    private async Task ThenTheContentHealthCheckResultShouldBeHealthy()
    {
        var result = api[RequestKey].Response;
        AssertHttpResponse.IsOk(result);

        var content = await result.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", content);
    }
}