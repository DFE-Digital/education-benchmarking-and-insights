using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding, Scope(Feature = "Benchmark healthcheck endpoint")]
public class BenchmarkHealthcheckSteps(BenchmarkApiDriver api)
{
    private const string RequestKey = "health-check";

    [Given("a valid benchmark health check request")]
    private void GivenAValidBenchmarkHealthCheckRequest()
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the benchmark health check request")]
    private async Task WhenISubmitTheBenchmarkHealthCheckRequest()
    {
        await api.Send();
    }

    [Then("the benchmark health check result should be healthy")]
    private async Task ThenTheBenchmarkHealthCheckResultShouldBeHealthy()
    {
        var result = api[RequestKey].Response;
        AssertHttpResponse.IsOk(result);

        var content = await result.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", content);
    }
}